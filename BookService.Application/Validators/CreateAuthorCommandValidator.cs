using FluentValidation;
using BookService.Application.Commands;

namespace BookService.Application.Validators
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Полное имя автора обязательно")
                .MaximumLength(100).WithMessage("Полное имя не должно превышать 100 символов");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Страна обязательна.")
                .MaximumLength(50).WithMessage("Название страны не должно превышать 50 символов");

            RuleFor(x => x.Biography)
                .MaximumLength(500).WithMessage("Биография не должна превышать 500 символов");
        }
    }
}
