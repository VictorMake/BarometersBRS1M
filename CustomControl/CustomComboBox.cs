using System.Windows.Forms;

namespace BarometersBRS1M
{
    public partial class CustomComboBox : ToolStripControlHost
    {
        public CustomComboBox() : base(new UserCombolBox())
        {
            //Этот вызов является обязательным для конструктора компонентов.
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        { base.OnPaint(pe); }

        public UserCombolBox ComboBoxToDelete { get { return (UserCombolBox)(UserControl)base.Control; } }
    }
}
