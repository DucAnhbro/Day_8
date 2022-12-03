using System;
using System.Collections.Generic;

namespace Day_8.Models
{
    public partial class Group
    {
        public Group()
        {
            Employe = new HashSet<Employe>();
        }
        public int Id { get; set; }

        public string? RoleName { get; set; }

        public bool Status { get; set; }
    }
}

