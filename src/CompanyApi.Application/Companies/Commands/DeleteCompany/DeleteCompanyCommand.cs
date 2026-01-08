using CompanyApi.Application.Common;

namespace CompanyApi.Application.Companies.Commands.DeleteCompany;

public record DeleteCompanyCommand : ICommand<bool>
{
    public string Id { get; init; } = null!;
}
