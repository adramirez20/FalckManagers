using EmployeeManagement.Domain.Interfaces;

namespace EmployeeManagement.Domain.Strategies
{
    public class ManagerBonusStrategy : IBonusStrategy
    {
        public decimal CalculateBonus(decimal salary)
        {
            return salary * 0.20m;
        }
    }
}
