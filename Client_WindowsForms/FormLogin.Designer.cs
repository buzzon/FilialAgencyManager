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
            this.buttonDownloadAnnualReport = new System.Windows.Forms.Button();
            this.buttonSendQuarterlyReport = new System.Windows.Forms.Button();
            this.buttonSubsidiaryAdd = new System.Windows.Forms.Button();
            this.groupBoxAdmin = new System.Windows.Forms.GroupBox();
            this.textBoxSubsidiaryAdd = new System.Windows.Forms.TextBox();
            this.groupBoxUser = new System.Windows.Forms.GroupBox();
            this.groupBoxAdmin.SuspendLayout();
            this.groupBoxUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelSubsidiary
            // 
            this.LabelSubsidiary.AutoSize = true;
            this.LabelSubsidiary.Location = new System.Drawing.Point(6, 25);
            this.LabelSubsidiary.Name = "LabelSubsidiary";
            this.LabelSubsidiary.Size = new System.Drawing.Size(182, 18);
            this.LabelSubsidiary.TabIndex = 0;
            this.LabelSubsidiary.Text = "Наименование филиала:";
            // 
            // comboBoxSubsidiary
            // 
            this.comboBoxSubsidiary.FormattingEnabled = true;
            this.comboBoxSubsidiary.Location = new System.Drawing.Point(188, 22);
            this.comboBoxSubsidiary.Name = "comboBoxSubsidiary";
            this.comboBoxSubsidiary.Size = new System.Drawing.Size(590, 26);
            this.comboBoxSubsidiary.TabIndex = 1;
            // 
            // buttonDownloadAnnualReport
            // 
            this.buttonDownloadAnnualReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDownloadAnnualReport.Location = new System.Drawing.Point(398, 59);
            this.buttonDownloadAnnualReport.Name = "buttonDownloadAnnualReport";
            this.buttonDownloadAnnualReport.Size = new System.Drawing.Size(380, 26);
            this.buttonDownloadAnnualReport.TabIndex = 8;
            this.buttonDownloadAnnualReport.Text = "Запросить годовой отчет";
            this.buttonDownloadAnnualReport.UseVisualStyleBackColor = true;
            // 
            // buttonSendQuarterlyReport
            // 
            this.buttonSendQuarterlyReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSendQuarterlyReport.Location = new System.Drawing.Point(9, 59);
            this.buttonSendQuarterlyReport.Name = "buttonSendQuarterlyReport";
            this.buttonSendQuarterlyReport.Size = new System.Drawing.Size(380, 26);
            this.buttonSendQuarterlyReport.TabIndex = 9;
            this.buttonSendQuarterlyReport.Text = "Отправить квартальный отчет";
            this.buttonSendQuarterlyReport.UseVisualStyleBackColor = true;
            this.buttonSendQuarterlyReport.Click += new System.EventHandler(this.buttonSendQuarterlyReport_Click);
            // 
            // buttonSubsidiaryAdd
            // 
            this.buttonSubsidiaryAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSubsidiaryAdd.Location = new System.Drawing.Point(6, 23);
            this.buttonSubsidiaryAdd.Name = "buttonSubsidiaryAdd";
            this.buttonSubsidiaryAdd.Size = new System.Drawing.Size(176, 26);
            this.buttonSubsidiaryAdd.TabIndex = 10;
            this.buttonSubsidiaryAdd.Text = "Добавить филиал";
            this.buttonSubsidiaryAdd.UseVisualStyleBackColor = true;
            this.buttonSubsidiaryAdd.Click += new System.EventHandler(this.buttonSubsidiaryAdd_Click);
            // 
            // groupBoxAdmin
            // 
            this.groupBoxAdmin.Controls.Add(this.textBoxSubsidiaryAdd);
            this.groupBoxAdmin.Controls.Add(this.buttonSubsidiaryAdd);
            this.groupBoxAdmin.Location = new System.Drawing.Point(12, 120);
            this.groupBoxAdmin.Name = "groupBoxAdmin";
            this.groupBoxAdmin.Size = new System.Drawing.Size(784, 262);
            this.groupBoxAdmin.TabIndex = 11;
            this.groupBoxAdmin.TabStop = false;
            this.groupBoxAdmin.Text = "Для администрации";
            // 
            // textBoxSubsidiaryAdd
            // 
            this.textBoxSubsidiaryAdd.Location = new System.Drawing.Point(199, 23);
            this.textBoxSubsidiaryAdd.Name = "textBoxSubsidiaryAdd";
            this.textBoxSubsidiaryAdd.Size = new System.Drawing.Size(579, 24);
            this.textBoxSubsidiaryAdd.TabIndex = 11;
            // 
            // groupBoxUser
            // 
            this.groupBoxUser.Controls.Add(this.LabelSubsidiary);
            this.groupBoxUser.Controls.Add(this.comboBoxSubsidiary);
            this.groupBoxUser.Controls.Add(this.buttonDownloadAnnualReport);
            this.groupBoxUser.Controls.Add(this.buttonSendQuarterlyReport);
            this.groupBoxUser.Location = new System.Drawing.Point(12, 12);
            this.groupBoxUser.Name = "groupBoxUser";
            this.groupBoxUser.Size = new System.Drawing.Size(784, 102);
            this.groupBoxUser.TabIndex = 12;
            this.groupBoxUser.TabStop = false;
            this.groupBoxUser.Text = "Для пользователей";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 394);
            this.Controls.Add(this.groupBoxUser);
            this.Controls.Add(this.groupBoxAdmin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лучший филиал";
            this.groupBoxAdmin.ResumeLayout(false);
            this.groupBoxAdmin.PerformLayout();
            this.groupBoxUser.ResumeLayout(false);
            this.groupBoxUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabelSubsidiary;
        private System.Windows.Forms.ComboBox comboBoxSubsidiary;
        private System.Windows.Forms.Button buttonDownloadAnnualReport;
        private System.Windows.Forms.Button buttonSendQuarterlyReport;
        private System.Windows.Forms.Button buttonSubsidiaryAdd;
        private System.Windows.Forms.GroupBox groupBoxAdmin;
        private System.Windows.Forms.TextBox textBoxSubsidiaryAdd;
        private System.Windows.Forms.GroupBox groupBoxUser;
    }
}

