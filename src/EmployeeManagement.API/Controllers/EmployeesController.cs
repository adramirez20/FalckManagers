using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Application.CQRS.Employees.Queries;
using EmployeeManagement.Application.CQRS.Employees.Commands;
using EmployeeManagement.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var employees = await _mediator.Send(new GetEmployeesQuery());
            return Ok(employees);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            var emp = await _mediator.Send(new GetEmployeeByIdQuery(id));
            if (emp == null) return NotFound();

            return Ok(emp);
        }

        [HttpGet("department/{departmentId}/with-projects")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetByDepartmentWithProjects(int departmentId)
        {
            var employees = await _mediator.Send(new GetEmployeesByDepartmentQuery(departmentId));
            return Ok(employees);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] CreateEmployeeDto create)
        {
            var id = await _mediator.Send(new CreateEmployeeCommand(create));
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateEmployeeDto update)
        {
            await _mediator.Send(new UpdateEmployeeCommand(id, update));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteEmployeeCommand(id));
            return NoContent();
        }
    }
}
