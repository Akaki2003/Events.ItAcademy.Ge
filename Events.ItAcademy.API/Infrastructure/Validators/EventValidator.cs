using Events.ItAcademy.Application.Events.Requests;
using Events.ItAcademy.Application.Users.Requests;
using FluentValidation;

namespace Events.ItAcademy.API.Infrastructure.Validators
{
    public class EventValidator : AbstractValidator<EventRequestModel>
    {
        public EventValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price field must not be mepty")
                .
                GreaterThanOrEqualTo(1).WithMessage("Price must be equal or more than 1");
            RuleFor(x => x.TicketCount).NotEmpty()
                .WithMessage("TicketCount must not be empty")
                .GreaterThanOrEqualTo(1).WithMessage("TicketCount must be equal or greater than 1");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title must not be empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description must not be empty");
            RuleFor(x => x.StartDate).NotEmpty().
                WithMessage("StartDate must be provided")
                .GreaterThan(DateTime.Now).WithMessage("StartDate must be greater than current datetime");
            RuleFor(x => x.EndDate).NotEmpty()
                .WithMessage("EndDate must not be empty").
                GreaterThan(x => x.StartDate).WithMessage("EndDate must be gerater than StartDate");
        }
    }
    public class EventPutValidator : AbstractValidator<EventRequestPutModel>
    {
        public EventPutValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price field must not be mepty").
            GreaterThanOrEqualTo(1).WithMessage("Price must be equal or more than 1");
            RuleFor(x => x.TicketCount).NotEmpty()
                .WithMessage("TicketCount must not be empty")
                .GreaterThanOrEqualTo(1).WithMessage("TicketCount must be equal or greater than 1");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title must not be empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description must not be empty");
            RuleFor(x => x.StartDate).NotEmpty().
                WithMessage("StartDate must be provided")
                .GreaterThan(DateTime.Now).WithMessage("StartDate must be greater than current datetime");
            RuleFor(x => x.EndDate).NotEmpty()
                .WithMessage("EndDate must not be empty").
                GreaterThan(x => x.StartDate).WithMessage("EndDate must be gerater than StartDate");
        }
    }
}
