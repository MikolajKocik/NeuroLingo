using Microsoft.AspNetCore.Identity;
using NeuroLingo.Features.Flashcards.Models;
using NeuroLingo.Features.Quizzes.Models;

namespace NeuroLingo.Features.Auth.Models;

public sealed class User : IdentityUser
{
    public DateTime RegisteredAt { get; private set; }

    private readonly List<QuizResult> _quizResults = new();
    public IReadOnlyCollection<QuizResult> QuizResults
        => _quizResults.AsReadOnly();

    private readonly List<FlashcardSet> _flashcardSets = new();
    public IReadOnlyCollection<FlashcardSet> FlashcardSets 
        => _flashcardSets.AsReadOnly();

    public User()
    {
        RegisteredAt = DateTime.UtcNow;
    }
}
