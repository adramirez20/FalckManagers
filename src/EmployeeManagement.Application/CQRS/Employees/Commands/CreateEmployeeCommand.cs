using EmployeeManagement.Application.DTOs;
using MediatR;

namespace EmployeeManagement.Application.CQRS.Employees.Commands
{
    public record CreateEmployeeCommand(CreateEmployeeDto Create) : IRequest<int>;
}
