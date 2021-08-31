namespace AcroPad.Views.Forms
{
    partial class OptionDialog
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
            this.grpView = new System.Windows.Forms.GroupBox();
            this.bmapHighlightsCurrentLine = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapShowsHRuler = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapShowsLineNumber = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapShowsDirtBar = new AcroBat.Views.Controls.CheckBoxEx();
            this.tpgBehavior = new System.Windows.Forms.TabPage();
            this.grpDefault = new System.Windows.Forms.GroupBox();
            this.lblWordWrapMode = new System.Windows.Forms.Label();
            this.emapWordWrapMode = new AcroBat.Views.Controls.ComboBoxEx();
            this.nmapWordWrapDigit = new AcroBat.Views.Controls.NumericUpDownEx();
            this.lblEolMode = new System.Windows.Forms.Label();
            this.emapEolMode = new AcroBat.Views.Controls.ComboBoxEx();
            this.lblEncodingMode = new System.Windows.Forms.Label();
            this.emapEncodingMode = new AcroBat.Views.Controls.ComboBoxEx();
            this.lblFileMode = new System.Windows.Forms.Label();
            this.emapLanguageMode = new AcroBat.Views.Controls.ComboBoxEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bmapScrollsBeyondLastLine = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapConvertsFullWidthSpaceToSpace = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapHighlightsMatchedBracket = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapConvertsTabToSpaces = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapUnindentsWithBackspace = new AcroBat.Views.Controls.CheckBoxEx();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblPreview = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgView = new System.Windows.Forms.TabPage();
            this.grpFont = new System.Windows.Forms.GroupBox();
            this.emapFontName = new AcroBat.Views.Controls.ComboBoxEx();
            this.nmapFontSize = new AcroBat.Views.Controls.NumericUpDownEx();
            this.grpColor = new System.Windows.Forms.GroupBox();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.lblForeColor = new System.Windows.Forms.Label();
            this.lstColor = new AcroBat.Views.Controls.ListBoxEx();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.btnForeColor = new System.Windows.Forms.Button();
            this.emapForeColor = new AcroBat.Views.Controls.ColorPicker();
            this.emapBackColor = new AcroBat.Views.Controls.ColorPicker();
            this.grpSymbol = new System.Windows.Forms.GroupBox();
            this.nmapTabWidth = new AcroBat.Views.Controls.NumericUpDownEx();
            this.lblTabWidth = new System.Windows.Forms.Label();
            this.bmapDrawsTab = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapDrawsFullWidthSpace = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapDrawsSpace = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapDrawsEofMark = new AcroBat.Views.Controls.CheckBoxEx();
            this.bmapDrawsEolCode = new AcroBat.Views.Controls.CheckBoxEx();
            this.textEditor = new AcroPad.Views.Controls.TextEditor();
            this.grpView.SuspendLayout();
            this.tpgBehavior.SuspendLayout();
            this.grpDefault.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmapWordWrapDigit)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgView.SuspendLayout();
            this.grpFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmapFontSize)).BeginInit();
            this.grpColor.SuspendLayout();
            this.grpSymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmapTabWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // grpView
            // 
            this.grpView.Controls.Add(this.bmapHighlightsCurrentLine);
            this.grpView.Controls.Add(this.bmapShowsHRuler);
            this.grpView.Controls.Add(this.bmapShowsLineNumber);
            this.grpView.Controls.Add(this.bmapShowsDirtBar);
            this.grpView.Location = new System.Drawing.Point(300, 10);
            this.grpView.Name = "grpView";
            this.grpView.Size = new System.Drawing.Size(135, 130);
            this.grpView.TabIndex = 3;
            this.grpView.TabStop = false;
            this.grpView.Text = "補助(&A)";
            // 
            // bmapHighlightsCurrentLine
            // 
            this.bmapHighlightsCurrentLine.AutoSize = true;
            this.bmapHighlightsCurrentLine.Location = new System.Drawing.Point(15, 100);
            this.bmapHighlightsCurrentLine.Name = "bmapHighlightsCurrentLine";
            this.bmapHighlightsCurrentLine.Size = new System.Drawing.Size(78, 19);
            this.bmapHighlightsCurrentLine.TabIndex = 4;
            this.bmapHighlightsCurrentLine.Text = "現在行(&U)";
            this.bmapHighlightsCurrentLine.UseVisualStyleBackColor = true;
            // 
            // bmapShowsHRuler
            // 
            this.bmapShowsHRuler.AutoSize = true;
            this.bmapShowsHRuler.Location = new System.Drawing.Point(15, 50);
            this.bmapShowsHRuler.Name = "bmapShowsHRuler";
            this.bmapShowsHRuler.Size = new System.Drawing.Size(101, 19);
            this.bmapShowsHRuler.TabIndex = 2;
            this.bmapShowsHRuler.Text = "水平ルーラー(&H)";
            this.bmapShowsHRuler.UseVisualStyleBackColor = true;
            // 
            // bmapShowsLineNumber
            // 
            this.bmapShowsLineNumber.AutoSize = true;
            this.bmapShowsLineNumber.Location = new System.Drawing.Point(15, 25);
            this.bmapShowsLineNumber.Name = "bmapShowsLineNumber";
            this.bmapShowsLineNumber.Size = new System.Drawing.Size(79, 19);
            this.bmapShowsLineNumber.TabIndex = 1;
            this.bmapShowsLineNumber.Text = "行番号(&N)";
            this.bmapShowsLineNumber.UseVisualStyleBackColor = true;
            // 
            // bmapShowsDirtBar
            // 
            this.bmapShowsDirtBar.AutoSize = true;
            this.bmapShowsDirtBar.Location = new System.Drawing.Point(15, 75);
            this.bmapShowsDirtBar.Name = "bmapShowsDirtBar";
            this.bmapShowsDirtBar.Size = new System.Drawing.Size(100, 19);
            this.bmapShowsDirtBar.TabIndex = 3;
            this.bmapShowsDirtBar.Text = "状態マーカー(&D)";
            this.bmapShowsDirtBar.UseVisualStyleBackColor = true;
            // 
            // tpgBehavior
            // 
            this.tpgBehavior.Controls.Add(this.grpDefault);
            this.tpgBehavior.Controls.Add(this.groupBox1);
            this.tpgBehavior.Location = new System.Drawing.Point(4, 24);
            this.tpgBehavior.Name = "tpgBehavior";
            this.tpgBehavior.Padding = new System.Windows.Forms.Padding(3);
            this.tpgBehavior.Size = new System.Drawing.Size(597, 207);
            this.tpgBehavior.TabIndex = 2;
            this.tpgBehavior.Text = "動作";
            this.tpgBehavior.UseVisualStyleBackColor = true;
            // 
            // grpDefault
            // 
            this.grpDefault.Controls.Add(this.lblWordWrapMode);
            this.grpDefault.Controls.Add(this.emapWordWrapMode);
            this.grpDefault.Controls.Add(this.nmapWordWrapDigit);
            this.grpDefault.Controls.Add(this.lblEolMode);
            this.grpDefault.Controls.Add(this.emapEolMode);
            this.grpDefault.Controls.Add(this.lblEncodingMode);
            this.grpDefault.Controls.Add(this.emapEncodingMode);
            this.grpDefault.Controls.Add(this.lblFileMode);
            this.grpDefault.Controls.Add(this.emapLanguageMode);
            this.grpDefault.Location = new System.Drawing.Point(15, 10);
            this.grpDefault.Name = "grpDefault";
            this.grpDefault.Size = new System.Drawing.Size(275, 155);
            this.grpDefault.TabIndex = 1;
            this.grpDefault.TabStop = false;
            this.grpDefault.Text = "デフォルト(&D)";
            // 
            // lblWordWrapMode
            // 
            this.lblWordWrapMode.AutoSize = true;
            this.lblWordWrapMode.Location = new System.Drawing.Point(15, 120);
            this.lblWordWrapMode.Name = "lblWordWrapMode";
            this.lblWordWrapMode.Size = new System.Drawing.Size(69, 15);
            this.lblWordWrapMode.TabIndex = 7;
            this.lblWordWrapMode.Text = "折り返し(&W):";
            // 
            // emapWordWrapMode
            // 
            this.emapWordWrapMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.emapWordWrapMode.Location = new System.Drawing.Point(110, 115);
            this.emapWordWrapMode.Name = "emapWordWrapMode";
            this.emapWordWrapMode.Size = new System.Drawing.Size(95, 23);
            this.emapWordWrapMode.TabIndex = 8;
            // 
            // nmapWordWrapDigit
            // 
            this.nmapWordWrapDigit.InValidColor = System.Drawing.Color.MistyRose;
            this.nmapWordWrapDigit.Location = new System.Drawing.Point(210, 115);
            this.nmapWordWrapDigit.Name = "nmapWordWrapDigit";
            this.nmapWordWrapDigit.Size = new System.Drawing.Size(50, 23);
            this.nmapWordWrapDigit.TabIndex = 9;
            this.nmapWordWrapDigit.ValidColor = System.Drawing.Color.White;
            // 
            // lblEolMode
            // 
            this.lblEolMode.AutoSize = true;
            this.lblEolMode.Location = new System.Drawing.Point(15, 90);
            this.lblEolMode.Name = "lblEolMode";
            this.lblEolMode.Size = new System.Drawing.Size(72, 15);
            this.lblEolMode.TabIndex = 5;
            this.lblEolMode.Text = "改行コード(&E):";
            // 
            // emapEolMode
            // 
            this.emapEolMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.emapEolMode.Location = new System.Drawing.Point(110, 85);
            this.emapEolMode.Name = "emapEolMode";
            this.emapEolMode.Size = new System.Drawing.Size(150, 23);
            this.emapEolMode.TabIndex = 6;
            // 
            // lblEncodingMode
            // 
            this.lblEncodingMode.AutoSize = true;
            this.lblEncodingMode.Location = new System.Drawing.Point(15, 60);
            this.lblEncodingMode.Name = "lblEncodingMode";
            this.lblEncodingMode.Size = new System.Drawing.Size(73, 15);
            this.lblEncodingMode.TabIndex = 3;
            this.lblEncodingMode.Text = "文字コード(&C):";
            // 
            // emapEncodingMode
            // 
            this.emapEncodingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.emapEncodingMode.Location = new System.Drawing.Point(110, 55);
            this.emapEncodingMode.Name = "emapEncodingMode";
            this.emapEncodingMode.Size = new System.Drawing.Size(150, 23);
            this.emapEncodingMode.TabIndex = 4;
            // 
            // lblFileMode
            // 
            this.lblFileMode.AutoSize = true;
            this.lblFileMode.Location = new System.Drawing.Point(15, 30);
            this.lblFileMode.Name = "lblFileMode";
            this.lblFileMode.Size = new System.Drawing.Size(73, 15);
            this.lblFileMode.TabIndex = 1;
            this.lblFileMode.Text = "言語モード(&L):";
            // 
            // emapLanguageMode
            // 
            this.emapLanguageMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.emapLanguageMode.Location = new System.Drawing.Point(110, 25);
            this.emapLanguageMode.Name = "emapLanguageMode";
            this.emapLanguageMode.Size = new System.Drawing.Size(150, 23);
            this.emapLanguageMode.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bmapScrollsBeyondLastLine);
            this.groupBox1.Controls.Add(this.bmapConvertsFullWidthSpaceToSpace);
            this.groupBox1.Controls.Add(this.bmapHighlightsMatchedBracket);
            this.groupBox1.Controls.Add(this.bmapConvertsTabToSpaces);
            this.groupBox1.Controls.Add(this.bmapUnindentsWithBackspace);
            this.groupBox1.Location = new System.Drawing.Point(300, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 155);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "その他(&O)";
            // 
            // bmapScrollsBeyondLastLine
            // 
            this.bmapScrollsBeyondLastLine.AutoSize = true;
            this.bmapScrollsBeyondLastLine.Location = new System.Drawing.Point(15, 25);
            this.bmapScrollsBeyondLastLine.Name = "bmapScrollsBeyondLastLine";
            this.bmapScrollsBeyondLastLine.Size = new System.Drawing.Size(178, 19);
            this.bmapScrollsBeyondLastLine.TabIndex = 1;
            this.bmapScrollsBeyondLastLine.Text = "最終行を越えてスクロールする(&L)";
            this.bmapScrollsBeyondLastLine.UseVisualStyleBackColor = true;
            // 
            // bmapConvertsFullWidthSpaceToSpace
            // 
            this.bmapConvertsFullWidthSpaceToSpace.AutoSize = true;
            this.bmapConvertsFullWidthSpaceToSpace.Location = new System.Drawing.Point(15, 100);
            this.bmapConvertsFullWidthSpaceToSpace.Name = "bmapConvertsFullWidthSpaceToSpace";
            this.bmapConvertsFullWidthSpaceToSpace.Size = new System.Drawing.Size(206, 19);
            this.bmapConvertsFullWidthSpaceToSpace.TabIndex = 4;
            this.bmapConvertsFullWidthSpaceToSpace.Text = "全角空白を半角空白に置き換える(&B)";
            this.bmapConvertsFullWidthSpaceToSpace.UseVisualStyleBackColor = true;
            // 
            // bmapHighlightsMatchedBracket
            // 
            this.bmapHighlightsMatchedBracket.AutoSize = true;
            this.bmapHighlightsMatchedBracket.Location = new System.Drawing.Point(15, 50);
            this.bmapHighlightsMatchedBracket.Name = "bmapHighlightsMatchedBracket";
            this.bmapHighlightsMatchedBracket.Size = new System.Drawing.Size(162, 19);
            this.bmapHighlightsMatchedBracket.TabIndex = 2;
            this.bmapHighlightsMatchedBracket.Text = "対応する括弧を強調する(&H)";
            this.bmapHighlightsMatchedBracket.UseVisualStyleBackColor = true;
            // 
            // bmapConvertsTabToSpaces
            // 
            this.bmapConvertsTabToSpaces.AutoSize = true;
            this.bmapConvertsTabToSpaces.Location = new System.Drawing.Point(15, 75);
            this.bmapConvertsTabToSpaces.Name = "bmapConvertsTabToSpaces";
            this.bmapConvertsTabToSpaces.Size = new System.Drawing.Size(175, 19);
            this.bmapConvertsTabToSpaces.TabIndex = 3;
            this.bmapConvertsTabToSpaces.Text = "タブを半角空白に置き換える(&S)";
            this.bmapConvertsTabToSpaces.UseVisualStyleBackColor = true;
            // 
            // bmapUnindentsWithBackspace
            // 
            this.bmapUnindentsWithBackspace.AutoSize = true;
            this.bmapUnindentsWithBackspace.Location = new System.Drawing.Point(15, 125);
            this.bmapUnindentsWithBackspace.Name = "bmapUnindentsWithBackspace";
            this.bmapUnindentsWithBackspace.Size = new System.Drawing.Size(190, 19);
            this.bmapUnindentsWithBackspace.TabIndex = 5;
            this.bmapUnindentsWithBackspace.Text = "バックスペースで逆インデントする(&U)";
            this.bmapUnindentsWithBackspace.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(14, 424);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.TabStop = false;
            this.btnReset.Text = "既定値に戻す(&R)";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(539, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(453, 424);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Location = new System.Drawing.Point(15, 259);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(63, 15);
            this.lblPreview.TabIndex = 2;
            this.lblPreview.Text = "プレビュー(&P)";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgView);
            this.tabControl1.Controls.Add(this.tpgBehavior);
            this.tabControl1.Location = new System.Drawing.Point(15, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(605, 235);
            this.tabControl1.TabIndex = 1;
            // 
            // tpgView
            // 
            this.tpgView.Controls.Add(this.grpFont);
            this.tpgView.Controls.Add(this.grpColor);
            this.tpgView.Controls.Add(this.grpSymbol);
            this.tpgView.Controls.Add(this.grpView);
            this.tpgView.Location = new System.Drawing.Point(4, 24);
            this.tpgView.Name = "tpgView";
            this.tpgView.Padding = new System.Windows.Forms.Padding(3);
            this.tpgView.Size = new System.Drawing.Size(597, 207);
            this.tpgView.TabIndex = 0;
            this.tpgView.Text = "表示";
            this.tpgView.UseVisualStyleBackColor = true;
            // 
            // grpFont
            // 
            this.grpFont.Controls.Add(this.emapFontName);
            this.grpFont.Controls.Add(this.nmapFontSize);
            this.grpFont.Location = new System.Drawing.Point(15, 10);
            this.grpFont.Name = "grpFont";
            this.grpFont.Size = new System.Drawing.Size(275, 55);
            this.grpFont.TabIndex = 1;
            this.grpFont.TabStop = false;
            this.grpFont.Text = "フォント(&F)";
            // 
            // emapFontName
            // 
            this.emapFontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.emapFontName.Location = new System.Drawing.Point(15, 20);
            this.emapFontName.Name = "emapFontName";
            this.emapFontName.Size = new System.Drawing.Size(200, 23);
            this.emapFontName.TabIndex = 1;
            // 
            // nmapFontSize
            // 
            this.nmapFontSize.InValidColor = System.Drawing.Color.MistyRose;
            this.nmapFontSize.Location = new System.Drawing.Point(220, 20);
            this.nmapFontSize.Name = "nmapFontSize";
            this.nmapFontSize.Size = new System.Drawing.Size(40, 23);
            this.nmapFontSize.TabIndex = 2;
            this.nmapFontSize.ValidColor = System.Drawing.Color.White;
            // 
            // grpColor
            // 
            this.grpColor.Controls.Add(this.lblBackColor);
            this.grpColor.Controls.Add(this.lblForeColor);
            this.grpColor.Controls.Add(this.lstColor);
            this.grpColor.Controls.Add(this.btnBackColor);
            this.grpColor.Controls.Add(this.btnForeColor);
            this.grpColor.Controls.Add(this.emapForeColor);
            this.grpColor.Controls.Add(this.emapBackColor);
            this.grpColor.Location = new System.Drawing.Point(15, 72);
            this.grpColor.Name = "grpColor";
            this.grpColor.Size = new System.Drawing.Size(275, 130);
            this.grpColor.TabIndex = 2;
            this.grpColor.TabStop = false;
            this.grpColor.Text = "カラー(&C)";
            // 
            // lblBackColor
            // 
            this.lblBackColor.AutoSize = true;
            this.lblBackColor.Location = new System.Drawing.Point(100, 70);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(58, 15);
            this.lblBackColor.TabIndex = 5;
            this.lblBackColor.Text = "背景色(&J):";
            // 
            // lblForeColor
            // 
            this.lblForeColor.AutoSize = true;
            this.lblForeColor.Location = new System.Drawing.Point(100, 20);
            this.lblForeColor.Name = "lblForeColor";
            this.lblForeColor.Size = new System.Drawing.Size(57, 15);
            this.lblForeColor.TabIndex = 2;
            this.lblForeColor.Text = "前景色(&I):";
            // 
            // lstColor
            // 
            this.lstColor.FormattingEnabled = true;
            this.lstColor.ItemHeight = 15;
            this.lstColor.Location = new System.Drawing.Point(15, 22);
            this.lstColor.Name = "lstColor";
            this.lstColor.Size = new System.Drawing.Size(80, 94);
            this.lstColor.TabIndex = 1;
            // 
            // btnBackColor
            // 
            this.btnBackColor.Location = new System.Drawing.Point(230, 90);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(30, 23);
            this.btnBackColor.TabIndex = 7;
            this.btnBackColor.Text = "...";
            this.btnBackColor.UseVisualStyleBackColor = true;
            // 
            // btnForeColor
            // 
            this.btnForeColor.Location = new System.Drawing.Point(230, 40);
            this.btnForeColor.Name = "btnForeColor";
            this.btnForeColor.Size = new System.Drawing.Size(30, 23);
            this.btnForeColor.TabIndex = 4;
            this.btnForeColor.Text = "...";
            this.btnForeColor.UseVisualStyleBackColor = true;
            // 
            // emapForeColor
            // 
            this.emapForeColor.FormattingEnabled = true;
            this.emapForeColor.Location = new System.Drawing.Point(105, 40);
            this.emapForeColor.Name = "emapForeColor";
            this.emapForeColor.Size = new System.Drawing.Size(120, 24);
            this.emapForeColor.TabIndex = 3;
            // 
            // emapBackColor
            // 
            this.emapBackColor.FormattingEnabled = true;
            this.emapBackColor.Location = new System.Drawing.Point(105, 90);
            this.emapBackColor.Name = "emapBackColor";
            this.emapBackColor.Size = new System.Drawing.Size(120, 24);
            this.emapBackColor.TabIndex = 6;
            // 
            // grpSymbol
            // 
            this.grpSymbol.Controls.Add(this.nmapTabWidth);
            this.grpSymbol.Controls.Add(this.lblTabWidth);
            this.grpSymbol.Controls.Add(this.bmapDrawsTab);
            this.grpSymbol.Controls.Add(this.bmapDrawsFullWidthSpace);
            this.grpSymbol.Controls.Add(this.bmapDrawsSpace);
            this.grpSymbol.Controls.Add(this.bmapDrawsEofMark);
            this.grpSymbol.Controls.Add(this.bmapDrawsEolCode);
            this.grpSymbol.Location = new System.Drawing.Point(445, 10);
            this.grpSymbol.Name = "grpSymbol";
            this.grpSymbol.Size = new System.Drawing.Size(135, 185);
            this.grpSymbol.TabIndex = 4;
            this.grpSymbol.TabStop = false;
            this.grpSymbol.Text = "記号(&M)";
            // 
            // nmapTabWidth
            // 
            this.nmapTabWidth.InValidColor = System.Drawing.Color.MistyRose;
            this.nmapTabWidth.Location = new System.Drawing.Point(80, 148);
            this.nmapTabWidth.Name = "nmapTabWidth";
            this.nmapTabWidth.Size = new System.Drawing.Size(40, 23);
            this.nmapTabWidth.TabIndex = 7;
            this.nmapTabWidth.ValidColor = System.Drawing.Color.White;
            // 
            // lblTabWidth
            // 
            this.lblTabWidth.AutoSize = true;
            this.lblTabWidth.Location = new System.Drawing.Point(30, 150);
            this.lblTabWidth.Name = "lblTabWidth";
            this.lblTabWidth.Size = new System.Drawing.Size(41, 15);
            this.lblTabWidth.TabIndex = 6;
            this.lblTabWidth.Text = "桁(&W):";
            // 
            // bmapDrawsTab
            // 
            this.bmapDrawsTab.AutoSize = true;
            this.bmapDrawsTab.Location = new System.Drawing.Point(15, 125);
            this.bmapDrawsTab.Name = "bmapDrawsTab";
            this.bmapDrawsTab.Size = new System.Drawing.Size(58, 19);
            this.bmapDrawsTab.TabIndex = 5;
            this.bmapDrawsTab.Text = "タブ(&T)";
            this.bmapDrawsTab.UseVisualStyleBackColor = true;
            // 
            // bmapDrawsFullWidthSpace
            // 
            this.bmapDrawsFullWidthSpace.AutoSize = true;
            this.bmapDrawsFullWidthSpace.Location = new System.Drawing.Point(15, 100);
            this.bmapDrawsFullWidthSpace.Name = "bmapDrawsFullWidthSpace";
            this.bmapDrawsFullWidthSpace.Size = new System.Drawing.Size(89, 19);
            this.bmapDrawsFullWidthSpace.TabIndex = 4;
            this.bmapDrawsFullWidthSpace.Text = "全角空白(&B)";
            this.bmapDrawsFullWidthSpace.UseVisualStyleBackColor = true;
            // 
            // bmapDrawsSpace
            // 
            this.bmapDrawsSpace.AutoSize = true;
            this.bmapDrawsSpace.Location = new System.Drawing.Point(15, 75);
            this.bmapDrawsSpace.Name = "bmapDrawsSpace";
            this.bmapDrawsSpace.Size = new System.Drawing.Size(88, 19);
            this.bmapDrawsSpace.TabIndex = 3;
            this.bmapDrawsSpace.Text = "半角空白(&S)";
            this.bmapDrawsSpace.UseVisualStyleBackColor = true;
            // 
            // bmapDrawsEofMark
            // 
            this.bmapDrawsEofMark.AutoSize = true;
            this.bmapDrawsEofMark.Location = new System.Drawing.Point(15, 50);
            this.bmapDrawsEofMark.Name = "bmapDrawsEofMark";
            this.bmapDrawsEofMark.Size = new System.Drawing.Size(64, 19);
            this.bmapDrawsEofMark.TabIndex = 2;
            this.bmapDrawsEofMark.Text = "末尾(&E)";
            this.bmapDrawsEofMark.UseVisualStyleBackColor = true;
            // 
            // bmapDrawsEolCode
            // 
            this.bmapDrawsEolCode.AutoSize = true;
            this.bmapDrawsEolCode.Location = new System.Drawing.Point(15, 25);
            this.bmapDrawsEolCode.Name = "bmapDrawsEolCode";
            this.bmapDrawsEolCode.Size = new System.Drawing.Size(64, 19);
            this.bmapDrawsEolCode.TabIndex = 1;
            this.bmapDrawsEolCode.Text = "改行(&L)";
            this.bmapDrawsEolCode.UseVisualStyleBackColor = true;
            // 
            // textEditor
            // 
            this.textEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(240)))));
            this.textEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textEditor.CleanedLineBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(239)))), ((int)(((byte)(175)))));
            this.textEditor.CommentCharColor = System.Drawing.Color.Green;
            this.textEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textEditor.DirtyLineBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(241)))), ((int)(((byte)(15)))));
            this.textEditor.DocCommentCharColor = System.Drawing.Color.Gray;
            this.textEditor.EofColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(169)))), ((int)(((byte)(214)))));
            this.textEditor.EolColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(169)))), ((int)(((byte)(214)))));
            this.textEditor.FirstVisibleLine = 0;
            this.textEditor.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.textEditor.ForeColor = System.Drawing.Color.Black;
            this.textEditor.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(98)))), ((int)(((byte)(87)))));
            this.textEditor.Keyword2CharColor = System.Drawing.Color.Maroon;
            this.textEditor.Keyword3CharColor = System.Drawing.Color.Navy;
            this.textEditor.KeywordCharColor = System.Drawing.Color.Blue;
            this.textEditor.LineNumberBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.textEditor.LineNumberForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.textEditor.Location = new System.Drawing.Point(15, 279);
            this.textEditor.MatchedBracketBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textEditor.MatchedBracketForeColor = System.Drawing.Color.Transparent;
            this.textEditor.Name = "textEditor";
            this.textEditor.NumeberCharColor = System.Drawing.Color.Black;
            this.textEditor.RightEdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(222)))), ((int)(((byte)(211)))));
            this.textEditor.ScrollPos = new System.Drawing.Point(0, 0);
            this.textEditor.SearchPatternsBackColor = System.Drawing.Color.Empty;
            this.textEditor.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(98)))), ((int)(((byte)(87)))));
            this.textEditor.SelectionForeColor = System.Drawing.Color.White;
            this.textEditor.Size = new System.Drawing.Size(603, 130);
            this.textEditor.StringCharColor = System.Drawing.Color.Teal;
            this.textEditor.TabIndex = 3;
            this.textEditor.TabStop = false;
            this.textEditor.WhiteSpaceColor = System.Drawing.Color.Silver;
            // 
            // OptionDialog
            // 
            this.ClientSize = new System.Drawing.Size(634, 461);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textEditor);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.tabControl1);
            this.Name = "OptionDialog";
            this.Text = "オプション";
            this.grpView.ResumeLayout(false);
            this.grpView.PerformLayout();
            this.tpgBehavior.ResumeLayout(false);
            this.grpDefault.ResumeLayout(false);
            this.grpDefault.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmapWordWrapDigit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpgView.ResumeLayout(false);
            this.grpFont.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmapFontSize)).EndInit();
            this.grpColor.ResumeLayout(false);
            this.grpColor.PerformLayout();
            this.grpSymbol.ResumeLayout(false);
            this.grpSymbol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmapTabWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpView;
        private AcroBat.Views.Controls.CheckBoxEx bmapHighlightsCurrentLine;
        private AcroBat.Views.Controls.CheckBoxEx bmapShowsHRuler;
        private AcroBat.Views.Controls.CheckBoxEx bmapShowsLineNumber;
        private AcroBat.Views.Controls.CheckBoxEx bmapShowsDirtBar;
        private System.Windows.Forms.TabPage tpgBehavior;
        private System.Windows.Forms.GroupBox grpDefault;
        private System.Windows.Forms.Label lblWordWrapMode;
        private AcroBat.Views.Controls.ComboBoxEx emapWordWrapMode;
        private AcroBat.Views.Controls.NumericUpDownEx nmapWordWrapDigit;
        private System.Windows.Forms.Label lblEolMode;
        private AcroBat.Views.Controls.ComboBoxEx emapEolMode;
        private System.Windows.Forms.Label lblEncodingMode;
        private AcroBat.Views.Controls.ComboBoxEx emapEncodingMode;
        private System.Windows.Forms.Label lblFileMode;
        private AcroBat.Views.Controls.ComboBoxEx emapLanguageMode;
        private System.Windows.Forms.GroupBox groupBox1;
        private AcroBat.Views.Controls.CheckBoxEx bmapScrollsBeyondLastLine;
        private AcroBat.Views.Controls.CheckBoxEx bmapConvertsFullWidthSpaceToSpace;
        private AcroBat.Views.Controls.CheckBoxEx bmapHighlightsMatchedBracket;
        private AcroBat.Views.Controls.CheckBoxEx bmapConvertsTabToSpaces;
        private AcroBat.Views.Controls.CheckBoxEx bmapUnindentsWithBackspace;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Controls.TextEditor textEditor;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgView;
        private System.Windows.Forms.GroupBox grpFont;
        private AcroBat.Views.Controls.ComboBoxEx emapFontName;
        private AcroBat.Views.Controls.NumericUpDownEx nmapFontSize;
        private System.Windows.Forms.GroupBox grpColor;
        private System.Windows.Forms.Label lblBackColor;
        private System.Windows.Forms.Label lblForeColor;
        private AcroBat.Views.Controls.ListBoxEx lstColor;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.Button btnForeColor;
        private AcroBat.Views.Controls.ColorPicker emapForeColor;
        private AcroBat.Views.Controls.ColorPicker emapBackColor;
        private System.Windows.Forms.GroupBox grpSymbol;
        private AcroBat.Views.Controls.NumericUpDownEx nmapTabWidth;
        private System.Windows.Forms.Label lblTabWidth;
        private AcroBat.Views.Controls.CheckBoxEx bmapDrawsTab;
        private AcroBat.Views.Controls.CheckBoxEx bmapDrawsFullWidthSpace;
        private AcroBat.Views.Controls.CheckBoxEx bmapDrawsSpace;
        private AcroBat.Views.Controls.CheckBoxEx bmapDrawsEofMark;
        private AcroBat.Views.Controls.CheckBoxEx bmapDrawsEolCode;
    }
}