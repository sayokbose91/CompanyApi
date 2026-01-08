using CompanyApi.Application.Common;
using CompanyApi.Application.Companies.DTOs;

namespace CompanyApi.Application.Companies.Queries.GetCompanyById;

public record GetCompanyByIdQuery : IQuery<CompanyDto?>
{
    public string Id { get; init; } = null!;
}
