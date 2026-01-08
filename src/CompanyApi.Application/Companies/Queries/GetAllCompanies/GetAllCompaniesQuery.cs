using CompanyApi.Application.Common;
using CompanyApi.Application.Companies.DTOs;

namespace CompanyApi.Application.Companies.Queries.GetAllCompanies;

public record GetAllCompaniesQuery : IQuery<PagedResult<CompanyDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SortBy { get; init; }
    public bool SortAscending { get; init; } = true;
    public bool? IsActive { get; init; }
    public string? Industry { get; init; }
}
