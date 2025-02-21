using System;

namespace UniVoting.Model
{
    public interface ILogger
    {
       void Log(Exception exception);
    }
}