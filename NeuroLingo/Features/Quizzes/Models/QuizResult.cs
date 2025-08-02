using NeuroLingo.Features.Auth.Models;
using NeuroLingo.Features.Flashcards.Models;

namespace NeuroLingo.Features.Quizzes.Models;

public sealed class QuizResult
{
    public int Id { get; private set; }
    public DateTime TakenAt { get; private set; }
    public int CorrectAnswers { get; private set; }
    public int TotalQuestions { get; private set; }
    public int TimeSpentSeconds { get; private set; }

    public string UserId { get; private set; }
    public User User { get; private set; }

    public int FlashcardSetId { get; private set; }
    public FlashcardSet Set { get; private set; }

    public QuizResult(
        int id,
        DateTime takenAt,
        int correctAnswers,
        int totalQuestions,
        int timeSpentSeconds, 
        string userId,
        int flashcardSetId
        )
    {
        Id = id;
        TakenAt = takenAt;
        CorrectAnswers = correctAnswers;
        TotalQuestions = totalQuestions;
        TimeSpentSeconds = timeSpentSeconds;
        UserId = userId;
        FlashcardSetId = flashcardSetId;
    }

    private QuizResult() { }
}

