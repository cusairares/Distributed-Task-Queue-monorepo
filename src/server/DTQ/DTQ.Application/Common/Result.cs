namespace DTQ.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string? error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException("Successful result cannot have an error.");
            if (!isSuccess && string.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException("Failed result must have an error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(string error) => new(false, error);
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Cannot access Value of a failure result.");

        private Result(T value) : base(true, null) => _value = value;
        private Result(string error) : base(false, error) => _value = default;

        public static Result<T> Success(T value) => new(value);
        public static new Result<T> Failure(string error) => new(error);
    }
}
