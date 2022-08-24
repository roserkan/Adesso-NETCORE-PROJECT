using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Adesso.Application.CrossCuttingConcerns.Exceptions;

public class BusinessProblemDetails : ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);
}
