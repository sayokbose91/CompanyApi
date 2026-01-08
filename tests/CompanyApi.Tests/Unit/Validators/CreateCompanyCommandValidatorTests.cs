using CompanyApi.Application.Companies.Commands.CreateCompany;
using FluentAssertions;

namespace CompanyApi.Tests.Unit.Validators;

public class CreateCompanyCommandValidatorTests
{
    private readonly CreateCompanyCommandValidator _validator = new();

    [Fact]
    public void Validate_ValidCommand_ShouldNotHaveValidationErrors()
    {
        var command = new CreateCompanyCommand
        {
            Name = "Test Company",
            Email = "test@example.com",
            Website = "https://example.com",
            EmployeeCount = 100
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_EmptyName_ShouldHaveValidationError()
    {
        var command = new CreateCompanyCommand { Name = "" };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public void Validate_InvalidEmail_ShouldHaveValidationError()
    {
        var command = new CreateCompanyCommand
        {
            Name = "Test Company",
            Email = "invalid-email"
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email");
    }
}
