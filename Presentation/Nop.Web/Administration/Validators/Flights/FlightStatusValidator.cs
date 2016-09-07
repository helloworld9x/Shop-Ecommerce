using FluentValidation;
using Nop.Admin.Models.Flights;
using Nop.Web.Framework.Validators;

namespace Nop.Admin.Validators.Flights
{
    public class FlightStatusValidator : BaseNopValidator<FlightStatusModel>
    {
        public FlightStatusValidator()
        {
            RuleFor(x => x.CommercialName).NotEmpty().WithMessage("This field is required");
            RuleFor(x => x.From).Length(0, 10).WithMessage("This field is between 1 to 10 characters");
            RuleFor(x => x.To).Length(0, 10).WithMessage("This field is between 1 to 10 characters");
        }
    }
}