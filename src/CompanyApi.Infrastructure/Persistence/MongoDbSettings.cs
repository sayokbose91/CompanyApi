namespace CompanyApi.Infrastructure.Persistence;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string DatabaseName { get; set; } = "CompanyDb";
    public string CompaniesCollectionName { get; set; } = "companies";
}
