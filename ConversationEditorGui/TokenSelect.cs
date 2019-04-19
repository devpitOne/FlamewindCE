using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace ConversationEditorGui
{
    public partial class TokenSelect : Form
    {
        public string SelectedToken;
        public ListViewItemCollection ListViewTokens { get { return listViewTokens.Items; } }

        public TokenSelect()
        {
            InitializeComponent();
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
