using FluentValidation;
using Identity.Application.Commands;

namespace Identity.Application.Validations;

public class InsertUsuarioValidation : AbstractValidator<InsertUsuarioCommand>
{
    public InsertUsuarioValidation()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("email nao pode ser nulo")
            .NotEmpty().WithMessage("email nao pode ser vazio")
            .MaximumLength(100).WithMessage("Tamanho de email nao permitido");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("senha nao pode ser nula")
            .NotEmpty().WithMessage("senha nao pode ser vazia")
            .MaximumLength(24).WithMessage("senha ultrapassou o limite de caracteres")
            .MinimumLength(8).WithMessage("Senha deve ter pelo menos 8 caracteres") // Recomendação de comprimento mínimo
            .Matches(@"[0-9]").WithMessage("Senha deve conter pelo menos um número")
            .Matches(@"[a-zA-Z]").WithMessage("Senha deve conter pelo menos uma letra")
            .Matches(@"[\W_]").WithMessage("Senha deve conter pelo menos um caractere especial"); // \W é um atalho para qualquer caractere que não seja uma letra ou número, e _ inclui o caractere de sublinhado
    }
}
