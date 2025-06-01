using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using EmptyBlazorApp1.Services;

namespace EmptyBlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

public enum Gender 
{
    Male,
    Female
}

[Table("users")]
public class User 
{
    [Key]
    public Guid Id { get; set; }
    
    [Column("name")]
    [MaxLength(50)]
    [Required(ErrorMessage = "Имя обязательно")]
    public string Name { get;set; }
    
    [Column("gender")]
    [Required(ErrorMessage = "Пол обязателен")]
    public Gender Gender { get; set; }
    
    [Column("email")]
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public String Email { get; set; }
    
    [Column("phone")]
    [Phone(ErrorMessage = "Некорректный номер телефона")]
    public String Phone { get; set; }
    
    [Column("date_of_birth")]
    [Required(ErrorMessage = "Дата рождения обязательна")]
    public DateTime DateOfBirth { get; set; }
    
    [Column("internship_direction_id")]
    [Required]
    public Guid InternshipDirectionId { get; set; }
    
    [ForeignKey(nameof(InternshipDirectionId))]
    public virtual InternshipDirection Direction { get; set; }
    
    [Column("current_project_id")]
    [Required]
    public Guid CurrentProjectId { get; set; }
    
    [ForeignKey(nameof(CurrentProjectId))]
    public virtual CurrentProject Project { get; set; }
    
}