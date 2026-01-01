namespace LiveSplit.UI.Components
{
    partial class SoundVolumeSettings
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
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.lblName = new System.Windows.Forms.Label();
            this.ttVolume = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // tbVolume
            // 
            this.tbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVolume.AutoSize = false;
            this.tbVolume.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbVolume.Location = new System.Drawing.Point(149, 3);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(296, 21);
            this.tbVolume.TabIndex = 5;
            this.tbVolume.TickFrequency = 10;
            this.tbVolume.Scroll += new System.EventHandler(this.VolumeTrackBarScrollHandler);
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(3, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(140, 12);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Split:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ttVolume
            // 
            this.ttVolume.AutoPopDelay = 5000;
            this.ttVolume.InitialDelay = 1000;
            this.ttVolume.ReshowDelay = 500;
            this.ttVolume.ShowAlways = true;
            // 
            // SoundVolumeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.tbVolume);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SoundVolumeSettings";
            this.Size = new System.Drawing.Size(448, 27);
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ToolTip ttVolume;

    }
}
