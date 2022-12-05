using System;
using System.Collections.Generic;

namespace Day_8.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? Action { get; set; }

    public string? Controller { get; set; }
}
