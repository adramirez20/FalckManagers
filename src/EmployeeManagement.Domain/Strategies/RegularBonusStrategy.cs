using EmployeeManagement.Domain.Interfaces;

namespace EmployeeManagement.Domain.Strategies
{
    public class RegularBonusStrategy : IBonusStrategy
    {
        public decimal CalculateBonus(decimal salary)
        {
            return salary * 0.10m;
        }
    }
}
