using NeuroLingo.Features.Flashcards.Models;

namespace NeuroLingo.Features.RepeatSchedules.Models;

public sealed class RepeatSchedule
{
    public int Id { get; private set; }
    public int FlashcardId { get; private set; }
    public Flashcard Card { get; private set; }

    public DateTime NextReviewDate { get; private set; }
    public int ReviewCount { get; private set; }
    public double EaseFactor { get; private set; }

    private RepeatSchedule() { }

    public RepeatSchedule(
        int id,
        int flashcardId,
        DateTime nextReviewDate,
        int reviewCount,
        double easeFactor
        )
    {
        Id = id;
        FlashcardId = flashcardId;
        NextReviewDate = nextReviewDate;
        ReviewCount = reviewCount;
        EaseFactor = easeFactor;
    }
}

