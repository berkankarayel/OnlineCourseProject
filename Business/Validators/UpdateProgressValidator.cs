using FluentValidation;
using OnlineCourseApi.Core.DTOs.Request;

namespace OnlineCourseApi.Business.Validators;

public class UpdateProgressValidator : AbstractValidator<UpdateProgressDto>
{
    public UpdateProgressValidator()
    {
        RuleFor(x => x.LectureId)
            .GreaterThan(0).WithMessage("Geçerli bir ders seçilmelidir!");

        RuleFor(x => x.WatchedPercentage)
            .InclusiveBetween(0, 100).WithMessage("İzlenme yüzdesi 0 ile 100 arasında olmalıdır!");
    }
}
