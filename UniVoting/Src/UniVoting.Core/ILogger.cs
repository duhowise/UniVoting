using System;

namespace UniVoting.Core
{
    public interface ILogger
    {
       void Log(Exception exception);
    }
}