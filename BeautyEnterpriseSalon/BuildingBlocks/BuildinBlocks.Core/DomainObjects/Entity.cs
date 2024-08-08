using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BuildinBlocks.Core.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace BuildinBlocks.Core.DomainObjects;

public abstract class Entity<TIdentifierType> : IValidatable
    {
        public required TIdentifierType Id { get; set; } 

        [NotMapped]
        private List<Event<TIdentifierType>>? _notificacoes;
    
        [JsonIgnore]
        [NotMapped]
        public IReadOnlyCollection<Event<TIdentifierType>>? Notificacoes => _notificacoes?.AsReadOnly();
    
        [JsonIgnore]
        [NotMapped]
        public required ValidationResult ValidationResult { get; set; }

        public void AddEvent(Event<TIdentifierType> evento)
        {
            _notificacoes = _notificacoes ?? new List<Event<TIdentifierType>>();
            _notificacoes.Add(evento);
        }

        public void RemoveEvent(Event<TIdentifierType> eventItem)
        {
            _notificacoes?.Remove(eventItem);
        }

        public void ClearEvent()
        {
            _notificacoes?.Clear();
        }

    #region Comparations
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
    {
            var compareTo = obj as Entity<TIdentifierType>;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (ReferenceEquals(null, compareTo))
            {
                return false;
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return Id.Equals(compareTo.Id);
    }

        public static bool operator ==(Entity<TIdentifierType> a, Entity<TIdentifierType> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TIdentifierType> a, Entity<TIdentifierType> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
        #endregion
        public abstract bool IsValid();

        public bool IsValid<T>(AbstractValidator<T> validation, T instance)
        {
            ValidationResult = validation.Validate(instance);
            return ValidationResult.IsValid;
        }
    }