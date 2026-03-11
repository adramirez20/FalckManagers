using System;
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
    public class LinqQueryTests
    {
        private readonly IMapper _mapper;

        public LinqQueryTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetEmployeesByDepartmentWithProjects_Returns_Only_Employees_With_Projects()
        {
            // Arrange
            var repo = new InMemoryEmployeeRepository();
            
            // Employee 1: Department 1, has projects
            var employee1 = new Employee 
            { 
                Id = 1, 
                Name = "Alice", 
                Salary = 1000m, 
                CurrentPosition = 0, 
                DepartmentId = 1,
                EmployeeProjects = new List<EmployeeProject>
                {
                    new EmployeeProject { EmployeeId = 1, ProjectId = 1 }
                }
            };
            
            // Employee 2: Department 1, NO projects
            var employee2 = new Employee 
            { 
                Id = 2, 
                Name = "Bob", 
                Salary = 2000m, 
                CurrentPosition = 1, 
                DepartmentId = 1,
                EmployeeProjects = new List<EmployeeProject>()
            };
            
            // Employee 3: Department 2, has projects
            var employee3 = new Employee 
            { 
                Id = 3, 
                Name = "Charlie", 
                Salary = 3000m, 
                CurrentPosition = 1, 
                DepartmentId = 2,
                EmployeeProjects = new List<EmployeeProject>
                {
                    new EmployeeProject { EmployeeId = 3, ProjectId = 2 }
                }
            };
            
            // Employee 4: Department 1, has projects
            var employee4 = new Employee 
            { 
                Id = 4, 
                Name = "Diana", 
                Salary = 4000m, 
                CurrentPosition = 2, 
                DepartmentId = 1,
                EmployeeProjects = new List<EmployeeProject>
                {
                    new EmployeeProject { EmployeeId = 4, ProjectId = 1 },
                    new EmployeeProject { EmployeeId = 4, ProjectId = 2 }
                }
            };

            await repo.AddAsync(employee1);
            await repo.AddAsync(employee2);
            await repo.AddAsync(employee3);
            await repo.AddAsync(employee4);

            var handler = new GetEmployeesByDepartmentHandler(repo, _mapper);

            // Act
            var result = await handler.Handle(new GetEmployeesByDepartmentQuery(1), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            var list = result.ToList();
            
            // Should return only employees from department 1 with projects
            Assert.Equal(2, list.Count);
            Assert.Contains(list, e => e.Name == "Alice");
            Assert.Contains(list, e => e.Name == "Diana");
            
            // Should NOT include Bob (no projects) or Charlie (different department)
            Assert.DoesNotContain(list, e => e.Name == "Bob");
            Assert.DoesNotContain(list, e => e.Name == "Charlie");
        }

        [Fact]
        public async Task GetEmployeesByDepartmentWithProjects_Returns_Empty_When_No_Employees_Match()
        {
            // Arrange
            var repo = new InMemoryEmployeeRepository();
            var handler = new GetEmployeesByDepartmentHandler(repo, _mapper);

            // Act - Query department 99 which has no employees
            var result = await handler.Handle(new GetEmployeesByDepartmentQuery(99), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetEmployeesByDepartmentWithProjects_Returns_Empty_When_Department_Has_No_Projects()
        {
            // Arrange
            var repo = new InMemoryEmployeeRepository();
            
            // Employees in department 5 but no projects assigned
            await repo.AddAsync(new Employee 
            { 
                Id = 1, 
                Name = "NoProjectEmployee", 
                DepartmentId = 5,
                EmployeeProjects = new List<EmployeeProject>()
            });

            var handler = new GetEmployeesByDepartmentHandler(repo, _mapper);

            // Act
            var result = await handler.Handle(new GetEmployeesByDepartmentQuery(5), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // Should be empty because employee has no projects
        }

        // Simple in-memory repository for testing
        private class InMemoryEmployeeRepository : IEmployeeRepository
        {
            private readonly List<Employee> _items = new();
            private int _nextId = 1;

            public Task AddAsync(Employee employee)
            {
                if (employee.Id == 0)
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
