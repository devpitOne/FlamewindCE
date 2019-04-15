namespace ConversationEditorGui
{
    partial class CloseDialog
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
            this.closeSave = new System.Windows.Forms.Button();
            this.closeDiscard = new System.Windows.Forms.Button();
            this.closeCancel = new System.Windows.Forms.Button();
            this.closeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeSave
            // 
            this.closeSave.Location = new System.Drawing.Point(12, 109);
            this.closeSave.Name = "closeSave";
            this.closeSave.Size = new System.Drawing.Size(97, 23);
            this.closeSave.TabIndex = 0;
            this.closeSave.Text = "&Save Changes";
            this.closeSave.UseVisualStyleBackColor = true;
            this.closeSave.Click += new System.EventHandler(this.closeSave_Click);
            // 
            // closeDiscard
            // 
            this.closeDiscard.Location = new System.Drawing.Point(115, 109);
            this.closeDiscard.Name = "closeDiscard";
            this.closeDiscard.Size = new System.Drawing.Size(100, 23);
            this.closeDiscard.TabIndex = 1;
            this.closeDiscard.Text = "&Discard Changes";
            this.closeDiscard.UseVisualStyleBackColor = true;
            this.closeDiscard.Click += new System.EventHandler(this.closeDiscard_Click);
            // 
            // closeCancel
            // 
            this.closeCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeCancel.Location = new System.Drawing.Point(221, 109);
            this.closeCancel.Name = "closeCancel";
            this.closeCancel.Size = new System.Drawing.Size(85, 23);
            this.closeCancel.TabIndex = 2;
            this.closeCancel.Text = "Do &Not Close";
            this.closeCancel.UseVisualStyleBackColor = true;
            this.closeCancel.Click += new System.EventHandler(this.closeCancel_Click);
            // 
            // closeLabel
            // 
            this.closeLabel.AutoSize = true;
            this.closeLabel.Location = new System.Drawing.Point(49, 26);
            this.closeLabel.Name = "closeLabel";
            this.closeLabel.Size = new System.Drawing.Size(222, 52);
            this.closeLabel.TabIndex = 3;
            this.closeLabel.Text = "The conversation\n\"sample.xml\"\nhas been changed since it was last saved.\nDo you wa" +
                "nt to save or discard the changes?";
            this.closeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CloseDialog
            // 
            this.AcceptButton = this.closeSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeCancel;
            this.ClientSize = new System.Drawing.Size(321, 145);
            this.Controls.Add(this.closeLabel);
            this.Controls.Add(this.closeCancel);
            this.Controls.Add(this.closeDiscard);
            this.Controls.Add(this.closeSave);
            this.Name = "CloseDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CloseDialog";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Icon = System.Drawing.Icon.FromHandle(global::ConversationEditorGui.Properties.Resources.MainIcon.GetHicon());
        }

        #endregion

        private System.Windows.Forms.Button closeSave;
        private System.Windows.Forms.Button closeDiscard;
        private System.Windows.Forms.Button closeCancel;
        private System.Windows.Forms.Label closeLabel;
    }
}