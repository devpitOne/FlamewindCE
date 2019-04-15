namespace ConversationEditorGui
{
    partial class TestConversation
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
            this.tag = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tag
            // 
            this.tag.AutoSize = true;
            this.tag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tag.Location = new System.Drawing.Point(11, 12);
            this.tag.Name = "tag";
            this.tag.Size = new System.Drawing.Size(41, 13);
            this.tag.TabIndex = 0;
            this.tag.Text = "label1";
            this.Icon = System.Drawing.Icon.FromHandle(global::ConversationEditorGui.Properties.Resources.MainIcon.GetHicon());
            this.TransparencyKey = System.Drawing.Color.Lime;
            // 
            // TestConversation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(501, 266);
            this.Controls.Add(this.tag);
            this.Name = "TestConversation";
            this.Text = "Test Conversation";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Shown += new System.EventHandler(TestConversation_Shown);
        }

        #endregion

        private System.Windows.Forms.Label tag;
    }
}