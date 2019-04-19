using System;
using System.IO;
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
                    AddToken_Click(sender, new RoutedEventArgs());
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
            var addToken = new MenuItem
            {
                Header = "Add Token",
                InputGestureText = "F9"
            };
            addToken.Click += AddToken_Click;
            var items = new MenuItem[] { addAction, addHigh, addCheck, addToken };
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

        private void AddToken_Click(object sender, RoutedEventArgs e)
        {
            var token = SelectToken();
            if (token != null)
                AddToken(token);
        }
        #endregion

        /// <summary>
        /// </summary>
        private void AddTag(string token, string endToken = "")
        {
            var selectionStart = SelectionStart;
            var newText = Text;
            var addLength = ("<Start" + token + ">[").Length;
            newText = newText.Insert(selectionStart + SelectionLength, endToken + "]</Start>");
            newText = newText.Insert(selectionStart, "<Start" + token + ">[");
            Text = newText;
            Select(selectionStart + addLength, 0);

        }

        private void AddToken(string token)
        {
            var selectionStart = SelectionStart;
            var newText = Text;
            Text = newText.Insert(selectionStart, token);
            Select(selectionStart + token.Length, 0);
        }

        private string SelectToken()
        {
            var tokenS = new TokenSelect();
            try
            {
                using (var tokenFile = new FileStream("Tokens.csv", FileMode.Open, FileAccess.Read))
                {
                    using (var streamReader = new StreamReader(tokenFile))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {

                            tokenS.ListViewTokens.Add(new System.Windows.Forms.ListViewItem
                            {
                                Text = line.Split(',')[0],
                                Tag = line.Split(',')[1]
                            });
                        }
                    }
                }
                tokenS.ShowDialog();
                return tokenS.SelectedToken;
            }
            catch (FileNotFoundException exc)
            {
                MessageBox.Show("Tokens.csv was not found in the same directory.");
                return null;
            }
            catch (IOException exc)
            {
                MessageBox.Show("Tokens.csv is open in another program or Access restricted.");
                return null;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Could not read Tokens.csv, the file may be corrupted or a row may be missing a value.");
                return null;
            }
        }
    }
}
