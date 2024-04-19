namespace Infrastructure.Models
{
    public enum StatusCodes
    {
        OK = 200,
        ERROR = 400,
        NOT_FOUND = 404,
        EXISTS = 409,
        UNAUTHORIZED = 401,
    }
    public class RepositoriesResult
    {
        public StatusCodes StatusCode { get; set; }

        public object? ContentResult { get; set; }

        public string? Message { get; set; }
    }
}
