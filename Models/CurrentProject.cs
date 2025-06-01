using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmptyBlazorApp1.Models;

[Table("current_project")]
public class CurrentProject
{
    [Key]
    public Guid Id { get; set; }
    
    [Column("name")]
    [MaxLength(50)]
    [Required]
    public string Name { get;set; }

    [InverseProperty(nameof(User.Project))]
    public virtual List<User> Users { get; set; } = new();
}