using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EmptyBlazorApp1.Models;

namespace EmptyBlazorApp1.Dto;

public class CurrentProjectDto : IViewedEntityWitchHaveUsers
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get;set; }
    
    public List<UserDtoForDataTransmission> Users { get; set; } = new();
    
    public CurrentProject ToCurrentProject()
    {
        return new CurrentProject
        {
            Id = Id,
            Name = Name,
            Users = Users.Select(u => u.ToUser()).ToList()
        };
    }
}

public static class CurrentProjectDtoExtensions
{
    public static CurrentProjectDto ToCurrentProjectDto(this CurrentProject currentProject)
    {
        return new CurrentProjectDto
        {
            Id = currentProject.Id,
            Name = currentProject.Name,
            Users = currentProject.Users.Select(u => u.ToUserDto()).ToList()
        };
    }
    
    public static void UpdateFromDto(this CurrentProject project, CurrentProjectDto dto)
    {
        project.Name = dto.Name;
    }
}