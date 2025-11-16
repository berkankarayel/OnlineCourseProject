using FluentValidation;
using OnlineCourseApi.Core.DTOs.Request;

namespace OnlineCourseApi.Business.Validators;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Kategori adı zorunludur!")
            .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir!");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir!")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}
