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

public class CurrentProjectService
{
    private readonly AppDbContext _context;

    public CurrentProjectService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<CurrentProjectDto?> GetCurrentProjectAsync(Guid id)
    {
        return (await _context.CurrentProjects
            .Include(p => p.Users) 
            .FirstOrDefaultAsync(p => p.Id == id))?.ToCurrentProjectDto();
    }
    
    public CurrentProjectDto? GetCurrentProject(Guid id)
    {
        return _context.CurrentProjects
            .FirstOrDefault(p => p.Id == id)?.ToCurrentProjectDto();
    }
    
    public async Task<List<CurrentProjectDto>> GetAllCurrentProjectsWithoutUsersAsync()
    {
        return await _context.CurrentProjects.Select(p => p.ToCurrentProjectDto()).ToListAsync();
    }
    
    public async Task<List<String>> GetAllCurrentProjectsNamesAsync()
    {
        return await _context.CurrentProjects.Select(x => x.Name).ToListAsync();
    }
    
    public async Task UpdateOrAddCurrentProjectAsync(CurrentProjectDto currentProject)
    {
        var existingProject = await _context.CurrentProjects
            .FirstOrDefaultAsync(p => p.Id == currentProject.Id);

        if (existingProject == null)
        {
            _context.CurrentProjects.Add(currentProject.ToCurrentProject());
        }
        else
            existingProject.UpdateFromDto(currentProject);

        await _context.SaveChangesAsync();
    }
    
    public async Task<Guid> CreateCurrentProjectIfNotExistAsync(String currentProjectName)
    {
        var contains = await _context.CurrentProjects.Where(x => x.Name == currentProjectName).FirstOrDefaultAsync();
        if (contains != null)
            return contains.Id;
        var newCurrentProject = new CurrentProject { Id = Guid.NewGuid(), Name = currentProjectName };
        _context.CurrentProjects.Add(newCurrentProject);
        await _context.SaveChangesAsync();
        return newCurrentProject.Id;
    }
    
    public async Task<List<CurrentProjectDto>> GetViewCurrentProjects(ViewMode viewMode)
    {
        var projects = _context.CurrentProjects.Include(p => p.Users);
        var projectsDto = projects.Select(p => p.ToCurrentProjectDto()).AsEnumerable();
        if (viewMode.FilteringMode.filter != null)
            projectsDto = projectsDto.Where(project => viewMode.FilteringMode.filter(project));
        if (viewMode.SortMode.comporator != null)
            projectsDto = projectsDto.OrderBy(project => viewMode.SortMode.comporator(project));
        var filteredAndSortedProjects = projectsDto.ToList();
        var chunkedProject = filteredAndSortedProjects.Chunk(viewMode.PaginationMode.amountElementsOnPage);
        var pageChunk = chunkedProject.Skip(viewMode.PaginationMode.indexPage - 1).FirstOrDefault();
        return pageChunk?.ToList() ?? new List<CurrentProjectDto>();
    }

    public async Task DeleteCurrentProjectsAsync(Guid id)
    {
        var project = await _context.CurrentProjects.FindAsync(id);
        if (project != null)
        {
            _context.CurrentProjects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}