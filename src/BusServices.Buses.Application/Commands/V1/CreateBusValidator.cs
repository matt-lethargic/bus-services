using FluentValidation;

namespace BusServices.Buses.Application.Commands.V1
{
    public class CreateBusValidator : AbstractValidator<CreateBus>
    {
        public CreateBusValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Registration).NotEmpty();
            RuleFor(x => x.YearBuilt).GreaterThan(1960);
        }
    }
}
