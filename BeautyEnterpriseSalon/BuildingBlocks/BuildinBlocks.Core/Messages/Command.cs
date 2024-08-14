using FluentValidation.Results;
using MediatR;

namespace BuildinBlocks.Core.Messages;

public abstract class Command<TAggregateType, TResult> : Message<TAggregateType>, IRequest<TResult>
{
    public DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {
        Timestamp = DateTime.Now.ToUniversalTime().AddHours(-3);
        ValidationResult = new ValidationResult();
    }

    public virtual ValidationResult IsValid()
    {
        throw new NotImplementedException();
    }
}