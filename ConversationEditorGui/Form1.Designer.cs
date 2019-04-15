namespace ConversationEditorGui
{
    partial class Form1
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
            this.tabControl1 = new TabControlDragDrop();
            this.mainDisplayPanel = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileAsDialog = new System.Windows.Forms.SaveFileDialog();
            this.closeTabButton = new System.Windows.Forms.PictureBox();
            this.mainDisplayPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeTabButton)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(917, 661);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            this.tabControl1.AllowDrop = true;
            // 
            // mainDisplayPanel
            // 
            this.mainDisplayPanel.Controls.Add(this.tabControl1);
            this.mainDisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDisplayPanel.Location = new System.Drawing.Point(0, 49);
            this.mainDisplayPanel.Name = "mainDisplayPanel";
            this.mainDisplayPanel.Size = new System.Drawing.Size(917, 661);
            this.mainDisplayPanel.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            // 
            // saveFileAsDialog
            // 
            this.saveFileAsDialog.DefaultExt = "yml";
            this.saveFileAsDialog.Filter = "All files (*.*)|*.*|NWN1 files (*.yml)|*.yml|NWN2 files (*.xml)|*.xml";
            // 
            // closeTabButton
            // 
            this.closeTabButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeTabButton.Image = global::ConversationEditorGui.Properties.Resources.closeButtonOff;
            this.closeTabButton.Location = new System.Drawing.Point(896, 52);
            this.closeTabButton.Name = "closeTabButton";
            this.closeTabButton.Size = new System.Drawing.Size(15, 15);
            this.closeTabButton.TabIndex = 7;
            this.closeTabButton.TabStop = false;
            this.closeTabButton.MouseLeave += new System.EventHandler(this.closeTabButton_MouseLeave);
            this.closeTabButton.Click += new System.EventHandler(this.closeTabButton_Click);
            this.closeTabButton.MouseHover += new System.EventHandler(this.closeTabButton_MouseHover);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 710);
            this.Controls.Add(this.closeTabButton);
            this.Controls.Add(this.mainDisplayPanel);
            this.Name = "Form1";
            this.Text = "Conversation Editor";
            this.Icon = System.Drawing.Icon.FromHandle(global::ConversationEditorGui.Properties.Resources.MainIcon.GetHicon());
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainDisplayPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.closeTabButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private TabControlDragDrop tabControl1;
        private System.Windows.Forms.Panel mainDisplayPanel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileAsDialog;
        private System.Windows.Forms.PictureBox closeTabButton;
    }
}

