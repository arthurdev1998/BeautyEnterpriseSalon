using FluentValidation.Results;

namespace BuildinBlocks.Core.DomainObjects;

public interface IValidatable
{
    ValidationResult ValidationResult { get; set; }
}
