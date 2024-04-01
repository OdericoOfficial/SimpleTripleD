using FluentValidation;

namespace SimpleTripleD.Domain.Commands
{
    public abstract class CommandValidator<TCommand> : AbstractValidator<TCommand> where TCommand : ValidationCommand
    {
    }
}
