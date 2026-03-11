using System.Collections.Generic;
using EmployeeManagement.Application.DTOs;
using MediatR;

namespace EmployeeManagement.Application.CQRS.Employees.Queries
{
    public record GetEmployeesQuery() : IRequest<IEnumerable<EmployeeDto>>;
}
