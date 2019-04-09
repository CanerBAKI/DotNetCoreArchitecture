using DotNetCore.AspNetCore;
using DotNetCore.Extensions;
using DotNetCore.Objects;
using DotNetCoreArchitecture.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DotNetCoreArchitecture.Web
{
    public abstract class BaseController : ControllerBase
    {
        public SignedInModel SignedInModel => new SignedInModel
        {
            UserId = User.Id(),
            Roles = (Roles)User.Roles<Roles>().Sum(value => (int)value)
        };

        public DefaultResult Result(IResult result)
        {
            return new DefaultResult(result);
        }
    }
}
