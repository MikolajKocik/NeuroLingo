namespace NeuroLingo.Features.Flashcards.Models;

public sealed class Flashcard
{
    public int Id { get; private set; }
    public string Term { get; private set; }
    public string Translation { get; private set; }
    public string Hint { get; private set; }  

    public int FlashcardSetId { get; private set; }
    public FlashcardSet Set { get; private set; }

    private Flashcard() { }

    public Flashcard(
        int id,
        string term,
        string translation,
        string hint,
        int flashcardSetId
        )
    {
        Id = id;
        Term = term;
        Translation = translation;
        Hint = hint;
        FlashcardSetId = flashcardSetId;
    }
}
