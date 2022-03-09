using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Negotiations.Application.Interfaces;
using Negotiations.Application.Models;

namespace Negotiations.Application.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(INegotiationsDbContext _dbContext)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.FirstName)
                .NotEmpty();

            RuleFor(u => u.LastName)
                .NotEmpty();

            RuleFor(u => u.Login)
                .NotEmpty()
                .Custom((value, context) => {
                    var loginInUse = _dbContext.Users.Any(u => u.Login == value);
                    if (loginInUse)
                    {
                        context.AddFailure("Username", "Username already taken");
                    }
                });

            RuleFor(u => u.Password)
                .NotEmpty()
                .Equal(u => u.ConfirmPassword)
                .WithMessage("Password and ConfirmPassword must be the same");
        }
    }
}