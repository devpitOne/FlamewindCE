using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.IO;

namespace ConversationEditorGui
{
    public partial class TokenSelect : Form
    {
        public string SelectedToken;

        public TokenSelect()
        {
            InitializeComponent();
            using (var tokenFile = new FileStream("Tokens.csv", FileMode.Open, FileAccess.Read)) {
                using (var streamReader = new StreamReader(tokenFile))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        
                        listViewTokens.Items.Add(new ListViewItem
                        {
                            Text = line.Split(',')[0],
                            Tag = line.Split(',')[1]
                    });
                    }
                }
            }
        }

        public void Item_DblClick(object sender, MouseEventArgs e)
        {
            if (listViewTokens.SelectedItems.Count > 0)
            {
                SelectedToken = listViewTokens.SelectedItems[0].Tag.ToString();
                Close();
            }
        }

        public void Ok_Click(object sender, EventArgs e)
        {
            Item_DblClick(sender, new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 0));
        }

        public void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
