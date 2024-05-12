namespace ProjectManagment.BLL;

public class Result
{
    public bool IsSuccess { get; private set; }
    public string ErrorMessage { get; private set; }

    private Result(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static Result Success()
    {
        return new Result(true, string.Empty);
    }

    public static Result Failure(string errorMessage)
    {
        return new Result(false, errorMessage);
    }
}
