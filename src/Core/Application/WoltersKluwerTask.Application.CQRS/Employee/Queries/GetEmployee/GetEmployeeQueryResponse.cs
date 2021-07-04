namespace WoltersKluwerTask.Application.CQRS.Employee.Queries.GetEmployee
{
    public class GetEmployeeQueryResponse
    {
        Domain.Entities.Employee Employee { get; set; }

        public GetEmployeeQueryResponse(Domain.Entities.Employee employee) : base()
        {
            Employee = employee;
        }
    }
}
