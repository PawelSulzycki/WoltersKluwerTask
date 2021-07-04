using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoltersKluwerTask.Api.Models.Employee;
using WoltersKluwerTask.Application.Common;
using WoltersKluwerTask.Application.CQRS.Employee.Commands.CreateEmployee;
using WoltersKluwerTask.Application.CQRS.Employee.Queries.GetAllEmployees;
using WoltersKluwerTask.Application.CQRS.Employee.Queries.GetEmployee;
using WoltersKluwerTask.Domain.Entities;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllEmployeesQuery());

            return Ok(result.Employees);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Employee>> GetById([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetEmployeeQuery() { EmployeeId = new EmployeeId(id)});

            if (result.Status == ResponseStatus.NotFound)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeId>> Create([FromBody] CreateEmployeeRequest request)
        {
            var result = await _mediator.Send(new CreateEmployeeCommand()
            {
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Name = new Name(request.FirstName, request.LastName),
                Pesel = new Pesel(request.Pesel)
            });

            if(result.Status == ResponseStatus.BadQuery)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.EmployeeId);
        }
    }
}
