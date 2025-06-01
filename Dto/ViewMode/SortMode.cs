using System;
using EmptyBlazorApp1.Models;

namespace EmptyBlazorApp1.Dto;

public class SortMode
{
    public Func<IViewedEntityWitchHaveUsers, IComparable>? comporator;
}