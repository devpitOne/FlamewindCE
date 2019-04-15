using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ConversationEditorGui
{
    public class LinkedTextBox : System.Windows.Controls.TextBox
    {
        public LinkedTree myTree = null;
        public LinkedCommentsBox myCommentsBox = null;
        public string textOnEntry = "";
        public UndoState stateOnEntry;

        public void PasteIntoBox(string toPaste)
        {
            string newText = "";
            int newPosition = 0;
            newText = this.Text.Substring(0, this.SelectionStart);
            newText += toPaste;
            newPosition = newText.Length;
            newText += this.Text.Substring((this.SelectionStart + this.SelectionLength));
            this.Text = newText;
            this.SelectionStart = newPosition;
        }
    }
}
