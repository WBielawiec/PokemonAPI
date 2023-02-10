using FluentValidation;

namespace PokemonAPI.Models.Validators
{
    public class PokemonQueryValidator : AbstractValidator<PokemonQuery>
    {
        public PokemonQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).GreaterThanOrEqualTo(5);
        }
    }
}
