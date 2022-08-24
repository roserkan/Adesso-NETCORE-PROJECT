using Adesso.Domain.Enums;

namespace Adesso.Application.CrossCuttingConcerns.Logging;

public static class Logger
{
    private static LogBase logger = null;
    public static void Log(LogTypes target, string message)
    {
        switch (target)
        {
            case LogTypes.File:
                logger = new FileLogger();
                logger.Log(message);
                break;
            default:
                return;
        }
    }
}