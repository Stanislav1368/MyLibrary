using BookService.Application.Commands;
using FluentValidation;

namespace BookService.Application.Validators
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название книги обязательно.")
                .MaximumLength(200).WithMessage("Название книги не должно превышать 200 символов");

            RuleFor(x => x.AuthorIds)
                .NotEmpty().WithMessage("Список авторов не может быть пустым.")
                .Must(authorIds => authorIds != null && authorIds.Count > 0)
                .WithMessage("Должен быть указан хотя бы один автор.");

            RuleFor(x => x.GenreIds)
                .NotEmpty().WithMessage("Список жанров не может быть пустым.")
                .Must(genreIds => genreIds != null && genreIds.Count > 0)
                .WithMessage("Должен быть указан хотя бы один жанр.");

            RuleFor(x => x.PublicationYear)
                .InclusiveBetween(1000, 2100).WithMessage("Год публикации должен быть в диапазоне от 1000 до 2100.")
                .When(x => x.PublicationYear.HasValue);

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Описание не должно превышать 1000 символов.");

            RuleFor(x => x.Condition)
               .MaximumLength(100).WithMessage("Состояние книги не должно превышать 100 символов.")
               .When(x => !string.IsNullOrEmpty(x.Condition));
        }
    }
}
