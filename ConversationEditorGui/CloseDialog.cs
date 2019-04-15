using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Editor;

namespace ConversationEditorGui
{
    public partial class CloseDialog : Form
    {
        public CloseDialogOption clickedButton;

        public CloseDialog()
        {
            InitializeComponent();
        }

        public void fileNameText(string myFileName)
        {
            myFileName = TreeTab.GetShortFileName(myFileName);
            this.Text = myFileName;
            this.closeLabel.Text = "The conversation\n\"" + myFileName + "\"\nhas been changed since it was last saved.\nDo you wa" +
                "nt to save or discard the changes?";
        }

        private void closeSave_Click(object sender, EventArgs e)
        {
            clickedButton = CloseDialogOption.Save;
            this.Hide();
        }

        private void closeDiscard_Click(object sender, EventArgs e)
        {
            clickedButton = CloseDialogOption.Discard;
            this.Hide();
        }

        private void closeCancel_Click(object sender, EventArgs e)
        {
            clickedButton = CloseDialogOption.Cancel;
            this.Hide();
        }
    }
}