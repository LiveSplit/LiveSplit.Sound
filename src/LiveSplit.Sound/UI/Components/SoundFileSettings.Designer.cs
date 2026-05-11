namespace LiveSplit.UI.Components
{
    partial class SoundFileSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblName = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.ttPaths = new System.Windows.Forms.ToolTip(this.components);
            this.btnBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(3, 1);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(140, 27);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Split:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFilePath
            // 
            this.txtFilePath.AllowDrop = true;
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(149, 4);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(134, 19);
            this.txtFilePath.TabIndex = 1;
            this.txtFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtFilePath_DragDrop);
            this.txtFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtFilePath_DragEnter);
            this.txtFilePath.Enter += new System.EventHandler(this.txtFilePath_Enter);
            this.txtFilePath.Leave += new System.EventHandler(this.txtFilePath_Leave);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(370, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 21);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(289, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 21);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // SoundFileSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnClear);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SoundFileSettings";
            this.Size = new System.Drawing.Size(448, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolTip ttPaths;
        private System.Windows.Forms.Button btnBrowse;
    }
}
