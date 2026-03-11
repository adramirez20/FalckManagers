using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.CQRS.Employees.Handlers;
using EmployeeManagement.Application.CQRS.Employees.Queries;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Mappings;
using EmployeeManagement.Domain.Entities;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class EmployeesHandlerTests
    {
        private readonly IMapper _mapper;

        public EmployeesHandlerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetEmployeesHandler_Returns_MappedDtos()
        {
            var repo = new InMemoryEmployeeRepository();
            await repo.AddAsync(new Employee { Id = 1, Name = "Alice", Salary = 1000m, CurrentPosition = 0, DepartmentId = 1 });
            await repo.AddAsync(new Employee { Id = 2, Name = "Bob", Salary = 2000m, CurrentPosition = 1, DepartmentId = 2 });

            var handler = new GetEmployeesHandler(repo, _mapper);
            var result = await handler.Handle(new GetEmployeesQuery(), CancellationToken.None);

            Assert.NotNull(result);
            var list = result.ToList();
            Assert.Equal(2, list.Count);
            Assert.Contains(list, e => e.Name == "Alice" && e.Id == 1);
            Assert.Contains(list, e => e.Name == "Bob" && e.Id == 2);
        }

        [Fact]
        public async Task CreateEmployeeHandler_Adds_Entity_And_Returns_Id()
        {
            var repo = new InMemoryEmployeeRepository();
            var handler = new CreateEmployeeHandler(repo, _mapper);

            var create = new CreateEmployeeDto("Charlie", 0, 1500m, 1);
            var command = new EmployeeManagement.Application.CQRS.Employees.Commands.CreateEmployeeCommand(create);

            var id = await handler.Handle(command, CancellationToken.None);

            Assert.True(id > 0);

            var added = await repo.GetByIdAsync(id);
            Assert.NotNull(added);
            Assert.Equal("Charlie", added.Name);
            Assert.Equal(1500m, added.Salary);
        }

        // Simple in-memory repository used for tests
        private class InMemoryEmployeeRepository : IEmployeeRepository
        {
            private readonly List<Employee> _items = new();
            private int _nextId = 1;

            public Task AddAsync(Employee employee)
            {
                employee.Id = _nextId++;
                _items.Add(employee);
                return Task.CompletedTask;
            }

            public Task DeleteAsync(Employee employee)
            {
                _items.RemoveAll(e => e.Id == employee.Id);
                return Task.CompletedTask;
            }

            public Task<List<Employee>> GetAllAsync()
            {
                // return shallow copy
                return Task.FromResult(_items.ToList());
            }

            public Task<Employee> GetByIdAsync(int id)
            {
                return Task.FromResult(_items.FirstOrDefault(e => e.Id == id));
            }

            public Task UpdateAsync(Employee employee)
            {
                var idx = _items.FindIndex(e => e.Id == employee.Id);
                if (idx >= 0) _items[idx] = employee;
                return Task.CompletedTask;
            }

            public Task<List<Employee>> GetEmployeesByDepartmentWithProjectsAsync(int departmentId)
            {
                var filtered = _items
                    .Where(e => e.DepartmentId == departmentId 
                                && e.EmployeeProjects != null 
                                && e.EmployeeProjects.Any())
                    .ToList();
                return Task.FromResult(filtered);
            }
        }
    }
}
