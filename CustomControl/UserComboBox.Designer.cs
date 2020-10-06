namespace BarometersBRS1M
{
    partial class UserCombolBox
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
            this.ComboBoxControlsToDelete = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // ComboBoxControlsToDelete
            // 
            this.ComboBoxControlsToDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxControlsToDelete.FormattingEnabled = true;
            this.ComboBoxControlsToDelete.Location = new System.Drawing.Point(3, 3);
            this.ComboBoxControlsToDelete.Name = "ComboBoxControlsToDelete";
            this.ComboBoxControlsToDelete.Size = new System.Drawing.Size(123, 21);
            this.ComboBoxControlsToDelete.TabIndex = 0;
            this.toolTip1.SetToolTip(this.ComboBoxControlsToDelete, "Выбрать барометр для удаления");
            // 
            // UserCombolBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ComboBoxControlsToDelete);
            this.Name = "UserCombolBox";
            this.Size = new System.Drawing.Size(129, 29);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox ComboBoxControlsToDelete;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
