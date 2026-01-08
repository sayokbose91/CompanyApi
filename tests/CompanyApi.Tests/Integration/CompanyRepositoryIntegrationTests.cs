using CompanyApi.Domain.Entities;
using CompanyApi.Infrastructure.Persistence;
using EphemeralMongo;
using FluentAssertions;
using MongoDB.Driver;

namespace CompanyApi.Tests.Integration;

public class CompanyRepositoryIntegrationTests : IDisposable
{
    private readonly IMongoRunner _mongoRunner;
    private readonly IMongoDatabase _database;
    private readonly CompanyRepository _repository;

    public CompanyRepositoryIntegrationTests()
    {
        var options = new MongoRunnerOptions
        {
            UseSingleNodeReplicaSet = false,
            StandardOutputLogger = line => Console.WriteLine(line),
            StandardErrorLogger = line => Console.WriteLine(line)
        };

        _mongoRunner = MongoRunner.Run(options);
        var client = new MongoClient(_mongoRunner.ConnectionString);
        _database = client.GetDatabase("TestDb");

        var settings = new MongoDbSettings
        {
            ConnectionString = _mongoRunner.ConnectionString,
            DatabaseName = "TestDb",
            CompaniesCollectionName = "companies"
        };

        _repository = new CompanyRepository(_database, settings);
    }

    [Fact]
    public async Task AddAsync_ShouldAddCompanyToDatabase()
    {
        // Arrange
        var company = new Company
        {
            Name = "Test Company",
            Description = "Test Description",
            Industry = "Technology",
            Email = "test@example.com",
            IsActive = true
        };

        // Act
        var result = await _repository.AddAsync(company);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeNullOrEmpty();
        result.Name.Should().Be(company.Name);
        result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCompany()
    {
        // Arrange
        var company = new Company
        {
            Name = "Test Company",
            Industry = "Technology",
            IsActive = true
        };
        var added = await _repository.AddAsync(company);

        // Act
        var result = await _repository.GetByIdAsync(added.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be(company.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCompany()
    {
        // Arrange
        var company = new Company
        {
            Name = "Original Name",
            IsActive = true
        };
        var added = await _repository.AddAsync(company);

        // Act
        added.Name = "Updated Name";
        var result = await _repository.UpdateAsync(added);

        // Assert
        result.Name.Should().Be("Updated Name");
        result.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveCompany()
    {
        // Arrange
        var company = new Company
        {
            Name = "Test Company",
            IsActive = true
        };
        var added = await _repository.AddAsync(company);

        // Act
        var deleted = await _repository.DeleteAsync(added.Id);
        var result = await _repository.GetByIdAsync(added.Id);

        // Assert
        deleted.Should().BeTrue();
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetPagedAsync_ShouldReturnPaginatedResults()
    {
        // Arrange
        for (int i = 0; i < 15; i++)
        {
            await _repository.AddAsync(new Company
            {
                Name = $"Company {i}",
                Email = $"company{i}@test.com",
                IsActive = true
            });
        }

        // Act
        var (items, totalCount) = await _repository.GetPagedAsync(1, 10);

        // Assert
        items.Count().Should().Be(10);
        totalCount.Should().Be(15);
    }

    public void Dispose()
    {
        _mongoRunner?.Dispose();
    }
}
