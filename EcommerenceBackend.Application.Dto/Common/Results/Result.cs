namespace EcommerenceBackend.Application.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new();

        public static Result Success(string message)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message ?? "Operation succeeded."
            };
        }

        public static Result Failure(string message)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message ?? "Operation failed.",
            };
        }

        public static Result Failure(string error, string message)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message ?? "Operation failed.",
                Errors = new List<string> { error }
            };
        }
    }
}
