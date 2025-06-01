using System.Collections.Generic;

namespace EmptyBlazorApp1.Dto;

public interface IViewedEntityWitchHaveUsers
{
    public string Name { get;set; }
    
    public List<UserDtoForDataTransmission> Users { get; set; }
}