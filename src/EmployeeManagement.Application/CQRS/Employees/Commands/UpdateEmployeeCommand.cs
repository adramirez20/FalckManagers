using EmployeeManagement.Application.DTOs;
using MediatR;

namespace EmployeeManagement.Application.CQRS.Employees.Commands
{
    public record UpdateEmployeeCommand(int Id, UpdateEmployeeDto Update) : IRequest;
}
