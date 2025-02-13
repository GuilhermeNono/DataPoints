namespace DataPoints.Domain.Errors.Abstractions.Interfaces;

public interface IErrorCatcher
{
    public IEnumerable<Error> Catch(Exception exception);
}