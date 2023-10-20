namespace YearEndCalculation.WindowsFormUI
{
    partial class ChooseForm
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
            this.lvMkys = new System.Windows.Forms.ListView();
            this.clmn1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOk = new System.Windows.Forms.Button();
            this.lvTdms = new System.Windows.Forms.ListView();
            this.tclmn1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tclmn2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tclmn3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tclmn4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tclmn5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnSkipAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvMkys
            // 
            this.lvMkys.CheckBoxes = true;
            this.lvMkys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmn1,
            this.clmn2,
            this.clmn3,
            this.clmn4,
            this.clmn5});
            this.lvMkys.FullRowSelect = true;
            this.lvMkys.GridLines = true;
            this.lvMkys.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMkys.HideSelection = false;
            this.lvMkys.Location = new System.Drawing.Point(15, 34);
            this.lvMkys.Name = "lvMkys";
            this.lvMkys.ShowItemToolTips = true;
            this.lvMkys.Size = new System.Drawing.Size(879, 160);
            this.lvMkys.TabIndex = 0;
            this.lvMkys.UseCompatibleStateImageBehavior = false;
            this.lvMkys.View = System.Windows.Forms.View.Details;
            this.lvMkys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMkys_KeyDown);
            // 
            // clmn1
            // 
            this.clmn1.Text = "Belge No";
            // 
            // clmn2
            // 
            this.clmn2.Text = "Tarih";
            // 
            // clmn3
            // 
            this.clmn3.Text = "Tür";
            this.clmn3.Width = 100;
            // 
            // clmn4
            // 
            this.clmn4.Text = "Açıklama";
            this.clmn4.Width = 550;
            // 
            // clmn5
            // 
            this.clmn5.Text = "Tutar";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(383, 401);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(145, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Eşleştir";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lvTdms
            // 
            this.lvTdms.CheckBoxes = true;
            this.lvTdms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tclmn1,
            this.tclmn2,
            this.tclmn3,
            this.tclmn4,
            this.tclmn5});
            this.lvTdms.FullRowSelect = true;
            this.lvTdms.GridLines = true;
            this.lvTdms.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTdms.HideSelection = false;
            this.lvTdms.Location = new System.Drawing.Point(15, 225);
            this.lvTdms.Name = "lvTdms";
            this.lvTdms.ShowItemToolTips = true;
            this.lvTdms.Size = new System.Drawing.Size(879, 160);
            this.lvTdms.TabIndex = 4;
            this.lvTdms.UseCompatibleStateImageBehavior = false;
            this.lvTdms.View = System.Windows.Forms.View.Details;
            this.lvTdms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvTdms_KeyDown);
            // 
            // tclmn1
            // 
            this.tclmn1.Text = "Belge No";
            // 
            // tclmn2
            // 
            this.tclmn2.Text = "Tarih";
            // 
            // tclmn3
            // 
            this.tclmn3.Text = "Tür";
            this.tclmn3.Width = 100;
            // 
            // tclmn4
            // 
            this.tclmn4.Text = "Açıklama";
            this.tclmn4.Width = 550;
            // 
            // tclmn5
            // 
            this.tclmn5.Text = "Tutar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "MKYS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "TDMS";
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(716, 401);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(75, 23);
            this.btnSkip.TabIndex = 7;
            this.btnSkip.Text = "Geç >>";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnSkipAll
            // 
            this.btnSkipAll.Location = new System.Drawing.Point(797, 401);
            this.btnSkipAll.Name = "btnSkipAll";
            this.btnSkipAll.Size = new System.Drawing.Size(97, 23);
            this.btnSkipAll.TabIndex = 8;
            this.btnSkipAll.Text = "Tümünü Geç >>";
            this.btnSkipAll.UseVisualStyleBackColor = true;
            this.btnSkipAll.Click += new System.EventHandler(this.btnSkipAll_Click);
            // 
            // ChooseForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 436);
            this.Controls.Add(this.btnSkipAll);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvTdms);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lvMkys);
            this.Name = "ChooseForm";
            this.Text = "Benzer Tutara Sahip Kayıtları Eşleştirme Ekranı";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMkys;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListView lvTdms;
        private System.Windows.Forms.ColumnHeader clmn1;
        private System.Windows.Forms.ColumnHeader clmn2;
        private System.Windows.Forms.ColumnHeader clmn3;
        private System.Windows.Forms.ColumnHeader clmn4;
        private System.Windows.Forms.ColumnHeader clmn5;
        private System.Windows.Forms.ColumnHeader tclmn1;
        private System.Windows.Forms.ColumnHeader tclmn2;
        private System.Windows.Forms.ColumnHeader tclmn3;
        private System.Windows.Forms.ColumnHeader tclmn4;
        private System.Windows.Forms.ColumnHeader tclmn5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnSkipAll;
    }
}