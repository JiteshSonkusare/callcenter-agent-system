using FluentValidation;
using System.Text.RegularExpressions;

namespace CallCenter.Agent.Server.Application.Agent.Commands
{
    public class CreateOrUpdateCommandValidator : AbstractValidator<CreateOrUpdateAgentCommand>
    {
        public CreateOrUpdateCommandValidator()
        {
            RuleFor(c => c.UserId)
                .MaximumLength(7)
                .Matches(new Regex(@"^[a-zA-Z]{2}[0-9]{5}$"))
                .NotEmpty();

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty.");

            RuleFor(c => c.Email)
                .Matches(new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                .WithMessage("Enter valid email address.");
        }
    }
}
