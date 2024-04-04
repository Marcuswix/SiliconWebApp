using Infrastructure.Models;

namespace Infrastructure.Factories
{
    public class ResponseFactory
    {
        public static RepositoriesResult Ok()
        {
            return new RepositoriesResult
            {
                Message = "Succeded",
                StatusCode = StatusCodes.OK,
            };
        }

        public static RepositoriesResult Ok(string? message = null!)
        {
            return new RepositoriesResult
            {
                Message = message ?? "Succeded",
                StatusCode = StatusCodes.OK,
            };
        }

        public static RepositoriesResult Ok(object obj, string? message = null)
        {
            return new RepositoriesResult
            {
                ContentResult = obj,
                Message = message ?? "Succeded",
                StatusCode = StatusCodes.OK,
            };
        }

        public static RepositoriesResult Ok(List<object> list, string? message = null)
        {
            return new RepositoriesResult
            {
                ContentResult = list,
                Message = message ?? "Succeded",
                StatusCode = StatusCodes.OK,
            };
        }

        public static RepositoriesResult NotFound(string? message = null!)
        {
                return new RepositoriesResult
                {
                    Message = "Not Found",
                    StatusCode = StatusCodes.NOT_FOUND,
                };
        }

        public static RepositoriesResult Error(string? message = null)
        {
            return new RepositoriesResult
            {
                Message = message ?? "Failed",
                StatusCode = StatusCodes.ERROR,
            };
        }

        public static RepositoriesResult AlreadyExist(string? message = null)
        {
            return new RepositoriesResult
            {
                Message = message ?? "Already Exist",
                StatusCode = StatusCodes.EXISTS,
            };
        }
    }
}
