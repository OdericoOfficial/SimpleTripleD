using MediatR;

namespace SimpleTripleD.Domain.Commands
{
    public abstract class CommandHandler<TValidationCommand> : IRequestHandler<TValidationCommand, bool>
        where TValidationCommand : ValidationCommand
    {
        public abstract Task<bool> Handle(TValidationCommand request, CancellationToken cancellationToken);
    }
}
