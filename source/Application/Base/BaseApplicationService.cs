using DotNetCore.Objects;

namespace DotNetCoreArchitecture.Application
{
    public abstract class BaseApplicationService
    {
        public ErrorDataResult<T> ErrorDataResult<T>(string message)
        {
            return new ErrorDataResult<T>(message);
        }

        public ErrorResult ErrorResult()
        {
            return new ErrorResult();
        }

        public ErrorResult ErrorResult(string message)
        {
            return new ErrorResult(message);
        }

        public SuccessDataResult<T> SuccessDataResult<T>(T data)
        {
            return new SuccessDataResult<T>(data);
        }

        public SuccessResult SuccessResult()
        {
            return new SuccessResult();
        }
    }
}
