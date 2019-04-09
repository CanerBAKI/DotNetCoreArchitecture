using DotNetCoreArchitecture.Model;

namespace DotNetCoreArchitecture.Domain
{
    public static class UserEntityFactory
    {
        public static UserEntity Create(long userId)
        {
            return new UserEntity(userId, null, null, null, Roles.None, Status.None);
        }

        public static UserEntity Create(AddUserModel addUserModel)
        {
            return new UserEntity
            (
                addUserModel.UserId,
                new FullName(addUserModel.FullName.Name, addUserModel.FullName.Surname),
                addUserModel.Email,
                new SignIn(addUserModel.SignIn.Login, addUserModel.SignIn.Password),
                addUserModel.Roles,
                Status.None
            );
        }
    }
}
