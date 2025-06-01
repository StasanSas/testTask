using System;
using System.Linq.Expressions;
using EmptyBlazorApp1.Models;

namespace EmptyBlazorApp1.Dto;

public class FilteringMode
{
    public Func<IViewedEntityWitchHaveUsers, bool>? filter;
}