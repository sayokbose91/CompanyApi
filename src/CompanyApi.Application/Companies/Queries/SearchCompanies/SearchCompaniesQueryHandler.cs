using CompanyApi.Application.Companies.DTOs;
using CompanyApi.Domain.Entities;
using CompanyApi.Domain.Interfaces;
using MediatR;

namespace CompanyApi.Application.Companies.Queries.SearchCompanies;

public class SearchCompaniesQueryHandler : IRequestHandler<SearchCompaniesQuery, IEnumerable<CompanyDto>>
{
    private readonly IRepository<Company> _repository;
    private readonly CompanyMapper _mapper;

    public SearchCompaniesQueryHandler(IRepository<Company> repository)
    {
        _repository = repository;
        _mapper = new CompanyMapper();
    }

    public async Task<IEnumerable<CompanyDto>> Handle(SearchCompaniesQuery request, CancellationToken cancellationToken)
    {
        var searchTerm = request.SearchTerm.ToLower();
        var companies = await _repository.FindAsync(
            c => c.Name.ToLower().Contains(searchTerm) ||
                 (c.Description != null && c.Description.ToLower().Contains(searchTerm)) ||
                 (c.Industry != null && c.Industry.ToLower().Contains(searchTerm)),
            cancellationToken);

        return companies.Select(c => _mapper.ToDto(c));
    }
}
