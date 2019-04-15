using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConversationEditorGui
{
    public partial class DeleteNodeDialog : Form
    {
        public DeleteNodeDialogOptions clickedButton;

        public DeleteNodeDialog()
        {
            InitializeComponent();
        }

        public void setNodeText(string nodeText)
        {
            this.textLabel.Text = "\"" + nodeText + "...\"";
        }

        private void DeleteLinks_Click(object sender, EventArgs e)
        {
            this.clickedButton = DeleteNodeDialogOptions.Delete;
            this.Hide();
        }

        private void CopyLinks_Click(object sender, EventArgs e)
        {
            this.clickedButton = DeleteNodeDialogOptions.Copy;
            this.Hide();
        }

        private void CancelDelete_Click(object sender, EventArgs e)
        {
            this.clickedButton = DeleteNodeDialogOptions.Cancel;
            this.Hide();
        }
    }
}