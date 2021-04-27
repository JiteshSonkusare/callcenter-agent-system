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
                .NotEmpty(); //ToDo: add custom validation

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty.");

            RuleFor(c => c.Email)
                .Matches(new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                .WithMessage("Enter valid email address.");
        }
    }
}
