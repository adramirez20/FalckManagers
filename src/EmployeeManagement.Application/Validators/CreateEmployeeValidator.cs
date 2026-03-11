using FluentValidation;
using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.Application.Validators
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Salary)
                .GreaterThan(0);

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0);
        }
    }
}
