namespace AcroPad.Views.Forms
{
    partial class GoToLineDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        ///// <summary>
        ///// Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numRowIndex = new AcroBat.Views.Controls.NumericUpDownEx();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblRowIndex = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numRowIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // numRowIndex
            // 
            this.numRowIndex.InValidColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(225)))));
            this.numRowIndex.Location = new System.Drawing.Point(18, 38);
            this.numRowIndex.Name = "numRowIndex";
            this.numRowIndex.ReverseUpDown = true;
            this.numRowIndex.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numRowIndex.Size = new System.Drawing.Size(250, 23);
            this.numRowIndex.TabIndex = 6;
            this.numRowIndex.ValidColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(189, 75);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(103, 75);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblRowIndex
            // 
            this.lblRowIndex.AutoSize = true;
            this.lblRowIndex.Location = new System.Drawing.Point(15, 13);
            this.lblRowIndex.Name = "lblRowIndex";
            this.lblRowIndex.Size = new System.Drawing.Size(43, 15);
            this.lblRowIndex.TabIndex = 5;
            this.lblRowIndex.Text = "行番号";
            // 
            // GoToLineDialog
            // 
            this.ClientSize = new System.Drawing.Size(284, 111);
            this.Controls.Add(this.numRowIndex);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblRowIndex);
            this.Name = "GoToLineDialog";
            this.Text = "指定行へ移動";
            ((System.ComponentModel.ISupportInitialize)(this.numRowIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AcroBat.Views.Controls.NumericUpDownEx numRowIndex;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblRowIndex;
    }
}