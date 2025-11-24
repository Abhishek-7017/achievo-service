using System;
using Microsoft.EntityFrameworkCore;

namespace Achievo.Shared;

[Owned]
public class Reference
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
