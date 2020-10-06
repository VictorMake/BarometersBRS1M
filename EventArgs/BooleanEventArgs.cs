using System;

namespace BarometersBRS1M
{
    public class BooleanEventArgs : EventArgs
    {
        public BooleanEventArgs(bool Checked)
        {
            this.Checked = Checked;
        }
        public bool Checked { get; private set; }
    }
}
