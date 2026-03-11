namespace EmployeeManagement.Application.DTOs
{
    public record EmployeeDto(int Id, string Name, int CurrentPosition, decimal Salary, int DepartmentId);

    public record CreateEmployeeDto(string Name, int CurrentPosition, decimal Salary, int DepartmentId);

    public record UpdateEmployeeDto(string Name, int CurrentPosition, decimal Salary, int DepartmentId);
}
