using System.Linq.Expressions;
using CompanyApi.Application.Companies.DTOs;
using CompanyApi.Domain.Entities;
using CompanyApi.Domain.Interfaces;
using MediatR;

namespace CompanyApi.Application.Companies.Queries.GetAllCompanies;

public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, PagedResult<CompanyDto>>
{
    private readonly IRepository<Company> _repository;
    private readonly CompanyMapper _mapper;

    public GetAllCompaniesQueryHandler(IRepository<Company> repository)
    {
        _repository = repository;
        _mapper = new CompanyMapper();
    }

    public async Task<PagedResult<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Company, bool>>? filter = null;

        if (request.IsActive.HasValue || !string.IsNullOrEmpty(request.Industry))
        {
            filter = c => (!request.IsActive.HasValue || c.IsActive == request.IsActive.Value)
                          && (string.IsNullOrEmpty(request.Industry) || c.Industry == request.Industry);
        }

        Expression<Func<Company, object>>? orderBy = request.SortBy?.ToLower() switch
        {
            "name" => c => c.Name,
            "createdat" => c => c.CreatedAt,
            "industry" => c => c.Industry ?? string.Empty,
            _ => c => c.CreatedAt
        };

        var (items, totalCount) = await _repository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            filter,
            orderBy,
            request.SortAscending,
            cancellationToken);

        var dtos = items.Select(c => _mapper.ToDto(c));

        return new PagedResult<CompanyDto>
        {
            Items = dtos,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}
