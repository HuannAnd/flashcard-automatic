using MongoDB.Driver;
using MongoDB.Infrastructure;

namespace Lab.Data.Mongo;
public sealed class WordDbContext : MongoDbContext
{
    public WordDbContext(MongoClientSettings clientSettings, string databaseName, MongoDatabaseSettings? databaseSettings = null)
        : base(clientSettings, databaseName, databaseSettings)
    {
        AcceptAllChangesOnSave = true;
        ApplyConfigurationsFromAssembly(typeof(WordDbContext).Assembly);
    }
}
