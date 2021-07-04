using FluentAssertions;
using System;
using WoltersKluwerTask.Domain.ValueObjects;
using Xunit;

namespace WoltersKluwerTask.UnitTests.DomainTests
{
    public class PeselTests
    {
        [Fact]
        public void When_Value_Is_Empty_Throw_Argument_Exception()
        {
            var value = "";

            Action act = () => new Pesel(value);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Value_Is_Null_Throw_Argument_Exception()
        {
            string value = null;

            Action act = () => new Pesel(value);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Value_Has_Fewer_Characters_Than_11_Throw_Argument_Exception()
        {
            var value = "123456789";

            Action act = () => new Pesel(value);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Value_Has_More_Characters_Than_11_Throw_Argument_Exception()
        {
            var value = "123456789101";

            Action act = () => new Pesel(value);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Value_Has_Numbers_And_Letters_Throw_Argument_Exception()
        {
            var value = "ab345678910";

            Action act = () => new Pesel(value);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Value_Has_Only_Numbers_And_11_Characters_Create_Object()
        {
            var value = "12345678910";

            var pesel = new Pesel(value);

            pesel.Value.Should().Be(value);
        }
    }
}
