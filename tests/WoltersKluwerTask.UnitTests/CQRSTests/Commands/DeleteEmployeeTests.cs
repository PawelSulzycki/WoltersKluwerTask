using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using WoltersKluwerTask.Application.Common;
using WoltersKluwerTask.Application.Contracts.Repositories;
using WoltersKluwerTask.Application.CQRS.Employee.Commands.DeleteEmployee;
using WoltersKluwerTask.Domain.ValueObjects;
using Xunit;

namespace WoltersKluwerTask.UnitTests.CQRSTests.Commands
{
    public partial class DeleteEmployeeTests
    {
        private readonly DeleteEmployeeCommandHandler _handler;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public DeleteEmployeeTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

            _handler = new DeleteEmployeeCommandHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task When_Remove_Employee_Return_Success()
        {
            var command = new DeleteEmployeeCommand
            {
                EmployeeId = new EmployeeId(int.MaxValue)
            };

            _mockEmployeeRepository
                .Setup(x => x.RemoveByIdAsync(It.Is<int>(x => x == command.EmployeeId.Value)))
                .Returns(Task.CompletedTask);

            var response = await _handler.Handle(command, new System.Threading.CancellationToken());

            response.Status.Should().Be(ResponseStatus.Success);
            response.Success.Should().Be(true);
        }

        [Fact]
        public async Task When_Remove_Employee_Verify_Method()
        {
            var command = new DeleteEmployeeCommand
            {
                EmployeeId = new EmployeeId(int.MaxValue)
            };

            _mockEmployeeRepository
                .Setup(x => x.RemoveByIdAsync(It.Is<int>(x => x == command.EmployeeId.Value)))
                .Returns(Task.CompletedTask);

            var response = await _handler.Handle(command, new System.Threading.CancellationToken());

            _mockEmployeeRepository
                .Verify(x => x.RemoveByIdAsync(command.EmployeeId.Value), Times.Once());
        }
    }
}
