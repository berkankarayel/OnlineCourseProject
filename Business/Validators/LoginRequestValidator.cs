using FluentValidation;
using OnlineCourseApi.Core.DTOs.Request;

namespace OnlineCourseApi.Business.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email adresi zorunludur!")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre zorunludur!")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır!");
    }
}
