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
            mainConversationDropdownMenu_TestConversation = GetMenuOption
            (
                "mainConversationDropdownMenu_TestConversation",
                "Test Conversation",
                Keys.F5
            );
            mainConversationDropdownMenu_TestConversation.Click += new EventHandler(mainConversationDropdownMenu_TestConversation_Click);

            mainConversationDropdownMenu_Separator1 = GetMenuSeparator("mainConversationDropdownMenu_Separator1");

            mainConversationDropdownMenu_ExpandAllNodes = GetMenuOption
            (
                "mainConversationDropdownMenu_ExpandAllNodes",
                "Expand All Nodes In Conversation",
                Keys.F2
            );
            mainConversationDropdownMenu_ExpandAllNodes.Click += new EventHandler(mainConversationDropdownMenu_ExpandAllNodes_Click);

            mainConversationDropdownMenu_CollapseAllNodes = GetMenuOption
            (
                "mainConversationDropdownMenu_CollapseAllNodes",
                "Collapse All Nodes In Conversation",
                Keys.F3
            );
            mainConversationDropdownMenu_CollapseAllNodes.Click += new EventHandler(mainConversationDropdownMenu_CollapseAllNodes_Click);

            mainConversationDropdownMenu_Separator2 = GetMenuSeparator("mainConversationDropdownMenu_Separator2");

            mainConversationDropdownMenu_CopyConversationAsText = GetMenuOption
            (
                "mainConversationDropdownMenu_CopyConversationAsText",
                "Copy Conversation As Text",
                Keys.None
            );
            mainConversationDropdownMenu_CopyConversationAsText.Click += new EventHandler(mainConversationDropdownMenu_CopyConversationAsText_Click);

            DropDownItems.AddRange
            (
                new ToolStripItem[]
                {
                    mainConversationDropdownMenu_TestConversation,
                    mainConversationDropdownMenu_Separator1,
                    mainConversationDropdownMenu_ExpandAllNodes,
                    mainConversationDropdownMenu_CollapseAllNodes,
                    mainConversationDropdownMenu_Separator2,
                    mainConversationDropdownMenu_CopyConversationAsText
                }
            );

            Name = "mainConversationDropdownMenu";
            ShortcutKeys = (Keys.Alt | Keys.C);
            Size = new Size(83, 20);
            Text = "&Conversation";

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
            ToolStripMenuItem newToolStripMenuItem = new ToolStripMenuItem
            {
                Size = new Size(331, 22),
                Name = name,
                Text = text
            };
            if (shortcuts != Keys.None)
            {
                newToolStripMenuItem.ShortcutKeys = shortcuts;
            }
            return newToolStripMenuItem;
        }

        private static ToolStripSeparator GetMenuSeparator(string name)
        {
            ToolStripSeparator newToolStripMenuSeparator = new ToolStripSeparator
            {
                Size = new Size(318, 6),
                Name = name
            };
            return newToolStripMenuSeparator;
        }
    }
}
