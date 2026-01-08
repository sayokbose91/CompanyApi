using CompanyApi.Application.Companies.DTOs;
using CompanyApi.Domain.Entities;
using CompanyApi.Domain.Interfaces;
using MediatR;

namespace CompanyApi.Application.Companies.Commands.CreateCompany;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    private readonly IRepository<Company> _repository;
    private readonly CompanyMapper _mapper;

    public CreateCompanyCommandHandler(IRepository<Company> repository)
    {
        _repository = repository;
        _mapper = new CompanyMapper();
    }

    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Company
        {
            Name = request.Name,
            Description = request.Description,
            Industry = request.Industry,
            Website = request.Website,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address != null ? _mapper.ToAddressEntity(request.Address) : null,
            EmployeeCount = request.EmployeeCount,
            FoundedDate = request.FoundedDate,
            Tags = request.Tags,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var createdCompany = await _repository.AddAsync(company, cancellationToken);
        return _mapper.ToDto(createdCompany);
    }
}
