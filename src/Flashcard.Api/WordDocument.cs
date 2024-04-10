namespace Flashcard.Api;

public sealed class WordDocument
{
    public Guid Id { get; set; }
    public required DateTime Date { get; set; }
    public required string Word {get; set;}

    //public FlashcardDocument(IEnumerable<string> words, DateOnly? date = null, Guid? id = null)
    //{
    //    Words = words;
    //    Date = date ?? DateOnly.FromDateTime(DateTime.Now);
    //    //Id = id ?? Guid.NewGuid();
    //}

    //public FlashcardDocument(IEnumerable<string> words, DateTime? date = null, Guid? id = null)
    //{
    //    Words = words;
    //    Date = DateOnly.FromDateTime(date ?? DateTime.Now);
    //    //Id = id ?? Guid.NewGuid();
    //}
}
