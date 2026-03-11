using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.CQRS.Employees.Queries;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using MediatR;

namespace EmployeeManagement.Application.CQRS.Employees.Handlers
{
    public class GetEmployeesByDepartmentHandler : IRequestHandler<GetEmployeesByDepartmentQuery, IEnumerable<EmployeeDto>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public GetEmployeesByDepartmentHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(GetEmployeesByDepartmentQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repository.GetEmployeesByDepartmentWithProjectsAsync(request.DepartmentId);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }
    }
}
