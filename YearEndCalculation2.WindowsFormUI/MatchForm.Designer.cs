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
            this.clmn1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMkysExit = new System.Windows.Forms.ListView();
            this.cclmn1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cclmn2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cclmn3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cclmn4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cclmn5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvTdmsEntry = new System.Windows.Forms.ListView();
            this.tclmn1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tclmn2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tclmn3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tclmn4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tclmn5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvTdmsExit = new System.Windows.Forms.ListView();
            this.tcclmn1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tcclmn2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tcclmn3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tcclmn4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tcclmn5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnMatch = new System.Windows.Forms.Button();
            this.flpMatched = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMkysEntry = new System.Windows.Forms.Label();
            this.lblMkysExit = new System.Windows.Forms.Label();
            this.lblTdmsEntry = new System.Windows.Forms.Label();
            this.lblTdmsExit = new System.Windows.Forms.Label();
            this.btnSuggest = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvMkysEntry
            // 
            this.lvMkysEntry.BackColor = System.Drawing.Color.Honeydew;
            this.lvMkysEntry.CheckBoxes = true;
            this.lvMkysEntry.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmn1,
            this.clmn2,
            this.clmn3,
            this.clmn4,
            this.clmn5});
            this.lvMkysEntry.FullRowSelect = true;
            this.lvMkysEntry.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMkysEntry.HideSelection = false;
            this.lvMkysEntry.Location = new System.Drawing.Point(12, 70);
            this.lvMkysEntry.Name = "lvMkysEntry";
            this.lvMkysEntry.ShowItemToolTips = true;
            this.lvMkysEntry.Size = new System.Drawing.Size(498, 216);
            this.lvMkysEntry.TabIndex = 2;
            this.lvMkysEntry.UseCompatibleStateImageBehavior = false;
            this.lvMkysEntry.View = System.Windows.Forms.View.Details;
            // 
            // clmn1
            // 
            this.clmn1.Text = "Belge No";
            // 
            // clmn2
            // 
            this.clmn2.Text = "Tarih";
            this.clmn2.Width = 65;
            // 
            // clmn3
            // 
            this.clmn3.Text = "Tür";
            this.clmn3.Width = 90;
            // 
            // clmn4
            // 
            this.clmn4.Text = "Açıklama";
            this.clmn4.Width = 200;
            // 
            // clmn5
            // 
            this.clmn5.Text = "Tutar";
            // 
            // lvMkysExit
            // 
            this.lvMkysExit.BackColor = System.Drawing.Color.Honeydew;
            this.lvMkysExit.CheckBoxes = true;
            this.lvMkysExit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cclmn1,
            this.cclmn2,
            this.cclmn3,
            this.cclmn4,
            this.cclmn5});
            this.lvMkysExit.FullRowSelect = true;
            this.lvMkysExit.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMkysExit.HideSelection = false;
            this.lvMkysExit.LabelWrap = false;
            this.lvMkysExit.Location = new System.Drawing.Point(518, 70);
            this.lvMkysExit.MultiSelect = false;
            this.lvMkysExit.Name = "lvMkysExit";
            this.lvMkysExit.ShowItemToolTips = true;
            this.lvMkysExit.Size = new System.Drawing.Size(498, 216);
            this.lvMkysExit.TabIndex = 2;
            this.lvMkysExit.UseCompatibleStateImageBehavior = false;
            this.lvMkysExit.View = System.Windows.Forms.View.Details;
            // 
            // cclmn1
            // 
            this.cclmn1.Text = "Belge No";
            // 
            // cclmn2
            // 
            this.cclmn2.Text = "Tarih";
            this.cclmn2.Width = 65;
            // 
            // cclmn3
            // 
            this.cclmn3.Text = "Tür";
            this.cclmn3.Width = 90;
            // 
            // cclmn4
            // 
            this.cclmn4.Text = "Açıklama";
            this.cclmn4.Width = 200;
            // 
            // cclmn5
            // 
            this.cclmn5.Text = "Tutar";
            // 
            // lvTdmsEntry
            // 
            this.lvTdmsEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(226)))));
            this.lvTdmsEntry.CheckBoxes = true;
            this.lvTdmsEntry.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tclmn1,
            this.tclmn2,
            this.tclmn3,
            this.tclmn4,
            this.tclmn5});
            this.lvTdmsEntry.FullRowSelect = true;
            this.lvTdmsEntry.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTdmsEntry.HideSelection = false;
            this.lvTdmsEntry.Location = new System.Drawing.Point(12, 323);
            this.lvTdmsEntry.MultiSelect = false;
            this.lvTdmsEntry.Name = "lvTdmsEntry";
            this.lvTdmsEntry.ShowItemToolTips = true;
            this.lvTdmsEntry.Size = new System.Drawing.Size(498, 216);
            this.lvTdmsEntry.TabIndex = 2;
            this.lvTdmsEntry.UseCompatibleStateImageBehavior = false;
            this.lvTdmsEntry.View = System.Windows.Forms.View.Details;
            // 
            // tclmn1
            // 
            this.tclmn1.Text = "Fiş No";
            // 
            // tclmn2
            // 
            this.tclmn2.Text = "Tarih";
            this.tclmn2.Width = 65;
            // 
            // tclmn3
            // 
            this.tclmn3.Text = "Tür";
            this.tclmn3.Width = 90;
            // 
            // tclmn4
            // 
            this.tclmn4.Text = "Açıklama";
            this.tclmn4.Width = 200;
            // 
            // tclmn5
            // 
            this.tclmn5.Text = "Tutar";
            // 
            // lvTdmsExit
            // 
            this.lvTdmsExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(226)))));
            this.lvTdmsExit.CheckBoxes = true;
            this.lvTdmsExit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tcclmn1,
            this.tcclmn2,
            this.tcclmn3,
            this.tcclmn4,
            this.tcclmn5});
            this.lvTdmsExit.FullRowSelect = true;
            this.lvTdmsExit.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTdmsExit.HideSelection = false;
            this.lvTdmsExit.Location = new System.Drawing.Point(518, 323);
            this.lvTdmsExit.MultiSelect = false;
            this.lvTdmsExit.Name = "lvTdmsExit";
            this.lvTdmsExit.ShowItemToolTips = true;
            this.lvTdmsExit.Size = new System.Drawing.Size(498, 216);
            this.lvTdmsExit.TabIndex = 2;
            this.lvTdmsExit.UseCompatibleStateImageBehavior = false;
            this.lvTdmsExit.View = System.Windows.Forms.View.Details;
            // 
            // tcclmn1
            // 
            this.tcclmn1.Text = "Fiş No";
            // 
            // tcclmn2
            // 
            this.tcclmn2.Text = "Tarih";
            this.tcclmn2.Width = 65;
            // 
            // tcclmn3
            // 
            this.tcclmn3.Text = "Tür";
            this.tcclmn3.Width = 90;
            // 
            // tcclmn4
            // 
            this.tcclmn4.Text = "Açıklama";
            this.tcclmn4.Width = 200;
            // 
            // tcclmn5
            // 
            this.tcclmn5.Text = "Tutar";
            // 
            // btnMatch
            // 
            this.btnMatch.BackColor = System.Drawing.Color.Bisque;
            this.btnMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMatch.Location = new System.Drawing.Point(394, 558);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(202, 52);
            this.btnMatch.TabIndex = 3;
            this.btnMatch.Text = "EŞLEŞTİR ";
            this.btnMatch.UseVisualStyleBackColor = false;
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // flpMatched
            // 
            this.flpMatched.AutoScroll = true;
            this.flpMatched.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpMatched.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMatched.Location = new System.Drawing.Point(1022, 70);
            this.flpMatched.Name = "flpMatched";
            this.flpMatched.Size = new System.Drawing.Size(302, 469);
            this.flpMatched.TabIndex = 4;
            this.flpMatched.WrapContents = false;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Bisque;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRemove.Location = new System.Drawing.Point(1055, 558);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(111, 29);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Seçilenleri Sil";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(1086, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Kaydedilmiş Eşleştirmeler";
            // 
            // lblMkysEntry
            // 
            this.lblMkysEntry.AutoSize = true;
            this.lblMkysEntry.Location = new System.Drawing.Point(218, 53);
            this.lblMkysEntry.Name = "lblMkysEntry";
            this.lblMkysEntry.Size = new System.Drawing.Size(69, 13);
            this.lblMkysEntry.TabIndex = 8;
            this.lblMkysEntry.Text = "MKYS GİRİŞ";
            // 
            // lblMkysExit
            // 
            this.lblMkysExit.AutoSize = true;
            this.lblMkysExit.Location = new System.Drawing.Point(735, 54);
            this.lblMkysExit.Name = "lblMkysExit";
            this.lblMkysExit.Size = new System.Drawing.Size(67, 13);
            this.lblMkysExit.TabIndex = 8;
            this.lblMkysExit.Text = "MKYS ÇIKIŞ";
            // 
            // lblTdmsEntry
            // 
            this.lblTdmsEntry.AutoSize = true;
            this.lblTdmsEntry.Location = new System.Drawing.Point(218, 307);
            this.lblTdmsEntry.Name = "lblTdmsEntry";
            this.lblTdmsEntry.Size = new System.Drawing.Size(70, 13);
            this.lblTdmsEntry.TabIndex = 8;
            this.lblTdmsEntry.Text = "TDMS GİRİŞ";
            // 
            // lblTdmsExit
            // 
            this.lblTdmsExit.AutoSize = true;
            this.lblTdmsExit.Location = new System.Drawing.Point(735, 307);
            this.lblTdmsExit.Name = "lblTdmsExit";
            this.lblTdmsExit.Size = new System.Drawing.Size(68, 13);
            this.lblTdmsExit.TabIndex = 8;
            this.lblTdmsExit.Text = "TDMS ÇIKIŞ";
            // 
            // btnSuggest
            // 
            this.btnSuggest.BackColor = System.Drawing.Color.Bisque;
            this.btnSuggest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuggest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSuggest.Location = new System.Drawing.Point(12, 558);
            this.btnSuggest.Name = "btnSuggest";
            this.btnSuggest.Size = new System.Drawing.Size(91, 29);
            this.btnSuggest.TabIndex = 9;
            this.btnSuggest.Text = "Öneri İste";
            this.btnSuggest.UseVisualStyleBackColor = false;
            this.btnSuggest.Visible = false;
            this.btnSuggest.Click += new System.EventHandler(this.btnSuggest_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.BackColor = System.Drawing.Color.Bisque;
            this.btnRemoveAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRemoveAll.Location = new System.Drawing.Point(1186, 558);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(111, 29);
            this.btnRemoveAll.TabIndex = 10;
            this.btnRemoveAll.Text = "Tümünü Sil";
            this.btnRemoveAll.UseVisualStyleBackColor = false;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // MatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(242)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(1364, 648);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnSuggest);
            this.Controls.Add(this.lblTdmsExit);
            this.Controls.Add(this.lblTdmsEntry);
            this.Controls.Add(this.lblMkysExit);
            this.Controls.Add(this.lblMkysEntry);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.flpMatched);
            this.Controls.Add(this.btnMatch);
            this.Controls.Add(this.lvTdmsExit);
            this.Controls.Add(this.lvTdmsEntry);
            this.Controls.Add(this.lvMkysExit);
            this.Controls.Add(this.lvMkysEntry);
            this.Name = "MatchForm";
            this.ShowIcon = false;
            this.Text = "Manuel Eşleştirme";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MatchForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lvMkysEntry;
        private System.Windows.Forms.ListView lvMkysExit;
        private System.Windows.Forms.ListView lvTdmsEntry;
        private System.Windows.Forms.ListView lvTdmsExit;
        private System.Windows.Forms.Button btnMatch;
        private System.Windows.Forms.FlowLayoutPanel flpMatched;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMkysEntry;
        private System.Windows.Forms.Label lblMkysExit;
        private System.Windows.Forms.Label lblTdmsEntry;
        private System.Windows.Forms.Label lblTdmsExit;
        private System.Windows.Forms.ColumnHeader clmn1;
        private System.Windows.Forms.ColumnHeader clmn2;
        private System.Windows.Forms.ColumnHeader clmn3;
        private System.Windows.Forms.ColumnHeader clmn4;
        private System.Windows.Forms.ColumnHeader clmn5;
        private System.Windows.Forms.ColumnHeader cclmn1;
        private System.Windows.Forms.ColumnHeader cclmn2;
        private System.Windows.Forms.ColumnHeader cclmn3;
        private System.Windows.Forms.ColumnHeader cclmn4;
        private System.Windows.Forms.ColumnHeader cclmn5;
        private System.Windows.Forms.ColumnHeader tclmn1;
        private System.Windows.Forms.ColumnHeader tclmn2;
        private System.Windows.Forms.ColumnHeader tclmn3;
        private System.Windows.Forms.ColumnHeader tclmn4;
        private System.Windows.Forms.ColumnHeader tclmn5;
        private System.Windows.Forms.ColumnHeader tcclmn1;
        private System.Windows.Forms.ColumnHeader tcclmn2;
        private System.Windows.Forms.ColumnHeader tcclmn3;
        private System.Windows.Forms.ColumnHeader tcclmn4;
        private System.Windows.Forms.ColumnHeader tcclmn5;
        private System.Windows.Forms.Button btnSuggest;
        private System.Windows.Forms.Button btnRemoveAll;
    }
}