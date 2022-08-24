using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adesso.Application.CrossCuttingConcerns.Logging;

public class FileLogger : LogBase
{
    public override void Log(string message)
    {
        string path = Directory.GetCurrentDirectory() + @"\\log.txt";



        using (StreamWriter w = File.AppendText(path))
        {
            w.WriteLine(message);
        }

    }
}
