using System;

namespace BarometersBRS1M
{
    public class UpdatePinStateEventArgs : EventArgs
    {
        public UpdatePinStateEventArgs(bool inCDHolding, bool inCtsHolding, bool inDsrHolding)
        {
            CDHolding = inCDHolding;
            CtsHolding = inCtsHolding;
            DsrHolding = inDsrHolding;
        }

        public bool CDHolding { get; private set; }
        public bool CtsHolding { get; private set; }
        public bool DsrHolding { get; private set; }
    }
}
