using CompanyApi.Application.Companies.Commands.CreateCompany;
using CompanyApi.Domain.Entities;
using CompanyApi.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace CompanyApi.Tests.Unit.Handlers;

public class CreateCompanyCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateCompany()
    {
        var mockRepository = new Mock<IRepository<Company>>();
        var handler = new CreateCompanyCommandHandler(mockRepository.Object);

        var command = new CreateCompanyCommand
        {
            Name = "Test Company",
            Email = "test@example.com"
        };

        var createdCompany = new Company
        {
            Id = "123",
            Name = command.Name,
            Email = command.Email,
            IsActive = true
        };

        mockRepository
            .Setup(r => r.AddAsync(It.IsAny<Company>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdCompany);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Company>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
