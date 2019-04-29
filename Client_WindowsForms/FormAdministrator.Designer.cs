namespace Client_WindowsForms
{
    partial class FormAdministrator
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
            this.groupBoxAdmin = new System.Windows.Forms.GroupBox();
            this.textBoxSubsidiaryAdd = new System.Windows.Forms.TextBox();
            this.buttonSubsidiaryAdd = new System.Windows.Forms.Button();
            this.groupBoxAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAdmin
            // 
            this.groupBoxAdmin.Controls.Add(this.textBoxSubsidiaryAdd);
            this.groupBoxAdmin.Controls.Add(this.buttonSubsidiaryAdd);
            this.groupBoxAdmin.Location = new System.Drawing.Point(13, 13);
            this.groupBoxAdmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxAdmin.Name = "groupBoxAdmin";
            this.groupBoxAdmin.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxAdmin.Size = new System.Drawing.Size(643, 371);
            this.groupBoxAdmin.TabIndex = 12;
            this.groupBoxAdmin.TabStop = false;
            this.groupBoxAdmin.Text = "Для администрации";
            // 
            // textBoxSubsidiaryAdd
            // 
            this.textBoxSubsidiaryAdd.Location = new System.Drawing.Point(192, 32);
            this.textBoxSubsidiaryAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSubsidiaryAdd.Name = "textBoxSubsidiaryAdd";
            this.textBoxSubsidiaryAdd.Size = new System.Drawing.Size(443, 24);
            this.textBoxSubsidiaryAdd.TabIndex = 11;
            // 
            // buttonSubsidiaryAdd
            // 
            this.buttonSubsidiaryAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSubsidiaryAdd.Location = new System.Drawing.Point(9, 32);
            this.buttonSubsidiaryAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSubsidiaryAdd.Name = "buttonSubsidiaryAdd";
            this.buttonSubsidiaryAdd.Size = new System.Drawing.Size(175, 24);
            this.buttonSubsidiaryAdd.TabIndex = 10;
            this.buttonSubsidiaryAdd.Text = "Добавить филиал";
            this.buttonSubsidiaryAdd.UseVisualStyleBackColor = true;
            // 
            // FormAdministrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 397);
            this.Controls.Add(this.groupBoxAdmin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormAdministrator";
            this.Text = "FormAdministrator";
            this.groupBoxAdmin.ResumeLayout(false);
            this.groupBoxAdmin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAdmin;
        private System.Windows.Forms.TextBox textBoxSubsidiaryAdd;
        private System.Windows.Forms.Button buttonSubsidiaryAdd;
    }
}