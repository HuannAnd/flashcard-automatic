using Flashcard.Api;

using MongoDB.Infrastructure;
using MongoDB.Repository;
using MongoDB.Repository.Extensions;

using MongoDB.UnitOfWork;

namespace Lab.Data.Mongo;

public interface IWordDbRepository
{
    void Add(WordDocument foo);
    void Delete(Guid id);
    WordDocument GetById(Guid id);
    IMongoDbPagedList<WordDocument> GetPagedList(int pageIndex = 1, int pageSize = 20);
    void Update(WordDocument foo);
}

public sealed class WordDbRepository : IWordDbRepository
{
    private readonly IMongoDbUnitOfWork<WordDbContext> _unitOfWork;

    public WordDbRepository(IMongoDbUnitOfWork<WordDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public IMongoDbPagedList<WordDocument> GetPagedList(int pageIndex = 1, int pageSize = 20)
    {
        var repository = _unitOfWork.Repository<WordDocument>();

        var query = repository
            .MultipleResultQuery()
            .Page(pageIndex, pageSize);

        var result = repository
            .Search(query)
            .ToPagedList(query.Paging.PageIndex, query.Paging.PageSize, query.Paging.TotalCount);

        return result;
    }

    public void Add(WordDocument foo)
    {
        var repository = _unitOfWork.Repository<WordDocument>();

        repository.InsertOne(foo);

        _unitOfWork.SaveChanges();
    }

    public void Update(WordDocument foo)
    {
        var repository = _unitOfWork.Repository<WordDocument>();

        repository.ReplaceOne(x => x.Id == foo.Id, foo);

        _unitOfWork.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var repository = _unitOfWork.Repository<WordDocument>();

        repository.DeleteOne(x => x.Id == id);

        _unitOfWork.SaveChanges();
    }

    public WordDocument GetById(Guid id)
    {
        var repository = _unitOfWork.Repository<WordDocument>();

        var query = repository.SingleResultQuery().AndFilter(x => x.Id == id);

        return repository.FirstOrDefault(query);
    }
}
