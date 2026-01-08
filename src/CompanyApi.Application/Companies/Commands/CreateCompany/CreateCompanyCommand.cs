using CompanyApi.Application.Common;
using CompanyApi.Application.Companies.DTOs;

namespace CompanyApi.Application.Companies.Commands.CreateCompany;

public record CreateCompanyCommand : ICommand<CompanyDto>
{
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public string? Industry { get; init; }
    public string? Website { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public AddressDto? Address { get; init; }
    public int? EmployeeCount { get; init; }
    public DateTime? FoundedDate { get; init; }
    public List<string> Tags { get; init; } = new();
}
