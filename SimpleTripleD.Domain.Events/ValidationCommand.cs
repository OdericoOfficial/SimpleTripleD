using FluentValidation.Results;
using MediatR;

namespace SimpleTripleD.Domain.Events
{
    public abstract class ValidationCommand : IRequest<bool>
    {
        public string CommandType { get; protected set; }
        
        public DateTime Timestamp { get; protected set; }

        public ValidationResult? ValidationResult { get; set; }

        public ValidationCommand()
        {
            CommandType = GetType().Name;
            Timestamp = DateTime.UtcNow;
        }

        public abstract bool IsValid();
    }
}
