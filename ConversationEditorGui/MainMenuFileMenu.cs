using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace ConversationEditorGui
{
    public class MainMenuFileMenu : ToolStripMenuItem
    {
        private ToolStripMenuItem mainFileDropdownMenu_NewConversation;
        private ToolStripMenuItem mainFileDropdownMenu_OpenConversation;
        private ToolStripMenuItem mainFileDropdownMenu_SaveConversation;
        private ToolStripMenuItem mainFileDropdownMenu_SaveConversationAs;
        private ToolStripMenuItem mainFileDropdownMenu_SaveConversationAutoIncrement;
        private ToolStripMenuItem mainFileDropdownMenu_CloseConversation;
        private ToolStripMenuItem mainFileDropdownMenu_Exit;

        private MainMenuStripClass myParentMenuStrip;

        public MainMenuFileMenu(MainMenuStripClass creator)
        {
            myParentMenuStrip = creator;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.mainFileDropdownMenu_NewConversation = GetMenuOption
            (
                "mainFileDropdownMenu_NewConversation",
                "New Conversation",
                (Keys)(Keys.Control | Keys.N)
            );
            this.mainFileDropdownMenu_NewConversation.Click += new EventHandler(mainFileDropdownMenu_NewConversation_Click);

            this.mainFileDropdownMenu_OpenConversation = GetMenuOption
            (
                "mainFileDropdownMenu_OpenConversation",
                "Open Conversation...",
                (Keys)(Keys.Control | Keys.O)
            );
            this.mainFileDropdownMenu_OpenConversation.Click += new EventHandler(mainFileDropdownMenu_OpenConversation_Click);

            this.mainFileDropdownMenu_SaveConversation = GetMenuOption
            (
                "mainFileDropdownMenu_SaveConversation",
                "Save Conversation",
                (Keys)(Keys.Control | Keys.S)
            );
            this.mainFileDropdownMenu_SaveConversation.Click += new EventHandler(mainFileDropdownMenu_SaveConversation_Click);

            this.mainFileDropdownMenu_SaveConversationAs = GetMenuOption
            (
                "mainFileDropdownMenu_SaveConversationAs",
                "Save Conversation As...",
                (Keys)(Keys.Control | Keys.Shift | Keys.S)
            );
            this.mainFileDropdownMenu_SaveConversationAs.Click += new EventHandler(mainFileDropdownMenu_SaveConversationAs_Click);

            this.mainFileDropdownMenu_SaveConversationAutoIncrement = GetMenuOption
            (
                "mainFileDropdownMenu_SaveConversationAutoIncrement",
                "Save Conversation AutoIncremented",
                (Keys)(Keys.Control | Keys.Alt | Keys.Shift | Keys.S)
            );
            this.mainFileDropdownMenu_SaveConversationAutoIncrement.Click += new EventHandler(mainFileDropdownMenu_SaveConversationAutoIncrement_Click);

            this.mainFileDropdownMenu_CloseConversation = GetMenuOption
            (
                "mainFileDropdownMenu_CloseConversation",
                "Close Conversation",
                (Keys)(Keys.Control | Keys.W)
            );
            this.mainFileDropdownMenu_CloseConversation.Click += new EventHandler(mainFileDropdownMenu_CloseConversation_Click);

            this.mainFileDropdownMenu_Exit = GetMenuOption
            (
                "mainFileDropdownMenu_Exit",
                "Exit",
                (Keys)(Keys.Alt | Keys.X)
            );
            this.mainFileDropdownMenu_Exit.Click += new EventHandler(mainFileDropdownMenu_Exit_Click);

            this.DropDownItems.AddRange
            (
                new ToolStripItem[]
                {
                    this.mainFileDropdownMenu_NewConversation,
                    this.mainFileDropdownMenu_OpenConversation,
                    this.mainFileDropdownMenu_SaveConversation,
                    this.mainFileDropdownMenu_SaveConversationAs,
                    this.mainFileDropdownMenu_SaveConversationAutoIncrement,
                    this.mainFileDropdownMenu_CloseConversation,
                    this.mainFileDropdownMenu_Exit
                }
            );

            this.Name = "mainFileDropdownMenu";
            this.ShortcutKeys = (Keys)(Keys.Alt | Keys.F);
            this.Size = new Size(35, 20);
            this.Text = "&File";

            this.mainFileDropdownMenu_SaveConversation.Enabled = false;
            this.mainFileDropdownMenu_SaveConversationAs.Enabled = false;
            this.mainFileDropdownMenu_SaveConversationAutoIncrement.Enabled = false;
            this.mainFileDropdownMenu_CloseConversation.Enabled = false;

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

        void mainFileDropdownMenu_NewConversation_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.NewConversation();
        }

        void mainFileDropdownMenu_OpenConversation_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.OpenConversation();
        }

        void mainFileDropdownMenu_SaveConversation_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.SaveConversation();
        }

        void mainFileDropdownMenu_SaveConversationAs_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.SaveConversationAs();
        }

        void mainFileDropdownMenu_SaveConversationAutoIncrement_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.SaveConversationAutoIncrement();
        }

        void mainFileDropdownMenu_CloseConversation_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.CloseConversation();
        }

        void mainFileDropdownMenu_Exit_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.ExitProgram();
        }

        public void ConversationOpened()
        {
            mainFileDropdownMenu_CloseConversation.Enabled = true;
            mainFileDropdownMenu_SaveConversationAs.Enabled = true;
            mainFileDropdownMenu_SaveConversationAutoIncrement.Enabled = true;
            mainFileDropdownMenu_SaveConversation.Enabled = true;
        }

        public void NoConversationsOpen()
        {
            mainFileDropdownMenu_CloseConversation.Enabled = false;
            mainFileDropdownMenu_SaveConversationAs.Enabled = false;
            mainFileDropdownMenu_SaveConversationAutoIncrement.Enabled = false;
            mainFileDropdownMenu_SaveConversation.Enabled = false;
        }

        public void DisableAutoIncrement()
        {
            mainFileDropdownMenu_SaveConversationAutoIncrement.Enabled = false;
        }

        public void EnableAutoIncrement()
        {
            mainFileDropdownMenu_SaveConversationAutoIncrement.Enabled = true;
        }
    }
}
