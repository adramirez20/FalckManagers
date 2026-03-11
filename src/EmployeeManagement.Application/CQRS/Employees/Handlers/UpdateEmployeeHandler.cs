using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Application.CQRS.Employees.Commands;
using EmployeeManagement.Application.Interfaces;
using MediatR;

namespace EmployeeManagement.Application.CQRS.Employees.Handlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;

        public UpdateEmployeeHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);
            if (existing == null) return Unit.Value;

            existing.Name = request.Update.Name;
            existing.Salary = request.Update.Salary;
            existing.CurrentPosition = request.Update.CurrentPosition;

            await _repository.UpdateAsync(existing);
            return Unit.Value;
        }
    }
}
