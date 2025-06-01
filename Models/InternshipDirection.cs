using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmptyBlazorApp1.Models;


[Table("internship_direction")]
public class InternshipDirection
{
    [Key]
    public Guid Id { get; set; }
    
    [Column("name")]
    [MaxLength(50)]
    [Required]
    public string Name { get;set; }
    
    [InverseProperty(nameof(User.Direction))]
    public List<User> Users { get; set; } = new();
}

