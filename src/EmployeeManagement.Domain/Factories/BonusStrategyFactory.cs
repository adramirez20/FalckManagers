using EmployeeManagement.Domain.Interfaces;

namespace EmployeeManagement.Domain.Factories
{
    public static class BonusStrategyFactory
    {
        public static IBonusStrategy Create(int position)
        {
            // Simple mapping: position 1 -> manager
            return position switch
            {
                1 => new Strategies.ManagerBonusStrategy(),
                _ => new Strategies.RegularBonusStrategy()
            };
        }
    }
}
