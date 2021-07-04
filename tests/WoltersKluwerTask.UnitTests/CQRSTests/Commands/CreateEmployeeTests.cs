using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using WoltersKluwerTask.Application.Common;
using WoltersKluwerTask.Application.Contracts.Repositories;
using WoltersKluwerTask.Application.CQRS.Employee.Commands.CreateEmployee;
using WoltersKluwerTask.Domain.Entities;
using WoltersKluwerTask.Domain.Enums;
using WoltersKluwerTask.Domain.ValueObjects;
using Xunit;

namespace WoltersKluwerTask.UnitTests.CQRSTests.Commands
{
    public class CreateEmployeeTests
    {
        private readonly CreateEmployeeCommandHandler _handler;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public CreateEmployeeTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

            _handler = new CreateEmployeeCommandHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task When_Pesel_Exists_In_Database_Return_Bad_Query()
        {
            var command = new CreateEmployeeCommand 
            {
                Pesel = new Pesel("12345678910")
            };

            _mockEmployeeRepository
                .Setup(x => x.ExistByPeselAsync(It.Is<Pesel>(x=> x == command.Pesel)))
                .ReturnsAsync(true);

            var response = await _handler.Handle(command, new System.Threading.CancellationToken());

            response.Status.Should().Be(ResponseStatus.BadQuery);
            response.Success.Should().Be(false);
            response.Message.Should().Be("The employee exists with the given PESEL number");
        } 

        [Fact]
        public async Task When_Pesel_Exists_In_Database_Verify_Methods()
        {
            var command = new CreateEmployeeCommand
            {
                Pesel = new Pesel("12345678910")
            };

            _mockEmployeeRepository
                .Setup(x => x.ExistByPeselAsync(It.Is<Pesel>(x => x == command.Pesel)))
                .ReturnsAsync(true);

            var response = await _handler.Handle(command, new System.Threading.CancellationToken());

            _mockEmployeeRepository
                .Verify(x => x.ExistByPeselAsync(command.Pesel), Times.Once());

            _mockEmployeeRepository
                .Verify(x => x.GetLastEvidenceNumberAsync(), Times.Never);

            _mockEmployeeRepository
                .Verify(x => x.AddAsync(It.IsAny<Employee>()), Times.Never);
        }

        [Fact]
        public async Task When_Pesel_Does_Not_Exists_In_Database_And_Add_Emplayee_Return_Success()
        {
            var command = new CreateEmployeeCommand
            {
                Pesel = new Pesel("12345678910"),
                DateOfBirth = DateTime.Now,
                Gender = Gender.Man,
                Name = new Name("TestFirst", "TestLast")
            };

            var emplayeeId = new EmployeeId(int.MaxValue);

            var lastEvidenceNumber = 1;

            _mockEmployeeRepository
                .Setup(x => x.ExistByPeselAsync(It.Is<Pesel>(x => x == command.Pesel)))
                .ReturnsAsync(false);

            _mockEmployeeRepository
                .Setup(x => x.GetLastEvidenceNumberAsync())
                .ReturnsAsync(lastEvidenceNumber);

            _mockEmployeeRepository
                .Setup(x => x.AddAsync(It.Is<Employee>(x=> 
                    x.Id.Equals(new EmployeeId(0))
                    && x.DateOfBirth == command.DateOfBirth
                    && x.Pesel.Equals(command.Pesel)
                    && x.Gender == command.Gender
                    && x.EvidenceNumber.Equals(EvidenceNumber.NewEvidenceNumberByLast(lastEvidenceNumber))
                    && x.Name.Equals(command.Name))))
                .ReturnsAsync(new Employee(
                    emplayeeId, 
                    EvidenceNumber.NewEvidenceNumberByLast(lastEvidenceNumber), 
                    command.Pesel, 
                    command.DateOfBirth, 
                    command.Name, 
                    command.Gender));

            var response = await _handler.Handle(command, new System.Threading.CancellationToken());

            response.Status.Should().Be(ResponseStatus.Success);
            response.Success.Should().Be(true);
            response.EmployeeId.Should().Be(emplayeeId);
        }

        [Fact]
        public async Task When_Pesel_Does_Not_Exists_In_Database_And_Add_Emplayee_Verify_Methods()
        {
            var command = new CreateEmployeeCommand
            {
                Pesel = new Pesel("12345678910"),
                DateOfBirth = DateTime.Now,
                Gender = Gender.Man,
                Name = new Name("TestFirst", "TestLast")
            };

            var emplayeeId = new EmployeeId(int.MaxValue);

            var lastEvidenceNumber = 1;

            _mockEmployeeRepository
                .Setup(x => x.ExistByPeselAsync(It.Is<Pesel>(x => x == command.Pesel)))
                .ReturnsAsync(false);

            _mockEmployeeRepository
                .Setup(x => x.GetLastEvidenceNumberAsync())
                .ReturnsAsync(lastEvidenceNumber);

            _mockEmployeeRepository
                .Setup(x => x.AddAsync(It.Is<Employee>(x =>
                    x.Id.Equals(new EmployeeId(0))
                    && x.DateOfBirth == command.DateOfBirth
                    && x.Pesel.Equals(command.Pesel)
                    && x.Gender == command.Gender
                    && x.EvidenceNumber.Equals(EvidenceNumber.NewEvidenceNumberByLast(lastEvidenceNumber))
                    && x.Name.Equals(command.Name))))
                .ReturnsAsync(new Employee(
                    emplayeeId,
                    EvidenceNumber.NewEvidenceNumberByLast(lastEvidenceNumber),
                    command.Pesel,
                    command.DateOfBirth,
                    command.Name,
                    command.Gender));

            var response = await _handler.Handle(command, new System.Threading.CancellationToken());

            _mockEmployeeRepository
                .Verify(x => x.ExistByPeselAsync(command.Pesel), Times.Once());

            _mockEmployeeRepository
                .Verify(x => x.GetLastEvidenceNumberAsync(), Times.Once());

            _mockEmployeeRepository
                .Verify(x => x.AddAsync(It.Is<Employee>(x =>
                    x.Id.Equals(new EmployeeId(0))
                    && x.DateOfBirth == command.DateOfBirth
                    && x.Pesel.Equals(command.Pesel)
                    && x.Gender == command.Gender
                    && x.EvidenceNumber.Equals(EvidenceNumber.NewEvidenceNumberByLast(lastEvidenceNumber))
                    && x.Name.Equals(command.Name))), Times.Once());
        }
    }
}
