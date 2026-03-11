using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
