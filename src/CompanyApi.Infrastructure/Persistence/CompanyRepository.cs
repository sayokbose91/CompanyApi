using CompanyApi.Domain.Entities;
using MongoDB.Driver;

namespace CompanyApi.Infrastructure.Persistence;

public class CompanyRepository : MongoRepository<Company>
{
    public CompanyRepository(IMongoDatabase database, MongoDbSettings settings)
        : base(database, settings.CompaniesCollectionName)
    {
        CreateIndexes();
    }

    private void CreateIndexes()
    {
        var indexKeysDefinition = Builders<Company>.IndexKeys.Text(c => c.Name)
            .Text(c => c.Description)
            .Text(c => c.Industry);

        var textIndexModel = new CreateIndexModel<Company>(indexKeysDefinition);

        var emailIndexModel = new CreateIndexModel<Company>(
            Builders<Company>.IndexKeys.Ascending(c => c.Email),
            new CreateIndexOptions { Unique = true, Sparse = true });

        var isActiveIndexModel = new CreateIndexModel<Company>(
            Builders<Company>.IndexKeys.Ascending(c => c.IsActive));

        var industryIsActiveIndexModel = new CreateIndexModel<Company>(
            Builders<Company>.IndexKeys
                .Ascending(c => c.Industry)
                .Ascending(c => c.IsActive));

        _collection.Indexes.CreateMany(new[]
        {
            textIndexModel,
            emailIndexModel,
            isActiveIndexModel,
            industryIsActiveIndexModel
        });
    }
}
