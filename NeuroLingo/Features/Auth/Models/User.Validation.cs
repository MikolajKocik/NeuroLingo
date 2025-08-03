using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NeuroLingo.Features.Auth.Models
{
    public sealed partial class User : IdentityUser, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext _)
        {
            foreach (var e in Required("Field is required",
                (Email, nameof(Email)),
                (UserName, nameof(UserName)),
                (Password, nameof(Password))
            ))
            {
                yield return e;
            }

            var pattern = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$");

            if(!Email!.Equals(pattern))
            {
                yield return new ValidationResult(
                    "Email must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one number.",
                    new[] { nameof(Email) });
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
