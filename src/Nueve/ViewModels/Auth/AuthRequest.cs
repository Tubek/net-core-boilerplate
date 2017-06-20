using FluentValidation;

namespace Nueve.ViewModels.Auth
{
    /// <summary>
    /// Auth request object
    /// </summary>
    public class AuthRequest
    {
        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// Auth request validator
    /// </summary>
    public class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
