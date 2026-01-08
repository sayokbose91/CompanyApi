using CompanyApi.Domain.Common;

namespace CompanyApi.Domain.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Industry { get; set; }
    public string? Website { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public Address? Address { get; set; }
    public int? EmployeeCount { get; set; }
    public DateTime? FoundedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public List<string> Tags { get; set; } = new();
}

public class Address
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
}
