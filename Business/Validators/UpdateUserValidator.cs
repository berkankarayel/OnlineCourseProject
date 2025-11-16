using FluentValidation;
using OnlineCourseApi.Core.DTOs.Request;

namespace OnlineCourseApi.Business.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad zorunludur!")
            .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir!");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad zorunludur!")
            .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir!");

        RuleFor(x => x.ProfileImageUrl)
            .MaximumLength(500).WithMessage("Profil resmi URL'i en fazla 500 karakter olabilir!")
            .When(x => !string.IsNullOrEmpty(x.ProfileImageUrl));
    }
}
