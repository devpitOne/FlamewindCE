using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using Editor;

namespace ConversationEditorGui
{
    public class LinkedTree : TreeView
    {
        private System.ComponentModel.IContainer components;
        public LinkedTextBox myTextBox = null;
        public LinkedCommentsBox myCommentsBox = null;
        public TreeConversation myTreeConversation = null;
        public TreeTab myParentTab = null;
        public ConversationNode selectedNode
        {
            get
            {
                return (ConversationNode)SelectedNode;
            }
            set
            {
                SelectedNode = value;
            }
        }

        #region Context Menu
        private ToolStripMenuItem addNewChildNodeMenuItem;
        private ToolStripMenuItem editTextMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem moveUpMenuItem;
        private ToolStripMenuItem moveDownMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem undoMenuItem;
        private ToolStripMenuItem redoMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem copyMenuItem;
        private ToolStripMenuItem pasteMenuItem;
        private ToolStripMenuItem pasteLinkMenuItem;
        private ToolStripMenuItem followLinkMenuItem;
        private ToolStripMenuItem makeLinkRealMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem deleteMenuItem;
        public ContextMenuStrip treeContextMenu;
        #endregion

        #region Paint Event
        Bitmap internalBitmap = null;
        Graphics internalGraphics = null;

        private void DisposeInternal()
        {
            if (internalGraphics != null)
                internalGraphics.Dispose();
            if (internalBitmap != null)
                internalBitmap.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                DisposeInternal();
            base.Dispose(disposing);
        }

        protected override void OnResize(EventArgs e)
        {
            if (internalBitmap == null || internalBitmap.Width != Width || internalBitmap.Height != Height)
            {
                if (Width != 0 && Height != 0)
                {
                    DisposeInternal();
                    internalBitmap = new Bitmap(Width, Height);
                    internalGraphics = Graphics.FromImage(internalBitmap);
                }
            }
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_PAINT = 0x000F;
            const int WM_PRINTCLIENT = 0x0318;
            const int WM_ERASEBKGND = 0x0014;

            switch (message.Msg)
            {
                case WM_ERASEBKGND:
                    return;
                case WM_PAINT:
                    if (internalGraphics == null)
                        OnResize(EventArgs.Empty);

                    Win32.RECT updateRect = new Win32.RECT();
                    if (Win32.GetUpdateRect(message.HWnd, ref updateRect, false) == 0)
                        break;
                    Win32.PAINTSTRUCT paintStruct = new Win32.PAINTSTRUCT();
                    IntPtr screenHdc = Win32.BeginPaint(message.HWnd, ref paintStruct);
                    using (Graphics screenGraphics = Graphics.FromHdc(screenHdc))
                    {
                        IntPtr hdc = internalGraphics.GetHdc();
                        Message printClientMessage = Message.Create(Handle, WM_PRINTCLIENT, hdc, IntPtr.Zero);
                        DefWndProc(ref printClientMessage);
                        internalGraphics.ReleaseHdc(hdc);

                        OnPaint(new PaintEventArgs(internalGraphics, Rectangle.FromLTRB(
                            updateRect.left,
                            updateRect.top,
                            updateRect.right,
                            updateRect.bottom
                            )));

                        screenGraphics.DrawImage(internalBitmap, 0, 0);
                    }

                    Win32.EndPaint(message.HWnd, ref paintStruct);
                    return;
            }
            base.WndProc(ref message);
        }

        [EditorBrowsableAttribute(EditorBrowsableState.Always), BrowsableAttribute(true)]
        public new event PaintEventHandler Paint
        {
            add
            {
                base.Paint += value;
            }
            remove
            {
                base.Paint -= value;
            }
        }

        #endregion

        private void InitializeComponent()
        {
            #region Initialize Context Menu
            this.components = new System.ComponentModel.Container();
            this.treeContextMenu = new ContextMenuStrip(this.components);
            this.addNewChildNodeMenuItem = new ToolStripMenuItem();
            this.editTextMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.moveUpMenuItem = new ToolStripMenuItem();
            this.moveDownMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.undoMenuItem = new ToolStripMenuItem();
            this.redoMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.copyMenuItem = new ToolStripMenuItem();
            this.pasteMenuItem = new ToolStripMenuItem();
            this.pasteLinkMenuItem = new ToolStripMenuItem();
            this.followLinkMenuItem = new ToolStripMenuItem();
            this.makeLinkRealMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.toolStripSeparator5 = new ToolStripSeparator();
            this.deleteMenuItem = new ToolStripMenuItem();
            this.treeContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeContextMenu
            // 
            this.treeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewChildNodeMenuItem,
            this.editTextMenuItem,
            this.toolStripSeparator1,
            this.moveUpMenuItem,
            this.moveDownMenuItem,
            this.toolStripSeparator2,
            this.undoMenuItem,
            this.redoMenuItem,
            this.toolStripSeparator3,
            this.copyMenuItem,
            this.pasteMenuItem,
            this.pasteLinkMenuItem,
            this.followLinkMenuItem,
            this.makeLinkRealMenuItem,
            this.toolStripSeparator4,
            this.toolStripSeparator5,
            this.deleteMenuItem});
            this.treeContextMenu.Name = "treeContextMenu";
            this.treeContextMenu.Size = new System.Drawing.Size(216, 192);
            // 
            // addNewChildNodeMenuItem
            // 
            this.addNewChildNodeMenuItem.Name = "addNewChildNodeMenuItem";
            this.addNewChildNodeMenuItem.Size = new System.Drawing.Size(215, 22);
            this.addNewChildNodeMenuItem.Text = "Create New Node As Child Of This Node";
            this.addNewChildNodeMenuItem.Click += new EventHandler(addNewChildNodeMenuItem_Click);
            // 
            // editTextMenuItem
            // 
            this.editTextMenuItem.Name = "editTextMenuItem";
            this.editTextMenuItem.Size = new System.Drawing.Size(215, 22);
            this.editTextMenuItem.Text = "Edit Text Of This Node";
            this.editTextMenuItem.Click += new EventHandler(editTextMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // moveUpMenuItem
            // 
            this.moveUpMenuItem.Name = "moveUpMenuItem";
            this.moveUpMenuItem.Size = new System.Drawing.Size(215, 22);
            this.moveUpMenuItem.Text = "Move This Node Up";
            this.moveUpMenuItem.Click += new EventHandler(moveUpMenuItem_Click);
            // 
            // moveDownMenuItem
            // 
            this.moveDownMenuItem.Name = "moveDownMenuItem";
            this.moveDownMenuItem.Size = new System.Drawing.Size(215, 22);
            this.moveDownMenuItem.Text = "Move This Node Down";
            this.moveDownMenuItem.Click += new EventHandler(moveDownMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            //
            // undoMenuItem
            //
            this.undoMenuItem.Name = "undoMenuItem";
            this.undoMenuItem.Size = new Size(215, 22);
            this.undoMenuItem.Text = "Undo";
            this.undoMenuItem.Click += new EventHandler(undoMenuItem_Click);
            //
            // redoMenuItem
            //
            this.redoMenuItem.Name = "redoMenuItem";
            this.redoMenuItem.Size = new Size(215, 22);
            this.redoMenuItem.Text = "Redo";
            this.redoMenuItem.Click += new EventHandler(redoMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Name = "copyMenuItem";
            this.copyMenuItem.Size = new System.Drawing.Size(215, 22);
            this.copyMenuItem.Text = "Copy This Node And All Subnodes";
            this.copyMenuItem.Click += new EventHandler(copyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Name = "pasteMenuItem";
            this.pasteMenuItem.Size = new System.Drawing.Size(215, 22);
            this.pasteMenuItem.Text = "Paste Node And Subnodes";
            this.pasteMenuItem.Click += new EventHandler(pasteMenuItem_Click);
            // 
            // pasteLinkMenuItem
            // 
            this.pasteLinkMenuItem.Name = "pasteLinkMenuItem";
            this.pasteLinkMenuItem.Size = new System.Drawing.Size(215, 22);
            this.pasteLinkMenuItem.Text = "Paste Node As Link";
            this.pasteLinkMenuItem.Click += new EventHandler(pasteLinkMenuItem_Click);
            //
            // followLinkMenuItem
            //
            this.followLinkMenuItem.Enabled = false;
            this.followLinkMenuItem.Name = "followLinkMenuItem";
            this.followLinkMenuItem.Size = new System.Drawing.Size(215, 22);
            this.followLinkMenuItem.Text = "Go Target Of This Link";
            this.followLinkMenuItem.Click += new EventHandler(followLinkMenuItem_Click);
            //
            // makeLinkRealMenuItem
            //
            this.makeLinkRealMenuItem.Enabled = false;
            this.makeLinkRealMenuItem.Name = "makeLinkRealMenuItem";
            this.makeLinkRealMenuItem.Size = new System.Drawing.Size(215, 22);
            this.makeLinkRealMenuItem.Text = "Redirect All Links Of This Type To This Link";
            this.makeLinkRealMenuItem.Click += new EventHandler(makeLinkRealMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(212, 6);
            //
            // toolStripSeparator5
            //
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new Size(212, 6);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.Size = new System.Drawing.Size(215, 22);
            this.deleteMenuItem.Text = "Delete This Node And All Subnodes";
            this.deleteMenuItem.Click += new EventHandler(deleteMenuItem_Click);
            #endregion
            // 
            // LinkedTree
            // 
            this.LineColor = System.Drawing.Color.Black;

            this.AfterExpand += new TreeViewEventHandler(LinkedTree_AfterExpand);
            this.AfterCollapse += new TreeViewEventHandler(LinkedTree_AfterCollapse);

            this.treeContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #region Menu Event Handlers

        void addNewChildNodeMenuItem_Click(object sender, EventArgs e)
        {
            AddNewNode();
        }

        void editTextMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.EditNode();
        }

        void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            MoveNodeUp();
        }

        void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            MoveNodeDown();
        }

        void undoMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.Undo();
        }

        void redoMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.Redo();
        }

        void followLinkMenuItem_Click(object sender, EventArgs e)
        {
            FollowSelectedLink();
        }

        void makeLinkRealMenuItem_Click(object sender, EventArgs e)
        {
            MakeSelectedLinkReal();
        }

        void copyMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.CopySelectedNodeToClipboard();
        }

        void pasteMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.PasteNodeAndSubnodes();
        }

        void pasteLinkMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.PasteNodeAsLink();
        }

        void deleteMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.DeleteTreeNode();
        }

        #endregion

        public LinkedTree()
        {
            InitializeComponent();
        }

        void LinkedTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            ((ConversationNode)e.Node).UpdateTextAndColor();
        }

        void LinkedTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            ((ConversationNode)e.Node).UpdateTextAndColor();
        }

        public void SortTree()
        {
            this.TreeViewNodeSorter = new NodeSorter();
        }

        public void NewNodeSelected(object sender, TreeViewEventArgs e)
        {
            if (selectedNode.isLink)
            {
                this.makeLinkRealMenuItem.Enabled = true;
                this.followLinkMenuItem.Enabled = true;
                this.addNewChildNodeMenuItem.Enabled = false;
            }
            else
            {
                this.makeLinkRealMenuItem.Enabled = false;
                this.followLinkMenuItem.Enabled = false;
                this.addNewChildNodeMenuItem.Enabled = true;
            }
        }

        public void SaveConversation(string fileName, int versionNumber)
        {
            myTreeConversation.SaveConversation(fileName, versionNumber);
        }

        public void EnablePaste(bool linksAllowed)
        {
            pasteMenuItem.Enabled = true;
            pasteLinkMenuItem.Enabled = linksAllowed;
        }

        public void DisablePaste()
        {
            pasteLinkMenuItem.Enabled = false;
            pasteMenuItem.Enabled = false;
        }

        public void EnableCopy()
        {
            copyMenuItem.Enabled = true;
        }

        public void DisableCopy()
        {
            copyMenuItem.Enabled = false;
        }

        public void EnableDelete()
        {
            deleteMenuItem.Enabled = true;
        }

        public void DisableDelete()
        {
            deleteMenuItem.Enabled = false;
        }

        public SelectedSection GetSelectedSection()
        {
            return myParentTab.GetSelectedSection();
        }

        public void FocusTextBox()
        {
            myParentTab.FocusTextBox();
        }

        public void FocusCommentsBox()
        {
            myParentTab.FocusCommentsBox();
        }

        public void UpdateTab()
        {
            myParentTab.Updated();
        }

        public void MakeSelectedLinkReal()
        {
            selectedNode.MakeLinkReal();
        }

        public void SelectRootNode()
        {
            SelectedNode = GetRootNode();
            SelectedNode.EnsureVisible();
        }

        public void DrawTree(TreeConversation treeConversation)
        {
            this.TreeViewNodeSorter = null;
            Nodes.Clear();
            myTreeConversation = treeConversation;
            treeConversation.myTree = this;
            Nodes.Add(treeConversation);
            SortTree();
        }

        public ConversationNode GetRootNode()
        {
            return myTreeConversation;
        }

        public void FollowSelectedLink()
        {
            if (selectedNode.isLink)
            {
                SelectedNode = myTreeConversation.GetConversationNodeById(selectedNode.myNode.linkTo);
            }
            else
            {
                MessageBox.Show("The selected node is not a link.");
            }
        }

        public void MoveNodeDown()
        {
            if (selectedNode != null)
            {
                UpdateTab();
                myParentTab.SetLastState();
                myParentTab.mySelectedNode = selectedNode;
                selectedNode.MoveNodeDown();
                selectedNode = myParentTab.mySelectedNode;
                myParentTab.UpdateUndo();
            }
        }

        public void MoveNodeUp()
        {
            if (selectedNode != null)
            {
                UpdateTab();
                myParentTab.SetLastState();
                myParentTab.mySelectedNode = selectedNode;
                selectedNode.MoveNodeUp();
                selectedNode = myParentTab.mySelectedNode;
                myParentTab.UpdateUndo();
            }
        }

        public void SaveContentConversation(string fileName, int versionNumber)
        {
            myTreeConversation.SaveContentConversation(fileName, versionNumber);
        }

        public void SetDeleteDialogText(string text)
        {
            myParentTab.SetDeleteDialogText(text);
        }

        public DeleteNodeDialogOptions ShowDeleteDialog()
        {
            return myParentTab.ShowDeleteDialog();
        }

        public void DeleteTreeNode()
        {
            UndoState undo = UndoState.CaptureCurrentState(this);
            if (selectedNode != null)
            {
                ConversationNode deleted = null;
                if (selectedNode.nodeType != ConversationNodeType.Root)
                {
                    deleted = selectedNode.IsAnyChildLinkedExternally(selectedNode);

                    if (deleted != null)
                    {
                        SetDeleteDialogText(deleted.Text);
                        DeleteNodeDialogOptions clickedButton = ShowDeleteDialog();
                        if (clickedButton == DeleteNodeDialogOptions.Delete)
                        {
                            UpdateTab();
                            selectedNode.myParent.DeleteChildAndExternalLinks(selectedNode);
                        }
                        else if (clickedButton == DeleteNodeDialogOptions.Copy)
                        {
                            selectedNode = selectedNode.CopyOutLinks();
                            selectedNode.myParent.DeleteChildAndExternalLinks(selectedNode);
                        }
                        else
                        {
                            //do not delete
                        }
                    }
                    else
                    {
                        ConversationNode success = selectedNode.myParent.DeleteChild(selectedNode);
                        if (success == null)  //deletion was successful
                        {
                            UpdateTab();
                            myParentTab.AddUndo(undo);
                        }
                        else
                        {
                            throw new Exception("There was an error while attempting to delete.");
                        }
                    }
                }
            }
        }

        public void AddNewNode()
        {
            ConversationNode parentNode = selectedNode;
            int nextIdNum = myTreeConversation.GetNextIdNum();
            int nextOrderNum = selectedNode.GetNextOrderNum();
            ConversationNode newNode = ConversationNode.NewConversationNode(nextIdNum, nextOrderNum);
            if (parentNode.nodeType == ConversationNodeType.Root)
            {
                parentNode.myTreeConversation.AddContentNodeToRoot(newNode.myNode);
            }
            else
            {
                parentNode.myNode.subNodes.Add(newNode.myNode);
            }
            if (parentNode.nodeType == ConversationNodeType.Root || parentNode.nodeType == ConversationNodeType.PC)
            {
                newNode.myNode.nodeType = ConversationNodeType.NPC;
            }
            else if (parentNode.nodeType == ConversationNodeType.NPC)
            {
                newNode.myNode.nodeType = ConversationNodeType.PC;
            }
            parentNode.Nodes.Add(newNode);
            myParentTab.programmaticExpansion = true;
            parentNode.Expand();
            myParentTab.programmaticExpansion = false;
            selectedNode = newNode;
            newNode.myTreeConversation = myTreeConversation;
            newNode.UpdateTextAndColor();
            parentNode.UpdateTextAndColor();
            myParentTab.EditNode();
        }

        public void GotUndos()
        {
            this.undoMenuItem.Enabled = true;
        }

        public void GotRedos()
        {
            this.redoMenuItem.Enabled = true;
        }

        public void NoUndos()
        {
            this.undoMenuItem.Enabled = false;
        }

        public void NoRedos()
        {
            this.redoMenuItem.Enabled = false;
        }

        public void myTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (!myParentTab.programmaticExpansion && ((ConversationNode)e.Node).nodeType != ConversationNodeType.Root)
            {
                if (this.myParentTab.collapsedNodes.Contains(((ConversationNode)e.Node).myId))
                {
                    this.myParentTab.collapsedNodes.Remove(((ConversationNode)e.Node).myId);
                }
            }
        }

        public void myTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (!myParentTab.programmaticExpansion)
            {
                this.myParentTab.collapsedNodes.Add(((ConversationNode)e.Node).myId);
            }
        }

        public int CheckVersion()
        {
            return myTreeConversation.CheckVersion();
        }
    }
}
