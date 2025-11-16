using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NeuroLingo.Features.Auth.Models
{
    public sealed partial class User : IdentityUser, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext _)
        {
            foreach (var e in Required("Email is required",
                (Email, nameof(Email))
            ))
            {
                yield return e;
            }
        }

        private static IEnumerable<ValidationResult> Required(
            string message,
            params (string? value, string memberName)[] fields)
        {
            foreach (var (value, member) in fields)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    yield return new ValidationResult(message, new[] { member });
                }
            }
        }
    }
}
