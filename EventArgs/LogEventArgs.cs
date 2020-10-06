using System;

namespace BarometersBRS1M
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(LogMsgType msgtype, string msg)
        {
            LogType = msgtype;
            Message = msg;
        }
        public string Message { get; private set; }

        public LogMsgType LogType { get; private set; }
    }
}
