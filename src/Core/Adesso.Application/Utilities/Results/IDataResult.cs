namespace Adesso.Application.Utilities.Results;

public interface IDataResult<T> : IResult
{
    T Data { get; }
}
