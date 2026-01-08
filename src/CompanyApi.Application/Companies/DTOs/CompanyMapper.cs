using CompanyApi.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace CompanyApi.Application.Companies.DTOs;

[Mapper]
public partial class CompanyMapper
{
    public partial CompanyDto ToDto(Company company);
    public partial Company ToEntity(CompanyDto dto);
    public partial Address ToAddressEntity(AddressDto dto);
    public partial AddressDto ToAddressDto(Address address);
}
