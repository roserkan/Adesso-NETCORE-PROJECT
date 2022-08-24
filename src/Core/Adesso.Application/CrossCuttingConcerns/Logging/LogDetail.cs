
using Adesso.Domain.Enums;

namespace Adesso.Application.CrossCuttingConcerns.Logging;

public class LogDetail
{
    public LogLevel LogLevel { get; set; }
    public string Message { get; set; }
    public string UserId { get; set; }
    public string UserEmailAddress { get; set; }
    public string Path { get; set; }
    public string MethodType { get; set; }
    public string StatusCode { get; set; }
    public string Date { get; set; }
}
