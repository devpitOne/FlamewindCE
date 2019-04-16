using System.Windows;
using System.Windows.Controls;

namespace ConversationEditorGui
{
    public class LinkedTextBox : TextBox
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

        public LinkedTextBox() : base()
        {
            this.ContextMenu = null;
            ContextMenuOpening += ContextMenu_Opening;
        }

        public void ContextMenu_Opening(object sender, ContextMenuEventArgs e)
        {
            var tokenAdd = new MenuItem();
            tokenAdd.Header = "Add Token";
            tokenAdd.Click += AddToken_Click;
            var items = new MenuItem[] { tokenAdd };
            this.InjectIntoDefaultMenu(e, p => base.OnContextMenuOpening(p), items);
        }

        /// <summary>
        /// TODO: Expand to list of menu items
        /// Tagging - 
        ///Standard -
        ///Highlight -
        ///<StartAction></Start>
        ///<StartHighlight></Start>
        ///<StartCheck>[SkillName?]</Start>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToken_Click(object sender, RoutedEventArgs e)
        {
            var selectionStart = SelectionStart;
            var newText = Text;
            newText = newText.Insert(selectionStart + SelectionLength, "</>");
            newText = newText.Insert(selectionStart, "<>");
            Text = newText;

        }
    }
}
