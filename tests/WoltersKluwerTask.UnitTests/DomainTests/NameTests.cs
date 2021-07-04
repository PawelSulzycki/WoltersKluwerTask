using FluentAssertions;
using System;
using System.Linq;
using WoltersKluwerTask.Domain.ValueObjects;
using Xunit;

namespace WoltersKluwerTask.UnitTests.DomainTests
{
    public class NameTests
    {
        [Fact]
        public void When_First_Name_Is_Empty_Throw_Argument_Exception()
        {
            var firstName = "";

            var lastName = "TestLast";

            Action act = () => new Name(firstName, lastName);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_First_Name_Is_Null_Throw_Argument_Exception()
        {
            string firstName = null;

            var lastName = "TestLast";

            Action act = () => new Name(firstName, lastName);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Last_Name_Is_Empty_Throw_Argument_Exception()
        {
            var firstName = "TestFirst";

            var lastName = "";

            Action act = () => new Name(firstName, lastName);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Last_Name_Is_Null_Throw_Argument_Exception()
        {
            var firstName = "TestFirst";

            string lastName = null;

            Action act = () => new Name(firstName, lastName);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_First_Name_Has_More_Characters_Than_25_Throw_Argument_Exception()
        {
            var firstName = string.Join("", Enumerable.Repeat(0, 26).Select(n => new Random().Next(10)));

            var lastName = "TestLast";

            Action act = () => new Name(firstName, lastName);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_Last_Name_Has_More_Characters_Than_50_Throw_Argument_Exception()
        {
            var firstName = "TestFirst";

            var lastName = string.Join("", Enumerable.Repeat(0, 51).Select(n => new Random().Next(10)));

            Action act = () => new Name(firstName, lastName);

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void When_First_Name_And_Last_Name_Are_Correct_Create_Object()
        {
            var firstName = "TestFirst";

            var lastName = "TestLast";

            var name = new Name(firstName, lastName);

            name.First.Should().Be(firstName);

            name.Last.Should().Be(lastName);
        }
    }
}
