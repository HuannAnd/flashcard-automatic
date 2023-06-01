namespace Flashcard.Api;

public sealed class FlashcardDocument
{
    public Guid Id { get; set; }
    public required DateOnly Date { get; set; }
    public required IEnumerable<string> Words { get; set;  }

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
