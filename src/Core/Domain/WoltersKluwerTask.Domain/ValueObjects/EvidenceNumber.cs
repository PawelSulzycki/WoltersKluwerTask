using Dawn;
using System.Collections.Generic;
using WoltersKluwerTask.Domain.Ddd;

namespace WoltersKluwerTask.Domain.ValueObjects
{
    public class EvidenceNumber : ValueObject
    {
        public string Value { get; private set; }

        public EvidenceNumber(string value)
        {
            Value = Guard.Argument(value, nameof(value)).NotEmpty().NotNull().Length(8);
        }

        public static EvidenceNumber Create(int value)
        {
            var result = $"{value:00000000}";

            return new EvidenceNumber(result);
        }

        public static EvidenceNumber NewEvidenceNumberByLast(int lastEvidenceNumber)
        {
            lastEvidenceNumber++;

            return Create(lastEvidenceNumber);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
