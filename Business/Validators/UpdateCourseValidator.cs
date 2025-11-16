using FluentValidation;
using OnlineCourseApi.Core.DTOs.Request;

namespace OnlineCourseApi.Business.Validators;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseDto>
{
    public UpdateCourseValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Kurs başlığı zorunludur!")
            .MaximumLength(200).WithMessage("Kurs başlığı en fazla 200 karakter olabilir!");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama zorunludur!")
            .MinimumLength(50).WithMessage("Açıklama en az 50 karakter olmalıdır!");

        RuleFor(x => x.ShortDescription)
            .NotEmpty().WithMessage("Kısa açıklama zorunludur!")
            .MaximumLength(500).WithMessage("Kısa açıklama en fazla 500 karakter olabilir!");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir!");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Fiyat 0 veya daha büyük olmalıdır!")
            .LessThanOrEqualTo(999999.99m).WithMessage("Fiyat çok yüksek!");

        RuleFor(x => x.Level)
            .NotEmpty().WithMessage("Seviye zorunludur!")
            .Must(level => new[] { "Beginner", "Intermediate", "Advanced" }.Contains(level))
            .WithMessage("Seviye Beginner, Intermediate veya Advanced olmalıdır!");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Durum zorunludur!")
            .Must(status => new[] { "Draft", "Published" }.Contains(status))
            .WithMessage("Durum Draft veya Published olmalıdır!");
    }
}
