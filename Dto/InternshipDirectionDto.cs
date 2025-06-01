using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EmptyBlazorApp1.Models;

namespace EmptyBlazorApp1.Dto;

public class InternshipDirectionDto : IViewedEntityWitchHaveUsers
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get;set; }
    
    public List<UserDtoForDataTransmission> Users { get; set; } = new();
    
    public InternshipDirection ToInternshipDirection()
    {
        return new InternshipDirection
        {
            Id = Id,
            Name = Name,
            Users = Users.Select(u => u.ToUser()).ToList()
        };
    }
}

public static class InternshipDirectionDtoExtensions
{
    public static InternshipDirectionDto ToInternshipDirectionDto(this InternshipDirection InternshipDirection)
    {
        return new InternshipDirectionDto
        {
            Id = InternshipDirection.Id,
            Name = InternshipDirection.Name,
            Users = InternshipDirection.Users.Select(u => u.ToUserDto()).ToList()
        };
    }
    
    public static void UpdateFromDto(this InternshipDirection project, InternshipDirectionDto dto)
    {
        project.Name = dto.Name;
    }
}