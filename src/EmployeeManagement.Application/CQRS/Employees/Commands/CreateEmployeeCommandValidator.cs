using FluentValidation;
using EmployeeManagement.Application.CQRS.Employees.Commands;

namespace EmployeeManagement.Application.CQRS.Employees.Commands
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.Create.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Create.Salary).GreaterThan(0);
            RuleFor(x => x.Create.DepartmentId).GreaterThan(0);
        }
    }
}
