using Dawn;
using System.Collections.Generic;
using WoltersKluwerTask.Domain.Ddd;

namespace WoltersKluwerTask.Domain.ValueObjects
{
    public class Pesel : ValueObject
    {
        public string Value { get; private set; }

        public Pesel(string value)
        {
            Value = Guard.Argument(value, nameof(value)).NotEmpty().NotNull().Length(11).Matches("^[0-9]*$");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
