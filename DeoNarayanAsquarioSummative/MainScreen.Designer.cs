namespace DeoNarayanAsquarioSummative
{
    partial class MainScreen
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.Color.White;
            this.titleLabel.Font = new System.Drawing.Font("Lucida Console", 40.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.titleLabel.Location = new System.Drawing.Point(3, 210);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1200, 107);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Asquar.io";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startLabel
            // 
            this.startLabel.Font = new System.Drawing.Font("Lucida Console", 10.125F, System.Drawing.FontStyle.Bold);
            this.startLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.startLabel.Location = new System.Drawing.Point(0, 380);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(1200, 57);
            this.startLabel.TabIndex = 1;
            this.startLabel.Text = "Press Green to Play";
            this.startLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "MainScreen";
            this.Size = new System.Drawing.Size(1200, 700);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.GameScreen_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label startLabel;
    }
}
