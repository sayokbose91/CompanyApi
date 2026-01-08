using CompanyApi.Application.Companies.DTOs;
using CompanyApi.Domain.Entities;
using CompanyApi.Domain.Interfaces;
using MediatR;

namespace CompanyApi.Application.Companies.Queries.GetCompanyById;

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
{
    private readonly IRepository<Company> _repository;
    private readonly CompanyMapper _mapper;

    public GetCompanyByIdQueryHandler(IRepository<Company> repository)
    {
        _repository = repository;
        _mapper = new CompanyMapper();
    }

    public async Task<CompanyDto?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return company != null ? _mapper.ToDto(company) : null;
    }
}
