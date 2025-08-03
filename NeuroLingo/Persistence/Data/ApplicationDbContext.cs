using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeuroLingo.Features.Auth.Models;
using NeuroLingo.Features.Flashcards.Models;
using NeuroLingo.Features.Quizzes.Models;
using NeuroLingo.Features.RepeatSchedules.Models;

namespace NeuroLingo.Persistence.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<User>(options)
{

    internal DbSet<Flashcard> Flashcards { get; set; } 
    internal DbSet<FlashcardSet> FlashcardSets { get; set; } 
    internal DbSet<QuizResult> QuizResults { get; set; } 
    internal DbSet<RepeatSchedule> RepeatSchedules { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("NeuroLingo");
    }
}
