using DotNetCore.Validation;
using FluentValidation;

namespace DotNetCoreArchitecture.Model
{
    public class UserModelValidator<T> : Validator<T> where T : UserModel
    {
        public UserModelValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.FullName).NotNull().NotEmpty();
            RuleFor(x => x.FullName.Name).NotNull().NotEmpty();
            RuleFor(x => x.FullName.Surname).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty();
        }
    }
}
