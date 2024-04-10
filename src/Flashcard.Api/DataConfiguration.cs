using Lab.Data.Mongo;

using Microsoft.Extensions.DependencyInjection.Extensions;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Infrastructure;
using MongoDB.Infrastructure.Extensions;
using MongoDB.UnitOfWork.Abstractions.Extensions;

namespace Flashcard.Api;
public static class DataConfiguration
{
    public static void AddDataConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        ConfigureMongoDbGuidSerializer();

        services.AddMongoDbContext<IMongoDbContext, WordDbContext>(_ =>
        {
            var connectionString = configuration.GetConnectionString("MongoDbConnection");

            var context = new WordDbContext(MongoClientSettings.FromConnectionString(connectionString), "Flashcards");

            return context;
        });

        services.AddMongoDbUnitOfWork<WordDbContext>();

        services.TryAddScoped<IWordDbRepository, WordDbRepository>();
    }

    /// <summary>
    /// Configures MongoDb GUID Serializer to use GuidRepresentation.Standard.
    /// </summary>
    /// <see href="https://mongodb.github.io/mongo-csharp-driver/2.1rcd/reference/bson/guidserialization/guidrepresentationmode/guidrepresentationmode/#opting-in-to-v3-guidrepresentationmode"/>
    private static void ConfigureMongoDbGuidSerializer()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }
}
