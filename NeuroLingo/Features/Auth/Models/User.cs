using Microsoft.AspNetCore.Identity;
using NeuroLingo.Features.Flashcards.Models;
using NeuroLingo.Features.Quizzes.Models;
using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.Models;

public sealed partial class User : IdentityUser, IValidatableObject
{
    public DateTime RegisteredAt { get; private set; }

    private readonly List<QuizResult> _quizResults = new();
    public IReadOnlyCollection<QuizResult> QuizResults
        => _quizResults.AsReadOnly();

    private readonly List<FlashcardSet> _flashcardSets = new();
    public IReadOnlyCollection<FlashcardSet> FlashcardSets 
        => _flashcardSets.AsReadOnly();

    public User(
        string email
        )
    {
        Email = email;
        RegisteredAt = DateTime.UtcNow;
    }
}
