using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Application.CQRS.Employees.Commands;
using EmployeeManagement.Application.Interfaces;
using MediatR;

namespace EmployeeManagement.Application.CQRS.Employees.Handlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;

        public DeleteEmployeeHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);
            if (existing == null) return Unit.Value;

            await _repository.DeleteAsync(existing);
            return Unit.Value;
        }
    }
}
