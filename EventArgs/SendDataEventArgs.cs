using System;

namespace BarometersBRS1M
{
    public class SendDataEventArgs : EventArgs
    {
        public SendDataEventArgs(string inSendData) { SendData = inSendData; }
        public String SendData { get; private set; } // readonly
    }
}
