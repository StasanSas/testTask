using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmptyBlazorApp1.Services;

namespace EmptyBlazorApp1.Dto;

public class UserDtoForCreateUser : UserDtoForDataTransmission, IValidatableObject
{
    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var result in base.Validate(validationContext))
            yield return result;
        
        var userService = validationContext.GetService(typeof(UserService)) as UserService;

        if (!string.IsNullOrWhiteSpace(Phone))
        {
            var existsPhone = userService.ExistUserWithNumberPhone(Phone);
            if (existsPhone)
                yield return new ValidationResult("Телефон уже используется");
        }
        
        var existsEmail = userService.ExistUserWithEmail(Email);
        if (existsEmail)
            yield return new ValidationResult("Email уже используется");
    }
}