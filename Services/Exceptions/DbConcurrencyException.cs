namespace SalesWebMVC.Services.Exceptions
{
    public class DbConcurrencyException(string? message) : ApplicationException(message);
}
