using Flashcard.Api;

using MongoDB.Infrastructure;
using MongoDB.Repository;
using MongoDB.Repository.Extensions;

using MongoDB.UnitOfWork;

namespace Lab.Data.Mongo;

public interface IFlashcardDbRepository
{
    void Add(FlashcardDocument foo);
    void Delete(Guid id);
    FlashcardDocument GetById(Guid id);
    IMongoDbPagedList<FlashcardDocument> GetPagedList(int pageIndex = 1, int pageSize = 20);
    void Update(FlashcardDocument foo);
}

public sealed class FlashcardDbRepository : IFlashcardDbRepository
{
    private readonly IMongoDbUnitOfWork<FlashcardDbContext> _unitOfWork;

    public FlashcardDbRepository(IMongoDbUnitOfWork<FlashcardDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public IMongoDbPagedList<FlashcardDocument> GetPagedList(int pageIndex = 1, int pageSize = 20)
    {
        var repository = _unitOfWork.Repository<FlashcardDocument>();

        var query = repository
            .MultipleResultQuery()
            .Page(pageIndex, pageSize);

        var result = repository
            .Search(query)
            .ToPagedList(query.Paging.PageIndex, query.Paging.PageSize, query.Paging.TotalCount);

        return result;
    }

    public void Add(FlashcardDocument foo)
    {
        var repository = _unitOfWork.Repository<FlashcardDocument>();

        repository.InsertOne(foo);

        _unitOfWork.SaveChanges();
    }

    public void Update(FlashcardDocument foo)
    {
        var repository = _unitOfWork.Repository<FlashcardDocument>();

        repository.ReplaceOne(x => x.Id == foo.Id, foo);

        _unitOfWork.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var repository = _unitOfWork.Repository<FlashcardDocument>();

        repository.DeleteOne(x => x.Id == id);

        _unitOfWork.SaveChanges();
    }

    public FlashcardDocument GetById(Guid id)
    {
        var repository = _unitOfWork.Repository<FlashcardDocument>();

        var query = repository.SingleResultQuery().AndFilter(x => x.Id == id);

        return repository.FirstOrDefault(query);
    }
}
