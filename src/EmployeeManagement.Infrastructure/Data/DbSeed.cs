using System.Linq;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Infrastructure.Data
{
    public static class DbSeed
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department { Name = "HR" },
                    new Department { Name = "Engineering" },
                    new Department { Name = "Sales" }
                );
                context.SaveChanges();
            }
        }
    }
}
