using MongoDB.Driver;
using MongoDB.Infrastructure;

namespace Lab.Data.Mongo;
public sealed class FlashcardDbContext : MongoDbContext
{
    public FlashcardDbContext(MongoClientSettings clientSettings, string databaseName, MongoDatabaseSettings? databaseSettings = null)
        : base(clientSettings, databaseName, databaseSettings)
    {
        AcceptAllChangesOnSave = true;
        ApplyConfigurationsFromAssembly(typeof(FlashcardDbContext).Assembly);
    }
}
