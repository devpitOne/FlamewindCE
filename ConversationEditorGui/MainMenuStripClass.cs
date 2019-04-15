using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ConversationEditorGui
{
    public class MainMenuStripClass : MenuStrip
    {
        private Form1 myParentForm;

        private MainMenuFileMenu mainFileDropdownMenu;
        private MainMenuConversationMenu mainConversationDropdownMenu;
        private MainMenuNodeMenu mainNodeDropdownMenu;
        private MainMenuAboutMenu mainAboutDropdownMenu;

        public MainMenuStripClass(Form1 sender)
        {
            myParentForm = sender;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            mainFileDropdownMenu = new MainMenuFileMenu(this);
            mainConversationDropdownMenu = new MainMenuConversationMenu(this);
            mainNodeDropdownMenu = new MainMenuNodeMenu(this);
            mainAboutDropdownMenu = new MainMenuAboutMenu(this);

            this.Items.AddRange
            (
                new ToolStripItem[] 
                {
                    this.mainFileDropdownMenu,
                    this.mainConversationDropdownMenu,
                    this.mainNodeDropdownMenu,
                    this.mainAboutDropdownMenu
                }
            );
            this.Location = new Point(0, 0);
            this.Name = "mainDropdownMenus";
            this.Size = new Size(917, 24);

            this.mainConversationDropdownMenu.Enabled = false;
            this.mainNodeDropdownMenu.Enabled = false;
        }

        public void PasteTextOnly()
        {
            if (myParentForm.GetSelectedSection() == SelectedSection.Comments || myParentForm.GetSelectedSection() == SelectedSection.TextBox)
            {
                mainNodeDropdownMenu.PasteTextOnly();
            }
            else
            {
                DisablePaste();
            }
        }
        
        public void EnablePaste()
        {
            if (myParentForm.GetSelectedSection() == SelectedSection.TextBox || myParentForm.GetSelectedSection() == SelectedSection.Comments)
            {
                this.mainNodeDropdownMenu.PasteTextOnly();
            }
            else
            {
                if (((ClipboardNode)Clipboard.GetData(ClipboardNode.format.Name)).linkTo > 0)
                {
                    this.mainNodeDropdownMenu.EnablePaste(false);
                }
                else
                {
                    this.mainNodeDropdownMenu.EnablePaste(true);
                }
            }

        }

        public void DisablePaste()
        {
            this.mainNodeDropdownMenu.DisablePaste();
        }

        public void TreeTabOpen()
        {
            this.mainConversationDropdownMenu.Enabled = true;
            this.mainNodeDropdownMenu.Enabled = true;
            this.mainFileDropdownMenu.ConversationOpened();
        }

        public void NoTabsOpen()
        {
            this.mainConversationDropdownMenu.Enabled = false;
            this.mainNodeDropdownMenu.Enabled = false;
        }

        public void LinkSelected()
        {
            this.mainNodeDropdownMenu.LinkSelected();
        }

        public void NonLinkSelected()
        {
            this.mainNodeDropdownMenu.NonLinkSelected();
        }

        public void EnableCopy()
        {
            this.mainNodeDropdownMenu.EnableCopy();
        }

        public void DisableCopy()
        {
            this.mainNodeDropdownMenu.DisableCopy();
        }

        public void EnableDelete()
        {
            this.mainNodeDropdownMenu.EnableDelete();
        }

        public void DisableDelete()
        {
            this.mainNodeDropdownMenu.DisableDelete();
        }

        public void EnableExpandCollapse()
        {
            this.mainNodeDropdownMenu.EnableExpandCollapse();
        }

        public void DisableExpandCollapse()
        {
            this.mainNodeDropdownMenu.DisableExpandCollapse();
        }

        public void ExpandAllSubnodes()
        {
            myParentForm.ExpandAllSubnodes();
        }

        public void CollapseAllExceptThis()
        {
            myParentForm.CollapseAllExceptThis();
        }

        public void ExpandAllNodes()
        {
            myParentForm.ExpandAllNodes();
        }

        public void CollapseAllNodes()
        {
            myParentForm.CollapseAllNodes();
        }
        
        public void DisableAutoIncrement()
        {
            this.mainFileDropdownMenu.DisableAutoIncrement();
        }

        public void EnableAutoIncrement()
        {
            this.mainFileDropdownMenu.EnableAutoIncrement();
        }

        public void NewConversation()
        {
            myParentForm.NewConversation();
        }

        public void OpenConversation()
        {
            myParentForm.OpenConversation();
        }

        public void SaveConversation()
        {
            myParentForm.SaveActiveConversation();
        }

        public void SaveConversationAs()
        {
            myParentForm.SaveActiveConversationAs();
        }

        public void SaveConversationAutoIncrement()
        {
            myParentForm.SaveActiveConversationAutoIncrement();
        }

        public void CloseConversation()
        {
            myParentForm.CloseCurrentTab();
        }

        public void ExitProgram()
        {
            myParentForm.Close();
        }

        public void TestConversation()
        {
            myParentForm.TestConversation();
        }

        public void FollowSelectedLink()
        {
            myParentForm.FollowSelectedLink();
        }

        public void MakeLinkReal()
        {
            myParentForm.MakeLinkReal();
        }

        public void CopyConversationAsText()
        {
            myParentForm.CopyConversationAsText();
        }

        public void NewChildNode()
        {
            myParentForm.NewChildNode();
        }

        public void EditNode()
        {
            myParentForm.EditNode();
        }

        public void Undo()
        {
            myParentForm.Undo();
        }

        public void Redo()
        {
            myParentForm.Redo();
        }
        
        public void MoveNodeUp()
        {
            myParentForm.MoveNodeUp();
        }

        public void MoveNodeDown()
        {
            myParentForm.MoveNodeDown();
        }

        public void CopyNodeAndSubnodes()
        {
            myParentForm.CopyNodeAndSubnodes();
        }

        public void PasteNodeAndSubnodes()
        {
            myParentForm.PasteNodeAndSubnodes();
        }

        public void PasteNodeAsLink()
        {
            myParentForm.PasteNodeAsLink();
        }

        public void DeleteNodeAndSubnodes()
        {
            myParentForm.DeleteNodeAndSubnodes();
        }

        public void GotUndos()
        {
            mainNodeDropdownMenu.GotUndos();
        }

        public void GotRedos()
        {
            mainNodeDropdownMenu.GotRedos();
        }

        public void NoUndos()
        {
            mainNodeDropdownMenu.NoUndos();
        }

        public void NoRedos()
        {
            mainNodeDropdownMenu.NoRedos();
        }
    }
}