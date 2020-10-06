using System;
using System.IO.Ports;

namespace BarometersBRS1M
{
    public class SettingsEventArgs : EventArgs
    {
        public SettingsEventArgs(string inPortName,
                                    string inParameterName,
                                    Parity inParity,
                                    StopBits inStopBits,
                                    int inDataBits,
                                    int inBaudRate,
                                    bool inClearOnOpen,
                                    bool inClearWithDTR,
                                    UnitOfMeasureInput inInput, 
                                    UnitOfMeasureOutput inOutput)

        {
            PortName = inPortName;
            ParameterName = inParameterName;
            Parity = inParity;
            StopBits = inStopBits;
            DataBits = inDataBits;
            BaudRate = inBaudRate;
            ClearOnOpen = inClearOnOpen;
            ClearWithDTR = inClearWithDTR;
            UnitInput = inInput;
            UnitOutput = inOutput;
        }
        public string PortName { get; private set; }
        public string ParameterName { get; private set; }
        public Parity Parity { get; private set; }
        public StopBits StopBits { get; private set; }
        public int DataBits { get; private set; }
        public int BaudRate { get; private set; }
        public bool ClearOnOpen { get; private set; }
        public bool ClearWithDTR { get; private set; }
        public UnitOfMeasureInput UnitInput { get; private set; }
        public UnitOfMeasureOutput UnitOutput { get; private set; }
    }
}
