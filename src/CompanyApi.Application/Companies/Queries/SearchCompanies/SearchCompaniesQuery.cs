using CompanyApi.Application.Common;
using CompanyApi.Application.Companies.DTOs;

namespace CompanyApi.Application.Companies.Queries.SearchCompanies;

public record SearchCompaniesQuery : IQuery<IEnumerable<CompanyDto>>
{
    public string SearchTerm { get; init; } = null!;
}
