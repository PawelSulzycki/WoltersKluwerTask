using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoltersKluwerTask.Application.CQRS.Employee.Queries.GetAllEmployees;
using WoltersKluwerTask.Domain.Entities;

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
    }
}
