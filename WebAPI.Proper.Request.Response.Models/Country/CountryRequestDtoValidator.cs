using FluentValidation;

namespace WebAPI.Proper.Request.Response.Models.Country
{
    public class CountryRequestDtoValidator : AbstractValidator<CountryEntity>
    {
        public CountryRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.ISO3).NotNull().NotEmpty().MaximumLength(3);
            RuleFor(x => x.ISO2).NotNull().NotEmpty().MaximumLength(2);
            RuleFor(x => x.Code).NotNull().NotEmpty().Custom((x, context) =>
            {
                if ((!(int.TryParse(x, out int value)) || value < 0))
                {
                    context.AddFailure($"{x} is not a valid number or less than 0");
                }
            });
            RuleFor(x => x.PhoneCode).NotNull().NotEmpty().MinimumLength(1);
            RuleFor(x => x.Capital).NotNull().NotEmpty().MinimumLength(1);
            RuleFor(x => x.Currency).NotNull().NotEmpty().MinimumLength(1);
            RuleFor(x => x.CurrencyName).NotNull().NotEmpty().MinimumLength(1);
            RuleFor(x => x.CurrencySymbol).NotNull().NotEmpty().MinimumLength(1);
            RuleFor(x => x.Region).NotNull().NotEmpty().MinimumLength(1);
            RuleFor(x => x.Subregion).NotNull().NotEmpty().MinimumLength(1);
        }
    }
}
