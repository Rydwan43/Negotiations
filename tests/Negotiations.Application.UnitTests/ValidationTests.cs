using Xunit;
using FluentValidation;
using FluentValidation.TestHelper;
using Negotiations.Application.Validators;
using Negotiations.Application.Models;
using Moq;
using Negotiations.Infrastructure.DatabaseContext;

namespace Negotiations.Application.UnitTests;

public class RegisterUserDtoValidatorTester
{
    [Fact]
    public void ShouldHaveErrorWhereEmailAddres()
    {


    }
}