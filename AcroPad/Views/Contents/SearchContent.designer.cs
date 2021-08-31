namespace AcroPad.Views.Contents
{
    partial class SearchContent
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
            this.panelDummy = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSearchNext = new System.Windows.Forms.Button();
            this.btnSearchPrev = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bmapGoRound = new System.Windows.Forms.CheckBox();
            this.bmapCaseSensitive = new System.Windows.Forms.CheckBox();
            this.bmapUseRegex = new System.Windows.Forms.CheckBox();
            this.cmdClean_1 = new System.Windows.Forms.Button();
            this.lblSearchTerm = new System.Windows.Forms.Label();
            this.cmbSearchTerm = new AcroBat.Views.Controls.ComboBoxEx();
            this.lblReplaceTerm = new System.Windows.Forms.Label();
            this.cmbReplaceTerm = new AcroBat.Views.Controls.ComboBoxEx();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdClean_2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.BasePanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // BasePanel
            // 
            this.BasePanel.Controls.Add(this.panel5);
            this.BasePanel.Controls.Add(this.panelDummy);
            this.BasePanel.Controls.Add(this.panel4);
            this.BasePanel.Controls.Add(this.panel3);
            this.BasePanel.Controls.Add(this.panel2);
            this.BasePanel.Controls.Add(this.panel1);
            this.BasePanel.Size = new System.Drawing.Size(234, 311);
            // 
            // panelDummy
            // 
            this.panelDummy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDummy.Location = new System.Drawing.Point(0, 275);
            this.panelDummy.Name = "panelDummy";
            this.panelDummy.Size = new System.Drawing.Size(234, 36);
            this.panelDummy.TabIndex = 27;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSearchNext);
            this.panel4.Controls.Add(this.btnSearchPrev);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 245);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(234, 30);
            this.panel4.TabIndex = 25;
            // 
            // btnSearchNext
            // 
            this.btnSearchNext.Location = new System.Drawing.Point(121, 5);
            this.btnSearchNext.Name = "btnSearchNext";
            this.btnSearchNext.Size = new System.Drawing.Size(100, 23);
            this.btnSearchNext.TabIndex = 2;
            this.btnSearchNext.Text = "次を検索(&N)";
            this.btnSearchNext.UseVisualStyleBackColor = true;
            // 
            // btnSearchPrev
            // 
            this.btnSearchPrev.Location = new System.Drawing.Point(14, 5);
            this.btnSearchPrev.Name = "btnSearchPrev";
            this.btnSearchPrev.Size = new System.Drawing.Size(100, 23);
            this.btnSearchPrev.TabIndex = 1;
            this.btnSearchPrev.Text = "前を検索(&P)";
            this.btnSearchPrev.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 115);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(234, 130);
            this.panel3.TabIndex = 24;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bmapGoRound);
            this.groupBox1.Controls.Add(this.bmapCaseSensitive);
            this.groupBox1.Controls.Add(this.bmapUseRegex);
            this.groupBox1.Location = new System.Drawing.Point(15, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 110);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索オプション(&O)";
            // 
            // bmapGoRound
            // 
            this.bmapGoRound.AutoSize = true;
            this.bmapGoRound.Location = new System.Drawing.Point(15, 75);
            this.bmapGoRound.Name = "bmapGoRound";
            this.bmapGoRound.Size = new System.Drawing.Size(172, 19);
            this.bmapGoRound.TabIndex = 3;
            this.bmapGoRound.Text = "先頭(末尾)から再検索する(&G)";
            this.bmapGoRound.UseVisualStyleBackColor = true;
            // 
            // bmapCaseSensitive
            // 
            this.bmapCaseSensitive.AutoSize = true;
            this.bmapCaseSensitive.Location = new System.Drawing.Point(15, 25);
            this.bmapCaseSensitive.Name = "bmapCaseSensitive";
            this.bmapCaseSensitive.Size = new System.Drawing.Size(174, 19);
            this.bmapCaseSensitive.TabIndex = 1;
            this.bmapCaseSensitive.Text = "大文字と小文字を区別する(&C)";
            this.bmapCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // bmapUseRegex
            // 
            this.bmapUseRegex.AutoSize = true;
            this.bmapUseRegex.Location = new System.Drawing.Point(15, 50);
            this.bmapUseRegex.Name = "bmapUseRegex";
            this.bmapUseRegex.Size = new System.Drawing.Size(141, 19);
            this.bmapUseRegex.TabIndex = 2;
            this.bmapUseRegex.Text = "正規表現を使用する(&R)";
            this.bmapUseRegex.UseVisualStyleBackColor = true;
            // 
            // cmdClean_1
            // 
            this.cmdClean_1.Location = new System.Drawing.Point(197, 29);
            this.cmdClean_1.Name = "cmdClean_1";
            this.cmdClean_1.Size = new System.Drawing.Size(23, 23);
            this.cmdClean_1.TabIndex = 3;
            this.cmdClean_1.UseVisualStyleBackColor = true;
            // 
            // lblSearchTerm
            // 
            this.lblSearchTerm.AutoSize = true;
            this.lblSearchTerm.Location = new System.Drawing.Point(10, 10);
            this.lblSearchTerm.Name = "lblSearchTerm";
            this.lblSearchTerm.Size = new System.Drawing.Size(100, 15);
            this.lblSearchTerm.TabIndex = 1;
            this.lblSearchTerm.Text = "検索する文字列(&S)";
            // 
            // cmbSearchTerm
            // 
            this.cmbSearchTerm.FormattingEnabled = true;
            this.cmbSearchTerm.Location = new System.Drawing.Point(15, 30);
            this.cmbSearchTerm.Name = "cmbSearchTerm";
            this.cmbSearchTerm.Size = new System.Drawing.Size(180, 23);
            this.cmbSearchTerm.TabIndex = 2;
            // 
            // lblReplaceTerm
            // 
            this.lblReplaceTerm.AutoSize = true;
            this.lblReplaceTerm.Location = new System.Drawing.Point(10, 5);
            this.lblReplaceTerm.Name = "lblReplaceTerm";
            this.lblReplaceTerm.Size = new System.Drawing.Size(103, 15);
            this.lblReplaceTerm.TabIndex = 1;
            this.lblReplaceTerm.Text = "置換後の文字列(&L)";
            // 
            // cmbReplaceTerm
            // 
            this.cmbReplaceTerm.FormattingEnabled = true;
            this.cmbReplaceTerm.Location = new System.Drawing.Point(15, 25);
            this.cmbReplaceTerm.Name = "cmbReplaceTerm";
            this.cmbReplaceTerm.Size = new System.Drawing.Size(180, 23);
            this.cmbReplaceTerm.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdClean_2);
            this.panel2.Controls.Add(this.lblReplaceTerm);
            this.panel2.Controls.Add(this.cmbReplaceTerm);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 55);
            this.panel2.TabIndex = 23;
            // 
            // cmdClean_2
            // 
            this.cmdClean_2.Location = new System.Drawing.Point(197, 24);
            this.cmdClean_2.Name = "cmdClean_2";
            this.cmdClean_2.Size = new System.Drawing.Size(23, 23);
            this.cmdClean_2.TabIndex = 3;
            this.cmdClean_2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdClean_1);
            this.panel1.Controls.Add(this.lblSearchTerm);
            this.panel1.Controls.Add(this.cmbSearchTerm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 60);
            this.panel1.TabIndex = 22;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnReplace);
            this.panel5.Controls.Add(this.btnReplaceAll);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 275);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(234, 30);
            this.panel5.TabIndex = 28;
            // 
            // btnReplace
            // 
            this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReplace.Location = new System.Drawing.Point(14, 5);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(100, 23);
            this.btnReplace.TabIndex = 1;
            this.btnReplace.Text = "置換(&R)";
            this.btnReplace.UseVisualStyleBackColor = true;
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReplaceAll.Location = new System.Drawing.Point(121, 5);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(100, 23);
            this.btnReplaceAll.TabIndex = 2;
            this.btnReplaceAll.Text = "すべて置換(&A)";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            // 
            // SearchContent
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(234, 311);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "SearchContent";
            this.TabText = "Search";
            this.Text = "SearchContent";
            this.BasePanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDummy;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSearchNext;
        private System.Windows.Forms.Button btnSearchPrev;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox bmapGoRound;
        private System.Windows.Forms.CheckBox bmapCaseSensitive;
        private System.Windows.Forms.CheckBox bmapUseRegex;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cmdClean_2;
        private System.Windows.Forms.Label lblReplaceTerm;
        private AcroBat.Views.Controls.ComboBoxEx cmbReplaceTerm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdClean_1;
        private System.Windows.Forms.Label lblSearchTerm;
        private AcroBat.Views.Controls.ComboBoxEx cmbSearchTerm;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnReplaceAll;
    }
}