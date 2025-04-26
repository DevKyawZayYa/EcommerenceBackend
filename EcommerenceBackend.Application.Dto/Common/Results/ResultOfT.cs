namespace EcommerenceBackend.Application.Common.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new();
        public T Data { get; set; }

        public static Result<T> Success(T data, string message = null)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Message = message ?? "Operation succeeded.",
                Data = data
            };
        }

        public static Result<T> Failure(List<string> errors, string message = null)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message ?? "Operation failed.",
                Errors = errors
            };
        }

        public static Result<T> Failure(string error, string message = null)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message ?? "Operation failed.",
                Errors = new List<string> { error }
            };
        }
    }
}
