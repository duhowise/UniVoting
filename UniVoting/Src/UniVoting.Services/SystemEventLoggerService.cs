using System;
using UniVoting.Core;

namespace UniVoting.Services
{
    public class SystemEventLoggerService:ILogger
    {
        //static readonly string _source = "Univoting";
        //readonly EventLog _appEventLog = new EventLog("LiveView");
        //public SystemEventLoggerService()
        //{
           
        //    if (!EventLog.SourceExists(_source))
        //    {
        //        EventLog.CreateEventSource(_source, _appEventLog.LogDisplayName);
        //    }
        //}
        public static void Log(string data)
        {
            Console.WriteLine( data);
        }

        void ILogger.Log(Exception exception)
        {
            Log($"{exception.Message}\n{exception.StackTrace}");
        }
    }
}