using DotNetCore.Objects;
using DotNetCoreArchitecture.Database;
using DotNetCoreArchitecture.Domain;
using DotNetCoreArchitecture.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application
{
    public sealed class UserApplicationService : BaseApplicationService, IUserApplicationService
    {
        public UserApplicationService
        (
            IDatabaseUnitOfWork databaseUnitOfWork,
            IUserDomainService userDomainService,
            IUserLogApplicationService userLogApplicationService,
            IUserRepository userRepository
        )
        {
            DatabaseUnitOfWork = databaseUnitOfWork;
            UserDomainService = userDomainService;
            UserLogApplicationService = userLogApplicationService;
            UserRepository = userRepository;
        }

        private IDatabaseUnitOfWork DatabaseUnitOfWork { get; }

        private IUserDomainService UserDomainService { get; }

        private IUserLogApplicationService UserLogApplicationService { get; }

        private IUserRepository UserRepository { get; }

        public async Task<IDataResult<long>> AddAsync(AddUserModel addUserModel)
        {
            var validation = new AddUserModelValidator().Valid(addUserModel);

            if (!validation.Success)
            {
                return ErrorDataResult<long>(validation.Message);
            }

            UserDomainService.GenerateHash(addUserModel.SignIn);

            var userEntity = UserEntityFactory.Create(addUserModel);

            userEntity.Add();

            await UserRepository.AddAsync(userEntity);

            await DatabaseUnitOfWork.SaveChangesAsync();

            return SuccessDataResult(userEntity.UserId);
        }

        public async Task<IResult> DeleteAsync(long userId)
        {
            await UserRepository.DeleteAsync(userId);

            await DatabaseUnitOfWork.SaveChangesAsync();

            return SuccessResult();
        }

        public async Task<IResult> InactivateAsync(long userId)
        {
            var userEntity = UserEntityFactory.Create(userId);

            userEntity.Inactivate();

            await UserRepository.UpdatePartialAsync(userEntity.UserId, new { userEntity.Status });

            await DatabaseUnitOfWork.SaveChangesAsync();

            return SuccessResult();
        }

        public async Task<PagedList<UserModel>> ListAsync(PagedListParameters parameters)
        {
            return await UserRepository.ListAsync<UserModel>(parameters);
        }

        public async Task<IEnumerable<UserModel>> ListAsync()
        {
            return await UserRepository.ListAsync<UserModel>();
        }

        public async Task<UserModel> SelectAsync(long userId)
        {
            return await UserRepository.SelectAsync<UserModel>(userId);
        }

        public async Task<IDataResult<SignedInModel>> SignInAsync(SignInModel signInModel)
        {
            var validation = new SignInModelValidator().Valid(signInModel);

            if (!validation.Success)
            {
                return ErrorDataResult<SignedInModel>(validation.Message);
            }

            UserDomainService.GenerateHash(signInModel);

            var signedInModel = await UserRepository.SignInAsync(signInModel);

            validation = new SignedInModelValidator().Valid(signedInModel);

            if (!validation.Success)
            {
                return ErrorDataResult<SignedInModel>(validation.Message);
            }

            var addUserLogModel = new AddUserLogModel(signedInModel.UserId, LogType.SignIn);

            await UserLogApplicationService.AddAsync(addUserLogModel);

            return SuccessDataResult(signedInModel);
        }

        public async Task<IDataResult<TokenModel>> SignInJwtAsync(SignInModel signInModel)
        {
            var result = await SignInAsync(signInModel).ConfigureAwait(false);

            if (!result.Success)
            {
                return ErrorDataResult<TokenModel>(result.Message);
            }

            var tokenModel = UserDomainService.GenerateToken(result.Data);

            return SuccessDataResult(tokenModel);
        }

        public async Task SignOutAsync(SignOutModel signOutModel)
        {
            var addUserLogModel = new AddUserLogModel(signOutModel.UserId, LogType.SignOut);

            await UserLogApplicationService.AddAsync(addUserLogModel);
        }

        public async Task<IResult> UpdateAsync(UpdateUserModel updateUserModel)
        {
            var validation = new UpdateUserModelValidator().Valid(updateUserModel);

            if (!validation.Success)
            {
                return ErrorResult(validation.Message);
            }

            var userEntity = await UserRepository.SelectAsync(updateUserModel.UserId);

            if (userEntity == null)
            {
                return SuccessResult();
            }

            userEntity.Update(updateUserModel);

            await UserRepository.UpdateAsync(userEntity.UserId, userEntity);

            await DatabaseUnitOfWork.SaveChangesAsync();

            return SuccessResult();
        }
    }
}
