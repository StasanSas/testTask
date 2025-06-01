using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmptyBlazorApp1.Config;
using EmptyBlazorApp1.Dto;
using EmptyBlazorApp1.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmptyBlazorApp1.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }
    

    public async Task<List<UserDtoForDataTransmission>> GetAllUsersAsync()
    {
        return await _context.Users.AsQueryable().Select(u => u.ToUserDto()).ToListAsync();
    }
    
    public List<UserDtoForDataTransmission> GetAllUsers()
    {
        return _context.Users.AsQueryable().Select(u => u.ToUserDto()).ToList();
    }


    public async Task CreateUserAsync(UserDtoForCreateUser user)
    {
        _context.Users.Add(user.ToUser());
        await _context.SaveChangesAsync();
    }
    
    public async Task EditUserAsync(UserDtoForDataTransmission user)
    {
        var userInDb = await _context.Users.FindAsync(user.Id);
        if (userInDb == null)
            throw new Exception("User not found.");
        userInDb.UpdateFromDto(user);
        await _context.SaveChangesAsync();
    }
    
    public bool ExistUserWithNumberPhone(String phone)
    {
        return _context.Users.Any(u => u.Phone == phone);
    }
    
    public bool ExistUserWithEmail(String email)
    {
        return _context.Users.Any(u => u.Email == email);
    }
    
    public IEnumerable<UserDtoForDataTransmission> GetAllUsersStartsOn(String startEmail)
    {
        return _context.Users.Where(u => u.Email.StartsWith(startEmail)).Select(u => u.ToUserDto()).AsEnumerable();
    }
    
    public async Task ChangeOrCreateProjectOfUser(UserDtoForDataTransmission user, CurrentProjectDto project)
    {
        
        var dbProject = await _context.CurrentProjects.FindAsync(project.Id);
        if (dbProject == null)
        {
            dbProject = project.ToCurrentProject();
            _context.CurrentProjects.Add(dbProject);
            await _context.SaveChangesAsync(); 
        }

        var dbUser = await _context.Users.FindAsync(user.Id);
        if (dbUser == null)
        {
            throw new Exception($"User with Id {user.Id} not found.");
        }
        dbUser.CurrentProjectId = dbProject.Id;

        await _context.SaveChangesAsync();
    }
    
    public async Task ChangeOrCreateDirectionOfUser(UserDtoForDataTransmission user, InternshipDirectionDto direction)
    {
        
        var dbProject = await _context.InternshipDirections.FindAsync(direction.Id);
        if (dbProject == null)
        {
            dbProject = direction.ToInternshipDirection();
            _context.InternshipDirections.Add(dbProject);
            await _context.SaveChangesAsync(); 
        }

        var dbUser = await _context.Users.FindAsync(user.Id);
        if (dbUser == null)
        {
            throw new Exception($"User with Id {user.Id} not found.");
        }
        dbUser.InternshipDirectionId = dbProject.Id;
        await _context.SaveChangesAsync();
    }
    
    public IEnumerable<UserDtoForDataTransmission> Distinct(IEnumerable<UserDtoForDataTransmission> source, IEnumerable<UserDtoForDataTransmission> usersMustNotContains)
    {
        var excludedIds = usersMustNotContains.Select(u => u.Id).ToHashSet();

        return source
            .Where(u => !excludedIds.Contains(u.Id))
            .ToList();
    }
}