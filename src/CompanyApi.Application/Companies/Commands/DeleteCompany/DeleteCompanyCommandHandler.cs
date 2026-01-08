using CompanyApi.Domain.Entities;
using CompanyApi.Domain.Interfaces;
using MediatR;

namespace CompanyApi.Application.Companies.Commands.DeleteCompany;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, bool>
{
    private readonly IRepository<Company> _repository;

    public DeleteCompanyCommandHandler(IRepository<Company> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id, cancellationToken);
    }
}
