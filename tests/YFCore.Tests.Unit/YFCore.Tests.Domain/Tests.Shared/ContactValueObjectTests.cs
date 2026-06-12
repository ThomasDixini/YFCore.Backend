using System;

using FluentAssertions;

using Xunit;

using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Tests.Unit.YFCore.Tests.Domain.TestsShared
{
    public class ContactValueObjectTests
    {
        [Fact]
        public void Email_ShouldInitialize_WhenValueIsValid()
        {
            var email = new Email("user@example.com");

            email.Value.Should().Be("user@example.com");
            email.ToString().Should().Be("user@example.com");
        }

        [Fact]
        public void Email_ShouldThrow_WhenValueIsNull()
        {
            string? value = null;
            Action act = () => new Email(value!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Email_ShouldThrow_WhenValueIsEmptyOrWhitespace(string value)
        {
            Action act = () => new Email(value);

            act.Should().Throw<ArgumentException>().WithMessage("Email cannot be empty.*");
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("user@")]
        [InlineData("@example.com")]
        [InlineData("user@@example.com")]
        public void Email_ShouldThrow_WhenValueIsInvalid(string value)
        {
            Action act = () => new Email(value);

            act.Should().Throw<ArgumentException>().WithMessage("Email is not valid.*");
        }

        [Fact]
        public void Phone_ShouldInitialize_WhenValueIsValid()
        {
            var phone = new Phone("+55 (11) 91234-5678");

            phone.Value.Should().Be("+5511912345678");
            phone.ToString().Should().Be("+5511912345678");
        }

        [Fact]
        public void Phone_ShouldThrow_WhenValueIsNull()
        {
            string? value = null;
            Action act = () => new Phone(value!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Phone_ShouldThrow_WhenValueIsEmptyOrWhitespace(string value)
        {
            Action act = () => new Phone(value);

            act.Should().Throw<ArgumentException>().WithMessage("Phone cannot be empty.*");
        }

        [Theory]
        [InlineData("abc123")]
        [InlineData("12-34-56")]
        [InlineData("+123456")]
        [InlineData("+1234567890123456")]
        public void Phone_ShouldThrow_WhenValueIsInvalid(string value)
        {
            Action act = () => new Phone(value);

            act.Should().Throw<ArgumentException>().WithMessage("Phone is not valid.*");
        }
    }
}
