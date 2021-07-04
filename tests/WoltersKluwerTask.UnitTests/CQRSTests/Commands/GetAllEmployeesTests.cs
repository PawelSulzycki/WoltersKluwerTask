using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoltersKluwerTask.Application.Common;
using WoltersKluwerTask.Application.Contracts.Repositories;
using WoltersKluwerTask.Application.CQRS.Employee.Queries.GetAllEmployees;
using WoltersKluwerTask.Domain.Entities;
using WoltersKluwerTask.Domain.Enums;
using WoltersKluwerTask.Domain.ValueObjects;
using Xunit;

namespace WoltersKluwerTask.UnitTests.CQRSTests.Commands
{
    public class GetAllEmployeesTests
    {
        private readonly GetAllEmployeesQueryHandler _handler;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public GetAllEmployeesTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

            _handler = new GetAllEmployeesQueryHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task When_Return_Empty_List_From_Database_Return_Success()
        {
            var query = new GetAllEmployeesQuery();

            _mockEmployeeRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Employee>());

            var response = await _handler.Handle(query, new System.Threading.CancellationToken());

            response.Status.Should().Be(ResponseStatus.Success);
            response.Success.Should().Be(true);
            response.Employees.Should().BeEmpty();
        }

        [Fact]
        public async Task When_Return_Empty_List_From_Database_Verify_Method()
        {
            var query = new GetAllEmployeesQuery();

            _mockEmployeeRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Employee>());

            var response = await _handler.Handle(query, new System.Threading.CancellationToken());

            _mockEmployeeRepository
                .Verify(x => x.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task When_Return_List_From_Database_Return_Success()
        {
            var query = new GetAllEmployeesQuery();

            var employees = new List<Employee>()
            {
                new Employee(new EmployeeId(1), EvidenceNumber.Create(1), new Pesel("12345678910"), DateTime.Now, new Name("TestFirst", "TestLast"), Gender.Man),
                new Employee(new EmployeeId(2), EvidenceNumber.Create(2), new Pesel("12345678911"), DateTime.Now, new Name("TestFirst2", "TestLast2"), Gender.Woman)
            };

            _mockEmployeeRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(employees);

            var response = await _handler.Handle(query, new System.Threading.CancellationToken());

            response.Status.Should().Be(ResponseStatus.Success);
            response.Success.Should().Be(true);
            response.Employees.Should().NotBeEmpty()
                .And.HaveCount(employees.Count);
        }

        [Fact]
        public async Task When_Return_List_From_Database_Verify_Method()
        {
            var query = new GetAllEmployeesQuery();

            var employees = new List<Employee>()
            {
                new Employee(new EmployeeId(1), EvidenceNumber.Create(1), new Pesel("12345678910"), DateTime.Now, new Name("TestFirst", "TestLast"), Gender.Man),
                new Employee(new EmployeeId(2), EvidenceNumber.Create(2), new Pesel("12345678911"), DateTime.Now, new Name("TestFirst2", "TestLast2"), Gender.Woman)
            };

            _mockEmployeeRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(employees);

            var response = await _handler.Handle(query, new System.Threading.CancellationToken());

            _mockEmployeeRepository
                .Verify(x => x.GetAllAsync(), Times.Once());
        }
    }
}
