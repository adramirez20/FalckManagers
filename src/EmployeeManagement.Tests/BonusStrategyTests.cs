using EmployeeManagement.Domain.Strategies;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class BonusStrategyTests
    {
        [Fact]
        public void RegularBonus_Is_10Percent()
        {
            var sut = new RegularBonusStrategy();
            var result = sut.CalculateBonus(1000m);
            Assert.Equal(100m, result);
        }

        [Fact]
        public void ManagerBonus_Is_20Percent()
        {
            var sut = new ManagerBonusStrategy();
            var result = sut.CalculateBonus(1000m);
            Assert.Equal(200m, result);
        }
    }
}
