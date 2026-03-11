using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.CQRS.Employees.Queries;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using MediatR;

namespace EmployeeManagement.Application.CQRS.Employees.Handlers
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var e = await _repository.GetByIdAsync(request.Id);
            if (e == null) return null;
            return _mapper.Map<EmployeeDto>(e);
        }
    }
}
