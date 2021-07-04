using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using WoltersKluwerTask.Application.Common;
using WoltersKluwerTask.Application.Contracts.Repositories;
using WoltersKluwerTask.Application.CQRS.Employee.Queries.GetEmployee;
using WoltersKluwerTask.Domain.Entities;
using WoltersKluwerTask.Domain.Enums;
using WoltersKluwerTask.Domain.ValueObjects;
using Xunit;

namespace WoltersKluwerTask.UnitTests.CQRSTests.Commands
{
    public class GetEmployeeTests
    {
        private readonly GetEmployeeQueryHandler _handler;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public GetEmployeeTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

            _handler = new GetEmployeeQueryHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task When_Not_Found_Employee_Return_Not_Found()
        {
            var query = new GetEmployeeQuery
            {
                EmployeeId = new EmployeeId(int.MaxValue)
            };

            _mockEmployeeRepository
                .Setup(x => x.GetByIdAsync(It.Is<int>(x => x == query.EmployeeId.Value)))
                .ReturnsAsync((Employee)null);

            var response = await _handler.Handle(query, new System.Threading.CancellationToken());

            response.Status.Should().Be(ResponseStatus.NotFound);
            response.Success.Should().Be(false);
            response.Message.Should().Be("The employee does not exist");
        }

        [Fact]
        public async Task When_Not_Found_Employee_Verify_Methods()
        {
            var query = new GetEmployeeQuery
            {
                EmployeeId = new EmployeeId(int.MaxValue)
            };

            _mockEmployeeRepository
                .Setup(x => x.GetByIdAsync(It.Is<int>(x => x == query.EmployeeId.Value)))
                .ReturnsAsync((Employee)null);

            var response = await _handler.Handle(query, new System.Threading.CancellationToken());

            _mockEmployeeRepository
                .Verify(x => x.GetByIdAsync(query.EmployeeId.Value), Times.Once());
        }

        [Fact]
        public async Task When_Found_Employee_Return_Success()
        {
            var query = new GetEmployeeQuery
            {
                EmployeeId = new EmployeeId(int.MaxValue)
            };

            var employee = new Employee(
                query.EmployeeId,
                EvidenceNumber.Create(1),
                new Pesel("12345678910"),
                DateTime.Now,
                new Name("TestFirst1", "TestLast1"),
                Gender.Man);

            _mockEmployeeRepository
                .Setup(x => x.GetByIdAsync(It.Is<int>(x => x == query.EmployeeId.Value)))
                .ReturnsAsync(employee);

            var response = await _handler.Handle(query, new System.Threading.CancellationToken());

            response.Status.Should().Be(ResponseStatus.Success);
            response.Success.Should().Be(true);
            response.Employee.Id.Should().Be(employee.Id);
            response.Employee.EvidenceNumber.Should().Be(employee.EvidenceNumber);
            response.Employee.Pesel.Should().Be(employee.Pesel);
            response.Employee.DateOfBirth.Should().Be(employee.DateOfBirth);
            response.Employee.Name.Should().Be(employee.Name);
            response.Employee.Gender.Should().Be(employee.Gender);
        }

        [Fact]
        public async Task When_Found_Employee_Verify_Methods()
        {
            var query = new GetEmployeeQuery
            {
                EmployeeId = new EmployeeId(int.MaxValue)
            };

            var employee = new Employee(
                query.EmployeeId,
                EvidenceNumber.Create(1),
                new Pesel("12345678910"),
                DateTime.Now,
                new Name("TestFirst1", "TestLast1"),
                Gender.Man);

            _mockEmployeeRepository
                .Setup(x => x.GetByIdAsync(It.Is<int>(x => x == query.EmployeeId.Value)))
                .ReturnsAsync(employee);

            var response = await _handler.Handle(query, new System.Threading.CancellationToken());

            _mockEmployeeRepository
                .Verify(x => x.GetByIdAsync(query.EmployeeId.Value), Times.Once());
        }
    }
}
