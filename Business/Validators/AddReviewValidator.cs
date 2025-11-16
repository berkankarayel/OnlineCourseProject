using FluentValidation;
using OnlineCourseApi.Core.DTOs.Request;

namespace OnlineCourseApi.Business.Validators;

public class AddReviewValidator : AbstractValidator<AddReviewDto>
{
    public AddReviewValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("Geçerli bir kurs seçilmelidir!");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Puan 1 ile 5 arasında olmalıdır!");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Yorum zorunludur!")
            .MinimumLength(10).WithMessage("Yorum en az 10 karakter olmalıdır!")
            .MaximumLength(1000).WithMessage("Yorum en fazla 1000 karakter olabilir!");
    }
}
