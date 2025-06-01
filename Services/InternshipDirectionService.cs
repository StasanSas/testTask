using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmptyBlazorApp1.Config;
using EmptyBlazorApp1.Dto;
using EmptyBlazorApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmptyBlazorApp1.Services;

public class InternshipDirectionService
{
    private readonly AppDbContext _context;

    public InternshipDirectionService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<InternshipDirectionDto?> GetInternshipDirectionAsync(Guid id)
    {
        return (await _context.InternshipDirections
            .Include(p => p.Users) 
            .FirstOrDefaultAsync(p => p.Id == id))?.ToInternshipDirectionDto();
    }
    
    public InternshipDirectionDto? GetInternshipDirection(Guid id)
    {
        return _context.InternshipDirections
            .FirstOrDefault(p => p.Id == id)?.ToInternshipDirectionDto();
    }
    
    public async Task<List<String>> GetAllInternshipDirectionNamesAsync()
    {
        return await _context.InternshipDirections.Select(x => x.Name).ToListAsync();
    }
    
    
    public async Task<List<InternshipDirectionDto>> GetAllInternshipDirectionsWithoutUsersAsync()
    {
        return await _context.InternshipDirections.Select(p => p.ToInternshipDirectionDto()).ToListAsync();
    }
    
    public async Task<List<String>> GetAllInternshipDirectionsNamesAsync()
    {
        return await _context.InternshipDirections.Select(x => x.Name).ToListAsync();
    }
    
    public async Task UpdateOrAddInternshipDirectionAsync(InternshipDirectionDto InternshipDirection)
    {
        var existingProject = await _context.InternshipDirections
            .FirstOrDefaultAsync(p => p.Id == InternshipDirection.Id);

        if (existingProject == null)
        {
            _context.InternshipDirections.Add(InternshipDirection.ToInternshipDirection());
        }
        else
            existingProject.UpdateFromDto(InternshipDirection);

        await _context.SaveChangesAsync();
    }
    
    public async Task<Guid> CreateInternshipDirectionIfNotExistAsync(String InternshipDirectionName)
    {
        var contains = await _context.InternshipDirections.Where(x => x.Name == InternshipDirectionName).FirstOrDefaultAsync();
        if (contains != null)
            return contains.Id;
        var newInternshipDirection = new InternshipDirection { Id = Guid.NewGuid(), Name = InternshipDirectionName };
        _context.InternshipDirections.Add(newInternshipDirection);
        await _context.SaveChangesAsync();
        return newInternshipDirection.Id;
    }
    
    public async Task<List<InternshipDirectionDto>> GetViewInternshipDirections(ViewMode viewMode)
    {
        var projects = _context.InternshipDirections.Include(p => p.Users);
        var projectsDto = projects.Select(p => p.ToInternshipDirectionDto()).AsEnumerable();
        if (viewMode.FilteringMode.filter != null)
            projectsDto = projectsDto.Where(project => viewMode.FilteringMode.filter(project));
        if (viewMode.SortMode.comporator != null)
            projectsDto = projectsDto.OrderBy(project => viewMode.SortMode.comporator(project));
        var filteredAndSortedProjects = projectsDto.ToList();
        var chunkedProject = filteredAndSortedProjects.Chunk(viewMode.PaginationMode.amountElementsOnPage);
        var pageChunk = chunkedProject.Skip(viewMode.PaginationMode.indexPage - 1).FirstOrDefault();
        return pageChunk?.ToList() ?? new List<InternshipDirectionDto>();
    }

    public async Task DeleteInternshipDirectionsAsync(Guid id)
    {
        var project = await _context.InternshipDirections.FindAsync(id);
        if (project != null)
        {
            _context.InternshipDirections.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}