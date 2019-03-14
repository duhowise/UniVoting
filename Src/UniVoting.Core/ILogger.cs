using System;

namespace Univoting.Core
{
    public interface ILogger
    {
       void Log(Exception exception);
    }
}