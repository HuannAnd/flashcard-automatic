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

        services.AddMongoDbContext<IMongoDbContext, FlashcardDbContext>(_ =>
        {
            var connectionString = configuration.GetConnectionString("Flashcard");

            var context = new FlashcardDbContext(MongoClientSettings.FromConnectionString(connectionString), "MyDb");

            return context;
        });

        services.AddMongoDbUnitOfWork<FlashcardDbContext>();

        services.TryAddScoped<IFlashcardDbRepository, FlashcardDbRepository>();
    }

    /// <summary>
    /// Configures MongoDb GUID Serializer to use GuidRepresentation.Standard.
    /// </summary>
    /// <see href="https://mongodb.github.io/mongo-csharp-driver/2.12/reference/bson/guidserialization/guidrepresentationmode/guidrepresentationmode/#opting-in-to-v3-guidrepresentationmode"/>
    private static void ConfigureMongoDbGuidSerializer()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }
}
