using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            ContextMenu = null;
            ContextMenuOpening += ContextMenu_Opening;
            KeyDown += ShortcutKeys;
        }

        #region Events
        public void ShortcutKeys(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F6:
                    AddAction_Click(sender, new RoutedEventArgs());
                    break;
                case Key.F7:
                    AddHighlight_Click(sender, new RoutedEventArgs());
                    break;
                case Key.F8:
                    AddCheck_Click(sender, new RoutedEventArgs());
                    break;
                case Key.F9:
                    //Add Tag
                    //AddAction_Click(sender, new RoutedEventArgs());
                    break;
                default: break;
            }
        }

        /// <summary>
        /// TODO: use RoutedUICommands by making menu items properties rather than created on the fly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ContextMenu_Opening(object sender, ContextMenuEventArgs e)
        {
            var addAction = new MenuItem
            {
                Header = "Add Action",
                InputGestureText = "F6"
            };
            addAction.Click += AddAction_Click;
            var addHigh = new MenuItem
            {
                Header = "Add Highlight",
                InputGestureText = "F7"
            };
            addHigh.Click += AddHighlight_Click;
            var addCheck = new MenuItem
            {
                Header = "Add Skill Check",
                InputGestureText = "F8"
            };
            addCheck.Click += AddCheck_Click;
            var items = new MenuItem[] { addAction, addHigh, addCheck };
            this.InjectIntoDefaultMenu(e, p => base.OnContextMenuOpening(p), items);
        }

        private void AddAction_Click(object sender, RoutedEventArgs e)
        {
            AddTag("Action");
        }

        private void AddHighlight_Click(object sender, RoutedEventArgs e)
        {
            AddTag("Highlight");
        }

        private void AddCheck_Click(object sender, RoutedEventArgs e)
        {
            AddTag("Check", "");
        }
        #endregion

        /// <summary>
        /// TODO: Expand to list of menu items
        /// Tagging - 
        ///Standard -
        ///Highlight -
        ///<StartAction></Start>
        ///<StartHighlight></Start>
        ///<StartCheck>[SkillName?]</Start>
        /// </summary>
        private void AddTag(string token, string endToken = "")
        {
            var selectionStart = SelectionStart;
            var newText = Text;
            var addLength = ("<Start" + token + ">[").Length;
            newText = newText.Insert(selectionStart + SelectionLength, endToken+ "]</Start>");
            newText = newText.Insert(selectionStart, "<Start"+token+">[");
            Text = newText;
            Select(selectionStart + addLength, 0);

        }

        private void AddToken(string token)
        {
            var selectionStart = SelectionStart;
            var newText = Text;
            Text = newText.Insert(selectionStart, token);
            Select(selectionStart, token.Length);
        }
    }
}
