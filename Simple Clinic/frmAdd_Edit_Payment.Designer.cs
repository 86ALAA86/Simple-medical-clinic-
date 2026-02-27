namespace Simple_Clinic
{
    partial class frmAdd_Edit_Payment
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
            System.Windows.Forms.Label label2;
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.cbAppointments = new System.Windows.Forms.ComboBox();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAddNotes = new Guna.UI2.WinForms.Guna2TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbMethod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAmount = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            label2.Location = new System.Drawing.Point(11, 71);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(331, 36);
            label2.TabIndex = 47;
            label2.Text = "Chose Appointment :";
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.Location = new System.Drawing.Point(292, 11);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(224, 41);
            this.lblFormTitle.TabIndex = 46;
            this.lblFormTitle.Text = "Add/Update";
            // 
            // cbAppointments
            // 
            this.cbAppointments.AllowDrop = true;
            this.cbAppointments.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.cbAppointments.FormattingEnabled = true;
            this.cbAppointments.Location = new System.Drawing.Point(93, 120);
            this.cbAppointments.Name = "cbAppointments";
            this.cbAppointments.Size = new System.Drawing.Size(586, 32);
            this.cbAppointments.TabIndex = 49;
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.CustomFormat = "YYYY-MM-DD";
            this.dtpPaymentDate.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPaymentDate.Location = new System.Drawing.Point(21, 223);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.ShowUpDown = true;
            this.dtpPaymentDate.Size = new System.Drawing.Size(316, 38);
            this.dtpPaymentDate.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(11, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(248, 36);
            this.label4.TabIndex = 51;
            this.label4.Text = "Payment Date :";
            // 
            // tbAddNotes
            // 
            this.tbAddNotes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.tbAddNotes.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.tbAddNotes.BorderThickness = 3;
            this.tbAddNotes.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbAddNotes.DefaultText = "";
            this.tbAddNotes.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbAddNotes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbAddNotes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbAddNotes.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbAddNotes.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbAddNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbAddNotes.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbAddNotes.Location = new System.Drawing.Point(256, 304);
            this.tbAddNotes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbAddNotes.Name = "tbAddNotes";
            this.tbAddNotes.PlaceholderText = "";
            this.tbAddNotes.SelectedText = "";
            this.tbAddNotes.Size = new System.Drawing.Size(516, 48);
            this.tbAddNotes.TabIndex = 54;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(11, 313);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(237, 30);
            this.label7.TabIndex = 53;
            this.label7.Text = "Additional Notes :";
            // 
            // cbMethod
            // 
            this.cbMethod.AllowDrop = true;
            this.cbMethod.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.cbMethod.FormattingEnabled = true;
            this.cbMethod.Location = new System.Drawing.Point(410, 226);
            this.cbMethod.Name = "cbMethod";
            this.cbMethod.Size = new System.Drawing.Size(316, 32);
            this.cbMethod.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(409, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 36);
            this.label1.TabIndex = 55;
            this.label1.Text = "Chose Method :";
            // 
            // tbAmount
            // 
            this.tbAmount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.tbAmount.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.tbAmount.BorderThickness = 3;
            this.tbAmount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbAmount.DefaultText = "";
            this.tbAmount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbAmount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbAmount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbAmount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbAmount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbAmount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbAmount.Location = new System.Drawing.Point(512, 388);
            this.tbAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.PlaceholderText = "";
            this.tbAmount.SelectedText = "";
            this.tbAmount.Size = new System.Drawing.Size(217, 48);
            this.tbAmount.TabIndex = 58;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(337, 397);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 30);
            this.label5.TabIndex = 57;
            this.label5.Text = "Amount :";
            // 
            // btnSave
            // 
            this.btnSave.BorderThickness = 3;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(45, 388);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 49);
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAdd_Edit_Payment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbAmount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbMethod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAddNotes);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpPaymentDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbAppointments);
            this.Controls.Add(label2);
            this.Controls.Add(this.lblFormTitle);
            this.Name = "frmAdd_Edit_Payment";
            this.Text = "frmAdd_Edit_Payment";
            this.Load += new System.EventHandler(this.frmAdd_Edit_Payment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.ComboBox cbAppointments;
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox tbAddNotes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbMethod;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox tbAmount;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Button btnSave;
    }
}