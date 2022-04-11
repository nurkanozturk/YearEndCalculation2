
namespace YearEndCalculation.WindowsFormUI
{
    partial class LoginForm
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
            this.btnSerial = new System.Windows.Forms.Button();
            this.mtbxSerial = new System.Windows.Forms.MaskedTextBox();
            this.lblSerial = new System.Windows.Forms.Label();
            this.lblSerialInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSerial
            // 
            this.btnSerial.Location = new System.Drawing.Point(377, 110);
            this.btnSerial.Name = "btnSerial";
            this.btnSerial.Size = new System.Drawing.Size(75, 26);
            this.btnSerial.TabIndex = 1;
            this.btnSerial.Text = "Tamam";
            this.btnSerial.UseVisualStyleBackColor = true;
            this.btnSerial.Click += new System.EventHandler(this.btnSerial_Click);
            // 
            // mtbxSerial
            // 
            this.mtbxSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.mtbxSerial.Location = new System.Drawing.Point(143, 110);
            this.mtbxSerial.Mask = "aaaa-aaaa-aaaa-aaaa-aaaa";
            this.mtbxSerial.Name = "mtbxSerial";
            this.mtbxSerial.Size = new System.Drawing.Size(228, 26);
            this.mtbxSerial.TabIndex = 0;
            this.mtbxSerial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtbxSerial_KeyDown);
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSerial.Location = new System.Drawing.Point(27, 116);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(110, 17);
            this.lblSerial.TabIndex = 2;
            this.lblSerial.Text = "Lisans Anahtarı:";
            // 
            // lblSerialInfo
            // 
            this.lblSerialInfo.AutoSize = true;
            this.lblSerialInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSerialInfo.Location = new System.Drawing.Point(158, 47);
            this.lblSerialInfo.Name = "lblSerialInfo";
            this.lblSerialInfo.Size = new System.Drawing.Size(200, 17);
            this.lblSerialInfo.TabIndex = 3;
            this.lblSerialInfo.Text = "Lütfen lisans anahtarını giriniz.";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnSerial;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 194);
            this.Controls.Add(this.lblSerialInfo);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.mtbxSerial);
            this.Controls.Add(this.btnSerial);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lisans Ekranı";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSerial;
        private System.Windows.Forms.MaskedTextBox mtbxSerial;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.Label lblSerialInfo;
    }
}