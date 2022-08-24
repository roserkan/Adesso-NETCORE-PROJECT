using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Adesso.Application.CrossCuttingConcerns.Exceptions;

public class AuthorizationProblemDetails : ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);
}
