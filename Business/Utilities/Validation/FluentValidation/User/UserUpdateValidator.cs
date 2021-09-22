using Business.Managers.Account.Commands.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Validation.FluentValidation.User
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateCommand>
    {
        public UserUpdateValidator()
        {
            RuleFor(user => user.Username).NotEmpty().WithMessage("Username is required").MinimumLength(3).WithMessage("Minimum length is 3");
            RuleFor(user => user.Password).NotEmpty().MinimumLength(6);
        }
    }
}
