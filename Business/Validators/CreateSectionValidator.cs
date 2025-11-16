using FluentValidation;
using OnlineCourseApi.Core.DTOs.Request;

namespace OnlineCourseApi.Business.Validators;

public class CreateSectionValidator : AbstractValidator<CreateSectionDto>
{
    public CreateSectionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Bölüm başlığı zorunludur!")
            .MaximumLength(200).WithMessage("Bölüm başlığı en fazla 200 karakter olabilir!");

        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("Geçerli bir kurs seçilmelidir!");

        RuleFor(x => x.OrderIndex)
            .GreaterThanOrEqualTo(0).WithMessage("Sıra numarası 0 veya daha büyük olmalıdır!");
    }
}
