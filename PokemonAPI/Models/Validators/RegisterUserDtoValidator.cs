using FluentValidation;
using PokemonAPI.Entities;
using System.Linq;

namespace PokemonAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterUserDtoValidator(PokeDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });

            RuleFor(x => x.Username)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var usernameInUse = dbContext.Users.Any(u => u.Username == value);
                    if (usernameInUse)
                    {
                        context.AddFailure("Username", "That username is taken");
                    }
                });

            RuleFor(x => x.PasswordConfirm)
                .Equal(e => e.Password);

            /*RuleFor(x => x.Email)*/
                

           /* RuleFor(x => x.Username)
*/
        }

    }
}
