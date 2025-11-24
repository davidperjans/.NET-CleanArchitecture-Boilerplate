using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Auth.Commands.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(30);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Måste innehålla minst en versal")
                .Matches("[a-z]").WithMessage("Måste innehålla minst en gemen")
                .Matches("[0-9]").WithMessage("Måste innehålla minst en siffra")
                .Matches(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]").WithMessage("Måste innehålla ett specialtecken");
        }
    }
}
