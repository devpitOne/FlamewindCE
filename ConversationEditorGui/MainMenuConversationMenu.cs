using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace ConversationEditorGui
{
    public class MainMenuConversationMenu : ToolStripMenuItem
    {
        private ToolStripMenuItem mainConversationDropdownMenu_TestConversation;
        private ToolStripSeparator mainConversationDropdownMenu_Separator1;
        private ToolStripMenuItem mainConversationDropdownMenu_ExpandAllNodes;
        private ToolStripMenuItem mainConversationDropdownMenu_CollapseAllNodes;
        private ToolStripSeparator mainConversationDropdownMenu_Separator2;
        private ToolStripMenuItem mainConversationDropdownMenu_CopyConversationAsText;

        private MainMenuStripClass myParentMenuStrip;

        public MainMenuConversationMenu(MainMenuStripClass creator)
        {
            myParentMenuStrip = creator;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.mainConversationDropdownMenu_TestConversation = GetMenuOption
            (
                "mainConversationDropdownMenu_TestConversation",
                "Test Conversation",
                Keys.F5
            );
            this.mainConversationDropdownMenu_TestConversation.Click += new EventHandler(mainConversationDropdownMenu_TestConversation_Click);

            this.mainConversationDropdownMenu_Separator1 = GetMenuSeparator("mainConversationDropdownMenu_Separator1");

            this.mainConversationDropdownMenu_ExpandAllNodes = GetMenuOption
            (
                "mainConversationDropdownMenu_ExpandAllNodes",
                "Expand All Nodes In Conversation",
                Keys.F2
            );
            this.mainConversationDropdownMenu_ExpandAllNodes.Click += new EventHandler(mainConversationDropdownMenu_ExpandAllNodes_Click);

            this.mainConversationDropdownMenu_CollapseAllNodes = GetMenuOption
            (
                "mainConversationDropdownMenu_CollapseAllNodes",
                "Collapse All Nodes In Conversation",
                Keys.F3
            );
            this.mainConversationDropdownMenu_CollapseAllNodes.Click += new EventHandler(mainConversationDropdownMenu_CollapseAllNodes_Click);

            this.mainConversationDropdownMenu_Separator2 = GetMenuSeparator("mainConversationDropdownMenu_Separator2");

            this.mainConversationDropdownMenu_CopyConversationAsText = GetMenuOption
            (
                "mainConversationDropdownMenu_CopyConversationAsText",
                "Copy Conversation As Text",
                Keys.None
            );
            this.mainConversationDropdownMenu_CopyConversationAsText.Click += new EventHandler(mainConversationDropdownMenu_CopyConversationAsText_Click);

            this.DropDownItems.AddRange
            (
                new ToolStripItem[]
                {
                    this.mainConversationDropdownMenu_TestConversation,
                    this.mainConversationDropdownMenu_Separator1,
                    this.mainConversationDropdownMenu_ExpandAllNodes,
                    this.mainConversationDropdownMenu_CollapseAllNodes,
                    this.mainConversationDropdownMenu_Separator2,
                    this.mainConversationDropdownMenu_CopyConversationAsText
                }
            );

            this.Name = "mainConversationDropdownMenu";
            this.ShortcutKeys = ((Keys)(Keys.Alt | Keys.C));
            this.Size = new Size(83, 20);
            this.Text = "&Conversation";

        }

        void mainConversationDropdownMenu_TestConversation_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.TestConversation();
        }

        void mainConversationDropdownMenu_ExpandAllNodes_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.ExpandAllNodes();
        }

        void mainConversationDropdownMenu_CollapseAllNodes_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.CollapseAllNodes();
        }

        void mainConversationDropdownMenu_CopyConversationAsText_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.CopyConversationAsText();
        }

        private static ToolStripMenuItem GetMenuOption(string name, string text, Keys shortcuts)
        {
            ToolStripMenuItem newToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem.Size = new Size(331, 22);
            newToolStripMenuItem.Name = name;
            newToolStripMenuItem.Text = text;
            if (shortcuts != Keys.None)
            {
                newToolStripMenuItem.ShortcutKeys = shortcuts;
            }
            return newToolStripMenuItem;
        }

        private static ToolStripSeparator GetMenuSeparator(string name)
        {
            ToolStripSeparator newToolStripMenuSeparator = new ToolStripSeparator();
            newToolStripMenuSeparator.Size = new Size(318, 6);
            newToolStripMenuSeparator.Name = name;
            return newToolStripMenuSeparator;
        }
    }
}
