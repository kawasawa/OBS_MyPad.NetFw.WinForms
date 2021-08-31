using AcroBat;
using AcroBat.Views;
using AcroPad.Properties;
using AcroPad.Views.Controls;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AcroPad.Views.Contents
{
    /// <summary>
    /// 検索を行うためのコンテンツを表します。
    /// </summary>
    public partial class SearchContent : ToolContentBase
    {
        private SearchContentBehaviorKind _behaviorMode;

        /// <summary>
        /// アクティブなドキュメントを取得します。
        /// </summary>
        private EditorDocument ActiveDocument => this.DockPanel?.ActiveDocument as EditorDocument;

        /// <summary>
        /// 検索パターンが空かどうかを示す値を取得します。
        /// </summary>
        private bool IsEmptySearchText => string.IsNullOrEmpty(this.cmbSearchTerm.Text);

        /// <summary>
        /// 検索コンテンツのふるまいを取得または設定します。
        /// </summary>
        public SearchContentBehaviorKind BehaviorMode
        {
            get { return this._behaviorMode; }
            set
            {
                this._behaviorMode = value;
                this.SetControlBehavior(value);
            }
        }

        /// <summary>
        /// 大文字小文字を区別するかどうかを示す値を取得します。
        /// </summary>
        public bool CaseSensitive
        {
            get { return this.bmapCaseSensitive.Checked; }
            set { this.bmapCaseSensitive.Checked = value; }
        }

        /// <summary>
        /// 正規表現を使用するかどうかを示す値を取得します。
        /// </summary>
        public bool UseRegex
        {
            get { return this.bmapUseRegex.Checked; }
            set { this.bmapUseRegex.Checked = value; }
        }

        /// <summary>
        /// 循環検索を行うかどうかを示す値を取得します。
        /// </summary>
        public bool GoRound
        {
            get { return this.bmapGoRound.Checked; }
            set { this.bmapGoRound.Checked = value; }
        }

        /// <summary>
        /// 検索履歴を取得または設定します。
        /// </summary>
        public string[] SearchTerm
        {
            get
            {
                List<string> result = new List<string>();
                foreach (object item in this.cmbSearchTerm.Items)
                {
                    result.Add(item.ToString());
                }
                return result.ToArray();
            }
            set
            {
                this.cmbSearchTerm.Items.Clear();
                foreach (object item in value)
                {
                    this.cmbSearchTerm.Items.Add(item);
                }
                if (0 < this.cmbSearchTerm.Items.Count)
                {
                    this.cmbSearchTerm.SelectedItem = this.cmbSearchTerm.Items[0];
                }
                this.UpdateToolCommandEnabled();
            }
        }

        /// <summary>
        /// 置換履歴を取得または設定します。
        /// </summary>
        public string[] ReplaceTerm
        {
            get
            {
                List<string> result = new List<string>();
                foreach (object item in this.cmbReplaceTerm.Items)
                {
                    result.Add(item.ToString());
                }
                return result.ToArray();
            }
            set
            {
                this.cmbReplaceTerm.Items.Clear();
                foreach (object item in value)
                {
                    this.cmbReplaceTerm.Items.Add(item);
                }
                if (0 < this.cmbReplaceTerm.Items.Count)
                {
                    this.cmbReplaceTerm.SelectedItem = this.cmbReplaceTerm.Items[0];
                }
                this.UpdateToolCommandEnabled();
            }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public SearchContent()
        {
            this.InitializeComponent();
            this.InitializeItems();
            this.AddEvengHandler();
        }

        /// <summary>
        /// 特定のコントロールを初期化します。
        /// </summary>
        private void InitializeItems()
        {
            foreach (Control control in this.BasePanel.Controls)
            {
                if (control is Panel)
                {
                    Panel panel = (Panel)control;
                    panel.Tag = panel.Height;
                }
            }

            this.SetFormIcon();
            this.SetItemImage();
            this.AcceptButton = this.btnSearchNext;
            this.BehaviorMode = SearchContentBehaviorKind.Search;
            this.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.Float;
            this.BasePanel.AutoScrollMinSize = this.Size;
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEvengHandler()
        {
            this.cmbSearchTerm.TextChanged += this.UpdateEditorViewCommand_Executed;
            this.bmapCaseSensitive.CheckedChanged += this.UpdateEditorViewCommand_Executed;
            this.bmapUseRegex.CheckedChanged += this.UpdateEditorViewCommand_Executed;
            this.btnSearchPrev.Click += this.SearchPrevCommand_Executed;
            this.btnSearchNext.Click += this.SearchNextCommand_Executed;
            this.btnReplace.Click += this.ReplaceCommand_Executed;
            this.btnReplaceAll.Click += this.ReplaceAllCommand_Executed;
            this.cmdClean_1.Click += this.ClearSearchHistoryCommand_Executed;
            this.cmdClean_2.Click += this.ClearReplaceHistoryCommand_Executed;
        }

        /// <summary>
        /// コンテンツを選択にします。
        /// </summary>
        public override void Select()
        {
            base.Select();
            this.cmbSearchTerm.Select();
        }

        /// <summary>
        /// 指定したパネルを展開します。
        /// </summary>
        /// <param name="panel">パネル</param>
        private void ExpandPanel(Panel panel)
        {
            panel.Enabled = true;
            panel.Height = (int)panel.Tag;
        }

        /// <summary>
        /// 指定したパネルをたたみます。
        /// </summary>
        /// <param name="panel">パネル</param>
        private void CollapsePanel(Panel panel)
        {
            panel.Enabled = false;
            panel.Height = 0;
        }

        /// <summary>
        /// コントロールのふるまいを設定します。
        /// </summary>
        /// <param name="mode">画面のふるまい</param>
        private void SetControlBehavior(SearchContentBehaviorKind mode)
        {
            switch (mode)
            {
                case SearchContentBehaviorKind.Search:
                    this.Text = this.TabText = nameof(SearchContentBehaviorKind.Search);
                    this.CollapsePanel(this.panel2);
                    this.CollapsePanel(this.panel5);
                    break;
                case SearchContentBehaviorKind.Replace:
                    this.Text = this.TabText = nameof(SearchContentBehaviorKind.Replace);
                    this.ExpandPanel(this.panel2);
                    this.ExpandPanel(this.panel5);
                    break;
            }
        }

        /// <summary>
        /// 検索パターンを取得します。
        /// </summary>
        /// <returns>検索パターン</returns>
        private string GetSearchPattern() => this.GetSearchPattern(this.UseRegex);

        /// <summary>
        /// 検索パターンを取得します。
        /// </summary>
        /// <param name="forRegex">正規表現に利用するかどうかを示す値</param>
        /// <returns>検索パターン</returns>
        private string GetSearchPattern(bool forRegex)
        {
            if (forRegex)
            {
                return this.UseRegex ?
                       Extensions.ToDotNetRegexPattern(this.cmbSearchTerm.Text) :
                       Regex.Escape(this.cmbSearchTerm.Text);
            }
            else
            {
                return this.cmbSearchTerm.Text;
            }
        }

        /// <summary>
        /// 置き換える文字列を取得します。
        /// </summary>
        /// <returns>置き換える文字列</returns>
        private string GetReplaceString() => this.cmbReplaceTerm.Text;

        /// <summary>
        /// 正規表現オプションを取得します。
        /// </summary>
        /// <param name="isRightToLeft">左方向に検索するかどうかを示す値</param>
        /// <returns>正規表現オプション</returns>
        private RegexOptions GetRegexOptions(bool isRightToLeft = false)
        {
            if (isRightToLeft)
            {
                return this.CaseSensitive ?
                       RegexOptions.Multiline | RegexOptions.RightToLeft :
                       RegexOptions.Multiline | RegexOptions.RightToLeft | RegexOptions.IgnoreCase;
            }
            else
            {
                return this.CaseSensitive ?
                       RegexOptions.Multiline :
                       RegexOptions.Multiline | RegexOptions.IgnoreCase;
            }
        }

        /// <summary>
        /// テキストエディターの表示を更新します。
        /// </summary>
        private void UpdtateEditorView()
        {
            if (this.ActiveDocument == null)
            {
                return;
            }

            try
            {
                this.ActiveDocument.TextEditor.SetSearchPatterns(new Regex(this.GetSearchPattern(true), this.GetRegexOptions()));
            }
            catch (ArgumentException)
            {
                this.ActiveDocument.TextEditor.ClearSearchPatterns();
            }
        }

        /// <summary>
        /// 先の方向に対して検索を行います。
        /// </summary>
        /// <returns>検索対象が見つかったかどうかを示す値</returns>
        public bool FindNext()
        {
            if (this.IsEmptySearchText)
            {
                MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_ENTERED);
                this.Select();
                return false;
            }

            this.UpdateSearchHistory();
            this.UpdateToolCommandEnabled();
            TextEditor.FindResult result;
            if (this.UseRegex)
            {
                try
                {
                    result = this.ActiveDocument.TextEditor.FindNext(new Regex(this.GetSearchPattern(), this.GetRegexOptions()), this.GoRound);
                }
                catch (ArgumentException e)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Error, string.Format(Resources.MSG_ERR_REGEX_FAILED, e.Message));
                    this.Select();
                    return false;
                }

                if (result.Result == false)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_FOUND);
                    this.Select();
                    return false;
                }
            }
            else
            {
                result = this.ActiveDocument.TextEditor.FindNext(this.GetSearchPattern(), this.GoRound, this.CaseSensitive);
                if (result.Result == false)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_FOUND);
                    this.Select();
                    return false;
                }
            }
            this.ActiveDocument.TextEditor.SetSelection(result);
            this.ActiveDocument.TextEditor.ScrollToCaret();
            return true;
        }

        /// <summary>
        /// 前の方向に対して検索を行います。
        /// </summary>
        /// <returns>検索対象が見つかったかどうかを示す値</returns>
        public bool FindPrev()
        {
            if (this.IsEmptySearchText)
            {
                MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_ENTERED);
                this.Select();
                return false;
            }

            this.UpdateSearchHistory();
            this.UpdateToolCommandEnabled();
            TextEditor.FindResult result;
            if (this.UseRegex)
            {
                try
                {
                    result = this.ActiveDocument.TextEditor.FindPrev(new Regex(this.GetSearchPattern(), this.GetRegexOptions(true)), this.GoRound);
                }
                catch (ArgumentException e)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Error, string.Format(Resources.MSG_ERR_REGEX_FAILED, e.Message));
                    this.Select();
                    return false;
                }

                if (result.Result == false)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_FOUND);
                    this.Select();
                    return false;
                }
            }
            else
            {
                result = this.ActiveDocument.TextEditor.FindPrev(this.GetSearchPattern(), this.GoRound, this.CaseSensitive);
                if (result.Result == false)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_FOUND);
                    this.Select();
                    return false;
                }
            }
            this.ActiveDocument.TextEditor.SetSelection(result);
            this.ActiveDocument.TextEditor.ScrollToCaret();
            return true;
        }

        /// <summary>
        /// 対象の文字列を指定された文字列で置き換えます。
        /// </summary>
        public void Replace()
        {
            if (this.IsEmptySearchText)
            {
                MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_ENTERED);
                this.Select();
                return;
            }

            this.UpdateSearchHistory();
            this.UpdateReplacdHistory();
            this.UpdateToolCommandEnabled();
            TextEditor.FindResult result;
            if (this.UseRegex)
            {
                try
                {
                    result = this.ActiveDocument.TextEditor.FindNext(new Regex(this.GetSearchPattern(), this.GetRegexOptions()), 0, this.ActiveDocument.TextEditor.TextLength);
                }
                catch (ArgumentException e)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Error, string.Format(Resources.MSG_ERR_REGEX_FAILED, e.Message));
                    this.Select();
                    return;
                }

                if (result.Result == false)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_FOUND);
                    this.Select();
                    return;
                }
            }
            else
            {
                result = this.ActiveDocument.TextEditor.FindNext(this.GetSearchPattern(), 0, this.ActiveDocument.TextEditor.TextLength, this.CaseSensitive);
                if (result.Result == false)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_FOUND);
                    this.Select();
                    return;
                }
            }
            this.ActiveDocument.TextEditor.SetSelection(result);
            this.ActiveDocument.TextEditor.ScrollToCaret();
            this.ActiveDocument.TextEditor.Replace(this.GetReplaceString(), result);
        }

        /// <summary>
        /// 対象の文字列を指定した文字列ですべて置き換えます。
        /// </summary>
        public void ReplaceAll()
        {
            if (this.IsEmptySearchText)
            {
                MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_ENTERED);
                this.Select();
                return;
            }

            this.UpdateSearchHistory();
            this.UpdateReplacdHistory();
            this.UpdateToolCommandEnabled();
            int count = 0;
            string replacedText;
            if (this.UseRegex)
            {
                MatchEvaluator evaluator = ((match) =>
                {
                    count++;
                    return this.GetReplaceString();
                });

                try
                {
                    replacedText = Regex.Replace(this.ActiveDocument.TextEditor.Text, this.GetSearchPattern(), evaluator);
                }
                catch (ArgumentException e)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Error, string.Format(Resources.MSG_ERR_REGEX_FAILED, e.Message));
                    this.Select();
                    return;
                }
            }
            else
            {
                string originalText = this.ActiveDocument.TextEditor.Text;
                string searchPattern = this.GetSearchPattern();
                count = (originalText.Length - originalText.Replace(searchPattern, string.Empty).Length) / searchPattern.Length;
                replacedText = originalText.Replace(searchPattern, this.GetReplaceString());
            }

            if (count == 0)
            {
                MessageBoxUtils.ShowMessageBox(MessageKind.Information, Resources.MSG_INF_NOT_FOUND);
                this.Select();
                return;
            }

            try
            {
                this.ActiveDocument.TextEditor.BeginUndo();
                this.ActiveDocument.TextEditor.Text = replacedText;
            }
            finally
            {
                this.ActiveDocument.TextEditor.EndUndo();
            }
            MessageBoxUtils.ShowMessageBox(MessageKind.Information, string.Format(Resources.MSG_INF_REPLACE_ALL, count));
        }

        /// <summary>
        /// ツールコマンドの有効状態を更新します。
        /// </summary>
        private void UpdateToolCommandEnabled()
        {
            this.cmdClean_1.Enabled = 0 < this.cmbSearchTerm.Items.Count;
            this.cmdClean_2.Enabled = 0 < this.cmbReplaceTerm.Items.Count;
        }

        /// <summary>
        /// 検索履歴を更新します。
        /// </summary>
        private void UpdateSearchHistory()
        {
            string text = this.cmbSearchTerm.Text;
            if (this.cmbSearchTerm.SelectedIndex < 0 &&
                this.cmbSearchTerm.Items.Contains(text) == false)
            {
                this.cmbSearchTerm.Items.Insert(0, text);
            }
            else
            {
                this.cmbSearchTerm.Items.Remove(text);
                this.cmbSearchTerm.Items.Insert(0, text);
                this.cmbSearchTerm.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 置換履歴を更新します。
        /// </summary>
        private void UpdateReplacdHistory()
        {
            string text = this.cmbReplaceTerm.Text;
            if (this.cmbReplaceTerm.SelectedIndex < 0 &&
                this.cmbReplaceTerm.Items.Contains(text) == false)
            {
                this.cmbReplaceTerm.Items.Insert(0, text);
            }
            else
            {
                this.cmbReplaceTerm.Items.Remove(text);
                this.cmbReplaceTerm.Items.Insert(0, text);
                this.cmbReplaceTerm.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 検索履歴をクリアします。
        /// </summary>
        private void ClearSearchHistory()
        {
            this.cmbSearchTerm.Items.Clear();
        }

        /// <summary>
        /// 置換履歴をクリアします。
        /// </summary>
        private void ClearReplaceHistory()
        {
            this.cmbReplaceTerm.Items.Clear();
        }

        /// <summary>
        /// コマンドキーを処理します。
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter | Keys.Shift:
                    this.btnSearchPrev.PerformClick();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UpdateEditorViewCommand_Executed(object sender, EventArgs e)
        {
            this.UpdtateEditorView();
        }

        private void SearchPrevCommand_Executed(object sender, EventArgs e)
        {
            this.FindPrev();
        }

        private void SearchNextCommand_Executed(object sender, EventArgs e)
        {
            this.FindNext();
        }

        private void ReplaceCommand_Executed(object sender, EventArgs e)
        {
            this.Replace();
        }

        private void ReplaceAllCommand_Executed(object sender, EventArgs e)
        {
            this.ReplaceAll();
        }

        private void ClearSearchHistoryCommand_Executed(object sender, EventArgs e)
        {
            if (MessageBoxUtils.ShowMessageBox(MessageKind.WarningCancelable, Resources.MSG_WAR_CLEAR_SEARCH_HISTORY) != DialogResult.OK)
            {
                this.Activate();
                ((Control)sender).Select();
                return;
            }
            this.ClearSearchHistory();
            this.UpdateToolCommandEnabled();
            this.Select();
        }

        private void ClearReplaceHistoryCommand_Executed(object sender, EventArgs e)
        {
            if (MessageBoxUtils.ShowMessageBox(MessageKind.WarningCancelable, Resources.MSG_WAR_CLEAR_REPLACE_HISTORY) != DialogResult.OK)
            {
                this.Activate();
                ((Control)sender).Select();
                return;
            }
            this.ClearReplaceHistory();
            this.UpdateToolCommandEnabled();
            this.Select();
        }
    }
}
