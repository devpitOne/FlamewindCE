using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace ConversationEditorGui
{
    public class MainMenuNodeMenu : ToolStripMenuItem
    {
        private ToolStripMenuItem mainNodeDropdownMenu_NewChildNode;
        private ToolStripMenuItem mainNodeDropdownMenu_EditNode;
        private ToolStripSeparator mainNodeDropdownMenu_Separator1;
        private ToolStripMenuItem mainNodeDropdownMenu_Undo;
        private ToolStripMenuItem mainNodeDropdownMenu_Redo;
        private ToolStripSeparator mainNodeDropdownMenu_Separator2;
        private ToolStripMenuItem mainNodeDropdownMenu_MoveNodeUp;
        private ToolStripMenuItem mainNodeDropdownMenu_MoveNodeDown;
        private ToolStripSeparator mainNodeDropdownMenu_Separator3;
        private ToolStripMenuItem mainNodeDropdownMenu_CopyNodeAndSubnodes;
        private ToolStripMenuItem mainNodeDropdownMenu_PasteNodeAndSubnodes;
        private ToolStripMenuItem mainNodeDropdownMenu_PasteNodeAsLink;
        private ToolStripSeparator mainNodeDropdownMenu_Separator4;
        private ToolStripMenuItem mainNodeDropdownMenu_FollowSelectedLink;
        private ToolStripMenuItem mainNodeDropdownMenu_MakeLinkReal;
        private ToolStripMenuItem mainNodeDropdownMenu_ExpandAllSubnodes;
        private ToolStripMenuItem mainNodeDropdownMenu_CollapseAllExceptThis;
        private ToolStripSeparator mainNodeDropdownMenu_Separator5;
        private ToolStripSeparator mainNodeDropdownMenu_Separator6;
        private ToolStripMenuItem mainNodeDropdownMenu_DeleteNodeAndSubnodes;

        private MainMenuStripClass myParentMenuStrip;

        public MainMenuNodeMenu(MainMenuStripClass creator)
        {
            myParentMenuStrip = creator;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.mainNodeDropdownMenu_NewChildNode = GetMenuOption
            (
                "mainNodeDropdownMenu_NewChildNode",
                "Create New Node As Child Of Selected Node",
                (Keys)(Keys.Control | Keys.Shift | Keys.N)
            );
            this.mainNodeDropdownMenu_NewChildNode.Click += new EventHandler(mainNodeDropdownMenu_NewChildNode_Click);

            this.mainNodeDropdownMenu_EditNode = GetMenuOption
            (
                "mainNodeDropdownMenu_EditNode",
                "Edit Text Of Selected Node",
                Keys.None
            );
            this.mainNodeDropdownMenu_EditNode.Click += new EventHandler(mainNodeDropdownMenu_EditNode_Click);

            this.mainNodeDropdownMenu_Separator1 = GetMenuSeparator("mainNodeDropdownMenu_Separator1");

            this.mainNodeDropdownMenu_Undo = GetMenuOption
            (
                "mainNodeDropdownMenu_Undo",
                "Undo Last Action",
                ((Keys)(Keys.Control | Keys.Z))
            );
            this.mainNodeDropdownMenu_Undo.Click += new EventHandler(mainNodeDropdownMenu_Undo_Click);

            this.mainNodeDropdownMenu_Redo = GetMenuOption
            (
                "mainNodeDropdownMenu_Redo",
                "Redo Last Undone Action",
                ((Keys)(Keys.Control | Keys.R))
            );
            this.mainNodeDropdownMenu_Redo.Click += new EventHandler(mainNodeDropdownMenu_Redo_Click);

            this.mainNodeDropdownMenu_Separator2 = GetMenuSeparator("mainNodeDropdownMenu_Separator2");

            this.mainNodeDropdownMenu_MoveNodeUp = GetMenuOption
            (
                "mainNodeDropdownMenu_MoveNodeUp",
                "Move Selected Node Up",
                ((Keys)(Keys.Control | Keys.Up))
            );
            this.mainNodeDropdownMenu_MoveNodeUp.Click += new EventHandler(mainNodeDropdownMenu_MoveNodeUp_Click);

            this.mainNodeDropdownMenu_MoveNodeDown = GetMenuOption
            (
                "mainNodeDropdownMenu_MoveNodeDown",
                "Move Selected Node Down",
                ((Keys)(Keys.Control | Keys.Down))
            );
            this.mainNodeDropdownMenu_MoveNodeDown.Click += new EventHandler(mainNodeDropdownMenu_MoveNodeDown_Click);

            this.mainNodeDropdownMenu_Separator3 = GetMenuSeparator("mainNodeDropdownMenu_Separator3");

            this.mainNodeDropdownMenu_CopyNodeAndSubnodes = GetMenuOption
            (
                "mainNodeDropdownMenu_CopyNodeAndSubnodes",
                "Copy Selected Node And Subnodes",
                ((Keys)(Keys.Control | Keys.C))
            );
            this.mainNodeDropdownMenu_CopyNodeAndSubnodes.Click += new EventHandler(mainNodeDropdownMenu_CopyNodeAndSubnodes_Click);

            this.mainNodeDropdownMenu_PasteNodeAndSubnodes = GetMenuOption
            (
                "mainNodeDropdownMenu_PasteNodeAndSubnodes",
                "Paste Node And Subnodes",
                ((Keys)(Keys.Control | Keys.V))
            );
            this.mainNodeDropdownMenu_PasteNodeAndSubnodes.Click += new EventHandler(mainNodeDropdownMenu_PasteNodeAndSubnodes_Click);

            this.mainNodeDropdownMenu_PasteNodeAsLink = GetMenuOption
            (
                "mainNodeDropdownMenu_PasteNodeAsLink",
                "Paste Node As Link",
                ((Keys)(Keys.Control | Keys.Shift | Keys.V))
            );
            this.mainNodeDropdownMenu_PasteNodeAsLink.Click += new EventHandler(mainNodeDropdownMenu_PasteNodeAsLink_Click);

            this.mainNodeDropdownMenu_Separator4 = GetMenuSeparator("mainNodeDropdownMenu_Separator4");

            this.mainNodeDropdownMenu_FollowSelectedLink = GetMenuOption
            (
                "mainNodeDropdownMenu_FollowSelectedLink",
                "Go To Target Of Selected Link",
                Keys.None
            );
            this.mainNodeDropdownMenu_FollowSelectedLink.Click += new EventHandler(mainNodeDropdownMenu_FollowSelectedLink_Click);

            this.mainNodeDropdownMenu_MakeLinkReal = GetMenuOption
            (
                "mainNodeDropdownMenu_MakeLinkReal",
                "Redirect All Links Of Selected Type To Selected Link",
                Keys.None
            );
            this.mainNodeDropdownMenu_MakeLinkReal.Click += new EventHandler(mainNodeDropdownMenu_MakeLinkReal_Click);

            this.mainNodeDropdownMenu_ExpandAllSubnodes = GetMenuOption
            (
                "mainNodeDropdownMenu_ExpandAllSubnodes",
                "Expand All Subnodes Of Selected Node",
                Keys.None
            );
            this.mainNodeDropdownMenu_ExpandAllSubnodes.Click += new EventHandler(mainNodeDropdownMenu_ExpandAllSubnodes_Click);

            this.mainNodeDropdownMenu_CollapseAllExceptThis = GetMenuOption
            (
                "mainNodeDropdownMenu_CollapseAllExceptThis",
                "Collapse All Nodes But Selected Node And Children",
                Keys.None
            );
            this.mainNodeDropdownMenu_CollapseAllExceptThis.Click += new EventHandler(mainNodeDropdownMenu_CollapseAllExceptThis_Click);

            this.mainNodeDropdownMenu_Separator5 = GetMenuSeparator("mainNodeDropdownMenu_Separator5");

            this.mainNodeDropdownMenu_Separator6 = GetMenuSeparator("mainNodeDropdownMenu_Separator6");

            this.mainNodeDropdownMenu_DeleteNodeAndSubnodes = GetMenuOption
            (
                "mainNodeDropdownMenu_DeleteNodeAndSubnodes",
                "Delete Selected Node And All Subnodes",
                Keys.None
            );
            this.mainNodeDropdownMenu_DeleteNodeAndSubnodes.Click += new EventHandler(mainNodeDropdownMenu_DeleteNodeAndSubnodes_Click);

            this.DropDownItems.AddRange
            (
                new ToolStripItem[]
                {
                    this.mainNodeDropdownMenu_NewChildNode,
                    this.mainNodeDropdownMenu_EditNode,
                    this.mainNodeDropdownMenu_Separator1,
                    this.mainNodeDropdownMenu_Undo,
                    this.mainNodeDropdownMenu_Redo,
                    this.mainNodeDropdownMenu_Separator2,
                    this.mainNodeDropdownMenu_CopyNodeAndSubnodes,
                    this.mainNodeDropdownMenu_PasteNodeAndSubnodes,
                    this.mainNodeDropdownMenu_PasteNodeAsLink,
                    this.mainNodeDropdownMenu_Separator3,
                    this.mainNodeDropdownMenu_MoveNodeUp,
                    this.mainNodeDropdownMenu_MoveNodeDown,
                    this.mainNodeDropdownMenu_Separator4,
                    this.mainNodeDropdownMenu_FollowSelectedLink,
                    this.mainNodeDropdownMenu_MakeLinkReal,
                    this.mainNodeDropdownMenu_ExpandAllSubnodes,
                    this.mainNodeDropdownMenu_CollapseAllExceptThis,
                    this.mainNodeDropdownMenu_Separator5,
                    this.mainNodeDropdownMenu_Separator6,
                    this.mainNodeDropdownMenu_DeleteNodeAndSubnodes
                }
            );

            this.Name = "mainNodeDropdownMenu";
            this.ShortcutKeys = ((Keys)(Keys.Alt | Keys.N));
            this.Size = new Size(44, 20);
            this.Text = "&Node";

            this.mainNodeDropdownMenu_CopyNodeAndSubnodes.Enabled = false;
            this.mainNodeDropdownMenu_PasteNodeAndSubnodes.Enabled = false;
            this.mainNodeDropdownMenu_PasteNodeAsLink.Enabled = false;
            this.mainNodeDropdownMenu_Undo.Enabled = false;
            this.mainNodeDropdownMenu_Redo.Enabled = false;
            this.mainNodeDropdownMenu_CollapseAllExceptThis.Enabled = false;
            this.mainNodeDropdownMenu_ExpandAllSubnodes.Enabled = false;
            this.mainNodeDropdownMenu_MakeLinkReal.Enabled = false;
            this.mainNodeDropdownMenu_FollowSelectedLink.Enabled = false;
        }

        void mainNodeDropdownMenu_NewChildNode_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.NewChildNode();
        }

        void mainNodeDropdownMenu_EditNode_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.EditNode();
        }

        void mainNodeDropdownMenu_Undo_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.Undo();
        }

        void mainNodeDropdownMenu_Redo_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.Redo();
        }

        void mainNodeDropdownMenu_MoveNodeUp_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.MoveNodeUp();
        }

        void mainNodeDropdownMenu_MoveNodeDown_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.MoveNodeDown();
        }

        void mainNodeDropdownMenu_CopyNodeAndSubnodes_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.CopyNodeAndSubnodes();
        }

        void mainNodeDropdownMenu_PasteNodeAndSubnodes_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.PasteNodeAndSubnodes();
        }

        void mainNodeDropdownMenu_PasteNodeAsLink_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.PasteNodeAsLink();
        }

        void mainNodeDropdownMenu_FollowSelectedLink_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.FollowSelectedLink();
        }

        void mainNodeDropdownMenu_MakeLinkReal_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.MakeLinkReal();
        }

        void mainNodeDropdownMenu_ExpandAllSubnodes_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.ExpandAllSubnodes();
        }

        void mainNodeDropdownMenu_CollapseAllExceptThis_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.CollapseAllExceptThis();
        }

        void mainNodeDropdownMenu_DeleteNodeAndSubnodes_Click(object sender, EventArgs e)
        {
            myParentMenuStrip.DeleteNodeAndSubnodes();
        }

        private static ToolStripMenuItem GetMenuOption(string name, string text, Keys shortcuts)
        {
            ToolStripMenuItem newToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem.Size = new Size(317, 22);
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
            newToolStripMenuSeparator.Size = new Size(314, 6);
            newToolStripMenuSeparator.Name = name;
            return newToolStripMenuSeparator;
        }

        public void PasteTextOnly()
        {
            this.mainNodeDropdownMenu_PasteNodeAsLink.Enabled = false;
            this.mainNodeDropdownMenu_PasteNodeAndSubnodes.Enabled = true;
            this.mainNodeDropdownMenu_PasteNodeAndSubnodes.Text = "Paste Text From Clipboard";
        }

        public void EnablePaste(bool linksAllowed)
        {
            this.mainNodeDropdownMenu_PasteNodeAndSubnodes.Enabled = true;
            this.mainNodeDropdownMenu_PasteNodeAsLink.Enabled = linksAllowed;
            this.mainNodeDropdownMenu_PasteNodeAndSubnodes.Text = "Paste Node And Subnodes";
        }

        public void DisablePaste()
        {
            this.mainNodeDropdownMenu_PasteNodeAndSubnodes.Enabled = false;
            this.mainNodeDropdownMenu_PasteNodeAsLink.Enabled = false;
            this.mainNodeDropdownMenu_PasteNodeAndSubnodes.Text = "Paste Node And Subnodes";
        }

        public void EnableCopy()
        {
            this.mainNodeDropdownMenu_CopyNodeAndSubnodes.Enabled = true;
        }

        public void DisableCopy()
        {
            this.mainNodeDropdownMenu_CopyNodeAndSubnodes.Enabled = false;
        }

        public void EnableDelete()
        {
            this.mainNodeDropdownMenu_DeleteNodeAndSubnodes.Enabled = true;
        }

        public void DisableDelete()
        {
            this.mainNodeDropdownMenu_DeleteNodeAndSubnodes.Enabled = false;
        }

        public void EnableExpandCollapse()
        {
            this.mainNodeDropdownMenu_ExpandAllSubnodes.Enabled = true;
            this.mainNodeDropdownMenu_CollapseAllExceptThis.Enabled = true;
        }

        public void DisableExpandCollapse()
        {
            this.mainNodeDropdownMenu_ExpandAllSubnodes.Enabled = false;
            this.mainNodeDropdownMenu_CollapseAllExceptThis.Enabled = false;
        }
        
        public void GotUndos()
        {
            this.mainNodeDropdownMenu_Undo.Enabled = true;
        }

        public void GotRedos()
        {
            this.mainNodeDropdownMenu_Redo.Enabled = true;
        }

        public void NoUndos()
        {
            this.mainNodeDropdownMenu_Undo.Enabled = false;
        }

        public void NoRedos()
        {
            this.mainNodeDropdownMenu_Redo.Enabled = false;
        }
        
        public void LinkSelected()
        {
            this.mainNodeDropdownMenu_FollowSelectedLink.Enabled = true;
            this.mainNodeDropdownMenu_MakeLinkReal.Enabled = true;
            this.mainNodeDropdownMenu_NewChildNode.Enabled = false;
        }
        
        public void NonLinkSelected()
        {
            this.mainNodeDropdownMenu_FollowSelectedLink.Enabled = false;
            this.mainNodeDropdownMenu_MakeLinkReal.Enabled = false;
            this.mainNodeDropdownMenu_NewChildNode.Enabled = true;
        }
    }
}
