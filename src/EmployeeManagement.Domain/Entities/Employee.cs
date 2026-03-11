using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Consider using an enum or FK to a Position entity in future iterations
        public int CurrentPosition { get; set; }

        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<PositionHistory> PositionHistories { get; set; } = new List<PositionHistory>();

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

        public decimal CalculateBonus(Interfaces.IBonusStrategy strategy)
        {
            return strategy.CalculateBonus(Salary);
        }
    }
}
