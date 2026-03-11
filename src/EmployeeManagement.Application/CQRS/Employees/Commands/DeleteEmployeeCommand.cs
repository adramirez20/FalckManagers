using MediatR;

namespace EmployeeManagement.Application.CQRS.Employees.Commands
{
    public record DeleteEmployeeCommand(int Id) : IRequest;
}
