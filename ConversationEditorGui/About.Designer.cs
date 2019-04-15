namespace ConversationEditorGui
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.HelpText = new System.Windows.Forms.Label();
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            this.newHelpText = new System.Windows.Forms.Label();
            this.newLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // HelpText
            // 
            this.HelpText.AutoSize = true;
            this.HelpText.Location = new System.Drawing.Point(25, 82);
            this.HelpText.Name = "HelpText";
            this.HelpText.Size = new System.Drawing.Size(0, 13);
            this.HelpText.TabIndex = 0;
            // 
            // HelpLink
            // 
            this.HelpLink.AutoSize = true;
            this.HelpLink.Location = new System.Drawing.Point(25, 128);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(135, 13);
            this.HelpLink.TabIndex = 1;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "http://www.flamewind.com";
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // newHelpText
            // 
            this.newHelpText.AutoSize = true;
            this.newHelpText.Location = new System.Drawing.Point(25, 23);
            this.newHelpText.Name = "newHelpText";
            this.newHelpText.Size = new System.Drawing.Size(149, 13);
            this.newHelpText.TabIndex = 2;
            this.newHelpText.Text = "For updates and support, visit:";
            // 
            // newLink
            // 
            this.newLink.AutoSize = true;
            this.newLink.Location = new System.Drawing.Point(25, 36);
            this.newLink.Name = "newLink";
            this.newLink.Size = new System.Drawing.Size(113, 13);
            this.newLink.TabIndex = 3;
            this.newLink.TabStop = true;
            this.newLink.Text = "The Neverwinter Vault";
            this.newLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Built by Kinslayer";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 150);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newLink);
            this.Controls.Add(this.newHelpText);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.HelpText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "How To Get Help...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label HelpText;
        private System.Windows.Forms.LinkLabel HelpLink;
        private System.Windows.Forms.Label newHelpText;
        private System.Windows.Forms.LinkLabel newLink;
        private System.Windows.Forms.Label label1;
    }
}