using EmployeeManagement.Application.DTOs;
using MediatR;

namespace EmployeeManagement.Application.CQRS.Employees.Queries
{
    public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDto>;
}
