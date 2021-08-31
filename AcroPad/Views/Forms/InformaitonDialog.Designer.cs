namespace AcroPad.Views.Forms
{
    partial class InformaitonDialog
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
            this.Title = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.Copyright = new System.Windows.Forms.Label();
            this.VersionString = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Title.Location = new System.Drawing.Point(30, 25);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(71, 33);
            this.Title.TabIndex = 5;
            this.Title.Text = "Title";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(220, 115);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // Copyright
            // 
            this.Copyright.AutoSize = true;
            this.Copyright.Location = new System.Drawing.Point(55, 90);
            this.Copyright.Name = "Copyright";
            this.Copyright.Size = new System.Drawing.Size(59, 15);
            this.Copyright.TabIndex = 7;
            this.Copyright.Text = "Copyright";
            // 
            // VersionString
            // 
            this.VersionString.AutoSize = true;
            this.VersionString.Location = new System.Drawing.Point(55, 70);
            this.VersionString.Name = "VersionString";
            this.VersionString.Size = new System.Drawing.Size(76, 15);
            this.VersionString.TabIndex = 6;
            this.VersionString.Text = "VersionString";
            // 
            // InformaitonDialog
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(334, 161);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.Copyright);
            this.Controls.Add(this.VersionString);
            this.Name = "InformaitonDialog";
            this.Text = "バージョン情報";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label Copyright;
        private System.Windows.Forms.Label VersionString;
    }
}