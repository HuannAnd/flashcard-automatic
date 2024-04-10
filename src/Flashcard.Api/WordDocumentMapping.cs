using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Infrastructure;

namespace Flashcard.Api;
public sealed class FlashcardDocumentMapping : IMongoDbFluentConfiguration
{
    public void Configure()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(WordDocument))) return;

        BsonClassMap.RegisterClassMap<WordDocument>(builder =>
        {
            builder.AutoMap();
            //builder.MapConstructor(typeof(FlashcardDocument).GetConstructor(new[] { typeof(IEnumerable<string>), typeof(DateOnly?), typeof(Guid?) }));
            builder.MapIdMember(x => x.Id).SetIdGenerator(AscendingGuidGenerator.Instance);
        });
    }
}
