namespace BarometersBRS1M
{
    partial class BarometerControlView
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            NationalInstruments.UI.ScaleCustomDivision scaleCustomDivision1 = new NationalInstruments.UI.ScaleCustomDivision();
            NationalInstruments.UI.ScaleCustomDivision scaleCustomDivision2 = new NationalInstruments.UI.ScaleCustomDivision();
            NationalInstruments.UI.ScaleCustomDivision scaleCustomDivision3 = new NationalInstruments.UI.ScaleCustomDivision();
            NationalInstruments.UI.ScaleCustomDivision scaleCustomDivision4 = new NationalInstruments.UI.ScaleCustomDivision();
            NationalInstruments.UI.ScaleCustomDivision scaleCustomDivision5 = new NationalInstruments.UI.ScaleCustomDivision();
            NationalInstruments.UI.ScaleCustomDivision scaleCustomDivision6 = new NationalInstruments.UI.ScaleCustomDivision();
            NationalInstruments.UI.ScaleCustomDivision scaleCustomDivision7 = new NationalInstruments.UI.ScaleCustomDivision();
            NationalInstruments.UI.ScaleCustomDivision scaleCustomDivision8 = new NationalInstruments.UI.ScaleCustomDivision();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSend = new System.Windows.Forms.Button();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.chkRTS = new System.Windows.Forms.CheckBox();
            this.chkCD = new System.Windows.Forms.CheckBox();
            this.chkDSR = new System.Windows.Forms.CheckBox();
            this.chkCTS = new System.Windows.Forms.CheckBox();
            this.chkDTR = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkClearOnOpen = new System.Windows.Forms.CheckBox();
            this.chkClearWithDTR = new System.Windows.Forms.CheckBox();
            this.txtParameterName = new System.Windows.Forms.TextBox();
            this.SlideMeasureInput = new NationalInstruments.UI.WindowsForms.Slide();
            this.SlideMeasureOutput = new NationalInstruments.UI.WindowsForms.Slide();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelSetting = new System.Windows.Forms.Panel();
            this.lblSend = new System.Windows.Forms.Label();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.gbMode = new System.Windows.Forms.GroupBox();
            this.rbText = new System.Windows.Forms.RadioButton();
            this.rbHex = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gbPortSettings = new System.Windows.Forms.GroupBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.lblParity = new System.Windows.Forms.Label();
            this.rtfTerminal = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBarometerInput = new System.Windows.Forms.Label();
            this.lblPressureInput = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblBarometerOutput = new System.Windows.Forms.Label();
            this.lblPressureOutput = new System.Windows.Forms.Label();
            this.panelNameParameter = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SlideMeasureInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SlideMeasureOutput)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelSetting.SuspendLayout();
            this.gbMode.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbPortSettings.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelNameParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.BackColor = System.Drawing.SystemColors.Control;
            this.btnSend.Location = new System.Drawing.Point(528, 7);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Отправить";
            this.toolTip.SetToolTip(this.btnSend, "Отправить данные пользователя, введенные в поле \"отправить\"");
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPort.BackColor = System.Drawing.SystemColors.Control;
            this.btnOpenPort.Location = new System.Drawing.Point(528, 54);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(156, 37);
            this.btnOpenPort.TabIndex = 6;
            this.btnOpenPort.Text = "&Открыть порт";
            this.toolTip.SetToolTip(this.btnOpenPort, "Открыть/закрыть порт");
            this.btnOpenPort.UseVisualStyleBackColor = false;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // chkRTS
            // 
            this.chkRTS.AutoSize = true;
            this.chkRTS.Location = new System.Drawing.Point(65, 19);
            this.chkRTS.Name = "chkRTS";
            this.chkRTS.Size = new System.Drawing.Size(48, 17);
            this.chkRTS.TabIndex = 1;
            this.chkRTS.Text = "RTS";
            this.toolTip.SetToolTip(this.chkRTS, "Pin 7 on DB9, Output, Request to Send");
            this.chkRTS.UseVisualStyleBackColor = true;
            this.chkRTS.CheckedChanged += new System.EventHandler(this.chkRTS_CheckedChanged);
            // 
            // chkCD
            // 
            this.chkCD.AutoSize = true;
            this.chkCD.Location = new System.Drawing.Point(226, 19);
            this.chkCD.Name = "chkCD";
            this.chkCD.Size = new System.Drawing.Size(41, 17);
            this.chkCD.TabIndex = 4;
            this.chkCD.Text = "CD";
            this.toolTip.SetToolTip(this.chkCD, "Pin 1 on DB9, Input, Data Carrier Detect");
            this.chkCD.UseVisualStyleBackColor = true;
            // 
            // chkDSR
            // 
            this.chkDSR.AutoSize = true;
            this.chkDSR.Location = new System.Drawing.Point(172, 19);
            this.chkDSR.Name = "chkDSR";
            this.chkDSR.Size = new System.Drawing.Size(49, 17);
            this.chkDSR.TabIndex = 3;
            this.chkDSR.Text = "DSR";
            this.toolTip.SetToolTip(this.chkDSR, "Pin 6 on DB9, Input, Data Set Ready");
            this.chkDSR.UseVisualStyleBackColor = true;
            // 
            // chkCTS
            // 
            this.chkCTS.AutoSize = true;
            this.chkCTS.Location = new System.Drawing.Point(119, 19);
            this.chkCTS.Name = "chkCTS";
            this.chkCTS.Size = new System.Drawing.Size(47, 17);
            this.chkCTS.TabIndex = 2;
            this.chkCTS.Text = "CTS";
            this.toolTip.SetToolTip(this.chkCTS, "Pin 8 on DB9, Input, Clear to Send");
            this.chkCTS.UseVisualStyleBackColor = true;
            // 
            // chkDTR
            // 
            this.chkDTR.AutoSize = true;
            this.chkDTR.Location = new System.Drawing.Point(10, 19);
            this.chkDTR.Name = "chkDTR";
            this.chkDTR.Size = new System.Drawing.Size(49, 17);
            this.chkDTR.TabIndex = 0;
            this.chkDTR.Text = "DTR";
            this.toolTip.SetToolTip(this.chkDTR, "Pin 4 on DB9, Output, Data Terminal Ready");
            this.chkDTR.UseVisualStyleBackColor = true;
            this.chkDTR.CheckedChanged += new System.EventHandler(this.chkDTR_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.Location = new System.Drawing.Point(609, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "&Очистить";
            this.toolTip.SetToolTip(this.btnClear, "Очистить терминал");
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkClearOnOpen
            // 
            this.chkClearOnOpen.AutoSize = true;
            this.chkClearOnOpen.Location = new System.Drawing.Point(282, 124);
            this.chkClearOnOpen.Name = "chkClearOnOpen";
            this.chkClearOnOpen.Size = new System.Drawing.Size(143, 17);
            this.chkClearOnOpen.TabIndex = 10;
            this.chkClearOnOpen.Text = "Очищать при открытии";
            this.toolTip.SetToolTip(this.chkClearOnOpen, "Очищать терминал при открытии");
            this.chkClearOnOpen.UseVisualStyleBackColor = true;
            this.chkClearOnOpen.CheckedChanged += new System.EventHandler(this.chkClearOnOpen_CheckedChanged);
            // 
            // chkClearWithDTR
            // 
            this.chkClearWithDTR.AutoSize = true;
            this.chkClearWithDTR.Location = new System.Drawing.Point(431, 124);
            this.chkClearWithDTR.Name = "chkClearWithDTR";
            this.chkClearWithDTR.Size = new System.Drawing.Size(118, 17);
            this.chkClearWithDTR.TabIndex = 11;
            this.chkClearWithDTR.Text = "Очищать при DTR";
            this.toolTip.SetToolTip(this.chkClearWithDTR, "Очищать терминал при сигнале DTR");
            this.chkClearWithDTR.UseVisualStyleBackColor = true;
            this.chkClearWithDTR.CheckedChanged += new System.EventHandler(this.chkClearWithDTR_CheckedChanged);
            // 
            // txtParameterName
            // 
            this.txtParameterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParameterName.Location = new System.Drawing.Point(234, 1);
            this.txtParameterName.Name = "txtParameterName";
            this.txtParameterName.Size = new System.Drawing.Size(451, 20);
            this.txtParameterName.TabIndex = 1;
            this.txtParameterName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip.SetToolTip(this.txtParameterName, "Введите наименование параметра");
            this.txtParameterName.TextChanged += new System.EventHandler(this.txtParameterName_TextChanged);
            // 
            // SlideMeasureInput
            // 
            this.SlideMeasureInput.Caption = "Вход";
            this.SlideMeasureInput.CaptionBackColor = System.Drawing.Color.Transparent;
            this.SlideMeasureInput.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SlideMeasureInput.CaptionForeColor = System.Drawing.Color.DarkGreen;
            this.SlideMeasureInput.CoercionMode = NationalInstruments.UI.NumericCoercionMode.ToDivisions;
            scaleCustomDivision1.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            scaleCustomDivision1.Text = "гектоПа";
            scaleCustomDivision1.TickColor = System.Drawing.Color.Black;
            scaleCustomDivision1.Value = 1D;
            scaleCustomDivision2.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            scaleCustomDivision2.Text = "мм рт.ст.";
            scaleCustomDivision2.TickColor = System.Drawing.Color.Black;
            scaleCustomDivision2.Value = 2D;
            this.SlideMeasureInput.CustomDivisions.AddRange(new NationalInstruments.UI.ScaleCustomDivision[] {
            scaleCustomDivision1,
            scaleCustomDivision2});
            this.SlideMeasureInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SlideMeasureInput.ForeColor = System.Drawing.Color.DarkGreen;
            this.SlideMeasureInput.Location = new System.Drawing.Point(3, 3);
            this.SlideMeasureInput.MajorDivisions.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SlideMeasureInput.MajorDivisions.LabelVisible = false;
            this.SlideMeasureInput.MajorDivisions.TickVisible = false;
            this.SlideMeasureInput.MinorDivisions.TickVisible = false;
            this.SlideMeasureInput.Name = "SlideMeasureInput";
            this.SlideMeasureInput.PointerColor = System.Drawing.Color.DarkGreen;
            this.SlideMeasureInput.Range = new NationalInstruments.UI.Range(1D, 2D);
            this.SlideMeasureInput.Size = new System.Drawing.Size(100, 124);
            this.SlideMeasureInput.TabIndex = 24;
            this.toolTip.SetToolTip(this.SlideMeasureInput, "Установить как на приборе БРС-1М");
            this.SlideMeasureInput.Value = 1D;
            this.SlideMeasureInput.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.SlideMeasureInput_AfterChangeValue);
            // 
            // SlideMeasureOutput
            // 
            this.SlideMeasureOutput.Caption = "Выход";
            this.SlideMeasureOutput.CaptionBackColor = System.Drawing.Color.Transparent;
            this.SlideMeasureOutput.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SlideMeasureOutput.CaptionForeColor = System.Drawing.Color.Blue;
            this.SlideMeasureOutput.CoercionMode = NationalInstruments.UI.NumericCoercionMode.ToDivisions;
            scaleCustomDivision3.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            scaleCustomDivision3.Text = "гектоПа";
            scaleCustomDivision3.TickColor = System.Drawing.Color.Black;
            scaleCustomDivision3.Value = 1D;
            scaleCustomDivision4.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            scaleCustomDivision4.Text = "мм рт.ст.";
            scaleCustomDivision4.TickColor = System.Drawing.Color.Black;
            scaleCustomDivision4.Value = 2D;
            scaleCustomDivision5.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            scaleCustomDivision5.Text = "атм";
            scaleCustomDivision5.TickColor = System.Drawing.Color.Black;
            scaleCustomDivision5.Value = 3D;
            scaleCustomDivision6.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            scaleCustomDivision6.Text = "мм вод.ст.";
            scaleCustomDivision6.TickColor = System.Drawing.Color.Black;
            scaleCustomDivision6.Value = 4D;
            scaleCustomDivision7.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            scaleCustomDivision7.Text = "бар";
            scaleCustomDivision7.TickColor = System.Drawing.Color.Black;
            scaleCustomDivision7.Value = 5D;
            scaleCustomDivision8.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            scaleCustomDivision8.Text = "кг/см2";
            scaleCustomDivision8.TickColor = System.Drawing.Color.Black;
            scaleCustomDivision8.Value = 6D;
            this.SlideMeasureOutput.CustomDivisions.AddRange(new NationalInstruments.UI.ScaleCustomDivision[] {
            scaleCustomDivision3,
            scaleCustomDivision4,
            scaleCustomDivision5,
            scaleCustomDivision6,
            scaleCustomDivision7,
            scaleCustomDivision8});
            this.SlideMeasureOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SlideMeasureOutput.ForeColor = System.Drawing.Color.Blue;
            this.SlideMeasureOutput.Location = new System.Drawing.Point(3, 3);
            this.SlideMeasureOutput.MajorDivisions.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SlideMeasureOutput.MajorDivisions.LabelVisible = false;
            this.SlideMeasureOutput.MajorDivisions.TickVisible = false;
            this.SlideMeasureOutput.MinorDivisions.TickVisible = false;
            this.SlideMeasureOutput.Name = "SlideMeasureOutput";
            this.SlideMeasureOutput.PointerColor = System.Drawing.Color.Blue;
            this.SlideMeasureOutput.Range = new NationalInstruments.UI.Range(1D, 6D);
            this.SlideMeasureOutput.Size = new System.Drawing.Size(109, 124);
            this.SlideMeasureOutput.TabIndex = 25;
            this.toolTip.SetToolTip(this.SlideMeasureOutput, "Установить пересчитанную величину для регистратора");
            this.SlideMeasureOutput.Value = 1D;
            this.SlideMeasureOutput.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.SlideMeasureOutput_AfterChangeValue);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelSetting, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.rtfTerminal, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelNameParameter, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(694, 394);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelSetting
            // 
            this.panelSetting.Controls.Add(this.lblSend);
            this.panelSetting.Controls.Add(this.chkClearWithDTR);
            this.panelSetting.Controls.Add(this.txtSendData);
            this.panelSetting.Controls.Add(this.chkClearOnOpen);
            this.panelSetting.Controls.Add(this.btnSend);
            this.panelSetting.Controls.Add(this.btnClear);
            this.panelSetting.Controls.Add(this.gbMode);
            this.panelSetting.Controls.Add(this.groupBox3);
            this.panelSetting.Controls.Add(this.btnOpenPort);
            this.panelSetting.Controls.Add(this.gbPortSettings);
            this.panelSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSetting.Location = new System.Drawing.Point(3, 243);
            this.panelSetting.Name = "panelSetting";
            this.panelSetting.Size = new System.Drawing.Size(688, 148);
            this.panelSetting.TabIndex = 1;
            // 
            // lblSend
            // 
            this.lblSend.AutoSize = true;
            this.lblSend.Location = new System.Drawing.Point(3, 12);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(108, 13);
            this.lblSend.TabIndex = 1;
            this.lblSend.Text = "Отправить &Данные:";
            // 
            // txtSendData
            // 
            this.txtSendData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendData.Location = new System.Drawing.Point(117, 9);
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(405, 20);
            this.txtSendData.TabIndex = 2;
            this.txtSendData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSendData_KeyDown);
            this.txtSendData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSendData_KeyPress);
            // 
            // gbMode
            // 
            this.gbMode.Controls.Add(this.rbText);
            this.gbMode.Controls.Add(this.rbHex);
            this.gbMode.Location = new System.Drawing.Point(419, 35);
            this.gbMode.Name = "gbMode";
            this.gbMode.Size = new System.Drawing.Size(75, 64);
            this.gbMode.TabIndex = 5;
            this.gbMode.TabStop = false;
            this.gbMode.Text = "Data &Mode";
            // 
            // rbText
            // 
            this.rbText.AutoSize = true;
            this.rbText.Location = new System.Drawing.Point(12, 19);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(46, 17);
            this.rbText.TabIndex = 0;
            this.rbText.Text = "Text";
            this.rbText.CheckedChanged += new System.EventHandler(this.rbText_CheckedChanged);
            // 
            // rbHex
            // 
            this.rbHex.AutoSize = true;
            this.rbHex.Location = new System.Drawing.Point(12, 39);
            this.rbHex.Name = "rbHex";
            this.rbHex.Size = new System.Drawing.Size(44, 17);
            this.rbHex.TabIndex = 1;
            this.rbHex.Text = "Hex";
            this.rbHex.CheckedChanged += new System.EventHandler(this.rbHex_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkRTS);
            this.groupBox3.Controls.Add(this.chkCD);
            this.groupBox3.Controls.Add(this.chkDSR);
            this.groupBox3.Controls.Add(this.chkCTS);
            this.groupBox3.Controls.Add(this.chkDTR);
            this.groupBox3.Location = new System.Drawing.Point(3, 105);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(272, 38);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Сигнальные &Линии";
            // 
            // gbPortSettings
            // 
            this.gbPortSettings.Controls.Add(this.cmbPortName);
            this.gbPortSettings.Controls.Add(this.cmbBaudRate);
            this.gbPortSettings.Controls.Add(this.cmbStopBits);
            this.gbPortSettings.Controls.Add(this.cmbParity);
            this.gbPortSettings.Controls.Add(this.cmbDataBits);
            this.gbPortSettings.Controls.Add(this.lblComPort);
            this.gbPortSettings.Controls.Add(this.lblStopBits);
            this.gbPortSettings.Controls.Add(this.lblBaudRate);
            this.gbPortSettings.Controls.Add(this.lblDataBits);
            this.gbPortSettings.Controls.Add(this.lblParity);
            this.gbPortSettings.Location = new System.Drawing.Point(3, 35);
            this.gbPortSettings.Name = "gbPortSettings";
            this.gbPortSettings.Size = new System.Drawing.Size(410, 64);
            this.gbPortSettings.TabIndex = 4;
            this.gbPortSettings.TabStop = false;
            this.gbPortSettings.Text = "Насройки последовательного COM Порта";
            // 
            // cmbPortName
            // 
            this.cmbPortName.BackColor = System.Drawing.SystemColors.Control;
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.cmbPortName.Location = new System.Drawing.Point(10, 35);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(67, 21);
            this.cmbPortName.TabIndex = 1;
            this.cmbPortName.SelectedIndexChanged += new System.EventHandler(this.cmbPortName_SelectedIndexChanged);
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.BackColor = System.Drawing.SystemColors.Control;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(89, 35);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(67, 21);
            this.cmbBaudRate.TabIndex = 3;
            this.cmbBaudRate.SelectedIndexChanged += new System.EventHandler(this.cmbBaudRate_SelectedIndexChanged);
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.BackColor = System.Drawing.SystemColors.Control;
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cmbStopBits.Location = new System.Drawing.Point(326, 35);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(67, 21);
            this.cmbStopBits.TabIndex = 9;
            this.cmbStopBits.SelectedIndexChanged += new System.EventHandler(this.cmbStopBits_SelectedIndexChanged);
            // 
            // cmbParity
            // 
            this.cmbParity.BackColor = System.Drawing.SystemColors.Control;
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.cmbParity.Location = new System.Drawing.Point(168, 35);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(67, 21);
            this.cmbParity.TabIndex = 5;
            this.cmbParity.SelectedIndexChanged += new System.EventHandler(this.cmbParity_SelectedIndexChanged);
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.BackColor = System.Drawing.SystemColors.Control;
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
            this.cmbDataBits.Location = new System.Drawing.Point(247, 35);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(67, 21);
            this.cmbDataBits.TabIndex = 7;
            this.cmbDataBits.SelectedIndexChanged += new System.EventHandler(this.cmbDataBits_SelectedIndexChanged);
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Location = new System.Drawing.Point(7, 19);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(56, 13);
            this.lblComPort.TabIndex = 0;
            this.lblComPort.Text = "COM Port:";
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Location = new System.Drawing.Point(326, 19);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(66, 13);
            this.lblStopBits.TabIndex = 8;
            this.lblStopBits.Text = "Стоп битов:";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(86, 19);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(58, 13);
            this.lblBaudRate.TabIndex = 2;
            this.lblBaudRate.Text = "Скорость:";
            // 
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Location = new System.Drawing.Point(229, 19);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(91, 13);
            this.lblDataBits.TabIndex = 6;
            this.lblDataBits.Text = "Данных в байте:";
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.Location = new System.Drawing.Point(165, 19);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(58, 13);
            this.lblParity.TabIndex = 4;
            this.lblParity.Text = "Чётность:";
            // 
            // rtfTerminal
            // 
            this.rtfTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfTerminal.Location = new System.Drawing.Point(3, 35);
            this.rtfTerminal.Name = "rtfTerminal";
            this.rtfTerminal.Size = new System.Drawing.Size(688, 60);
            this.rtfTerminal.TabIndex = 0;
            this.rtfTerminal.Text = "";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 101);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(688, 136);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.SlideMeasureInput, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(338, 130);
            this.tableLayoutPanel3.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblBarometerInput);
            this.panel1.Controls.Add(this.lblPressureInput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(109, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 124);
            this.panel1.TabIndex = 0;
            // 
            // lblBarometerInput
            // 
            this.lblBarometerInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBarometerInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBarometerInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBarometerInput.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblBarometerInput.Location = new System.Drawing.Point(0, 20);
            this.lblBarometerInput.Name = "lblBarometerInput";
            this.lblBarometerInput.Size = new System.Drawing.Size(226, 104);
            this.lblBarometerInput.TabIndex = 12;
            this.lblBarometerInput.Text = "0000.00";
            this.lblBarometerInput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPressureInput
            // 
            this.lblPressureInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPressureInput.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPressureInput.Location = new System.Drawing.Point(0, 0);
            this.lblPressureInput.Name = "lblPressureInput";
            this.lblPressureInput.Size = new System.Drawing.Size(226, 20);
            this.lblPressureInput.TabIndex = 14;
            this.lblPressureInput.Text = "Измерено:";
            this.lblPressureInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.SlideMeasureOutput, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(347, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(338, 130);
            this.tableLayoutPanel4.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblBarometerOutput);
            this.panel2.Controls.Add(this.lblPressureOutput);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(118, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(217, 124);
            this.panel2.TabIndex = 0;
            // 
            // lblBarometerOutput
            // 
            this.lblBarometerOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBarometerOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBarometerOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBarometerOutput.ForeColor = System.Drawing.Color.Blue;
            this.lblBarometerOutput.Location = new System.Drawing.Point(0, 20);
            this.lblBarometerOutput.Name = "lblBarometerOutput";
            this.lblBarometerOutput.Size = new System.Drawing.Size(217, 104);
            this.lblBarometerOutput.TabIndex = 12;
            this.lblBarometerOutput.Text = "0000.00";
            this.lblBarometerOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPressureOutput
            // 
            this.lblPressureOutput.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPressureOutput.ForeColor = System.Drawing.Color.Blue;
            this.lblPressureOutput.Location = new System.Drawing.Point(0, 0);
            this.lblPressureOutput.Name = "lblPressureOutput";
            this.lblPressureOutput.Size = new System.Drawing.Size(217, 20);
            this.lblPressureOutput.TabIndex = 14;
            this.lblPressureOutput.Text = "Пересчитано:";
            this.lblPressureOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelNameParameter
            // 
            this.panelNameParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNameParameter.Controls.Add(this.txtParameterName);
            this.panelNameParameter.Controls.Add(this.label8);
            this.panelNameParameter.Location = new System.Drawing.Point(3, 3);
            this.panelNameParameter.Name = "panelNameParameter";
            this.panelNameParameter.Size = new System.Drawing.Size(688, 26);
            this.panelNameParameter.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(225, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Наименование параметра в регистраторе:";
            // 
            // BarometerControlView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "BarometerControlView";
            this.Size = new System.Drawing.Size(700, 400);
            this.Load += new System.EventHandler(this.BarometerControlView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SlideMeasureInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SlideMeasureOutput)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelSetting.ResumeLayout(false);
            this.panelSetting.PerformLayout();
            this.gbMode.ResumeLayout(false);
            this.gbMode.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbPortSettings.ResumeLayout(false);
            this.gbPortSettings.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelNameParameter.ResumeLayout(false);
            this.panelNameParameter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelSetting;
        private System.Windows.Forms.Label lblSend;
        private System.Windows.Forms.CheckBox chkClearWithDTR;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.CheckBox chkClearOnOpen;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox gbMode;
        private System.Windows.Forms.RadioButton rbText;
        private System.Windows.Forms.RadioButton rbHex;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkRTS;
        private System.Windows.Forms.CheckBox chkCD;
        private System.Windows.Forms.CheckBox chkDSR;
        private System.Windows.Forms.CheckBox chkCTS;
        private System.Windows.Forms.CheckBox chkDTR;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.GroupBox gbPortSettings;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.Panel panelNameParameter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblBarometerInput;
        private System.Windows.Forms.RichTextBox rtfTerminal;
        private System.Windows.Forms.Label lblPressureInput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        internal NationalInstruments.UI.WindowsForms.Slide SlideMeasureOutput;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblBarometerOutput;
        private System.Windows.Forms.Label lblPressureOutput;
        internal NationalInstruments.UI.WindowsForms.Slide SlideMeasureInput;
        private System.Windows.Forms.TextBox txtParameterName;
    }
}
