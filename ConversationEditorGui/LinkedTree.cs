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
            components = new System.ComponentModel.Container();
            treeContextMenu = new ContextMenuStrip(components);
            addNewChildNodeMenuItem = new ToolStripMenuItem();
            editTextMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            moveUpMenuItem = new ToolStripMenuItem();
            moveDownMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            undoMenuItem = new ToolStripMenuItem();
            redoMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            copyMenuItem = new ToolStripMenuItem();
            pasteMenuItem = new ToolStripMenuItem();
            pasteLinkMenuItem = new ToolStripMenuItem();
            followLinkMenuItem = new ToolStripMenuItem();
            makeLinkRealMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripSeparator5 = new ToolStripSeparator();
            deleteMenuItem = new ToolStripMenuItem();
            treeContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // treeContextMenu
            // 
            treeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            addNewChildNodeMenuItem,
            editTextMenuItem,
            toolStripSeparator1,
            moveUpMenuItem,
            moveDownMenuItem,
            toolStripSeparator2,
            undoMenuItem,
            redoMenuItem,
            toolStripSeparator3,
            copyMenuItem,
            pasteMenuItem,
            pasteLinkMenuItem,
            followLinkMenuItem,
            makeLinkRealMenuItem,
            toolStripSeparator4,
            toolStripSeparator5,
            deleteMenuItem});
            treeContextMenu.Name = "treeContextMenu";
            treeContextMenu.Size = new System.Drawing.Size(216, 192);
            // 
            // addNewChildNodeMenuItem
            // 
            addNewChildNodeMenuItem.Name = "addNewChildNodeMenuItem";
            addNewChildNodeMenuItem.Size = new System.Drawing.Size(215, 22);
            addNewChildNodeMenuItem.Text = "Create New Node As Child Of This Node";
            addNewChildNodeMenuItem.Click += new EventHandler(AddNewChildNodeMenuItem_Click);
            // 
            // editTextMenuItem
            // 
            editTextMenuItem.Name = "editTextMenuItem";
            editTextMenuItem.Size = new System.Drawing.Size(215, 22);
            editTextMenuItem.Text = "Edit Text Of This Node";
            editTextMenuItem.Click += new EventHandler(EditTextMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // moveUpMenuItem
            // 
            moveUpMenuItem.Name = "moveUpMenuItem";
            moveUpMenuItem.Size = new System.Drawing.Size(215, 22);
            moveUpMenuItem.Text = "Move This Node Up";
            moveUpMenuItem.Click += new EventHandler(MoveUpMenuItem_Click);
            // 
            // moveDownMenuItem
            // 
            moveDownMenuItem.Name = "moveDownMenuItem";
            moveDownMenuItem.Size = new System.Drawing.Size(215, 22);
            moveDownMenuItem.Text = "Move This Node Down";
            moveDownMenuItem.Click += new EventHandler(MoveDownMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            //
            // undoMenuItem
            //
            undoMenuItem.Name = "undoMenuItem";
            undoMenuItem.Size = new Size(215, 22);
            undoMenuItem.Text = "Undo";
            undoMenuItem.Click += new EventHandler(UndoMenuItem_Click);
            //
            // redoMenuItem
            //
            redoMenuItem.Name = "redoMenuItem";
            redoMenuItem.Size = new Size(215, 22);
            redoMenuItem.Text = "Redo";
            redoMenuItem.Click += new EventHandler(RedoMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
            // 
            // copyMenuItem
            // 
            copyMenuItem.Name = "copyMenuItem";
            copyMenuItem.Size = new System.Drawing.Size(215, 22);
            copyMenuItem.Text = "Copy This Node And All Subnodes";
            copyMenuItem.Click += new EventHandler(CopyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            pasteMenuItem.Name = "pasteMenuItem";
            pasteMenuItem.Size = new System.Drawing.Size(215, 22);
            pasteMenuItem.Text = "Paste Node And Subnodes";
            pasteMenuItem.Click += new EventHandler(PasteMenuItem_Click);
            // 
            // pasteLinkMenuItem
            // 
            pasteLinkMenuItem.Name = "pasteLinkMenuItem";
            pasteLinkMenuItem.Size = new System.Drawing.Size(215, 22);
            pasteLinkMenuItem.Text = "Paste Node As Link";
            pasteLinkMenuItem.Click += new EventHandler(PasteLinkMenuItem_Click);
            //
            // followLinkMenuItem
            //
            followLinkMenuItem.Enabled = false;
            followLinkMenuItem.Name = "followLinkMenuItem";
            followLinkMenuItem.Size = new System.Drawing.Size(215, 22);
            followLinkMenuItem.Text = "Go Target Of This Link";
            followLinkMenuItem.Click += new EventHandler(FollowLinkMenuItem_Click);
            //
            // makeLinkRealMenuItem
            //
            makeLinkRealMenuItem.Enabled = false;
            makeLinkRealMenuItem.Name = "makeLinkRealMenuItem";
            makeLinkRealMenuItem.Size = new System.Drawing.Size(215, 22);
            makeLinkRealMenuItem.Text = "Redirect All Links Of This Type To This Link";
            makeLinkRealMenuItem.Click += new EventHandler(MakeLinkRealMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(212, 6);
            //
            // toolStripSeparator5
            //
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(212, 6);
            // 
            // deleteMenuItem
            // 
            deleteMenuItem.Name = "deleteMenuItem";
            deleteMenuItem.Size = new System.Drawing.Size(215, 22);
            deleteMenuItem.Text = "Delete This Node And All Subnodes";
            deleteMenuItem.Click += new EventHandler(DeleteMenuItem_Click);
            #endregion
            // 
            // LinkedTree
            // 
            LineColor = System.Drawing.Color.Black;

            AfterExpand += new TreeViewEventHandler(LinkedTree_AfterExpand);
            AfterCollapse += new TreeViewEventHandler(LinkedTree_AfterCollapse);

            treeContextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #region Menu Event Handlers

        void AddNewChildNodeMenuItem_Click(object sender, EventArgs e)
        {
            AddNewNode();
        }

        void EditTextMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.EditNode();
        }

        void MoveUpMenuItem_Click(object sender, EventArgs e)
        {
            MoveNodeUp();
        }

        void MoveDownMenuItem_Click(object sender, EventArgs e)
        {
            MoveNodeDown();
        }

        void UndoMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.Undo();
        }

        void RedoMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.Redo();
        }

        void FollowLinkMenuItem_Click(object sender, EventArgs e)
        {
            FollowSelectedLink();
        }

        void MakeLinkRealMenuItem_Click(object sender, EventArgs e)
        {
            MakeSelectedLinkReal();
        }

        void CopyMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.CopySelectedNodeToClipboard();
        }

        void PasteMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.PasteNodeAndSubnodes();
        }

        void PasteLinkMenuItem_Click(object sender, EventArgs e)
        {
            myParentTab.PasteNodeAsLink();
        }

        void DeleteMenuItem_Click(object sender, EventArgs e)
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
            TreeViewNodeSorter = new NodeSorter();
        }

        public void NewNodeSelected(object sender, TreeViewEventArgs e)
        {
            if (selectedNode.isLink)
            {
                makeLinkRealMenuItem.Enabled = true;
                followLinkMenuItem.Enabled = true;
                addNewChildNodeMenuItem.Enabled = false;
            }
            else
            {
                makeLinkRealMenuItem.Enabled = false;
                followLinkMenuItem.Enabled = false;
                addNewChildNodeMenuItem.Enabled = true;
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
            TreeViewNodeSorter = null;
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
            undoMenuItem.Enabled = true;
        }

        public void GotRedos()
        {
            redoMenuItem.Enabled = true;
        }

        public void NoUndos()
        {
            undoMenuItem.Enabled = false;
        }

        public void NoRedos()
        {
            redoMenuItem.Enabled = false;
        }

        public void myTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (!myParentTab.programmaticExpansion && ((ConversationNode)e.Node).nodeType != ConversationNodeType.Root)
            {
                if (myParentTab.collapsedNodes.Contains(((ConversationNode)e.Node).myId))
                {
                    myParentTab.collapsedNodes.Remove(((ConversationNode)e.Node).myId);
                }
            }
        }

        public void myTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (!myParentTab.programmaticExpansion)
            {
                myParentTab.collapsedNodes.Add(((ConversationNode)e.Node).myId);
            }
        }

        public int CheckVersion()
        {
            return myTreeConversation.CheckVersion();
        }
    }
}
