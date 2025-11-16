using FluentValidation;
using OnlineCourseApi.Core.DTOs.Request;

namespace OnlineCourseApi.Business.Validators;

public class CreateLectureValidator : AbstractValidator<CreateLectureDto>
{
    public CreateLectureValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Ders başlığı zorunludur!")
            .MaximumLength(200).WithMessage("Ders başlığı en fazla 200 karakter olabilir!");

        RuleFor(x => x.SectionId)
            .GreaterThan(0).WithMessage("Geçerli bir bölüm seçilmelidir!");

        RuleFor(x => x.VideoUrl)
            .NotEmpty().WithMessage("Video URL'i zorunludur!")
            .MaximumLength(500).WithMessage("Video URL'i en fazla 500 karakter olabilir!");

        RuleFor(x => x.DurationInSeconds)
            .GreaterThan(0).WithMessage("Süre 0'dan büyük olmalıdır!");

        RuleFor(x => x.OrderIndex)
            .GreaterThanOrEqualTo(0).WithMessage("Sıra numarası 0 veya daha büyük olmalıdır!");
    }
}
