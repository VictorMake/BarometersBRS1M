using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using static BarometersBRS1M.GlobalUtility;

namespace BarometersBRS1M
{
    public partial class BarometerControlView : UserControl, IView
    {
        #region Events IView
        public event EventHandler<EventArgs> SetTextEvent;
        public event EventHandler<EventArgs> SetHexEvent;
        public event EventHandler<SettingsEventArgs> ChangedSettingsEvent;
        public event EventHandler<EventArgs> OpenPortEvent;
        public event EventHandler<BooleanEventArgs> DtrEnableEvent;
        public event EventHandler<BooleanEventArgs> RtsEnableEvent;
        public event EventHandler<SendDataEventArgs> SendDataEvent;
        #endregion

        #region Property IView
        public string PortName { get { return cmbPortName.Text; } set { cmbPortName.Text = value; ChangedSettings(); } }
        public string ParameterName
        {
            get { return txtParameterName.Text; }
            set
            {
                this.Tag = value;
                txtParameterName.Text = value; ChangedSettings();
            }
        }
        public bool TextData { get { return rbText.Checked; } set => rbText.Checked = value; }
        public bool HexData { get { return rbHex.Checked; } set => rbHex.Checked = value; }
        public Parity Parity { get { return (Parity)Enum.Parse(typeof(Parity), cmbParity.Text); } set { cmbParity.Text = value.ToString(); ChangedSettings(); } }
        public StopBits StopBits { get { return (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text); } set { cmbStopBits.Text = value.ToString(); ChangedSettings(); } }
        public int DataBits { get { return int.Parse(cmbDataBits.Text); } set { cmbDataBits.Text = value.ToString(); ChangedSettings(); } }
        public int BaudRate { get { return int.Parse(cmbBaudRate.Text); } set { cmbBaudRate.Text = value.ToString(); ChangedSettings(); } }
        public bool ClearOnOpen { get { return chkClearOnOpen.Checked; } set { chkClearOnOpen.Checked = value; ChangedSettings(); } }
        public bool ClearWithDTR { get { return chkClearWithDTR.Checked; } set { chkClearWithDTR.Checked = value; ChangedSettings(); } }
        public bool DtrEnable { get { return chkDTR.Checked; } set => chkDTR.Checked = value; }
        public bool RtsEnable { get { return chkRTS.Checked; } set => chkRTS.Checked = value; }
        public DataMode CurrentDataMode
        {
            get
            {
                if (rbHex.Checked) return DataMode.Hex;
                else return DataMode.Text;
            }
            set
            {
                if (value == DataMode.Text) rbText.Checked = true;
                else rbHex.Checked = true;
            }
        }

        /// <summary>
        /// Предотвратить вызов событий до конца инициализации
        /// </summary>
        public bool InitializeIsSuccess { get; set; }
        public UnitOfMeasureInput UnitInput
        {
            get { return (UnitOfMeasureInput)Enum.Parse(typeof(UnitOfMeasureInput), SlideMeasureInput.Value.ToString()); }
            set { SlideMeasureInput.Value = (double)value; ChangedSettings(); }
        }
        public UnitOfMeasureOutput UnitOutput
        {
            get { return (UnitOfMeasureOutput)Enum.Parse(typeof(UnitOfMeasureOutput), SlideMeasureOutput.Value.ToString()); }
            set { SlideMeasureOutput.Value = (double)value; ChangedSettings(); }
        }
        #endregion

        #region Local Variables
        private bool KeyHandled = false;// Временный указатель, была ли клавиша нажата
        #endregion

        #region Constructor
        public BarometerControlView()
        { InitializeComponent(); }// создание фомы

        private void BarometerControlView_Load(object sender, EventArgs e)
        {
            // ComboBox cmbParity и cmbStopBits должен быть заполнен специфицированным типом
            cmbParity.Items.Clear(); cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBits.Items.Clear(); cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            Log(LogMsgType.Normal, String.Format("Приложение запущено в {0}\n", DateTime.Now));
        }
        #endregion

        #region Methods реализации интерфейса IView

        /// <summary> Установить значения контролов формы по умолчанию. </summary>
        public void InitializeControlValues(string inPortName, bool IsPortOpen, ManagerComPorts inManagerPorts)
        {
            inManagerPorts.RefreshComPortList(cmbPortName, IsPortOpen);

            // Если всё ещё доступно, выбрать последний com port для использовния
            if (cmbPortName.Items.Contains(inPortName)) cmbPortName.Text = inPortName;
            else if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = cmbPortName.Items.Count - 1;
            else
            {
                //MessageBox.Show(this, "На этом компьютере нет ни одного открытого COM Порта.\nИнсталлируйте COM Порт и перезагрузите приложение.", "Нет инсталлированных COM Портов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Close();
                Log(LogMsgType.Error, "На этом компьютере нет ни одного открытого COM Порта.\nИнсталлируйте COM Порт и перезагрузите приложение.\n");
                this.Enabled = false;
            }

            EnableControls(IsPortOpen);
        }

        /// <summary> Логгирование данных в окно терминала. </summary>
        /// <param name="msgtype"> Тип сообщения для записи. </param>
        /// <param name="msg"> Строка содержащая сообщения для вывода. </param>
        public void Log(LogMsgType msgtype, string msg)
        {
            rtfTerminal.Invoke(new EventHandler(delegate
            {
                rtfTerminal.SelectedText = string.Empty;
                rtfTerminal.SelectionFont = new Font(rtfTerminal.SelectionFont, FontStyle.Bold);
                rtfTerminal.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtfTerminal.AppendText(msg);
                rtfTerminal.ScrollToCaret();
            }));
        }

        /// <summary> Включить/отключить контролы на основании открытости порта. </summary>
        public void EnableControls(bool IsPortOpen)
        {
            gbPortSettings.Enabled = !IsPortOpen;
            txtSendData.Enabled = btnSend.Enabled = IsPortOpen;
            //chkDTR.Enabled = chkRTS.Enabled = IsPortOpen;

            if (IsPortOpen) { btnOpenPort.Text = "&Закрыть Порт"; btnOpenPort.BackColor = Color.Orange; }
            else { btnOpenPort.Text = "&Открыть Порт"; btnOpenPort.BackColor = SystemColors.Control; }
        }

        /// <summary> Отправить пользвательские данные введённые в поле для отправки.</summary>
        public void SendData()
        {
            SendDataEvent?.Invoke(this, new SendDataEventArgs(txtSendData.Text));
            txtSendData.SelectAll();
        }
        /// <summary> Обновить значение барометра входное.</summary>
        public void BarometerInput(string inBarometerInput)
        { lblBarometerInput.Invoke(new EventHandler(delegate { lblBarometerInput.Text = inBarometerInput; })); }

        /// <summary> Обновить значение барометра пересчитанное.</summary>
        public void BarometerOtput(string inBarometerOutput)
        { lblBarometerOutput.Invoke(new EventHandler(delegate { lblBarometerOutput.Text = inBarometerOutput; })); }

        /// <summary>
        /// Обновить состояние пинов
        /// </summary>
        /// <param name="e"></param>
        public void UpdatePinState(UpdatePinStateEventArgs e)
        {
            this.Invoke(new ThreadStart(() =>
            {
                chkCD.Checked = e.CDHolding;
                chkCTS.Checked = e.CtsHolding;
                chkDSR.Checked = e.DsrHolding;
            }));
        }

        /// <summary>
        /// Очистка поля вывода
        /// </summary>
        public void ClearTerminal() { rtfTerminal.Clear(); }

        /// <summary>
        /// Фокус на поле отправки данных
        /// </summary>
        public void TextSendDataFocus() { txtSendData.Focus(); }

        //public void CloseTerminal()
        //{ // this.Close();
        //}

        #endregion

        #region Event Handlers
        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { (new frmAbout()).ShowDialog(this); }// Показать диалог о программе

        private void rbText_CheckedChanged(object sender, EventArgs e)
        { if (rbText.Checked) SetTextEvent?.Invoke(this, EventArgs.Empty); }

        private void rbHex_CheckedChanged(object sender, EventArgs e)
        { if (rbHex.Checked) SetHexEvent?.Invoke(this, EventArgs.Empty); }

        private void btnOpenPort_Click(object sender, EventArgs e)
        { OpenPortEvent?.Invoke(this, new EventArgs()); }

        /// <summary>
        /// При изменении пользователем значений контролов
        /// оповестить об этом презентера, который перезапишет модель
        /// </summary>
        private void ChangedSettings()
        {
            if (InitializeIsSuccess)
            {
                // При любом изменении пользовательские настроек передать их контроллеру
                ChangedSettingsEvent?.Invoke(this, new SettingsEventArgs(
                    PortName,
                    ParameterName,
                    Parity,
                    StopBits,
                    DataBits,
                    BaudRate,
                    ClearOnOpen,
                    ClearWithDTR,
                    UnitInput,
                    UnitOutput));
            }
        }

        private void chkDTR_CheckedChanged(object sender, EventArgs e)
        {
            DtrEnableEvent?.Invoke(this, new BooleanEventArgs(chkDTR.Checked));

            if (chkDTR.Checked && chkClearWithDTR.Checked) ClearTerminal();
        }

        private void chkRTS_CheckedChanged(object sender, EventArgs e)
        { RtsEnableEvent?.Invoke(this, new BooleanEventArgs(chkRTS.Checked)); }

        private void cmbBaudRate_Validating(object sender, CancelEventArgs e)
        { e.Cancel = !int.TryParse(cmbBaudRate.Text, out int x); }

        private void cmbDataBits_Validating(object sender, CancelEventArgs e)
        { e.Cancel = !int.TryParse(cmbDataBits.Text, out int x); }

        private void btnSend_Click(object sender, EventArgs e)
        { SendData(); }

        private void txtSendData_KeyDown(object sender, KeyEventArgs e)
        {
            // Если пользователь нажал [ENTER], отправить данные сейчас же
            if (KeyHandled = e.KeyCode == Keys.Enter) { e.Handled = true; SendData(); }
        }

        private void txtSendData_KeyPress(object sender, KeyPressEventArgs e)
        { e.Handled = KeyHandled; }

        private void btnClear_Click(object sender, EventArgs e)
        { ClearTerminal(); }

        //private void tmrCheckComPorts_Tick(object sender, EventArgs e)
        //{
        //    // checks to see if COM ports have been added or removed
        //    // since it is quite common now with USB-to-Serial adapters
        //    //RefreshComPortList();
        //}

        private void cmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        { PortName = cmbPortName.Text; }

        private void txtParameterName_TextChanged(object sender, EventArgs e)
        { ParameterName = txtParameterName.Text; }

        private void cmbBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        { BaudRate = int.Parse(cmbBaudRate.Text); }

        private void cmbParity_SelectedIndexChanged(object sender, EventArgs e)
        { Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text); }

        private void cmbDataBits_SelectedIndexChanged(object sender, EventArgs e)
        { DataBits = int.Parse(cmbDataBits.Text); }

        private void cmbStopBits_SelectedIndexChanged(object sender, EventArgs e)
        { StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text); }

        private void chkClearOnOpen_CheckedChanged(object sender, EventArgs e)
        { ClearOnOpen = chkClearOnOpen.Checked; }

        private void chkClearWithDTR_CheckedChanged(object sender, EventArgs e)
        { ClearWithDTR = chkClearWithDTR.Checked; }

        private void SlideMeasureInput_AfterChangeValue(object sender, NationalInstruments.UI.AfterChangeNumericValueEventArgs e)
        { UnitInput = (UnitOfMeasureInput)Enum.Parse(typeof(UnitOfMeasureInput), SlideMeasureInput.Value.ToString()); }

        private void SlideMeasureOutput_AfterChangeValue(object sender, NationalInstruments.UI.AfterChangeNumericValueEventArgs e)
        { UnitOutput = (UnitOfMeasureOutput)Enum.Parse(typeof(UnitOfMeasureOutput), SlideMeasureOutput.Value.ToString()); }

        #endregion
    }
}
