using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Editor;

namespace ConversationEditorGui
{
    [Serializable]
    public class TreeTab : TabPage
    {
        #region Parents and Children
        public SplitContainer splitTreeFromText = null;
        public SplitContainer splitNodeTextFromComments = null;
        public SplitContainer splitLabelFromComments = null;
        public SplitContainer splitLabelFromNodeText = null;
        public System.Windows.Forms.Integration.ElementHost wpfHolder = null;
        public LinkedTextBox myTextBox = null;
        public LinkedTree myTree = null;
        public LinkedCommentsBox myCommentsBox = null;
        public Form1 myParentForm = null;
        public TabControl myParentTabControl = null;
        public Label myCommentsLabel = null;
        public Label myNodeTextLabel = null;
        private UndoCollection myUndos = null;
        #endregion

        #region Specific Properties
        public string myFileName = "";
        public ConversationNode mySelectedNode = null;
        public bool changedSinceLastSave = false;
        public FormClosingEventHandler myFormIsClosingNotifyMe;
        public int[] newPlaceholder = new int[3] { 0, 0, 0 };
        public int[] currentPlaceholder = new int[3] { 0, 0, 0 };
        public int[] mouseOutExPlaceholder = new int[3] { 0, 0, 0 };
        public ConversationNode lastDropTarget = null;
        public ConversationNode myNodeDrop;
        public string myNodeDropPosition;
        public bool everSaved = false;
        public List<int> collapsedNodes = new List<int>();
        public bool programmaticExpansion = false;
        public UndoState lastState;

        #endregion

        public TreeTab(string fileName, TabControl myParent, Form1 caller)
        {
            #region Set Parents and Children Variables
            this.myParentTabControl = myParent;
            this.myParentForm = caller;

            this.splitTreeFromText = new SplitContainer();
            this.splitNodeTextFromComments = new SplitContainer();
            this.splitLabelFromComments = new SplitContainer();
            this.splitLabelFromNodeText = new SplitContainer();
            this.myTree = new LinkedTree();
            this.wpfHolder = new System.Windows.Forms.Integration.ElementHost();
            this.myTextBox = new LinkedTextBox();
            this.myCommentsBox = new LinkedCommentsBox();
            this.myCommentsLabel = new Label();
            this.myNodeTextLabel = new Label();

            this.myUndos = new UndoCollection(this);
            #endregion

            #region Set Filename
            if (fileName == "")
            {
                this.myFileName = "";
                this.Name = "";
                this.Text = "Untitled";
            }
            else
            {
                this.myFileName = fileName;
                this.Name = fileName;
                this.Text = GetShortFileName(fileName);
                this.everSaved = true;
            }
            #endregion

            #region ElementHost
            wpfHolder.Dock = DockStyle.Fill;
            #endregion

            #region Add Controls
            //myTextBox.MouseRightButtonUp += myTextBox_RightClick;
            //myTextBox.ContextMenu = new System.Windows.Controls.ContextMenu();
            //myTextBox.ContextMenu.Items.Add("TEST");
            myParent.Controls.Add(this);
            this.Controls.Add(this.splitTreeFromText);
            this.splitTreeFromText.Panel1.Controls.Add(this.myTree);
            this.splitTreeFromText.Panel2.Controls.Add(this.splitNodeTextFromComments);
            this.splitNodeTextFromComments.Panel1.Controls.Add(this.splitLabelFromNodeText);
            this.splitNodeTextFromComments.Panel2.Controls.Add(this.splitLabelFromComments);
            this.splitLabelFromComments.Panel1.Controls.Add(this.myCommentsLabel);
            this.splitLabelFromComments.Panel2.Controls.Add(this.myCommentsBox);
            this.splitLabelFromNodeText.Panel1.Controls.Add(this.myNodeTextLabel);
            this.splitLabelFromNodeText.Panel2.Controls.Add(wpfHolder);
            #endregion

            this.UseVisualStyleBackColor = true;

            #region SplitPanels
            this.splitTreeFromText.Dock = DockStyle.Fill;
            this.splitTreeFromText.Orientation = Orientation.Horizontal;
            this.splitTreeFromText.SplitterDistance = 314;

            this.splitNodeTextFromComments.Dock = DockStyle.Fill;
            this.splitNodeTextFromComments.Orientation = Orientation.Vertical;
            this.splitNodeTextFromComments.SplitterDistance = 700;

            this.splitLabelFromComments.Dock = DockStyle.Fill;
            this.splitLabelFromComments.Orientation = Orientation.Horizontal;
            this.splitLabelFromComments.FixedPanel = FixedPanel.Panel1;

            this.splitLabelFromNodeText.Dock = DockStyle.Fill;
            this.splitLabelFromNodeText.Orientation = Orientation.Horizontal;
            this.splitLabelFromNodeText.FixedPanel = FixedPanel.Panel1;
            #endregion

            this.myFormIsClosingNotifyMe = new FormClosingEventHandler(myParentForm_FormClosing);
            this.myParentForm.FormClosing += myFormIsClosingNotifyMe;

            #region myTree Properties and Events
            this.myTree.myTextBox = this.myTextBox;
            this.myTree.myCommentsBox = this.myCommentsBox;
            this.myTree.myParentTab = this;
            this.myTree.Dock = DockStyle.Fill;
            this.myTree.ImageList = new ImageList();
            this.myTree.ImageList.Images.Add(global::ConversationEditorGui.Properties.Resources.EmptyCommentIconNoSubnodes);
            this.myTree.ImageList.Images.Add(global::ConversationEditorGui.Properties.Resources.EmptyCommentIconSubnodes);
            this.myTree.ImageList.Images.Add(global::ConversationEditorGui.Properties.Resources.CommentIcon);
            //this.myTree.ImageList.TransparentColor = Color.White;
            this.myTree.ImageIndex = 0;
            this.myTree.AllowDrop = true;
            this.myTree.AfterSelect += new TreeViewEventHandler(this.SelectNode);
            this.myTree.AfterSelect += new TreeViewEventHandler(this.myParentForm.NewNodeSelected);
            this.myTree.AfterSelect += new TreeViewEventHandler(this.myTree.NewNodeSelected);
            this.myTree.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(myTree_NodeMouseDoubleClick);
            this.myTree.GotFocus += new EventHandler(myTree_GotFocus);
            this.myTree.GotFocus += new EventHandler(this.myParentForm.LinkedTreeInFocus);
            this.myTree.LostFocus += new EventHandler(myTree_LostFocus);
            this.myTree.LostFocus += new EventHandler(myParentForm.LinkedTreeLostFocus);
            this.myTree.KeyDown += new KeyEventHandler(myTree_KeyDown);
            this.myTree.MouseDown += new MouseEventHandler(myTree_MouseDown);
            this.myTree.KeyPress += new KeyPressEventHandler(myTree_KeyPress);
            this.myTree.ItemDrag += new ItemDragEventHandler(myTree_ItemDrag);
            this.myTree.DragEnter += new DragEventHandler(myTree_DragEnter);
            this.myTree.DragDrop += new DragEventHandler(myTree_DragDrop);
            this.myTree.DragOver += new DragEventHandler(myTree_DragOver);
            this.myTree.Paint += new PaintEventHandler(myTree_Paint);
            this.myTree.DragLeave += new EventHandler(myTree_DragLeave);
            this.myTree.BeforeExpand += new TreeViewCancelEventHandler(this.myTree.myTree_BeforeExpand);
            this.myTree.BeforeCollapse += new TreeViewCancelEventHandler(this.myTree.myTree_BeforeCollapse);

            #endregion

            #region myTextBox Properties and Events
            wpfHolder.BackColor = Color.White;
            this.myTextBox.myTree = this.myTree;
            this.myTextBox.myCommentsBox = this.myCommentsBox;
            this.myTextBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            this.myTextBox.SpellCheck.IsEnabled = true;
            this.myTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(myTextBox_TextChanged);
            this.myTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.myParentForm.LinkedTextBoxInFocus);
            this.myTextBox.LostFocus += new System.Windows.RoutedEventHandler(myParentForm.LinkedTextBoxLostFocus);
            wpfHolder.Child = this.myTextBox;
            #endregion

            this.myCommentsLabel.Text = "Comments:";
            this.myCommentsLabel.Dock = DockStyle.Fill;
            this.myCommentsLabel.AutoSize = true;

            this.myNodeTextLabel.Text = "";
            this.myNodeTextLabel.Dock = DockStyle.Fill;
            this.myNodeTextLabel.AutoSize = true;

            this.splitLabelFromComments.IsSplitterFixed = true;
            this.splitLabelFromComments.Panel1MinSize = 1;
            this.splitLabelFromComments.SplitterDistance = 15;

            this.splitLabelFromNodeText.IsSplitterFixed = true;
            this.splitLabelFromNodeText.Panel1MinSize = 1;
            this.splitLabelFromNodeText.SplitterDistance = 15;

            #region myCommentsBox Properties and Events
            this.myCommentsBox.myTextBox = this.myTextBox;
            this.myCommentsBox.myTree = this.myTree;
            this.myCommentsBox.Multiline = true;
            this.myCommentsBox.Dock = DockStyle.Fill;
            this.myCommentsBox.TextChanged += new EventHandler(myCommentsBox_TextChanged);
            this.myCommentsBox.KeyPress += new KeyPressEventHandler(myCommentsBox_KeyPress);
            this.myCommentsBox.GotFocus += new EventHandler(this.myParentForm.CommentsBoxInFocus);
            this.myCommentsBox.LostFocus += new EventHandler(myParentForm.CommentsBoxLostFocus);
            #endregion

        }

        void myTree_Paint(object sender, PaintEventArgs e)
        {
            DrawPlaceholderIfNeeded(e.Graphics);
        }

        #region Drag and Drop

        private void ScrollWindow(int delta, ConversationNode overNode)
        {
            if (overNode != null)
            {
                if ((delta < myTree.Height / 2) && delta > 0)
                {
                    if (overNode.NextVisibleNode != null)
                    {
                        overNode.NextVisibleNode.EnsureVisible();
                    }
                }
                else if ((delta > myTree.Height / 2) && (delta < myTree.Height))
                {
                    if (overNode.PrevVisibleNode != null)
                    {
                        overNode.PrevVisibleNode.EnsureVisible();
                    }
                }
            }
        }

        private void myTree_DragOver(object sender, DragEventArgs e)
        {
            DragOverTree(e);
        }

        private void DragOverTree(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)) || e.Data.GetDataPresent(typeof(ConversationNode)))
            {
                Point pt = myTree.PointToClient(new Point(e.X, e.Y));
                ConversationNode overNode = (ConversationNode)myTree.GetNodeAt(pt.X, pt.Y);
                ConversationNode dragNode = (ConversationNode)e.Data.GetData("ConversationEditorGui.ConversationNode");
                DraggingOnTree(pt, overNode, dragNode);
            }
        }

        private void DraggingOnTree(Point pt, ConversationNode overNode, ConversationNode dragNode)
        {
            bool overPlacementLegal = false;
            bool underPlacementLegal = false;
            bool underParentPlacementLegal = false;
            bool childPlacementLegal = false;

            int delta = myTree.Height - pt.Y;

            if (overNode != null) // There is a node under the cursor
            {
                ScrollWindow(delta, overNode);

                if (dragNode.nodeType == ConversationNodeType.Root)
                {
                    throw new Exception("Should not be able to drag Root node");
                }

                if (overNode.IsChildOf(dragNode))
                {
                    // It is being dragged onto itself or one of its children, no placement is allowed
                }
                else if (overNode.nodeType == ConversationNodeType.Root && dragNode.nodeType == ConversationNodeType.PC)
                {
                    // A PC node is being dragged onto the Root, no placement is allowed
                }
                else if (overNode.nodeType == ConversationNodeType.PC && dragNode.nodeType == ConversationNodeType.PC)
                {
                    // A PC node is being dragged onto another PC node, only allowed placements are
                    // above and below
                    overPlacementLegal = true;
                    underPlacementLegal = true;
                }
                else if (overNode.nodeType == ConversationNodeType.NPC && dragNode.nodeType == ConversationNodeType.NPC)
                {
                    // An NPC node is being dragged onto another NPC node, only allowed placements are
                    // above and below
                    overPlacementLegal = true;
                    underPlacementLegal = true;
                }
                else if (overNode.nodeType == ConversationNodeType.Root && dragNode.nodeType == ConversationNodeType.NPC)
                {
                    // An NPC node is being dragged onto the root, only allowed placement is child
                    childPlacementLegal = true;
                }
                else if ((overNode.nodeType == ConversationNodeType.NPC && dragNode.nodeType == ConversationNodeType.PC) || (overNode.nodeType == ConversationNodeType.PC && dragNode.nodeType == ConversationNodeType.NPC))
                {
                    // The node is being dragged onto its opposite type, only allowed placements are
                    // child and below parent
                    childPlacementLegal = true;
                    underParentPlacementLegal = true;
                }
                if (overNode.isLink)
                {
                    // Links cannot have children
                    childPlacementLegal = false;
                }
                if (overNode.Nodes.Count > 0)
                {
                    // The node has children, therefore it cannot have something added beneath it
                    underPlacementLegal = false;
                }

                if (overPlacementLegal || underPlacementLegal || underParentPlacementLegal || childPlacementLegal)
                {
                    ShowDragTargetEffect(overNode, overPlacementLegal, underPlacementLegal, underParentPlacementLegal, childPlacementLegal);
                }
            }
        }

        private void ShowDragTargetEffect(ConversationNode overNode, bool overPlacementLegal, bool underPlacementLegal, bool underParentPlacementLegal, bool childPlacementLegal)
        {
            int offsetY = this.myTree.PointToClient(Cursor.Position).Y - overNode.Bounds.Top;

            if (offsetY < overNode.Bounds.Height / 2)
            {
                if (overPlacementLegal)
                {
                    DrawOverPlaceholder(overNode);
                }
                else if (childPlacementLegal)
                {
                    HighlightDropTarget(overNode);
                }
            }
            else if (offsetY > overNode.Bounds.Height / 2)
            {
                if (underPlacementLegal)
                {
                    DrawUnderPlaceholder(overNode);
                }
                else if (underParentPlacementLegal)
                {
                    if (offsetY > (overNode.Bounds.Height - (overNode.Bounds.Height / 4)))
                    {
                        if (overNode.Parent.NextNode != null && overNode.Index == (overNode.Parent.Nodes.Count - 1))
                        {
                            DrawOverPlaceholder((ConversationNode)overNode.Parent.NextNode);
                        }
                        else if (childPlacementLegal)
                        {
                            HighlightDropTarget(overNode);
                        }
                    }
                    else if (childPlacementLegal)
                    {
                        HighlightDropTarget(overNode);
                    }
                }
                else
                {
                    if (childPlacementLegal)
                    {
                        HighlightDropTarget(overNode);
                    }
                }
            }
            else if (childPlacementLegal)
            {
                HighlightDropTarget(overNode);
            }
        }

        private void ClearPlaceholders()
        {
            if (!(currentPlaceholder[0] == newPlaceholder[0] && currentPlaceholder[1] == newPlaceholder[1] && currentPlaceholder[2] == newPlaceholder[2]))
            {
                if (!(currentPlaceholder[0] == 0 && currentPlaceholder[1] == 0 && currentPlaceholder[2] == 0))
                {
                    this.myTree.Invalidate();
                }
            }
        }

        private void RemovePlaceHolders()
        {
            currentPlaceholder[0] = 0;
            currentPlaceholder[1] = 0;
            currentPlaceholder[2] = 0;
            newPlaceholder[0] = 0;
            newPlaceholder[1] = 0;
            newPlaceholder[2] = 0;
            this.myTree.Invalidate();
        }

        private void DrawPlaceholderIfNeeded(Graphics myGraphics)
        {
            if (!(newPlaceholder[0] == currentPlaceholder[0] && newPlaceholder[1] == currentPlaceholder[1] && newPlaceholder[2] == currentPlaceholder[2]))
            {
                DrawPlaceholder(newPlaceholder[0], newPlaceholder[1], newPlaceholder[2], myGraphics);

                currentPlaceholder[0] = newPlaceholder[0];
                currentPlaceholder[1] = newPlaceholder[1];
                currentPlaceholder[2] = newPlaceholder[2];
            }
        }

        private void DrawOverPlaceholder(ConversationNode overNode)
        {
            if (lastDropTarget != null)
            {
                lastDropTarget.BackColor = SystemColors.Window;
                lastDropTarget = null;
            }
            int imageWidth = 0;
            if (myTree.ImageList != null && overNode.ImageIndex > -1)
            {
                imageWidth = myTree.ImageList.Images[overNode.ImageIndex].Size.Width;
            }
            int leftPos = overNode.Bounds.Left - imageWidth;
            int rightPos = myTree.Width - 4;
            int vertPos = overNode.Bounds.Top;
            if (!(currentPlaceholder[0] == leftPos && currentPlaceholder[1] == rightPos && currentPlaceholder[2] == vertPos))
            {
                newPlaceholder[0] = leftPos;
                newPlaceholder[1] = rightPos;
                newPlaceholder[2] = vertPos;
                ClearPlaceholders();
            }
            myNodeDrop = overNode;
            myNodeDropPosition = "Above";
        }

        private void DrawUnderPlaceholder(ConversationNode overNode)
        {
            if (lastDropTarget != null)
            {
                lastDropTarget.BackColor = SystemColors.Window;
                lastDropTarget = null;
            }
            int imageWidth = 0;
            if (myTree.ImageList != null && overNode.ImageIndex > -1)
            {
                imageWidth = myTree.ImageList.Images[overNode.ImageIndex].Size.Width;
            }
            int leftPos = overNode.Bounds.Left - imageWidth;
            int rightPos = myTree.Width - 4;
            int vertPos = overNode.Bounds.Bottom;
            if (!(currentPlaceholder[0] == leftPos && currentPlaceholder[1] == rightPos && currentPlaceholder[2] == vertPos))
            {
                newPlaceholder[0] = leftPos;
                newPlaceholder[1] = rightPos;
                newPlaceholder[2] = vertPos;
                ClearPlaceholders();
            }
            myNodeDrop = overNode;
            myNodeDropPosition = "Below";
        }

        private void DrawPlaceholder(int leftPos, int rightPos, int vertPos, Graphics myGraphics)
        {

            Point[] leftTrapezoid = new Point[5]{
                new Point(leftPos, vertPos - 4),
                new Point(leftPos, vertPos + 4),
                new Point(leftPos + 4, vertPos),
                new Point(leftPos + 4, vertPos - 1),
                new Point(leftPos, vertPos - 5)};

            Point[] rightTrapezoid = new Point[5]{
                new Point(rightPos, vertPos - 4),
                new Point(rightPos, vertPos + 4),
                new Point(rightPos - 4, vertPos),
                new Point(rightPos - 4, vertPos - 1),
                new Point(rightPos, vertPos - 5)};


            myGraphics.FillPolygon(System.Drawing.Brushes.Black, leftTrapezoid);
            myGraphics.FillPolygon(System.Drawing.Brushes.Black, rightTrapezoid);
            myGraphics.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(leftPos, vertPos), new Point(rightPos, vertPos));
        }

        private void HighlightDropTarget(ConversationNode overNode)
        {
            ClearHighlight();
            overNode.BackColor = Color.Gray;
            lastDropTarget = overNode;

            myNodeDrop = overNode;
            myNodeDropPosition = "Child";
        }

        private void ClearHighlight()
        {
            if (lastDropTarget != null)
            {
                lastDropTarget.BackColor = SystemColors.Window;
                lastDropTarget = null;
            }
        }

        void myTree_DragLeave(object sender, EventArgs e)
        {
            mouseOutExPlaceholder[0] = currentPlaceholder[0];
            mouseOutExPlaceholder[1] = currentPlaceholder[1];
            mouseOutExPlaceholder[2] = currentPlaceholder[2];
            RemovePlaceHolders();
            ClearHighlight();
        }

        void myTree_DragDrop(object sender, DragEventArgs e)
        {
            currentPlaceholder[0] = 0;
            currentPlaceholder[1] = 0;
            currentPlaceholder[2] = 0;
            newPlaceholder[0] = 0;
            newPlaceholder[1] = 0;
            newPlaceholder[2] = 0;

            if (lastDropTarget != null)
            {
                lastDropTarget.BackColor = SystemColors.Window;
                lastDropTarget = null;
            }

            this.myTree.Invalidate(true);
            ConversationNode draggedNode = (ConversationNode)e.Data.GetData("ConversationEditorGui.ConversationNode");
            RearrangeDraggedNode(draggedNode);
        }

        void RearrangeDraggedNode(ConversationNode draggedNode)
        {
            if (draggedNode != null)
            {
                this.Updated();
                SetLastState();
                if (myNodeDropPosition == "Child")
                {
                    draggedNode.ChangeMyParent(myNodeDrop);
                    myNodeDrop.Expand();
                }
                else if (myNodeDropPosition == "Above")
                {
                    draggedNode.ChangeMyParent(myNodeDrop.myParent);
                    while (draggedNode.myNode.orderNum >= myNodeDrop.myNode.orderNum)
                    {
                        draggedNode.MoveNodeUp();
                    }
                }
                else if (myNodeDropPosition == "Below")
                {
                    draggedNode.ChangeMyParent(myNodeDrop.myParent);
                    while (draggedNode.myNode.orderNum > (myNodeDrop.myNode.orderNum + 1))
                    {
                        draggedNode.MoveNodeUp();
                    }
                }
                myNodeDrop = null;
                myNodeDropPosition = "";
                this.myTree.SortTree();
                this.myTree.SelectedNode = draggedNode;
                UpdateUndo();
            }
        }

        void myTree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            DragOverTree(e);
        }

        void myTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);

        }
        #endregion

        #region Closing

        public void myParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool test = AttemptClosing(Form1.VERSION_NUMBER);
            if (test)
            {
                myParentTabControl.TabPages.Remove(this);
            }
            else
            {
                e.Cancel = true;
            }
        }

        public void CloseTab()
        {
            FocusTree();

            if (AttemptClosing(Form1.VERSION_NUMBER))
            {
                myParentTabControl.TabPages.Remove(this);
                this.myParentForm.FormClosing -= this.myFormIsClosingNotifyMe;
            }
        }

        public void SetCloseDialogText(string fileName)
        {
            myParentForm.SetCloseDialogText(fileName);
        }

        public CloseDialogOption ShowCloseDialog()
        {
            return myParentForm.ShowCloseDialog();
        }

        private bool AttemptClosing(int versionNumber)
        {
            myParentForm.SetExpandState(myFileName, collapsedNodes);

            bool toReturn = false;
            this.myTree.Focus();
            if (this.changedSinceLastSave == true)
            {
                SetDeleteDialogText(this.myFileName);
                CloseDialogOption clickedButton = ShowCloseDialog();
                switch (clickedButton)
                {
                    case CloseDialogOption.Save:
                        this.SaveConversation(versionNumber);
                        this.changedSinceLastSave = false;
                        toReturn = true;
                        break;
                    case CloseDialogOption.Discard:
                        toReturn = true;
                        break;
                    case CloseDialogOption.Cancel:
                    default:
                        break;
                }
            }
            else
            {
                toReturn = true;
            }
            return toReturn;
        }
        #endregion

        #region Conversation Changes

        public void EditNode()
        {
            this.myTextBox.SelectAll();
            this.myTextBox.Focus();
        }

        /* TODO
         * Tagging - 
        Standard -

        Highlight -
        <StartAction></Start>
        <StartHighlight></Start>
        <StartCheck>[SkillName?]</Start>
         * */
        private void myTextBox_RightClick(object sender, EventArgs e)
        {
            var selectionStart = myTextBox.SelectionStart;
            myTextBox.Text = myTextBox.Text.Insert(selectionStart + myTextBox.SelectionLength, "</>");
            myTextBox.Text = myTextBox.Text.Insert(selectionStart, "<>");

        }

        private void myTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBoxChanged();
        }

        private void TextBoxChanged()
        {
            if (this.myTextBox.IsFocused)
            {
                if (this.mySelectedNode != null)
                {
                    this.Updated();
                    if (mySelectedNode.isLink)
                    {
                        throw new Exception("Cannot update a link");
                    }
                    mySelectedNode.baseText = myTextBox.Text;
                    mySelectedNode.UpdateTextAndColor();
                    foreach (ConversationNode link in mySelectedNode.myLinks)
                    {
                        link.UpdateTextAndColor();
                    }
                }
            }
        }

        private void myCommentsBox_TextChanged(object sender, EventArgs e)
        {
            CommentsBoxChanged();
        }

        private void CommentsBoxChanged()
        {
            if (this.myCommentsBox.Focused)
            {
                if (this.mySelectedNode != null)
                {
                    this.Updated();
                    if (mySelectedNode.isLink)
                    {
                        throw new Exception("Cannot update the text of a link.");
                    }
                    mySelectedNode.myComments = myCommentsBox.Text;
                }
            }
        }

        public void ClipboardUpdated()
        {
            if (Clipboard.GetDataObject().GetDataPresent(ClipboardNode.format.Name))
            {
                if (myTree.Focused)
                {
                    myTree.EnablePaste(true);
                }
                if (((ClipboardNode)Clipboard.GetData(ClipboardNode.format.Name)).linkTo > 0)
                {
                    myTree.EnablePaste(false);
                }
            }
            else
            {
                DisablePaste();
            }
        }

        public void DisablePaste()
        {
            myTree.DisablePaste();
        }

        public void EnableCopy()
        {
            myTree.EnableCopy();
        }

        public void DisableCopy()
        {
            myTree.DisableCopy();
        }

        public void EnableDelete()
        {
            myTree.EnableDelete();
        }

        public void DisableDelete()
        {
            myTree.DisableDelete();
        }

        public void Updated()
        {
            if (this.changedSinceLastSave == false)
            {
                this.changedSinceLastSave = true;
                this.Text += "*";
            }
        }

        public void AddUndo(UndoState undo)
        {
            myUndos.Update(undo);
            lastState = UndoState.CaptureCurrentState(myTree);
            GotUndos();
            NoRedos();
        }

        public void UpdateUndo()
        {
            myUndos.Update(lastState);
            lastState = UndoState.CaptureCurrentState(myTree);
            GotUndos();
            NoRedos();
        }

        public void SetLastState()
        {
            lastState = UndoState.CaptureCurrentState(myTree);
        }

        public void SetDeleteDialogText(string text)
        {
            myParentForm.SetDeleteDialogText(text);
        }

        public DeleteNodeDialogOptions ShowDeleteDialog()
        {
            return myParentForm.ShowDeleteDialog();
        }

        public void DeleteTreeNode()
        {
            myTree.DeleteTreeNode();
        }
        #endregion

        #region Keyboard, Mouse and Focus Events
        private void myTree_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13: // Enter is the pressed key
                    e.Handled = true;
                    EditNode();
                    break;
                default:
                    break;
            }
        }

        private void myTextBox_KeyDown_NumberOnly(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var isNumber = e.Key >= System.Windows.Input.Key.D0 && e.Key <= System.Windows.Input.Key.D9;
            if (!isNumber)
            {
                e.Handled = true;
            }
        }

        private void myCommentsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13: // Enter is the pressed key
                    e.Handled = true;
                    myTextBox.Focus();
                    break;
                default:
                    break;
            }
        }

        private void myTree_LostFocus(object sender, EventArgs e)
        {
            if (this.mySelectedNode != null)
            {
                this.mySelectedNode.BackColor = Color.LightGray;
            }
        }

        private void myTree_GotFocus(object sender, EventArgs e)
        {
            MakeNodeSelected();
        }

        private void myTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                Point pt = new Point(e.X, e.Y);
                this.myTree.PointToClient(pt);
                TreeNode node = this.myTree.GetNodeAt(pt);
                if (node != null)
                {
                    this.myTree.SelectedNode = node;
                    if (node.Bounds.Contains(pt) && e.Button == MouseButtons.Right)
                    {
                        this.myTree.treeContextMenu.Show(this.myTree, pt);
                    }
                }
            }
        }

        public void myTree_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteTreeNode();
                    break;
                case Keys.Enter:
                    EditNode();
                    break;
                default:
                    break;
            }
        }

        public void myTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            myTree.SelectedNode = e.Node;
        }

        public void SelectNode(object sender, TreeViewEventArgs e)
        {
            myTextBox.KeyDown -= new System.Windows.Input.KeyEventHandler(myTextBox_KeyDown_NumberOnly);
            if (e.Node != null)
            {
                mySelectedNode = (ConversationNode)e.Node;
                if (mySelectedNode.nodeType != ConversationNodeType.Root)
                {
                    myTextBox.Text = mySelectedNode.baseText;
                    if (mySelectedNode.isTlk)
                    {
                        this.myNodeTextLabel.Text = "Dialog.tlk Reference:";
                        this.myTextBox.IsEnabled = true;
                        myTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(myTextBox_KeyDown_NumberOnly);
                    }
                    else
                    {
                        this.myCommentsBox.Text = mySelectedNode.myComments;
                        if (mySelectedNode.isLink == true)
                        {
                            this.myTextBox.IsEnabled = false;
                            this.myCommentsBox.Enabled = false;
                        }
                        if (mySelectedNode.isLink == false)
                        {
                            this.myTextBox.IsEnabled = true;
                            this.myCommentsBox.Enabled = true;
                        }
                        if (mySelectedNode.nodeType == ConversationNodeType.NPC)
                        {
                            this.myNodeTextLabel.Text = "NPC Says:";
                        }
                        else
                        {
                            this.myNodeTextLabel.Text = "PC Says:";
                        }
                    }
                }
                else if (mySelectedNode.nodeType == ConversationNodeType.Root)
                {
                    this.myTextBox.Text = "";
                    this.myCommentsBox.Text = "";
                    this.myTextBox.IsEnabled = false;
                    this.myCommentsBox.Enabled = false;
                    this.myNodeTextLabel.Text = "";
                }
            }
        }

        #endregion

        #region Static Functions
        public static string GetFilePath(string fileName)
        {
            char[] slashes = new char[2] { '/', '\\' };
            return fileName.Substring(0, fileName.LastIndexOfAny(slashes) + 1);
        }

        public static string GetShortFileName(string fileName)
        {
            char[] slashes = new char[2] { '/', '\\' };
            return fileName.Substring(fileName.LastIndexOfAny(slashes) + 1);
        }

        public static string GetShortFileNameNoExtension(string fileName)
        {
            fileName = GetShortFileName(fileName);
            return fileName.Substring(0, fileName.LastIndexOf('.'));
        }
        private static string IncrementFileName(string newFileName)
        {
            string fileNumber = GetEndNumber(newFileName);
            newFileName = newFileName.Substring(0, (newFileName.Length - fileNumber.Length));
            int fileNumberInt = int.Parse(fileNumber);
            fileNumberInt++;
            fileNumber = fileNumberInt.ToString();
            fileNumber = NumberToPlaces(fileNumber, 3);
            newFileName = newFileName + fileNumber;
            return newFileName;
        }

        private static string GetEndNumber(string str)
        {
            string toReturn = "";
            if (Char.IsNumber(str, (str.Length - 1)))
            {
                toReturn = GetEndNumber(str.Substring(0, str.Length - 1));
                toReturn += str.Substring((str.Length - 1), 1);
            }
            return toReturn;
        }

        private static string NumberToPlaces(string number, int places)
        {
            if (number.Length < places)
            {
                for (int i = (places - number.Length); i > 0; i--)
                {
                    number = "0" + number;
                }
            }
            return number;
        }

        #endregion

        #region Menu Functions
        public void SaveConversation(int versionNumber)
        {
            if (!everSaved)
            {
                myParentForm.SaveActiveConversationAs();
            }
            else
            {
                bool tbFocused = false;
                if (IsTextBoxSelected)
                {
                    tbFocused = true;
                }
                myTree.Focus();
                SaveContentConversation(myFileName, versionNumber);
                this.changedSinceLastSave = false;
                this.Text = GetShortFileName(myFileName);
                if (tbFocused)
                {
                    myTextBox.Focus();
                }
            }
        }

        public void SaveContentConversation(string fileName, int versionNumber)
        {
            myTree.SaveContentConversation(fileName, versionNumber);
        }

        public void SaveConversationAs(string fileName, int versionNumber)
        {
            myTree.SaveConversation(fileName, versionNumber);
            myFileName = fileName;
            this.changedSinceLastSave = false;
            this.everSaved = true;
            this.Text = GetShortFileName(fileName);
        }

        public void MoveNodeUp()
        {
            myTree.MoveNodeUp();
        }

        public void MoveNodeDown()
        {
            myTree.MoveNodeDown();
        }

        public void FollowSelectedLink()
        {
            myTree.FollowSelectedLink();
        }
        #endregion

        private void MakeNodeSelected()
        {
            if (mySelectedNode != null)
            {
                ConversationNode nodeToSelect = mySelectedNode;
                this.mySelectedNode.BackColor = Color.Transparent;
                this.myTree.SelectedNode = this.mySelectedNode;
            }
        }

        public bool IsTreeSelected
        {
            get
            {
                return myTree.Focused;
            }
        }

        public bool IsTextBoxSelected
        {
            get
            {
                return myTextBox.IsFocused;
            }
        }

        public bool IsCommentsBoxSelected
        {
            get
            {
                return myCommentsBox.Focused;
            }
        }

        public void MakeLinkReal()
        {
            myTree.MakeSelectedLinkReal();
        }

        public ConversationNode GetRootNode()
        {
            return myTree.GetRootNode();
        }

        public void CopySelectedNodeToClipboard()
        {
            mySelectedNode.CopyToClipboard();
        }

        public void CopyTextBox()
        {
            myTextBox.Copy();
        }

        public void PasteNodeAndSubnodes()
        {
            if (Clipboard.GetDataObject().GetDataPresent(ClipboardNode.format.Name)) // a ClipboardNode is stored on the clipboard
            {
                ClipboardNode node = ClipboardNode.GetNodeFromClipboard();
                if (node != null)
                {
                    if (IsTreeSelected)
                    {
                        mySelectedNode.PasteChildFromClipboard();
                    }
                    else if (IsTextBoxSelected)
                    {
                        if (node.linkTo > 0)
                        {
                            myTextBox.PasteIntoBox(node.myLinkText);
                        }
                        else
                        {
                            myTextBox.PasteIntoBox(node.myNode.conversationText);
                        }
                    }
                    else if (IsCommentsBoxSelected)
                    {
                        if (node.linkTo > 0)
                        {
                            myCommentsBox.PasteIntoBox(node.myLinkText);
                        }
                        else
                        {
                            myCommentsBox.PasteIntoBox(node.myNode.conversationText);
                        }
                    }
                    else // wth, they shouldn't be able to click here
                    {
                        throw new Exception("Pasting should not be possible in this situation -- nothing is selected.");
                    }
                }
            }
            else if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text)) // something which has text is stored on the clipboard
            {
                string clipboardText = (string)(Clipboard.GetDataObject().GetData(DataFormats.Text));
                if (IsTreeSelected)
                {
                    throw new Exception("Pasting should not be possible in this situation -- clipboard contains text and tree is selected.");
                }
                else if (IsTextBoxSelected)
                {
                    myTextBox.PasteIntoBox(clipboardText);
                }
                else if (IsCommentsBoxSelected)
                {
                    myCommentsBox.PasteIntoBox(clipboardText);
                }
                else
                {
                    throw new Exception("Pasting should not be possible -- nothing is selected.");
                }
            }
            else throw new Exception("Pasting should not be possible -- nothing usable is on the clipboard.");
        }

        public void PasteNodeAsLink()
        {
            if (IsTreeSelected)
            {
                ClipboardNode node = ClipboardNode.GetNodeFromClipboard();
                if (node != null)
                {
                    mySelectedNode.PasteChildAsLinkFromClipboard();
                }
            }
            else
            {
                throw new Exception("Pasting a link should not be possible in this situation.");
            }
        }

        public void DrawTree(string fileName)
        {
            TreeConversation root = TreeConversation.GetTree(fileName);
            myTree.DrawTree(root);
        }

        public void ExpandAll()
        {
            programmaticExpansion = true;
            myTree.ExpandAll();
            programmaticExpansion = false;
        }

        public void ExpandAllMenuSelection()
        {
            myTree.ExpandAll();
        }

        public void CollapseAll()
        {
            programmaticExpansion = true;
            myTree.CollapseAll();
            myTree.Nodes[0].Expand();
            programmaticExpansion = false;
        }

        public void ExpandAllSubnodes()
        {
            mySelectedNode.ExpandAll();
        }

        public void CollapseAllExceptThis()
        {
            ConversationNode holder = mySelectedNode;
            myTree.CollapseAll();
            holder.EnsureVisible();
            holder.ExpandAll();
        }

        public void CollapseAllMenuSelection()
        {
            myTree.CollapseAll();
            myTree.Nodes[0].Expand();
        }

        public void FocusTree()
        {
            myTree.Focus();
        }

        public void SelectRootNode()
        {
            myTree.SelectRootNode();
        }

        public void SelectThisNode(ConversationNode node)
        {
            mySelectedNode = node;
            myTree.selectedNode = node;
        }

        public void SaveConversationAutoIncremented(int versionNumber)
        {
            if (everSaved == false)
            {
                throw new Exception("Should not be able to save file autoincremented in this case");
            }
            string newFileName = myFileName;
            string filePath;
            string extension;
            bool selectTextBox = IsTextBoxSelected;
            FocusTree();
            filePath = GetFilePath(myFileName);
            newFileName = GetShortFileName(myFileName);
            extension = newFileName.Substring(newFileName.IndexOf("."));
            newFileName = newFileName.Substring(0, newFileName.IndexOf("."));
            if (GetEndNumber(newFileName) == "") // there isn't a number at the end of the filename
                newFileName = newFileName + "001";
            else
                newFileName = IncrementFileName(newFileName);
            newFileName = newFileName + extension;
            if (File.Exists(filePath + newFileName))
            {
                if (MessageBox.Show("A file with the name " + newFileName + " already exists in this directory. Overwrite it?", "Save As Autoincremented", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    SaveConversationAs(filePath + newFileName, versionNumber);
                else
                    myParentForm.SaveActiveConversationAs();
            }
            else
                SaveConversationAs(filePath + newFileName, versionNumber);
        }

        public SelectedSection GetSelectedSection()
        {
            if (IsTextBoxSelected)
            {
                return SelectedSection.TextBox;
            }
            else if (IsCommentsBoxSelected)
            {
                return SelectedSection.Comments;
            }
            else if (IsTreeSelected)
            {
                return SelectedSection.Tree;
            }
            else
            {
                return SelectedSection.None;
            }
        }

        public void FocusTextBox()
        {
            myTextBox.Focus();
        }

        public void FocusCommentsBox()
        {
            myCommentsBox.Focus();
        }

        public void AddNewNode()
        {
            myTree.AddNewNode();
        }

        public bool HasUndos()
        {
            return myUndos.HasUndos();
        }

        public bool HasRedos()
        {
            return myUndos.HasRedos();
        }

        public void Undo()
        {
            UndoState undo = myUndos.Undo();
            TreeConversation treeConversation = undo.treeConversation.DuplicateTreeConversation();
            myTree.DrawTree(treeConversation);
            ExpandAll();
            ApplyExpandState();
            FocusTree();
            if (undo.selectedNode != null && undo.selectedNode.myId != 0)
            {
                GetRootNode().EnsureVisible();
                ConversationNode newSelectedNode = treeConversation.GetConversationNodeById(undo.selectedNode.myId);
                SelectThisNode(newSelectedNode);
                newSelectedNode.EnsureVisible();
            }
            else
            {
                SelectRootNode();
                GetRootNode().EnsureVisible();
            }

            if (!HasUndos())
            {
                NoUndos();
            }
            GotRedos();
        }

        public void Redo()
        {
            FocusTree();
            UndoState redo = myUndos.Redo();
            TreeConversation treeConversation = redo.treeConversation.DuplicateTreeConversation();
            myTree.DrawTree(treeConversation);
            ExpandAll();
            ApplyExpandState();
            FocusTree();
            if (redo.selectedNode != null && redo.selectedNode.myId != 0)
            {
                GetRootNode().EnsureVisible();
                ConversationNode newSelectedNode = treeConversation.GetConversationNodeById(redo.selectedNode.myId);
                SelectThisNode(newSelectedNode);
                newSelectedNode.EnsureVisible();
            }
            else
            {
                SelectRootNode();
                GetRootNode().EnsureVisible();
            }

            if (!HasRedos())
            {
                NoRedos();
            }
            GotUndos();
        }

        public void GotUndos()
        {
            myParentForm.GotUndos();
            myTree.GotUndos();
        }

        public void GotRedos()
        {
            myParentForm.GotRedos();
            myTree.GotRedos();
        }

        public void NoUndos()
        {
            myParentForm.NoUndos();
            myTree.NoUndos();
        }

        public void NoRedos()
        {
            myParentForm.NoRedos();
            myTree.NoRedos();
        }

        public void ApplyExpandState()
        {
            foreach (int idNum in collapsedNodes)
            {
                programmaticExpansion = true;
                myTree.myTreeConversation.GetConversationNodeById(idNum).Collapse();
                programmaticExpansion = false;
            }
        }

        public void SetupFinishedOpening()
        {
            ExpandAll();
            this.collapsedNodes = myParentForm.GetExpandState(myFileName);
            ApplyExpandState();
            FocusTree();
            SelectRootNode();
            lastState = UndoState.CaptureCurrentState(myTree);
        }

        public int CheckVersion()
        {
            return myTree.CheckVersion();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}