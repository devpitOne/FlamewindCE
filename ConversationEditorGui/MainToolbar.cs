using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ConversationEditorGui
{
    public class MainToolbar : ToolStrip
    {
        Form1 myParentForm;
        ToolStripButton mainToolbar_NewConversation;
        ToolStripButton mainToolbar_OpenConversation;
        ToolStripButton mainToolbar_SaveConversation;
        ToolStripButton mainToolbar_SaveConversationAs;
        ToolStripButton mainToolbar_SaveConversationAutoIncremented;
        ToolStripButton mainToolbar_TestConversation;
        ToolStripSeparator mainToolbar_Separator1;
        ToolStripButton mainToolbar_Undo;
        ToolStripButton mainToolbar_Redo;
        ToolStripSeparator mainToolbar_Separator2;
        ToolStripButton mainToolbar_NewChildNode;
        ToolStripButton mainToolbar_MoveNodeUp;
        ToolStripButton mainToolbar_MoveNodeDown;
        ToolStripSeparator mainToolbar_Separator3;
        ToolStripButton mainToolbar_CopyNode;
        ToolStripButton mainToolbar_CopyConversationAsText;
        ToolStripButton mainToolbar_PasteNode;
        ToolStripButton mainToolbar_PasteNodeAsLink;
        ToolStripSeparator mainToolbar_Separator4;
        ToolStripSeparator mainToolbar_Separator5;
        ToolStripButton mainToolbar_DeleteNode;

        public MainToolbar(Form1 sender)
        {
            myParentForm = sender;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.GripStyle = ToolStripGripStyle.Hidden;
            this.Location = new Point(0, 24);
            this.Name = "mainToolbar";
            this.Size = new Size(917, 25);

            mainToolbar_NewConversation = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.NewConversationIcon,
                "mainToolbar_NewConversation",
                "New Conversation"
            );
            mainToolbar_NewConversation.Click += new EventHandler(mainToolbar_NewConversation_Click);

            mainToolbar_OpenConversation = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.OpenConversation,
                "mainToolbar_OpenConversation",
                "Open Conversation"
            );
            mainToolbar_OpenConversation.Click += new EventHandler(mainToolbar_OpenConversation_Click);

            mainToolbar_SaveConversation = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.SaveConversation,
                "mainToolbar_SaveConversation",
                "Save Conversation"
            );
            mainToolbar_SaveConversation.Click += new EventHandler(mainToolbar_SaveConversation_Click);

            mainToolbar_SaveConversationAs = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.SaveConversationAs,
                "mainToolbar_SaveConversationAs",
                "Save Conversation As..."
            );
            mainToolbar_SaveConversationAs.Click += new EventHandler(mainToolbar_SaveConversationAs_Click);

            mainToolbar_SaveConversationAutoIncremented = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.SaveConversationAutoInc,
                "mainToolbar_SaveConversationAutoIncremented",
                "Save Conversation AutoIncremented"
            );
            mainToolbar_SaveConversationAutoIncremented.Click += new EventHandler(mainToolbar_SaveConversationAutoIncremented_Click);

            mainToolbar_TestConversation = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.TestConversation,
                "mainToolbar_TestConversation",
                "Test Conversation"
            );
            mainToolbar_TestConversation.Click += new EventHandler(mainToolbar_TestConversation_Click);

            mainToolbar_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            mainToolbar_Separator1.Size = new Size(6, 25);

            mainToolbar_Undo = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.Undo,
                "mainToolbar_Undo",
                "Undo"
            );
            mainToolbar_Undo.Click += new EventHandler(mainToolbar_Undo_Click);

            mainToolbar_Redo = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.Redo,
                "mainToolbar_Redo",
                "Redo"
            );
            mainToolbar_Redo.Click += new EventHandler(mainToolbar_Redo_Click);

            mainToolbar_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            mainToolbar_Separator2.Size = new Size(6, 25);

            mainToolbar_NewChildNode = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.NewChildNode,
                "mainToolbar_NewChildNode",
                "Create New Node As Child Of Selected Node"
            );
            mainToolbar_NewChildNode.Click += new EventHandler(mainToolbar_NewChildNode_Click);

            mainToolbar_MoveNodeUp = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.MoveNodeUp,
                "mainToolbar_MoveNodeUp",
                "Move Selected Node Up"
            );
            mainToolbar_MoveNodeUp.Click += new EventHandler(mainToolbar_MoveNodeUp_Click);

            mainToolbar_MoveNodeDown = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.MoveNodeDown,
                "mainToolbar_MoveNodeDown",
                "Move Selected Node Down"
            );
            mainToolbar_MoveNodeDown.Click += new EventHandler(mainToolbar_MoveNodeDown_Click);

            mainToolbar_Separator3 = new System.Windows.Forms.ToolStripSeparator();
            mainToolbar_Separator3.Size = new Size(6, 25);

            mainToolbar_CopyNode = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.CopyNodes,
                "mainToolbar_CopyNode",
                "Copy Selected Node And Subnodes"
            );
            mainToolbar_CopyNode.Click += new EventHandler(mainToolbar_CopyNode_Click);

            mainToolbar_CopyConversationAsText = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.CopyConversationToText,
                "mainToolbar_CopyConversationAsText",
                "Copy Conversation As Text"
            );
            mainToolbar_CopyConversationAsText.Click += new EventHandler(mainToolbar_CopyConversationAsText_Click);

            mainToolbar_PasteNode = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.PasteNodes,
                "mainToolbar_PasteNode",
                "Paste Node And Subnodes From Clipboard"
            );
            mainToolbar_PasteNode.Click += new EventHandler(mainToolbar_PasteNode_Click);

            mainToolbar_PasteNodeAsLink = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.PasteNodeLink,
                "mainToolbar_PasteNodeAsLink",
                "Paste Node As Link From Clipboard"
            );
            mainToolbar_PasteNodeAsLink.Click += new EventHandler(mainToolbar_PasteNodeAsLink_Click);

            mainToolbar_Separator4 = new System.Windows.Forms.ToolStripSeparator();
            mainToolbar_Separator4.Size = new Size(6, 25);

            mainToolbar_Separator5 = new System.Windows.Forms.ToolStripSeparator();
            mainToolbar_Separator5.Size = new Size(6, 25);

            mainToolbar_DeleteNode = GetButton
            (
                global::ConversationEditorGui.Properties.Resources.DeleteNode,
                "mainToolbar_DeleteNode",
                "Delete Node And All Subnodes"
            );
            mainToolbar_DeleteNode.Click += new EventHandler(mainToolbar_DeleteNode_Click);

            this.Items.AddRange
            (
                new ToolStripItem[]
                {
                    mainToolbar_NewConversation,
                    mainToolbar_OpenConversation,
                    mainToolbar_SaveConversation,
                    mainToolbar_SaveConversationAs,
                    mainToolbar_SaveConversationAutoIncremented,
                    mainToolbar_TestConversation,
                    mainToolbar_Separator1,
                    mainToolbar_Undo,
                    mainToolbar_Redo,
                    mainToolbar_Separator2,
                    mainToolbar_NewChildNode,
                    mainToolbar_MoveNodeUp,
                    mainToolbar_MoveNodeDown,
                    mainToolbar_Separator3,
                    mainToolbar_CopyNode,
                    mainToolbar_CopyConversationAsText,
                    mainToolbar_PasteNode,
                    mainToolbar_PasteNodeAsLink,
                    mainToolbar_Separator4,
                    mainToolbar_Separator5,
                    mainToolbar_DeleteNode
                }
            );

            mainToolbar_CopyConversationAsText.Enabled = false;
            mainToolbar_CopyNode.Enabled = false;
            mainToolbar_DeleteNode.Enabled = false;
            mainToolbar_DeleteNode.Enabled = true;
            mainToolbar_MoveNodeDown.Enabled = false;
            mainToolbar_MoveNodeUp.Enabled = false;
            mainToolbar_NewChildNode.Enabled = false;
            mainToolbar_PasteNode.Enabled = false;
            mainToolbar_PasteNodeAsLink.Enabled = false;
            mainToolbar_SaveConversation.Enabled = false;
            mainToolbar_SaveConversationAs.Enabled = false;
            mainToolbar_SaveConversationAutoIncremented.Enabled = false;
            mainToolbar_TestConversation.Enabled = false;
            mainToolbar_Undo.Enabled = false;
            mainToolbar_Redo.Enabled = false;
        }

        void mainToolbar_NewConversation_Click(object sender, EventArgs e)
        {
            myParentForm.NewConversation();
        }

        void mainToolbar_OpenConversation_Click(object sender, EventArgs e)
        {
            myParentForm.OpenConversation();
        }

        void mainToolbar_SaveConversation_Click(object sender, EventArgs e)
        {
            myParentForm.SaveActiveConversation();
        }

        void mainToolbar_SaveConversationAs_Click(object sender, EventArgs e)
        {
            myParentForm.SaveActiveConversationAs();
        }

        void mainToolbar_SaveConversationAutoIncremented_Click(object sender, EventArgs e)
        {
            myParentForm.SaveActiveConversationAutoIncrement();
        }

        void mainToolbar_Undo_Click(object sender, EventArgs e)
        {
            myParentForm.Undo();
        }

        void mainToolbar_Redo_Click(object sender, EventArgs e)
        {
            myParentForm.Redo();
        }

        void mainToolbar_TestConversation_Click(object sender, EventArgs e)
        {
            myParentForm.TestConversation();
        }

        void mainToolbar_CopyConversationAsText_Click(object sender, EventArgs e)
        {
            myParentForm.CopyConversationAsText();
        }

        void mainToolbar_NewChildNode_Click(object sender, EventArgs e)
        {
            myParentForm.NewChildNode();
        }

        void mainToolbar_MoveNodeUp_Click(object sender, EventArgs e)
        {
            myParentForm.MoveNodeUp();
        }

        void mainToolbar_MoveNodeDown_Click(object sender, EventArgs e)
        {
            myParentForm.MoveNodeDown();
        }

        void mainToolbar_CopyNode_Click(object sender, EventArgs e)
        {
            myParentForm.CopyNodeAndSubnodes();
        }

        void mainToolbar_PasteNode_Click(object sender, EventArgs e)
        {
            myParentForm.PasteNodeAndSubnodes();
        }

        void mainToolbar_PasteNodeAsLink_Click(object sender, EventArgs e)
        {
            myParentForm.PasteNodeAsLink();
        }

        void mainToolbar_DeleteNode_Click(object sender, EventArgs e)
        {
            myParentForm.DeleteNodeAndSubnodes();
        }

        private static ToolStripButton GetButton(Image icon, String name, String toolTipText)
        {
            ToolStripButton toolStripButton = new ToolStripButton();
            toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton.ImageTransparentColor = Color.Lime;
            toolStripButton.Size = new Size(23, 22);
            toolStripButton.Name = name;
            toolStripButton.Image = icon;
            toolStripButton.ToolTipText = toolTipText;
            return toolStripButton;
        }

        public void EnablePaste()
        {
            if (myParentForm.GetSelectedSection() == SelectedSection.Comments || myParentForm.GetSelectedSection() == SelectedSection.TextBox)
            {
                PasteTextOnly();
            }
            else
            {
                EnablePasteNode();
                if (((ClipboardNode)Clipboard.GetData(ClipboardNode.format.Name)).linkTo > 0)
                {
                    this.mainToolbar_PasteNodeAsLink.Enabled = false;
                }
                else
                {
                    this.mainToolbar_PasteNodeAsLink.Enabled = true;
                }
            }
        }

        public void DisablePaste()
        {
            this.mainToolbar_PasteNode.Enabled = false;
            this.mainToolbar_PasteNodeAsLink.Enabled = false;
            this.mainToolbar_PasteNode.ToolTipText = "Paste Node And Subnodes";
        }

        public void PasteTextOnly()
        {
            if (myParentForm.GetSelectedSection() == SelectedSection.Comments || myParentForm.GetSelectedSection() == SelectedSection.TextBox)
            {
                this.mainToolbar_PasteNodeAsLink.Enabled = false;
                this.mainToolbar_PasteNode.Enabled = true;
                this.mainToolbar_PasteNode.ToolTipText = "Paste Text From Clipboard";
            }
            else
            {
                DisablePaste();
            }
        }

        private void EnablePasteNode()
        {
            this.mainToolbar_PasteNode.Enabled = true;
            this.mainToolbar_PasteNode.ToolTipText = "Paste Node And Subnodes";
        }

        public void EnableCreate(bool enabled)
        {
            this.mainToolbar_NewChildNode.Enabled = enabled;
        }

        public void EnableCopy()
        {
            this.mainToolbar_CopyNode.Enabled = true;
        }

        public void DisableCopy()
        {
            this.mainToolbar_CopyNode.Enabled = false;
        }

        public void EnableDelete()
        {
            this.mainToolbar_DeleteNode.Enabled = true;
        }

        public void DisableDelete()
        {
            this.mainToolbar_DeleteNode.Enabled = false;
        }
        
        public void TreeTabOpen()
        {
            mainToolbar_CopyConversationAsText.Enabled = true;
            mainToolbar_NewChildNode.Enabled = true;
            mainToolbar_SaveConversation.Enabled = true;
            EnableAutoIncrement();
            EnableSaveAs();
            mainToolbar_TestConversation.Enabled = true;
        }

        public void NoTabsOpen()
        {
            mainToolbar_CopyConversationAsText.Enabled = false;
            mainToolbar_CopyNode.Enabled = false;
            mainToolbar_DeleteNode.Enabled = false;
            mainToolbar_MoveNodeDown.Enabled = false;
            mainToolbar_MoveNodeUp.Enabled = false;
            mainToolbar_NewChildNode.Enabled = false;
            mainToolbar_PasteNode.Enabled = false;
            mainToolbar_PasteNodeAsLink.Enabled = false;
            mainToolbar_SaveConversation.Enabled = false;
            mainToolbar_TestConversation.Enabled = false;
            mainToolbar_Undo.Enabled = false;
            mainToolbar_Redo.Enabled = false;
            DisableAutoIncrement();
            DisableSaveAs();
        }

        public void EnableAutoIncrement()
        {
            mainToolbar_SaveConversationAutoIncremented.Enabled = true;
        }

        public void DisableAutoIncrement()
        {
            mainToolbar_SaveConversationAutoIncremented.Enabled = false;
        }

        public void EnableSaveAs()
        {
            mainToolbar_SaveConversationAs.Enabled = true;
        }

        public void DisableSaveAs()
        {
            mainToolbar_SaveConversationAs.Enabled = false;
        }

        public void GotUndos()
        {
            mainToolbar_Undo.Enabled = true;
        }

        public void GotRedos()
        {
            mainToolbar_Redo.Enabled = true;
        }

        public void NoUndos()
        {
            mainToolbar_Undo.Enabled = false;
        }

        public void NoRedos()
        {
            mainToolbar_Redo.Enabled = false;
        }
    }
}
