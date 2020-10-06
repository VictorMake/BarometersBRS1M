namespace BarometersBRS1M
{
    partial class FormComBarometers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComBarometers));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSerializeCollection = new System.Windows.Forms.Button();
            this.btnDeserializeCollection = new System.Windows.Forms.Button();
            this.ToolStripInstrument = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslSaveSetting = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslRunAllComPort = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslAddBarometer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tslDeleteBarometer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rtfTerminal = new System.Windows.Forms.RichTextBox();
            this.tlpBarometers = new System.Windows.Forms.TableLayoutPanel();
            this.ToolStripInstrument.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSerializeCollection
            // 
            this.btnSerializeCollection.Location = new System.Drawing.Point(432, 2);
            this.btnSerializeCollection.Name = "btnSerializeCollection";
            this.btnSerializeCollection.Size = new System.Drawing.Size(113, 23);
            this.btnSerializeCollection.TabIndex = 24;
            this.btnSerializeCollection.Text = "Сериализовать";
            this.toolTip1.SetToolTip(this.btnSerializeCollection, "Сериализовать коллекцию");
            this.btnSerializeCollection.UseVisualStyleBackColor = true;
            this.btnSerializeCollection.Visible = false;
            this.btnSerializeCollection.Click += new System.EventHandler(this.btnSerializeCollection_Click);
            // 
            // btnDeserializeCollection
            // 
            this.btnDeserializeCollection.Location = new System.Drawing.Point(551, 2);
            this.btnDeserializeCollection.Name = "btnDeserializeCollection";
            this.btnDeserializeCollection.Size = new System.Drawing.Size(113, 23);
            this.btnDeserializeCollection.TabIndex = 25;
            this.btnDeserializeCollection.Text = "Десериализовать";
            this.toolTip1.SetToolTip(this.btnDeserializeCollection, "Десериализовать коллекцию");
            this.btnDeserializeCollection.UseVisualStyleBackColor = true;
            this.btnDeserializeCollection.Visible = false;
            this.btnDeserializeCollection.Click += new System.EventHandler(this.btnDeserializeCollection_Click);
            // 
            // ToolStripInstrument
            // 
            this.ToolStripInstrument.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStripInstrument.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.tslSaveSetting,
            this.toolStripSeparator2,
            this.tslRunAllComPort,
            this.toolStripSeparator3,
            this.tslAddBarometer,
            this.toolStripSeparator4,
            this.tslDeleteBarometer,
            this.toolStripSeparator5});
            this.ToolStripInstrument.Location = new System.Drawing.Point(0, 0);
            this.ToolStripInstrument.Name = "ToolStripInstrument";
            this.ToolStripInstrument.Size = new System.Drawing.Size(858, 25);
            this.ToolStripInstrument.TabIndex = 20;
            this.ToolStripInstrument.Text = "ToolStrip6";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslSaveSetting
            // 
            this.tslSaveSetting.IsLink = true;
            this.tslSaveSetting.Name = "tslSaveSetting";
            this.tslSaveSetting.Size = new System.Drawing.Size(65, 22);
            this.tslSaveSetting.Text = "Сохранить";
            this.tslSaveSetting.ToolTipText = "Сохранить настройки";
            this.tslSaveSetting.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslRunAllComPort
            // 
            this.tslRunAllComPort.IsLink = true;
            this.tslRunAllComPort.Name = "tslRunAllComPort";
            this.tslRunAllComPort.Size = new System.Drawing.Size(101, 22);
            this.tslRunAllComPort.Text = "Запустить Опрос";
            this.tslRunAllComPort.ToolTipText = "Запустить опрос всех Com Port";
            this.tslRunAllComPort.Click += new System.EventHandler(this.tslRunAllComPort_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tslAddBarometer
            // 
            this.tslAddBarometer.Enabled = false;
            this.tslAddBarometer.IsLink = true;
            this.tslAddBarometer.Name = "tslAddBarometer";
            this.tslAddBarometer.Size = new System.Drawing.Size(121, 22);
            this.tslAddBarometer.Text = "[+] Новый Барометр";
            this.tslAddBarometer.ToolTipText = "Добавить новый барометр";
            this.tslAddBarometer.Click += new System.EventHandler(this.tslAddAddBarometer_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tslDeleteBarometer
            // 
            this.tslDeleteBarometer.Enabled = false;
            this.tslDeleteBarometer.IsLink = true;
            this.tslDeleteBarometer.Name = "tslDeleteBarometer";
            this.tslDeleteBarometer.Size = new System.Drawing.Size(141, 22);
            this.tslDeleteBarometer.Text = "[-] Удалить Выделенный";
            this.tslDeleteBarometer.ToolTipText = "Удалить выделенный барометр";
            this.tslDeleteBarometer.Click += new System.EventHandler(this.tslDeleteBarometer_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.rtfTerminal, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tlpBarometers, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(834, 446);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // rtfTerminal
            // 
            this.rtfTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfTerminal.Location = new System.Drawing.Point(3, 3);
            this.rtfTerminal.Name = "rtfTerminal";
            this.rtfTerminal.Size = new System.Drawing.Size(828, 74);
            this.rtfTerminal.TabIndex = 1;
            this.rtfTerminal.Text = "";
            // 
            // tlpBarometers
            // 
            this.tlpBarometers.AutoScroll = true;
            this.tlpBarometers.BackColor = System.Drawing.Color.Gray;
            this.tlpBarometers.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tlpBarometers.ColumnCount = 1;
            this.tlpBarometers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBarometers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBarometers.Location = new System.Drawing.Point(0, 80);
            this.tlpBarometers.Margin = new System.Windows.Forms.Padding(0);
            this.tlpBarometers.Name = "tlpBarometers";
            this.tlpBarometers.RowCount = 1;
            this.tlpBarometers.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpBarometers.Size = new System.Drawing.Size(834, 366);
            this.tlpBarometers.TabIndex = 21;
            this.tlpBarometers.Tag = "";
            // 
            // FormComBarometers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 494);
            this.Controls.Add(this.btnSerializeCollection);
            this.Controls.Add(this.btnDeserializeCollection);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ToolStripInstrument);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "FormComBarometers";
            this.Text = "Подключение барометров БРС-1М";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormComBarometers_FormClosing);
            this.Load += new System.EventHandler(this.FormComBarometers_Load);
            this.ToolStripInstrument.ResumeLayout(false);
            this.ToolStripInstrument.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.ToolStrip ToolStripInstrument;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslAddBarometer;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tslDeleteBarometer;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel tslSaveSetting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox rtfTerminal;
        internal System.Windows.Forms.TableLayoutPanel tlpBarometers;
        internal System.Windows.Forms.Button btnSerializeCollection;
        internal System.Windows.Forms.Button btnDeserializeCollection;
        private System.Windows.Forms.ToolStripLabel tslRunAllComPort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}