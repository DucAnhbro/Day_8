using System;
using System.Collections.Generic;

namespace Day_8.Models;

public partial class Employe
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? BirthDay { get; set; }

    public string? Adress { get; set; }

    public string? Email { get; set; }

    public int? Age { get; set; }

    public bool Gender { get; set; }

    public int GroupId { get; set; }

    public bool Status { get; set; }

    public int? IsSystem { get; set; }

    public string? UserName { get; set; }

    public virtual Group? Group { get; set; }
    public virtual ICollection<EmployeRole> EmployeRoles { get; set; }
}
