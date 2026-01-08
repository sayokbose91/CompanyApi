using CompanyApi.Application.Companies.DTOs;
using CompanyApi.Domain.Entities;
using CompanyApi.Domain.Interfaces;
using MediatR;

namespace CompanyApi.Application.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
    private readonly IRepository<Company> _repository;
    private readonly CompanyMapper _mapper;

    public UpdateCompanyCommandHandler(IRepository<Company> repository)
    {
        _repository = repository;
        _mapper = new CompanyMapper();
    }

    public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var existingCompany = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (existingCompany == null)
        {
            throw new KeyNotFoundException($"Company with ID {request.Id} not found");
        }

        existingCompany.Name = request.Name;
        existingCompany.Description = request.Description;
        existingCompany.Industry = request.Industry;
        existingCompany.Website = request.Website;
        existingCompany.Email = request.Email;
        existingCompany.Phone = request.Phone;
        existingCompany.Address = request.Address != null ? _mapper.ToAddressEntity(request.Address) : null;
        existingCompany.EmployeeCount = request.EmployeeCount;
        existingCompany.FoundedDate = request.FoundedDate;
        existingCompany.IsActive = request.IsActive;
        existingCompany.Tags = request.Tags;
        existingCompany.UpdatedAt = DateTime.UtcNow;

        var updatedCompany = await _repository.UpdateAsync(existingCompany, cancellationToken);
        return _mapper.ToDto(updatedCompany);
    }
}
