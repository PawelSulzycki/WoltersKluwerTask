using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using WoltersKluwerTask.Application.Common;
using WoltersKluwerTask.Application.Contracts.Repositories;
using WoltersKluwerTask.Application.CQRS.Employee.Commands.UpdateEmployee;
using WoltersKluwerTask.Domain.Entities;
using WoltersKluwerTask.Domain.Enums;
using WoltersKluwerTask.Domain.ValueObjects;
using Xunit;

namespace WoltersKluwerTask.UnitTests.CQRSTests.Commands
{
    public class UpdateEmployeeTests
    {
        private readonly UpdateEmployeeCommandHandler _handler;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public UpdateEmployeeTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

            _handler = new UpdateEmployeeCommandHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task When_Not_Found_Employee_Return_Not_Found()
        {
            var command = new UpdateEmployeeCommand
            {
                EmployeeId = new EmployeeId(int.MaxValue)
            };

            _mockEmployeeRepository
                .Setup(x => x.GetByIdAsync(It.Is<int>(x => x == command.EmployeeId.Value)))
                .ReturnsAsync((Employee)null);

            var response = await _handler.Handle(command, new System.Threading.CancellationToken());

            response.Status.Should().Be(ResponseStatus.NotFound);
            response.Success.Should().Be(false);
            response.Message.Should().Be("The employee does not exist");
        }

        [Fact]
        public async Task When_Not_Found_Employee_Verify_Methods()
        {
            var command = new UpdateEmployeeCommand
            {
                EmployeeId = new EmployeeId(int.MaxValue)
            };

            _mockEmployeeRepository
                .Setup(x => x.GetByIdAsync(It.Is<int>(x => x == command.EmployeeId.Value)))
                .ReturnsAsync((Employee)null);

            var response = await _handler.Handle(command, new System.Threading.CancellationToken());

            _mockEmployeeRepository
                .Verify(x => x.GetByIdAsync(command.EmployeeId.Value), Times.Once());

            _mockEmployeeRepository
                .Verify(x => x.UpdateAsync(It.IsAny<Employee>()), Times.Never);
        }

        [Fact]
        public async Task When_Found_Emploee_And_Update_Employee_Return_Success()
        {
            var command = new UpdateEmployeeCommand
            {
                EmployeeId = new EmployeeId(int.MaxValue),
                DateOfBirth = DateTime.Now,
                Gender = Gender.Man,
                Name = new Name("TestFirst", "TestLast")
            };

            var evidenceNumber = EvidenceNumber.Create(1);

            var pesel = new Pesel("12345678910");

            _mockEmployeeRepository
                .Setup(x => x.GetByIdAsync(It.Is<int>(x => x == command.EmployeeId.Value)))
                .ReturnsAsync(new Employee(
                    command.EmployeeId,
                    evidenceNumber,
                    pesel,
                    DateTime.Now.AddDays(-1),
                    new Name("TestFirst1", "TestLast1"),
                    Gender.Woman));

            _mockEmployeeRepository
                .Setup(x => x.UpdateAsync(It.Is<Employee>(x =>
                     x.Id.Equals(command.EmployeeId)
                     && x.DateOfBirth == command.DateOfBirth
                     && x.EvidenceNumber.Equals(evidenceNumber)
                     && x.Gender == command.Gender
                     && x.Name.Equals(command.Name)
                     && x.Pesel.Equals(pesel))))
                .Returns(Task.CompletedTask);

            var response = await _handler.Handle(command, new System.Threading.CancellationToken());

            response.Status.Should().Be(ResponseStatus.Success);
            response.Success.Should().Be(true);
        }

        [Fact]
        public async Task When_Found_Emploee_And_Update_Employee_Verify_Methods()
        {
            var command = new UpdateEmployeeCommand
            {
                EmployeeId = new EmployeeId(int.MaxValue),
                DateOfBirth = DateTime.Now,
                Gender = Gender.Man,
                Name = new Name("TestFirst", "TestLast")
            };

            var evidenceNumber = EvidenceNumber.Create(1);

            var pesel = new Pesel("12345678910");

            _mockEmployeeRepository
                .Setup(x => x.GetByIdAsync(It.Is<int>(x => x == command.EmployeeId.Value)))
                .ReturnsAsync(new Employee(
                    command.EmployeeId,
                    evidenceNumber,
                    pesel,
                    DateTime.Now.AddDays(-1),
                    new Name("TestFirst1", "TestLast1"),
                    Gender.Woman));

            _mockEmployeeRepository
                .Setup(x => x.UpdateAsync(It.Is<Employee>(x =>
                     x.Id.Equals(command.EmployeeId)
                     && x.DateOfBirth == command.DateOfBirth
                     && x.EvidenceNumber.Equals(evidenceNumber)
                     && x.Gender == command.Gender
                     && x.Name.Equals(command.Name)
                     && x.Pesel.Equals(pesel))))
                .Returns(Task.CompletedTask);

            var response = await _handler.Handle(command, new System.Threading.CancellationToken());

            _mockEmployeeRepository
                .Verify(x => x.GetByIdAsync(command.EmployeeId.Value), Times.Once());

            _mockEmployeeRepository
                .Verify(x => x.UpdateAsync(It.Is<Employee>((x =>
                     x.Id.Equals(command.EmployeeId)
                     && x.DateOfBirth == command.DateOfBirth
                     && x.EvidenceNumber.Equals(evidenceNumber)
                     && x.Gender == command.Gender
                     && x.Name.Equals(command.Name)
                     && x.Pesel.Equals(pesel)))), Times.Once());
        }
    }
}
