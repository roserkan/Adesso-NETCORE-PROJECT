﻿namespace Adesso.Application.Utilities.Results;



public interface IResult
{
    bool Success { get; }
    string Message { get; }

}
