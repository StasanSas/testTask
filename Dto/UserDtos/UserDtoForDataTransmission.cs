using EmptyBlazorApp1.Models;

namespace EmptyBlazorApp1.Dto;

public class UserDtoForDataTransmission : UserDto
{
    
}

public static class UserDtoForDataTransmissionExtensions
{
    public static UserDtoForDataTransmission ToUserDto(this User user)
    {
        return new UserDtoForDataTransmission()
        {
            Id = user.Id,
            Name = user.Name,
            Gender = user.Gender,
            Email = user.Email,
            Phone = user.Phone,
            DateOfBirth = user.DateOfBirth,
            InternshipDirectionId = user.InternshipDirectionId,
            CurrentProjectId = user.CurrentProjectId
        };
    }
    
    public static void UpdateFromDto(this User user, UserDto dto)
    {
        user.Name = dto.Name;
        user.Email = dto.Email;
        user.Phone = dto.Phone;
        user.DateOfBirth = dto.DateOfBirth;
        user.Gender = dto.Gender;
        user.InternshipDirectionId = dto.InternshipDirectionId;
        user.CurrentProjectId = dto.CurrentProjectId;
    }
}