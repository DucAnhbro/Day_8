using System;
using System.Collections.Generic;

namespace Day_8.Models;

public partial class EmployeRole
{
    public int Id { get; set; }

    public int EmployeId { get; set; }

    public int RoleId { get; set; }

    public virtual Role? Role { get; set; }
    public virtual Employe? Employe { get; set; }
}
