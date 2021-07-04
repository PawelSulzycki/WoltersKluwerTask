using FluentAssertions;
using System;
using WoltersKluwerTask.Domain.ValueObjects;
using Xunit;

namespace WoltersKluwerTask.UnitTests.DomainTests
{
    public class EvidenceNumberTests
    {
        [Fact]
        public void When_Value_Is_Empty_Throw_Argument_Exception()
        {
            var value = "";

            Action act = () => new EvidenceNumber(value);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Value_Is_Null_Throw_Argument_Exception()
        {
            string value = null;

            Action act = () => new EvidenceNumber(value);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Value_Has_Fewer_Characters_Than_8_Throw_Argument_Exception()
        {
            var value = "0000001";

            Action act = () => new EvidenceNumber(value);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Value_Has_More_Characters_Than_8_Throw_Argument_Exception()
        {
            var value = "000000001";

            Action act = () => new EvidenceNumber(value);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Value_Is_Correct_Create_Object()
        {
            var value = "00000001";

            var evidenceNumber = new EvidenceNumber(value);

            evidenceNumber.Value.Should().Be(value);
        }

        [Fact]
        public void Create_Number_From_4_Return_00000004()
        {
            var value = 4;

            var evidenceNumber = EvidenceNumber.Create(value);

            evidenceNumber.Value.Should().Be("00000004");
        }

        [Fact]
        public void Create_Number_From_1234567_Return_01234567()
        {
            var value = 1234567;

            var evidenceNumber = EvidenceNumber.Create(value);

            evidenceNumber.Value.Should().Be("01234567");
        }

        [Fact]
        public void New_Evidence_Number_For_Last_Evidence_Number_2_Return_00000003()
        {
            var value = 2;

            var evidenceNumber = EvidenceNumber.NewEvidenceNumberByLast(value);

            evidenceNumber.Value.Should().Be("00000003");
        }
    }
}
