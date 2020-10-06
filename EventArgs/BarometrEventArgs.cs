using System;

namespace BarometersBRS1M
{
    public class BarometrEventArgs : EventArgs
    {
        public BarometrEventArgs(string inBarometerInput, string inBarometerOutput)
        {
            this.BarometerInput = inBarometerInput;
            this.BarometerOutput = inBarometerOutput;
        }
        public string BarometerInput { get; private set; }
        public string BarometerOutput { get; private set; }
    }
}
