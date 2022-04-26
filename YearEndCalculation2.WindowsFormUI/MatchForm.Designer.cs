namespace YearEndCalculation.WindowsFormUI
{
    partial class MatchForm
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
            this.lvMkysEntry = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMkysExit = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvTdmsEntry = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvTdmsExit = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnMatch = new System.Windows.Forms.Button();
            this.flpMatched = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvMkysEntry
            // 
            this.lvMkysEntry.CheckBoxes = true;
            this.lvMkysEntry.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvMkysEntry.FullRowSelect = true;
            this.lvMkysEntry.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMkysEntry.HideSelection = false;
            this.lvMkysEntry.Location = new System.Drawing.Point(12, 43);
            this.lvMkysEntry.Name = "lvMkysEntry";
            this.lvMkysEntry.ShowItemToolTips = true;
            this.lvMkysEntry.Size = new System.Drawing.Size(240, 460);
            this.lvMkysEntry.TabIndex = 2;
            this.lvMkysEntry.UseCompatibleStateImageBehavior = false;
            this.lvMkysEntry.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "FİŞ NO";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "AÇIKLAMA";
            this.columnHeader2.Width = 181;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "TUTAR";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 63;
            // 
            // lvMkysExit
            // 
            this.lvMkysExit.CheckBoxes = true;
            this.lvMkysExit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvMkysExit.FullRowSelect = true;
            this.lvMkysExit.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMkysExit.HideSelection = false;
            this.lvMkysExit.LabelWrap = false;
            this.lvMkysExit.Location = new System.Drawing.Point(258, 43);
            this.lvMkysExit.MultiSelect = false;
            this.lvMkysExit.Name = "lvMkysExit";
            this.lvMkysExit.Size = new System.Drawing.Size(240, 460);
            this.lvMkysExit.TabIndex = 2;
            this.lvMkysExit.UseCompatibleStateImageBehavior = false;
            this.lvMkysExit.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "FİŞ NO";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "AÇIKLAMA";
            this.columnHeader5.Width = 174;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "TUTAR";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lvTdmsEntry
            // 
            this.lvTdmsEntry.CheckBoxes = true;
            this.lvTdmsEntry.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lvTdmsEntry.FullRowSelect = true;
            this.lvTdmsEntry.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTdmsEntry.HideSelection = false;
            this.lvTdmsEntry.Location = new System.Drawing.Point(504, 43);
            this.lvTdmsEntry.MultiSelect = false;
            this.lvTdmsEntry.Name = "lvTdmsEntry";
            this.lvTdmsEntry.Size = new System.Drawing.Size(240, 460);
            this.lvTdmsEntry.TabIndex = 2;
            this.lvTdmsEntry.UseCompatibleStateImageBehavior = false;
            this.lvTdmsEntry.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "FİŞ NO ";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "AÇIKLAMA";
            this.columnHeader8.Width = 174;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "TUTAR";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lvTdmsExit
            // 
            this.lvTdmsExit.CheckBoxes = true;
            this.lvTdmsExit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.lvTdmsExit.FullRowSelect = true;
            this.lvTdmsExit.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTdmsExit.HideSelection = false;
            this.lvTdmsExit.Location = new System.Drawing.Point(750, 43);
            this.lvTdmsExit.MultiSelect = false;
            this.lvTdmsExit.Name = "lvTdmsExit";
            this.lvTdmsExit.Size = new System.Drawing.Size(240, 460);
            this.lvTdmsExit.TabIndex = 2;
            this.lvTdmsExit.UseCompatibleStateImageBehavior = false;
            this.lvTdmsExit.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "FİŞ NO";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "AÇIKLAMA";
            this.columnHeader11.Width = 174;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "TUTAR";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnMatch
            // 
            this.btnMatch.BackColor = System.Drawing.Color.PeachPuff;
            this.btnMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMatch.Location = new System.Drawing.Point(366, 541);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(322, 69);
            this.btnMatch.TabIndex = 3;
            this.btnMatch.Text = "EŞLEŞTİR";
            this.btnMatch.UseVisualStyleBackColor = false;
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // flpMatched
            // 
            this.flpMatched.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpMatched.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMatched.Location = new System.Drawing.Point(1031, 43);
            this.flpMatched.Name = "flpMatched";
            this.flpMatched.Size = new System.Drawing.Size(302, 460);
            this.flpMatched.TabIndex = 4;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(1123, 541);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(120, 69);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "GERİ AL";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // MatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 749);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.flpMatched);
            this.Controls.Add(this.btnMatch);
            this.Controls.Add(this.lvTdmsExit);
            this.Controls.Add(this.lvTdmsEntry);
            this.Controls.Add(this.lvMkysExit);
            this.Controls.Add(this.lvMkysEntry);
            this.Name = "MatchForm";
            this.Text = "MatchForm";
            this.Load += new System.EventHandler(this.MatchForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvMkysEntry;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView lvMkysExit;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView lvTdmsEntry;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ListView lvTdmsExit;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Button btnMatch;
        private System.Windows.Forms.FlowLayoutPanel flpMatched;
        private System.Windows.Forms.Button btnRemove;
    }
}