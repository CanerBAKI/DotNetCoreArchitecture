using DotNetCore.Validation;
using FluentValidation;

namespace DotNetCoreArchitecture.Model
{
    public sealed class AddUserModelValidator : UserModelValidator<AddUserModel>
    {
        public AddUserModelValidator()
        {
            RuleFor(x => x.SignIn).NotNull().NotEmpty();
            RuleFor(x => x.SignIn.Login).NotNull().NotEmpty();
            RuleFor(x => x.SignIn.Password).NotNull().NotEmpty();
        }
    }
}
