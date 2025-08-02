using NeuroLingo.Features.Auth.Models;

namespace NeuroLingo.Features.Flashcards.Models;

public sealed class FlashcardSet
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Language { get; private set; }
    public string Description { get; private set; }

    public string UserId { get; private set; }
    public User User { get; private set; } = new();

    private readonly List<Flashcard> _flashcards = new();
    public IReadOnlyCollection<Flashcard> Flashcards 
        => _flashcards.AsReadOnly();

    private FlashcardSet() { }

    public FlashcardSet(
        int id,
        string title,
        string language,
        string description,
        string userId
        )
    {
        Id = id;
        Title = title;
        Language = language;
        Description = description;
        UserId = userId;
    }
}
