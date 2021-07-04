using Dawn;
using System.Collections.Generic;
using WoltersKluwerTask.Domain.Ddd;

namespace WoltersKluwerTask.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string First { get; private set; }
        public string Last { get; private set; }

        public Name(string first, string last)
        {
            First = Guard.Argument(first, nameof(first)).NotEmpty().NotNull().MaxLength(25);
            Last = Guard.Argument(last, nameof(last)).NotEmpty().NotNull().MaxLength(50);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return First;
            yield return Last;
        }
    }
}
