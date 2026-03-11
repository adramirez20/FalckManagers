using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.CQRS.Employees.Commands;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;

namespace EmployeeManagement.Application.CQRS.Employees.Handlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request.Create);

            await _repository.AddAsync(employee);
            return employee.Id;
        }
    }
}
