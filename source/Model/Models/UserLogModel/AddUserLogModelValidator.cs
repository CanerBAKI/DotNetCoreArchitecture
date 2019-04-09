using DotNetCore.Validation;
using FluentValidation;

namespace DotNetCoreArchitecture.Model
{
    public sealed class AddUserLogModelValidator : Validator<AddUserLogModel>
    {
        public AddUserLogModelValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.LogType).NotEqual(LogType.None);
        }
    }
}
