using System.Collections.Generic;
using WoltersKluwerTask.Domain.Ddd;

namespace WoltersKluwerTask.Domain.ValueObjects
{
    public class EmployeeId : ValueObject
    {
        public int Value { get; set; }

        public EmployeeId(int value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
