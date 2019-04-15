namespace ConversationEditorGui
{
    partial class DeleteNodeDialog
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
            this.DeleteNodeLabel = new System.Windows.Forms.Label();
            this.DeleteLinks = new System.Windows.Forms.Button();
            this.CopyLinks = new System.Windows.Forms.Button();
            this.CancelDelete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            this.Icon = System.Drawing.Icon.FromHandle(global::ConversationEditorGui.Properties.Resources.MainIcon.GetHicon());
            // 
            // DeleteNodeLabel
            // 
            this.DeleteNodeLabel.AutoSize = true;
            this.DeleteNodeLabel.Location = new System.Drawing.Point(24, 18);
            this.DeleteNodeLabel.Name = "DeleteNodeLabel";
            this.DeleteNodeLabel.Size = new System.Drawing.Size(242, 52);
            this.DeleteNodeLabel.TabIndex = 0;
            this.DeleteNodeLabel.Text = "This conversation has one or more links to a node\nin the section you are deleting" +
                ".\n\nSome text of the node is below.";
            this.DeleteNodeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DeleteLinks
            // 
            this.DeleteLinks.Location = new System.Drawing.Point(12, 187);
            this.DeleteLinks.Name = "DeleteLinks";
            this.DeleteLinks.Size = new System.Drawing.Size(75, 44);
            this.DeleteLinks.TabIndex = 1;
            this.DeleteLinks.Text = "&Delete these links";
            this.DeleteLinks.UseVisualStyleBackColor = true;
            this.DeleteLinks.Click += new System.EventHandler(this.DeleteLinks_Click);
            // 
            // CopyLinks
            // 
            this.CopyLinks.Location = new System.Drawing.Point(101, 187);
            this.CopyLinks.Name = "CopyLinks";
            this.CopyLinks.Size = new System.Drawing.Size(76, 44);
            this.CopyLinks.TabIndex = 2;
            this.CopyLinks.Text = "&Move link target";
            this.CopyLinks.UseVisualStyleBackColor = true;
            this.CopyLinks.Click += new System.EventHandler(this.CopyLinks_Click);
            // 
            // CancelDelete
            // 
            this.CancelDelete.Location = new System.Drawing.Point(188, 187);
            this.CancelDelete.Name = "CancelDelete";
            this.CancelDelete.Size = new System.Drawing.Size(92, 44);
            this.CancelDelete.TabIndex = 3;
            this.CancelDelete.Text = "Ca&ncel: do not delete section";
            this.CancelDelete.UseVisualStyleBackColor = true;
            this.CancelDelete.Click += new System.EventHandler(this.CancelDelete_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textLabel);
            this.panel1.Location = new System.Drawing.Point(32, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 52);
            this.panel1.TabIndex = 4;
            // 
            // textLabel
            // 
            this.textLabel.AutoSize = true;
            this.textLabel.Location = new System.Drawing.Point(12, 18);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(206, 13);
            this.textLabel.TabIndex = 0;
            this.textLabel.Text = "\"Eiwn en sken asnd ien qlwe ias n eikd...\"";
            this.textLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DeleteNodeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 248);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CancelDelete);
            this.Controls.Add(this.CopyLinks);
            this.Controls.Add(this.DeleteLinks);
            this.Controls.Add(this.DeleteNodeLabel);
            this.Name = "DeleteNodeDialog";
            this.Text = "Warning";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DeleteNodeLabel;
        private System.Windows.Forms.Button DeleteLinks;
        private System.Windows.Forms.Button CopyLinks;
        private System.Windows.Forms.Button CancelDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label textLabel;
    }
}