using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace ConversationEditorGui
{
    public class MainMenuAboutMenu : ToolStripMenuItem
    {
        private ToolStripMenuItem mainAboutDropdownMenu_Help;

        private MainMenuStripClass myParentMenuStrip;

        private About myAboutForm;

        public MainMenuAboutMenu(MainMenuStripClass creator)
        {
            myParentMenuStrip = creator;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.mainAboutDropdownMenu_Help = GetMenuOption
            (
                "mainAboutDropdownMenu_Help",
                "How To Get Help...",
                (Keys)(Keys.Control | Keys.H)
            );
            this.mainAboutDropdownMenu_Help.Click += new EventHandler(mainAboutDropdownMenu_Help_Click);

            this.DropDownItems.Add(this.mainAboutDropdownMenu_Help);

            this.Name = "mainAboutDropdownMenu";
            this.ShortcutKeys = (Keys)(Keys.Control | Keys.B);
            this.Size = new Size(35, 20);
            this.Text = "A&bout";
        }

        private void mainAboutDropdownMenu_Help_Click(object sender, EventArgs e)
        {
            myAboutForm = new About();
            myAboutForm.Show();
        }
        private static ToolStripMenuItem GetMenuOption(string name, string text, Keys shortcuts)
        {
            ToolStripMenuItem newToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem.Size = new Size(352, 22);
            newToolStripMenuItem.Name = name;
            newToolStripMenuItem.Text = text;
            if (shortcuts != Keys.None)
            {
                newToolStripMenuItem.ShortcutKeys = shortcuts;
            }
            return newToolStripMenuItem;
        }
    }
}
