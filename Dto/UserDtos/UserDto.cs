using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmptyBlazorApp1.Models;

namespace EmptyBlazorApp1.Dto;

public abstract class UserDto : IValidatableObject
{
    
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Имя обязательно")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Пол обязателен")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Некорректный номер телефона")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Дата рождения обязательна")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Направление стажировки обязательно")]
    public Guid InternshipDirectionId { get; set; }

    [Required(ErrorMessage = "Проект обязателен")]
    public Guid CurrentProjectId { get; set; }
    
    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(Phone))
        {
            if (!Phone.StartsWith("+7"))
                yield return new ValidationResult("Телефон должен начинаться с +7");
        }
    }
    
    public User ToUser()
    {
        return new User
        {
            Id = Id,
            Name = Name,
            Gender = Gender,
            Email = Email,
            Phone = Phone,
            DateOfBirth = DateOfBirth,
            InternshipDirectionId = InternshipDirectionId,
            CurrentProjectId = CurrentProjectId
        };
    }
    

}

