namespace Client_WindowsForms
{
    partial class FormLogin
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.LabelSubsidiary = new System.Windows.Forms.Label();
            this.comboBoxSubsidiary = new System.Windows.Forms.ComboBox();
            this.labelQuarter = new System.Windows.Forms.Label();
            this.comboBoxQuarter = new System.Windows.Forms.ComboBox();
            this.buttonDownloadAnnualReport = new System.Windows.Forms.Button();
            this.buttonSendQuarterlyReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelSubsidiary
            // 
            this.LabelSubsidiary.AutoSize = true;
            this.LabelSubsidiary.Location = new System.Drawing.Point(12, 12);
            this.LabelSubsidiary.Name = "LabelSubsidiary";
            this.LabelSubsidiary.Size = new System.Drawing.Size(182, 18);
            this.LabelSubsidiary.TabIndex = 0;
            this.LabelSubsidiary.Text = "Наименование филиала:";
            // 
            // comboBoxSubsidiary
            // 
            this.comboBoxSubsidiary.FormattingEnabled = true;
            this.comboBoxSubsidiary.Location = new System.Drawing.Point(200, 9);
            this.comboBoxSubsidiary.Name = "comboBoxSubsidiary";
            this.comboBoxSubsidiary.Size = new System.Drawing.Size(596, 26);
            this.comboBoxSubsidiary.TabIndex = 1;
            // 
            // labelQuarter
            // 
            this.labelQuarter.AutoSize = true;
            this.labelQuarter.Location = new System.Drawing.Point(12, 46);
            this.labelQuarter.Name = "labelQuarter";
            this.labelQuarter.Size = new System.Drawing.Size(70, 18);
            this.labelQuarter.TabIndex = 2;
            this.labelQuarter.Text = "Квартал:";
            // 
            // comboBoxQuarter
            // 
            this.comboBoxQuarter.FormattingEnabled = true;
            this.comboBoxQuarter.Location = new System.Drawing.Point(88, 43);
            this.comboBoxQuarter.Name = "comboBoxQuarter";
            this.comboBoxQuarter.Size = new System.Drawing.Size(106, 26);
            this.comboBoxQuarter.TabIndex = 3;
            // 
            // buttonDownloadAnnualReport
            // 
            this.buttonDownloadAnnualReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDownloadAnnualReport.Location = new System.Drawing.Point(501, 43);
            this.buttonDownloadAnnualReport.Name = "buttonDownloadAnnualReport";
            this.buttonDownloadAnnualReport.Size = new System.Drawing.Size(295, 26);
            this.buttonDownloadAnnualReport.TabIndex = 8;
            this.buttonDownloadAnnualReport.Text = "Запросить годовой отчет";
            this.buttonDownloadAnnualReport.UseVisualStyleBackColor = true;
            // 
            // buttonSendQuarterlyReport
            // 
            this.buttonSendQuarterlyReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSendQuarterlyReport.Location = new System.Drawing.Point(200, 43);
            this.buttonSendQuarterlyReport.Name = "buttonSendQuarterlyReport";
            this.buttonSendQuarterlyReport.Size = new System.Drawing.Size(295, 26);
            this.buttonSendQuarterlyReport.TabIndex = 9;
            this.buttonSendQuarterlyReport.Text = "Отправить данные за квартал";
            this.buttonSendQuarterlyReport.UseVisualStyleBackColor = true;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 81);
            this.Controls.Add(this.buttonSendQuarterlyReport);
            this.Controls.Add(this.buttonDownloadAnnualReport);
            this.Controls.Add(this.comboBoxQuarter);
            this.Controls.Add(this.labelQuarter);
            this.Controls.Add(this.comboBoxSubsidiary);
            this.Controls.Add(this.LabelSubsidiary);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лучший филиал";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelSubsidiary;
        private System.Windows.Forms.ComboBox comboBoxSubsidiary;
        private System.Windows.Forms.Label labelQuarter;
        private System.Windows.Forms.ComboBox comboBoxQuarter;
        private System.Windows.Forms.Button buttonDownloadAnnualReport;
        private System.Windows.Forms.Button buttonSendQuarterlyReport;
    }
}

