using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Editor;
using ConversationEditorGui.Properties;

namespace ConversationEditorGui
{
    public partial class Form1 : Form
    {
        public const int VERSION_NUMBER = 2;

        private Bitmap _backBuffer;

        private const int WM_DRAWCLIPBOARD = 776;

        private MainMenuStripClass mainDropdownMenus;

        private MainToolbar mainToolbar;

        private Timer textboxTimer;

        private Timer commentsTimer;

        public TreeTab ActiveTab
        {
            get
            {
                return (TreeTab)tabControl1.SelectedTab;
            }
        }

        private CloseDialog closeDialog = null;
        private DeleteNodeDialog deleteNodeDialog = null;
        private TestConversation testConversationDialog = null;

        private ProgramSettings programSettings;

        public Form1()
        {
            InitializeComponent();
            mainDropdownMenus = new MainMenuStripClass(this);
            mainToolbar = new MainToolbar(this);
            this.Controls.Add(this.mainToolbar);
            this.Controls.Add(this.mainDropdownMenus);
            this.MainMenuStrip = this.mainDropdownMenus;
            this.mainDropdownMenus.ResumeLayout(false);
            this.mainDropdownMenus.PerformLayout();
            this.mainToolbar.ResumeLayout(false);
            this.mainToolbar.PerformLayout();

            Win32.SetClipboardViewer(this.Handle.ToInt32());

            textboxTimer = new Timer
            {
                Interval = 3000
            };
            textboxTimer.Tick += new EventHandler(textboxTimer_Tick);
            textboxTimer.Stop();

            commentsTimer = new Timer
            {
                Interval = 3000
            };
            commentsTimer.Tick += new EventHandler(commentsTimer_Tick);
            commentsTimer.Stop();
        }

        #region Overridden functions

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_backBuffer == null)
            {
                _backBuffer = new Bitmap(ClientSize.Width > 0 ? ClientSize.Width : 1, ClientSize.Height > 0 ? ClientSize.Height : 1);
            }
            Graphics g = Graphics.FromImage(_backBuffer);
            g.Dispose();
            e.Graphics.DrawImageUnscaled(_backBuffer, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_backBuffer != null)
            {
                _backBuffer.Dispose();
                _backBuffer = null;
            }
            base.OnSizeChanged(e);
        }

        protected override void WndProc(ref Message m)
        {
            //if (m.Msg == 123 /*WM_UAHINITMENU*/ || m.Msg == 279 /*WM_INITMENUPOPUP*/ || m.Msg == 0x0116 /*WM_INITMENU*/)
            //{                
            //    IntPtr shortcut = m.Msg == 0x0093 ? Marshal.ReadIntPtr(m.LParam) : m.WParam;
            //    // add <your menu id> to shortcut
            //}
            base.WndProc(ref m);

            if (m.Msg == WM_DRAWCLIPBOARD) // The clipboard contents have been changed
            {
                CheckClipboard();
            }
        }

        #endregion

        #region Dropdown Menu Functions

        #region File Dropdown Menu

        public void NewConversation()
        {
            TreeTab myTreeTab = new TreeTab("", tabControl1, this);
            TreeConversation root = TreeConversation.NewEmptyTree();
            myTreeTab.myTree.myTreeConversation = root;
            root.myTree = myTreeTab.myTree;
            myTreeTab.myTree.Nodes.Add(root);
            myTreeTab.lastState = UndoState.CaptureCurrentState(myTreeTab.myTree);
            AddATab(myTreeTab);
            myTreeTab.FocusTree();
            myTreeTab.SelectRootNode();
        }

        public void AddATab(TreeTab treeTab)
        {
            tabControl1.SelectedTab = treeTab;
            if (closeTabButton.Visible == false)
            {
                closeTabButton.Show();
            }
            mainToolbar.TreeTabOpen();
            this.mainDropdownMenus.TreeTabOpen();
            TabSwitch();
        }

        public void OpenConversation()
        {
            string fileName;
            this.openFileDialog1.Filter = "All files (*.*)|*.*|NWN1 files (*.yml)|*.yml|NWN2 files (*.xml)|*.xml";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //open
                if (openFileDialog1.FileNames.Length > 0)
                {
                    for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                    {
                        fileName = openFileDialog1.FileNames[i];
                        if (IsFileAlreadyOpen(fileName) == null)
                        {
                            TreeTab myTreeTab = new TreeTab(fileName, tabControl1, this);
                            DrawTree(myTreeTab, fileName);
                            myTreeTab.SetupFinishedOpening();
                            AddATab(myTreeTab);
                            CheckVersion(myTreeTab);
                        }
                        else
                        {
                            tabControl1.SelectedTab = IsFileAlreadyOpen(fileName);
                        }
                    }
                }
            }
            else
            {
                //cancel open
            }
        }

        private TreeTab IsFileAlreadyOpen(string fileName)
        {
            if (tabControl1.TabPages.Count > 0)
            {
                foreach (TreeTab tab in tabControl1.TabPages)
                {
                    if (tab.myFileName == fileName)
                    {
                        return tab;
                    }
                }
            }
            return null;
        }

        public void SaveActiveConversation()
        {
            if (ActiveTab != null)
            {
                ActiveTab.SaveConversation(VERSION_NUMBER);
            }
        }

        public void SaveActiveConversationAs()
        {
            if (ActiveTab != null)
            {
                SelectedSection selectedSection = GetSelectedSection();
                ActiveTab.FocusTree();
                saveFileAsDialog.Title = ActiveTab.Text;
                if (ActiveTab.myFileName != "")
                {
                    this.saveFileAsDialog.FileName = ActiveTab.myFileName;
                }
                else
                {
                    this.saveFileAsDialog.FileName = "Untitled.yml";
                }
                if (this.saveFileAsDialog.ShowDialog() == DialogResult.OK)
                {
                    //save
                    ActiveTab.SaveConversationAs(saveFileAsDialog.FileName, VERSION_NUMBER);
                    TabSwitch();
                }
                else
                {
                    //cancel save
                }
                if (selectedSection == SelectedSection.TextBox)
                {
                    ActiveTab.FocusTextBox();
                }
                else if (selectedSection == SelectedSection.Comments)
                {
                    ActiveTab.FocusCommentsBox();
                }
            }
        }

        public void SaveActiveConversationAutoIncrement()
        {
            if (ActiveTab != null)
            {
                ActiveTab.SaveConversationAutoIncremented(VERSION_NUMBER);
            }
        }

        private void CloseTab()
        {
            ActiveTab.CloseTab();
        }


        #endregion

        #region Conversation Menu

        public void TestConversation()
        {
            SetTestConversation(ActiveTab.myTree.myTreeConversation);
            ShowTestConversationDialog();
        }

        public void ExpandAllNodes()
        {
            ActiveTab.ExpandAllMenuSelection();
        }

        public void CollapseAllNodes()
        {
            ActiveTab.CollapseAllMenuSelection();
        }

        public void CopyConversationAsText()
        {
            IDataObject dataObj = new DataObject();
            ConversationNode rootNode = GetActiveRootNode();
            dataObj.SetData(DataFormats.Text, true, rootNode.GetTextCopy(""));
            Clipboard.SetDataObject(dataObj);
        }

        #endregion

        #region Node Menu

        public void NewChildNode()
        {
            if (ActiveTab != null)
            {
                if (ActiveTab.mySelectedNode != null)
                {
                    ActiveTab.FocusTree();
                    ActiveTab.Updated();
                    ActiveTab.SetLastState();
                    ActiveTab.AddNewNode();
                    ActiveTab.UpdateUndo();
                }
            }
        }

        public void EditNode()
        {
            ActiveTab.EditNode();
        }

        public void Undo()
        {
            if (ActiveTab.HasUndos())
            {
                ActiveTab.Undo();
            }
            else
                throw new Exception("Nothing to undo.");
        }

        public void Redo()
        {
            if (ActiveTab.HasRedos())
            {
                ActiveTab.Redo();
            }
            else
                throw new Exception("Nothing to redo.");
        }

        public void CopyNodeAndSubnodes()
        {
            if (ActiveTab.IsTreeSelected)
            {
                ActiveTab.CopySelectedNodeToClipboard();
            }
            else if (ActiveTab.IsTextBoxSelected)
            {
                ActiveTab.CopyTextBox();
            }
            else // Nothing is available for copying
            {

            }
        }

        public void PasteNodeAndSubnodes()
        {
            ActiveTab.PasteNodeAndSubnodes();
        }

        public void PasteNodeAsLink()
        {
            ActiveTab.PasteNodeAsLink();
        }

        public void FollowSelectedLink()
        {
            ActiveTab.FollowSelectedLink();
        }

        public void MakeLinkReal()
        {
            ActiveTab.MakeLinkReal();
        }

        public void ExpandAllSubnodes()
        {
            ActiveTab.ExpandAllSubnodes();
        }

        public void CollapseAllExceptThis()
        {
            ActiveTab.CollapseAllExceptThis();
        }

        public void MoveNodeUp()
        {
            ActiveTab.MoveNodeUp();
        }

        public void MoveNodeDown()
        {
            ActiveTab.MoveNodeDown();
        }

        public void DeleteNodeAndSubnodes()
        {
            ActiveTab.DeleteTreeNode();
        }

        #endregion

        #endregion

        public List<int> GetExpandState(string fileName)
        {
            if (programSettings.ContainsKey(fileName))
            {
                return programSettings.GetValue(fileName);
            }
            return new List<int>();
        }

        public void SetExpandState(string fileName, List<int> state)
        {
            programSettings.AddSetting(fileName, state);
            programSettings.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            closeDialog = new CloseDialog();
            closeDialog.Hide();
            closeTabButton.Hide();
            deleteNodeDialog = new DeleteNodeDialog();
            deleteNodeDialog.Hide();
            testConversationDialog = new TestConversation();
            testConversationDialog.Hide();

            programSettings = ProgramSettings.LoadSettings();
        }

        private void DrawTree(TreeTab treeTab, string fileName)
        {
            treeTab.DrawTree(fileName);
        }

        public void NewNodeSelected(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                ConversationNode selectedNode = (ConversationNode)e.Node;
                if (selectedNode.nodeType != ConversationNodeType.Root)
                {
                    EnableCopy();
                    EnableDelete();
                    EnableExpandCollapse();
                    if (selectedNode.isLink)
                    {
                        this.mainDropdownMenus.LinkSelected();
                        this.mainToolbar.EnableCreate(false);
                        DisablePaste();
                    }
                    else
                    {
                        this.mainDropdownMenus.NonLinkSelected();
                        this.mainToolbar.EnableCreate(true);
                        CheckClipboard();
                    }
                }
                else
                {
                    DisableCopy();
                    DisableDelete();
                    DisableExpandCollapse();
                }
            }
            else
            {
                DisableCopy();
                DisableDelete();
                DisableExpandCollapse();
            }
        }

        public void CloseCurrentTab()
        {
            if (tabControl1.TabCount > 0)
            {
                CloseTab();
            }
            if (tabControl1.TabCount == 0)
            {
                closeTabButton.Hide();
                mainDropdownMenus.NoTabsOpen();
                mainToolbar.NoTabsOpen();
            }
        }

        private void closeTabButton_Click(object sender, EventArgs e)
        {
            CloseCurrentTab();
        }

        private void closeTabButton_MouseHover(object sender, System.EventArgs e)
        {
            closeTabButton.Image = global::ConversationEditorGui.Properties.Resources.closeButtonHover;
        }

        private void closeTabButton_MouseLeave(object sender, System.EventArgs e)
        {
            closeTabButton.Image = global::ConversationEditorGui.Properties.Resources.closeButtonOff;
        }

        public void LinkedTreeInFocus(object sender, EventArgs e)
        {
            CheckClipboard();
        }

        public void LinkedTreeLostFocus(object sender, EventArgs e)
        {
            DisablePaste();
        }

        public void LinkedTextBoxInFocus(object sender, EventArgs e)
        {
            CheckClipboard();
            ActiveTab.myTextBox.textOnEntry = ActiveTab.myTextBox.Text;
            ActiveTab.myTextBox.stateOnEntry = UndoState.CaptureCurrentState(ActiveTab.myTree);
            textboxTimer.Start();
        }

        private void textboxTimer_Tick(object sender, EventArgs e)
        {
            IfTextboxChangedUpdateUndo();
        }

        private void IfTextboxChangedUpdateUndo()
        {
            if (ActiveTab.myTextBox.textOnEntry != ActiveTab.myTextBox.Text)
            {
                ActiveTab.AddUndo(ActiveTab.myTextBox.stateOnEntry);
                ActiveTab.myTextBox.textOnEntry = ActiveTab.myTextBox.Text;
                ActiveTab.myTextBox.stateOnEntry = UndoState.CaptureCurrentState(ActiveTab.myTree);
            }
            if (GetSelectedSection() == SelectedSection.TextBox)
            {
                textboxTimer.Start();
            }
        }

        public void LinkedTextBoxLostFocus(object sender, EventArgs e)
        {
            DisablePaste();
            if (ActiveTab.myTextBox.textOnEntry != ActiveTab.myTextBox.Text)
            {
                ActiveTab.AddUndo(ActiveTab.myTextBox.stateOnEntry);
            }
            ActiveTab.myTextBox.textOnEntry = "";
            ActiveTab.myTextBox.stateOnEntry = null;
            textboxTimer.Stop();
        }

        public void CommentsBoxInFocus(object sender, EventArgs e)
        {
            CheckClipboard();
            ActiveTab.myCommentsBox.textOnEntry = ActiveTab.myCommentsBox.Text;
            ActiveTab.myCommentsBox.stateOnEntry = UndoState.CaptureCurrentState(ActiveTab.myTree);
            commentsTimer.Start();
        }

        private void commentsTimer_Tick(object sender, EventArgs e)
        {
            IfCommentsBoxChangedUpdateUndo();
        }

        private void IfCommentsBoxChangedUpdateUndo()
        {
            if (ActiveTab.myCommentsBox.textOnEntry != ActiveTab.myCommentsBox.Text)
            {
                ActiveTab.AddUndo(ActiveTab.myCommentsBox.stateOnEntry);
                ActiveTab.myCommentsBox.textOnEntry = ActiveTab.myCommentsBox.Text;
                ActiveTab.myCommentsBox.stateOnEntry = UndoState.CaptureCurrentState(ActiveTab.myTree);
            }
            if (GetSelectedSection() == SelectedSection.Comments)
            {
                commentsTimer.Start();
            }
        }

        public void CommentsBoxLostFocus(object sender, EventArgs e)
        {
            DisablePaste();
            if (ActiveTab.myCommentsBox.textOnEntry != ActiveTab.myCommentsBox.Text)
            {
                ActiveTab.AddUndo(ActiveTab.myCommentsBox.stateOnEntry);
            }
            ActiveTab.myCommentsBox.textOnEntry = "";
            ActiveTab.myCommentsBox.stateOnEntry = null;
            commentsTimer.Stop();
        }

        void tabControl1_Selected(object sender, System.Windows.Forms.TabControlEventArgs e)
        {
            TabSwitch();
        }

        public void TabSwitch()
        {
            if (ActiveTab != null)
            {
                if (ActiveTab.everSaved)
                {
                    this.mainDropdownMenus.EnableAutoIncrement();
                    this.mainToolbar.EnableAutoIncrement();
                    this.mainToolbar.EnableSaveAs();
                }
                else
                {
                    this.mainDropdownMenus.DisableAutoIncrement();
                    this.mainToolbar.DisableAutoIncrement();
                    this.mainToolbar.DisableSaveAs();
                }
            }
        }

        public ConversationNode GetActiveRootNode()
        {
            return ActiveTab.GetRootNode();
        }

        public SelectedSection GetSelectedSection()
        {
            if (tabControl1.TabPages.Count == 0)
            {
                return SelectedSection.None;
            }
            else return ActiveTab.GetSelectedSection();
        }

        private void CheckClipboard()
        {
            if (GetSelectedSection() == SelectedSection.None)
            {
                DisablePaste();
            }
            else
            {
                if (Clipboard.GetDataObject().GetDataPresent(ClipboardNode.format.Name)) // a ClipboardNode is stored on the clipboard
                {
                    mainDropdownMenus.EnablePaste();
                    mainToolbar.EnablePaste();
                }
                else if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text)) // something which has text is stored on the clipboard
                {
                    mainDropdownMenus.PasteTextOnly();
                    mainToolbar.PasteTextOnly();
                }
                else
                {
                    mainDropdownMenus.DisablePaste();
                    mainToolbar.DisablePaste();
                }

                foreach (TabPage tabPage in this.tabControl1.TabPages)
                {
                    ((TreeTab)tabPage).ClipboardUpdated();
                }
            }
        }

        private void DisablePaste()
        {
            mainDropdownMenus.DisablePaste();
            mainToolbar.DisablePaste();
            foreach (TabPage tabPage in this.tabControl1.TabPages)
            {
                ((TreeTab)tabPage).DisablePaste();
            }
        }

        private void EnableCopy()
        {
            ActiveTab.EnableCopy();
            mainDropdownMenus.EnableCopy();
            mainToolbar.EnableCopy();
        }

        private void DisableCopy()
        {
            ActiveTab.DisableCopy();
            mainDropdownMenus.DisableCopy();
            mainToolbar.DisableCopy();
        }

        private void EnableDelete()
        {
            ActiveTab.EnableDelete();
            mainDropdownMenus.EnableDelete();
            mainToolbar.EnableDelete();
        }

        private void DisableDelete()
        {
            ActiveTab.DisableDelete();
            mainDropdownMenus.DisableDelete();
            mainToolbar.DisableDelete();
        }

        private void EnableExpandCollapse()
        {
            mainDropdownMenus.EnableExpandCollapse();
        }

        private void DisableExpandCollapse()
        {
            mainDropdownMenus.DisableExpandCollapse();
        }

        public void SetDeleteDialogText(string text)
        {
            deleteNodeDialog.setNodeText(text);
        }

        public DeleteNodeDialogOptions ShowDeleteDialog()
        {
            deleteNodeDialog.ShowDialog();
            return deleteNodeDialog.clickedButton;
        }

        public void SetCloseDialogText(string fileName)
        {
            closeDialog.fileNameText(fileName);
        }

        public CloseDialogOption ShowCloseDialog()
        {
            closeDialog.ShowDialog();
            return closeDialog.clickedButton;
        }

        public void ShowTestConversationDialog()
        {
            testConversationDialog.ShowDialog();
        }

        public void SetTestConversation(TreeConversation treeConversation)
        {
            testConversationDialog.SetConversation(treeConversation);
        }

        public void GotUndos()
        {
            mainDropdownMenus.GotUndos();
            mainToolbar.GotUndos();
        }

        public void GotRedos()
        {
            mainDropdownMenus.GotRedos();
            mainToolbar.GotRedos();
        }

        public void NoUndos()
        {
            mainDropdownMenus.NoUndos();
            mainToolbar.NoUndos();
        }

        public void NoRedos()
        {
            mainDropdownMenus.NoRedos();
            mainToolbar.NoRedos();
        }

        public void CheckVersion(TreeTab checkTab)
        {
            if (checkTab.CheckVersion() < VERSION_NUMBER)
            {
                if (MessageBox.Show("This conversation was saved in a previous version of Conversation Editor. It is recommended that you save to update your file. Would you like to save now?", "Older File Version Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveActiveConversationAs();
                }
            }
        }
    }
}