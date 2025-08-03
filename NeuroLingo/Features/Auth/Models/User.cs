using Microsoft.AspNetCore.Identity;
using NeuroLingo.Features.Flashcards.Models;
using NeuroLingo.Features.Quizzes.Models;
using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.Models;

public sealed partial class User : IdentityUser, IValidatableObject
{
    public string Password { get; internal set; }
    public DateTime RegisteredAt { get; private set; }

    private readonly List<QuizResult> _quizResults = new();
    public IReadOnlyCollection<QuizResult> QuizResults
        => _quizResults.AsReadOnly();

    private readonly List<FlashcardSet> _flashcardSets = new();
    public IReadOnlyCollection<FlashcardSet> FlashcardSets 
        => _flashcardSets.AsReadOnly();

    public User(
        string password,
        string email,
        string username
        )
    {
        Password = password;
        UserName = username;
        Email = email;
        RegisteredAt = DateTime.UtcNow;
    }

    public User()
    {
        RegisteredAt = DateTime.UtcNow;
    }
}
