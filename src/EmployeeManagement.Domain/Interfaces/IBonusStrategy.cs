namespace EmployeeManagement.Domain.Interfaces
{
    public interface IBonusStrategy
    {
        decimal CalculateBonus(decimal salary);
    }
}
