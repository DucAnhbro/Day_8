using Day_8.Models;

namespace Day_8.Dto
{
    public class EmployeDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EmployeRoleDto>? Roles { get; set; }

        public EmployeDetail() { }
        public EmployeDetail(Employe employe)
        {
            Id = employe.Id;
            Name = employe.UserName;
        }
    }

    public class EmployeRoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public bool Status { get; set; }
    }
}
