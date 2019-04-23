using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using Editor;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConversationEditorGui
{
    public class ConversationNode : TreeNode
    {
        #region Properties
        public ContentNode myNode = null;

        public int myId
        {
            get
            {
                if (nodeType == ConversationNodeType.Root)
                {
                    return 0;
                }
                else
                    return myNode.idNum;
            }
        }

        public string myComments
        {
            get
            {
                if (nodeType == ConversationNodeType.Root)
                    return "";
                else
                    return myNode.conversationComments;
            }
            set
            {
                myNode.conversationComments = value;
            }
        }

        public int linkTo
        {
            get
            {
                if (nodeType == ConversationNodeType.Root)
                {
                    return 0;
                }
                else return myNode.linkTo;
            }
        }

        public List<ConversationNode> myLinks = new List<ConversationNode>();

        public bool isLink
        {
            get
            {
                return (linkTo != 0);
            }
        }

        /// <summary>
        /// In class handlers for the str_refs. this keeps all the ugly finds down here.
        /// </summary>
        public bool isTlk
        {
            get
            {
                if (!_isTlk.HasValue)
                {
                    if (myNode != null && myNode.additionalData.Count > 0)
                    {
                        Info strrefNode;
                        _isTlk = (strrefNode = myNode.additionalData.Find(x => x.variableName == "str_ref")) != null;
                        _tlkRef = _isTlk.Value ? strrefNode.variableValue : null;
                    }
                    else _isTlk = false;
                }
                return _isTlk.Value;
            }
        }

        private bool? _isTlk;

        private string _tlkRef;

        public TreeConversation myTreeConversation = null;

        public string myTreeConversationName
        {
            get
            {
                return myTreeConversation.myName;
            }
        }

        public ConversationNodeType nodeType
        {
            get
            {
                if (myNode == null)
                {
                    return ConversationNodeType.Root;
                }
                else return myNode.nodeType;
            }
        }

        public ConversationNode myParent
        {
            get
            {
                if (nodeType == ConversationNodeType.Root)
                {
                    return this;
                }
                else
                {
                    return (ConversationNode)this.Parent;
                }
            }
        }

        public string baseText
        {
            get
            {
                if (nodeType == ConversationNodeType.Root)
                    return "Root";
                else if (isTlk)
                    return _tlkRef;
                else
                    return myNode.conversationText;
            }
            set
            {
                if (nodeType == ConversationNodeType.Root)
                { }
                else if (isTlk)
                {
                    _tlkRef = value;
                    myNode.additionalData.Find(x => x.variableName == "str_ref").variableValue = value;
                }
                else
                    myNode.conversationText = value;
            }
        }

        public bool shouldBeExpanded = true;
        #endregion

        public void UpdateTextAndColor()
        {
            ContentNode tempNode = null;

            if (nodeType == ConversationNodeType.Root)
            {
                Text = baseText;
                ForeColor = Color.Black;
            }
            else if (linkTo == 0)
            {
                if (Nodes.Count == 0)
                {
                    Text = baseText + " [End Dialog]";
                }
                else
                {
                    Text = baseText;
                }

                if (baseText == "" || Text == "")
                {
                    if (nodeType == ConversationNodeType.PC)
                    {
                        Text = "[Continue]";
                    }
                    else if (nodeType == ConversationNodeType.NPC)
                    {
                        Text = "[End Dialog]";
                    }
                }

                if (isTlk)
                {
                    Text = "STRREF: "+Text;
                }

                if (nodeType == ConversationNodeType.NPC)
                {
                    ForeColor = Color.Maroon;
                }
                else if (nodeType == ConversationNodeType.PC)
                {
                    ForeColor = Color.Navy;
                }
            }
            else
            {
                tempNode = myTreeConversation.myConversation.GetContentNodeById(linkTo);
                baseText = tempNode.conversationText;
                if (tempNode.subNodes.Count == 0)
                {
                    Text = baseText + " [End Dialog]";
                }
                else if (baseText == "")
                {
                    Text = "[Continue]";
                }
                else
                {
                    Text = baseText;
                }

                ForeColor = Color.Gray;
            }

            // Image for node
            if (myComments != "" && myComments != null)
            {
                this.ImageIndex = 2;
                this.SelectedImageIndex = 2;
            }
            else if (this.Nodes.Count > 0 && this.IsExpanded)
            {
                this.ImageIndex = 1;
                this.SelectedImageIndex = 1;
            }
            else
            {
                this.ImageIndex = 0;
                this.SelectedImageIndex = 0;
            }
        }

        #region Initialization and Setup Functions
        public static ConversationNode GetConversationNode(ContentNode myContentNode, TreeConversation thisTreeConversation)
        {
            ConversationNode newNode = new ConversationNode
            {
                myNode = myContentNode,
                myTreeConversation = thisTreeConversation,
                Name = ""
            };

            for (int i = 0; i < myContentNode.subNodes.Count; i++)
            {
                ContentNode subNode = myContentNode.subNodes[i];
                newNode.Nodes.Add(GetConversationNode(subNode, thisTreeConversation));
            }

            newNode.UpdateTextAndColor();
            return newNode;
        }

        public static ConversationNode NewConversationNode(int nextIdNum, int nextOrderNum)
        {
            ConversationNode newNode = new ConversationNode
            {
                myNode = ContentNode.NewContentNode(nextIdNum, nextOrderNum)
            };
            return newNode;
        }

        public void GetConversationNode(ClipboardNode clipboardNode) // called on parent
        {
            ConversationNode newNode = new ConversationNode();
            int nextIdNum = myTreeConversation.GetNextIdNum();
            int nextOrderNum = GetNextOrderNum();
            newNode.myNode = clipboardNode.DuplicateMyContentNode(nextIdNum, nextOrderNum);
            newNode.myTreeConversation = myTreeConversation;
            newNode.Name = "";
            foreach (ClipboardNode subNode in clipboardNode.subNodes)
            {
                newNode.GetConversationNode(subNode);
            }
            if (this.nodeType == ConversationNodeType.Root)
            {
                myTreeConversation.AddContentNodeToRoot(newNode.myNode);
            }
            else
            {
                myNode.subNodes.Add(newNode.myNode);
            }
            Nodes.Add(newNode);
            newNode.UpdateTextAndColor();
        }

        public void BuildLinks()
        {
            // function for building out links for myself and all subnodes by attaching them to their originals
            if (this.linkTo != 0)
            {
                ConversationNode myOriginal = myTreeConversation.GetConversationNodeById(linkTo);
                myOriginal.myLinks.Add(this);
            }
            foreach (ConversationNode subNode in this.Nodes)
            {
                subNode.BuildLinks();
            }
        }

        #endregion

        #region Deletion
        public ConversationNode IsAnyChildLinkedExternally(ConversationNode parentNode)
        {
            foreach (ConversationNode link in myLinks)
            {
                if (!link.IsChildOf(parentNode))
                {
                    return link;
                }
            }
            foreach (ConversationNode subNode in this.Nodes)
            {
                ConversationNode canDelete = subNode.IsAnyChildLinkedExternally(parentNode);
                if (canDelete != null)
                {
                    return canDelete;
                }
            }
            return null;
        }

        public void DeleteAllChildren()
        {
            foreach (ConversationNode subNode in this.Nodes)
            {
                subNode.DeleteAllChildren();
            }
            foreach (ConversationNode subNode in this.Nodes)
            {
                if (this.nodeType == ConversationNodeType.Root)
                {
                    myTreeConversation.RemoveContentNodeFromRoot(subNode.myNode);
                }
                else
                {
                    myNode.subNodes.Remove(subNode.myNode);
                }
                if (subNode.isLink)
                {
                    subNode.BreakLink();
                }
            }
            this.Nodes.Clear();
        }

        private void BreakLink()
        {
            ConversationNode myOriginal = myTreeConversation.GetConversationNodeById(linkTo);
            myOriginal.myLinks.Remove(this);
        }

        public virtual SelectedSection GetSelectedSection()
        {
            return myTreeConversation.GetSelectedSection();
        }

        public ConversationNode DeleteChild(ConversationNode myChild)
        {
            ConversationNode deleteOK = myChild.IsAnyChildLinkedExternally(myChild);
            if (deleteOK == null)
            {
                SelectedSection oldSelection = GetSelectedSection();
                myChild.DeleteAllChildren();
                this.Nodes.Remove(myChild);
                if (nodeType != ConversationNodeType.Root)
                {
                    this.myNode.subNodes.Remove(myChild.myNode);
                }
                else
                {
                    myTreeConversation.RemoveContentNodeFromRoot(myChild.myNode);
                }
                if (myChild.isLink)
                {
                    myChild.BreakLink();
                }
                myTreeConversation.FocusTree();
                myTreeConversation.SelectThisNode(this);
                if (oldSelection == SelectedSection.TextBox)
                {
                    myTreeConversation.FocusTextBox();
                }
                else if (oldSelection == SelectedSection.Comments)
                {
                    myTreeConversation.FocusCommentsBox();
                }
                return null;
            }
            else
            {
                return deleteOK;
            }
        }

        public void DeleteChildAndExternalLinks(ConversationNode myChild)
        {
            UndoState undo = UndoState.CaptureCurrentState(myTreeConversation.myTree);
            ConversationNode outsideLinkNode = null;
            outsideLinkNode = myChild.IsAnyChildLinkedExternally(myChild);
            while (outsideLinkNode != null)
            {
                outsideLinkNode.BreakLink();
                outsideLinkNode.DeleteSelf();

                outsideLinkNode = myChild.IsAnyChildLinkedExternally(myChild);
            }
            myChild.DeleteAllChildren();
            if (myChild.isLink)
            {
                myChild.BreakLink();
            }
            if (nodeType == ConversationNodeType.Root)
            {
                myTreeConversation.RemoveContentNodeFromRoot(myChild.myNode);
            }
            else
            {
                myNode.subNodes.Remove(myChild.myNode);
            }
            Nodes.Remove(myChild);
            myTreeConversation.myTree.myParentTab.AddUndo(undo);
        }

        public void DeleteSelf()
        {
            myParent.Nodes.Remove(this);
        }

        #endregion

        #region Copy, Paste, and Link

        public void PasteChildAsLinkFromClipboard()
        {
            ClipboardNode clipboardNode = ClipboardNode.GetNodeFromClipboard();

            if (clipboardNode == null)
            {
                throw new Exception("Nothing is on the clipboard to paste.");
            }
            else if (clipboardNode.linkTo > 0)
            {
                throw new Exception("Should not be able to create a link to another link.");
            }
            else if (clipboardNode.nodeType == ConversationNodeType.PC && this.nodeType == ConversationNodeType.PC)
            {
                MessageBox.Show("Cannot create a link to a PC node as a child of another PC node.");
            }
            else if (this.nodeType == ConversationNodeType.Root && clipboardNode.nodeType == ConversationNodeType.PC)
            {
                MessageBox.Show("Cannot create a link to a PC node directly under the conversation root.");
            }
            else if (this.nodeType == ConversationNodeType.NPC && clipboardNode.nodeType == ConversationNodeType.NPC)
            {
                MessageBox.Show("Cannot create a link to an NPC node as a child of another NPC node.");
            }
            else if (this.myTreeConversationName != clipboardNode.myTreeConversationName)
            {
                MessageBox.Show("Cannot create a link to a node in a different conversation.");
            }
            else // all our bases are covered, we're good
            {
                if (myTreeConversation.GetTextById(clipboardNode.myNode.idNum) == clipboardNode.myNode.conversationText)
                {
                    UndoState undo = UndoState.CaptureCurrentState(myTreeConversation.myTree);
                    myTreeConversation.UpdateTab();
                    ConversationNode newNode = new ConversationNode();
                    ContentNode newContent = ContentNode.NewContentNodeLink(GetNextOrderNum());
                    newNode.myNode = newContent;
                    if (this.nodeType == ConversationNodeType.PC || this.nodeType == ConversationNodeType.Root)
                    {
                        newContent.nodeType = ConversationNodeType.NPC;
                    }
                    else
                    {
                        newContent.nodeType = ConversationNodeType.PC;
                    }
                    newContent.idNum = 0;
                    newContent.conversationComments = "";
                    newContent.conversationText = "";
                    newContent.linkTo = clipboardNode.myNode.idNum;
                    newNode.myTreeConversation = this.myTreeConversation;
                    this.Nodes.Add(newNode);
                    if (this.nodeType == ConversationNodeType.Root)
                    {
                        myTreeConversation.AddContentNodeToRoot(newContent);
                    }
                    else
                    {
                        this.myNode.subNodes.Add(newContent);
                    }
                    ConversationNode linkTo = myTreeConversation.GetConversationNodeById(clipboardNode.myNode.idNum);
                    linkTo.myLinks.Add(newNode);
                    newNode.UpdateTextAndColor();
                    this.Expand();
                    myTreeConversation.myTree.myParentTab.AddUndo(undo);
                }
                else
                {
                    MessageBox.Show("The text of the node has changed since it was copied to the clipboard, or this is a different conversation. The link could not be pasted.");
                }
            }
        }

        public void PasteChildFromClipboard()
        {
            ClipboardNode clipboardNode = ClipboardNode.GetNodeFromClipboard();

            if (clipboardNode == null)
            {
                throw new Exception("Nothing is on the clipboard to paste.");
            }
            else if (clipboardNode.nodeType == ConversationNodeType.PC && this.nodeType == ConversationNodeType.PC)
            {
                MessageBox.Show("Cannot create a PC node as a child of another PC node.");
            }
            else if (this.nodeType == ConversationNodeType.Root && clipboardNode.nodeType == ConversationNodeType.PC)
            {
                MessageBox.Show("Cannot create a PC node directly under the conversation root.");
            }
            else if (this.nodeType == ConversationNodeType.NPC && clipboardNode.nodeType == ConversationNodeType.NPC)
            {
                MessageBox.Show("Cannot create an NPC node as a child of another NPC node.");
            }
            else // all our bases are covered, we're good
            {
                UndoState undo = UndoState.CaptureCurrentState(myTreeConversation.myTree);
                myTreeConversation.UpdateTab();
                ConversationNode tempNode;
                ConversationNode newNode = new ConversationNode();
                int nextIdNum = myTreeConversation.GetNextIdNum();
                int nextOrderNum = GetNextOrderNum();
                newNode.myNode = clipboardNode.myNode.DuplicateContentNode(nextIdNum, nextOrderNum);
                newNode.myTreeConversation = myTreeConversation;
                newNode.Name = "";

                if (this.nodeType == ConversationNodeType.Root)
                {
                    myTreeConversation.AddContentNodeToRoot(newNode.myNode);
                }
                else
                {
                    myNode.subNodes.Add(newNode.myNode);
                }
                Nodes.Add(newNode);

                foreach (ClipboardNode subNode in clipboardNode.subNodes)
                {
                    if (subNode.linkTo != 0)
                    {
                        if (this.myTreeConversation.GetConversationNodeById(subNode.linkTo) != null && this.myTreeConversation.GetTextById(subNode.linkTo) == subNode.myLinkText)
                        {
                            ConversationNode originalNode = myTreeConversation.GetConversationNodeById(subNode.linkTo);
                            originalNode.CreateLinkToMe(newNode);
                        }
                        else
                        {
                            tempNode = ConversationNode.MakeBrokenLink(myTreeConversation.GetNextIdNum(), newNode.GetNextOrderNum());
                            tempNode.myNode.conversationText += subNode.myLinkText;
                            tempNode.myTreeConversation = myTreeConversation;
                            if (newNode.nodeType == ConversationNodeType.PC)
                            {
                                tempNode.myNode.nodeType = ConversationNodeType.NPC;
                            }
                            else
                            {
                                tempNode.myNode.nodeType = ConversationNodeType.PC;
                            }
                            newNode.Nodes.Add(tempNode);
                            newNode.myNode.subNodes.Add(tempNode.myNode);
                            tempNode.UpdateTextAndColor();
                        }
                    }
                    else
                    {
                        newNode.GetConversationNode(subNode);
                    }
                }

                newNode.UpdateTextAndColor();
                newNode.ExpandAll();
                this.Expand();
                this.EnsureVisible();
                myTreeConversation.myTree.myParentTab.AddUndo(undo);
            }
        }

        public void DuplicateLink(ConversationNode parent)
        {
            if (nodeType == ConversationNodeType.PC && parent.nodeType == ConversationNodeType.PC)
            {
                throw new Exception("Cannot copy a link to a PC node as a child of another PC node.");
            }
            else if (parent.nodeType == ConversationNodeType.Root && nodeType == ConversationNodeType.PC)
            {
                throw new Exception("Cannot copy a link to a PC node directly under the conversation root.");
            }
            else if (parent.nodeType == ConversationNodeType.NPC && nodeType == ConversationNodeType.NPC)
            {
                throw new Exception("Cannot copy a link to an NPC node as a child of another NPC node.");
            }
            else if (parent.myTreeConversation != this.myTreeConversation)
            {
                throw new Exception("Cannot copy a link to a node in a different conversation.");
            }
            else // all our bases are covered, we're good
            {
                UndoState undo = UndoState.CaptureCurrentState(myTreeConversation.myTree);
                parent.myTreeConversation.UpdateTab();
                ConversationNode newNode = new ConversationNode
                {
                    myTreeConversation = myTreeConversation
                };
                ConversationNode originalNode = myTreeConversation.GetConversationNodeById(linkTo);
                originalNode.myLinks.Add(newNode);
                newNode.myNode = ContentNode.NewContentNodeLink(parent.GetNextOrderNum());
                newNode.myNode.linkTo = myNode.linkTo;
                parent.Nodes.Add(newNode);
                if (parent.nodeType == ConversationNodeType.Root)
                {
                    myTreeConversation.AddContentNodeToRoot(newNode.myNode);
                }
                else
                {
                    parent.myNode.subNodes.Add(newNode.myNode);
                }
                newNode.UpdateTextAndColor();
                myTreeConversation.myTree.myParentTab.AddUndo(undo);
            }
        }

        public void MakeLinkReal()
        {
            if (!this.IsChildOf(myTreeConversation.GetConversationNodeById(myNode.linkTo)))
            {
                UndoState undo = UndoState.CaptureCurrentState(myTreeConversation.myTree);
                myTreeConversation.UpdateTab();
                ConversationNode formerRealNode = this.myTreeConversation.GetConversationNodeById(this.linkTo);
                ConversationNode myFormerParent = this.myParent;
                ConversationNode formerRealNodeParent = formerRealNode.myParent;

                int formerOrderNumber = this.myNode.orderNum;
                this.myNode.orderNum = formerRealNode.myNode.orderNum;
                formerRealNode.myNode.orderNum = formerOrderNumber;

                myFormerParent.Nodes.Remove(this);
                if (myFormerParent.nodeType == ConversationNodeType.Root)
                {
                    myFormerParent.myTreeConversation.RemoveContentNodeFromRoot(myNode);
                }
                else
                {
                    myFormerParent.myNode.subNodes.Remove(myNode);
                }
                formerRealNodeParent.Nodes.Remove(formerRealNode);
                if (formerRealNodeParent.nodeType == ConversationNodeType.Root)
                {
                    formerRealNodeParent.myTreeConversation.RemoveContentNodeFromRoot(formerRealNode.myNode);
                }
                else
                {
                    formerRealNodeParent.myNode.subNodes.Remove(formerRealNode.myNode);
                }

                myFormerParent.Nodes.Add(formerRealNode);
                if (myFormerParent.nodeType == ConversationNodeType.Root)
                {
                    myFormerParent.myTreeConversation.AddContentNodeToRoot(formerRealNode.myNode);
                }
                else
                {
                    myFormerParent.myNode.subNodes.Add(formerRealNode.myNode);
                }
                formerRealNodeParent.Nodes.Add(this);
                if (formerRealNodeParent.nodeType == ConversationNodeType.Root)
                {
                    formerRealNodeParent.myTreeConversation.AddContentNodeToRoot(myNode);
                }
                else
                {
                    formerRealNodeParent.myNode.subNodes.Add(myNode);
                }
                myTreeConversation.SortTree();
                myTreeConversation.myTree.myParentTab.AddUndo(undo);
            }
            else MessageBox.Show("This link is a child of its target, so the target cannot be redirected here.");
        }

        public void CreateLinkToMe(ConversationNode parent)
        {
            if (nodeType == ConversationNodeType.PC && parent.nodeType == ConversationNodeType.PC)
            {
                MessageBox.Show("Cannot create a link to a PC node as a child of another PC node.");
            }
            else if (parent.nodeType == ConversationNodeType.Root && nodeType == ConversationNodeType.PC)
            {
                MessageBox.Show("Cannot create a link to a PC node directly under the conversation root.");
            }
            else if (parent.nodeType == ConversationNodeType.NPC && nodeType == ConversationNodeType.NPC)
            {
                MessageBox.Show("Cannot create a link to an NPC node as a child of another NPC node.");
            }
            else if (parent.myTreeConversation != this.myTreeConversation)
            {
                MessageBox.Show("Cannot create a link to a node in a different conversation.");
            }
            else // all our bases are covered, we're good
            {
                UndoState undo = UndoState.CaptureCurrentState(myTreeConversation.myTree);
                parent.myTreeConversation.UpdateTab();
                ConversationNode newNode = new ConversationNode();
                ContentNode newContent = ContentNode.NewContentNodeLink(myParent.GetNextOrderNum());
                newNode.myNode = newContent;
                newContent.nodeType = this.nodeType;
                newContent.idNum = 0;
                newContent.conversationComments = "";
                newContent.conversationText = "";
                newContent.linkTo = this.myNode.idNum;
                newNode.myTreeConversation = parent.myTreeConversation;
                parent.Nodes.Add(newNode);
                if (parent.nodeType == ConversationNodeType.Root)
                {
                    parent.myTreeConversation.AddContentNodeToRoot(newContent);
                }
                else
                {
                    parent.myNode.subNodes.Add(newContent);
                }
                this.myLinks.Add(newNode);
                newNode.UpdateTextAndColor();
                myTreeConversation.myTree.myParentTab.AddUndo(undo);
            }
        }

        public void CopyToClipboard()
        {
            ClipboardNode clipboardNode = ClipboardNode.NewClipboardNode(this);
            IDataObject dataObj = new DataObject();
            dataObj.SetData(ClipboardNode.format.Name, true, clipboardNode);
            dataObj.SetData(DataFormats.Text, true, this.GetTextCopy(""));
            Clipboard.SetDataObject(dataObj);
        }

        public string GetTextCopyLastSubnode(string lines)
        {
            string linkText = "";
            if (this.isLink)
            {
                linkText = " (link)";
            }

            if (lines == "")
            {
                throw new Exception("In parsing the last subnode of a segment of the copied section, somehow the lines prefix is empty.");
            }
            if (this.Nodes.Count == 0)
            {
                return lines + this.Text + linkText + Environment.NewLine;
            }
            else
            {
                string toReturn = "";
                toReturn += lines + this.Text + linkText + Environment.NewLine;

                if (lines == "|-")
                {
                    lines = "  |-";
                }
                else
                {
                    lines = lines.Substring(0, lines.Length - 2);
                    lines += "  |-";
                }

                for (int i = 0; i < this.Nodes.Count; i++)
                {
                    if (i == this.Nodes.Count - 1) // this is the last subnode for this node
                    {
                        toReturn += ((ConversationNode)this.Nodes[i]).GetTextCopyLastSubnode(lines);
                    }
                    else
                    {
                        toReturn += ((ConversationNode)this.Nodes[i]).GetTextCopy(lines);
                    }
                }
                return toReturn;
            }
        }

        public string GetTextCopy(string lines)
        {
            string linkText = "";
            if (this.isLink)
            {
                linkText = " (link)";
            }
            if (this.Nodes.Count == 0)
            {
                return lines + this.Text + linkText + Environment.NewLine;
            }
            else
            {
                string toReturn = "";
                toReturn += lines + this.Text + linkText + Environment.NewLine;

                if (lines == "")
                {
                    lines = "|-";
                }
                else
                {
                    lines = lines.Substring(0, lines.Length - 1);
                    lines += " |-";
                }

                for (int i = 0; i < this.Nodes.Count; i++)
                {
                    if (i == this.Nodes.Count - 1) // this is the last subnode for this node
                    {
                        toReturn += ((ConversationNode)this.Nodes[i]).GetTextCopyLastSubnode(lines);
                    }
                    else
                    {
                        toReturn += ((ConversationNode)this.Nodes[i]).GetTextCopy(lines);
                    }
                }
                return toReturn;
            }
        }

        public static ConversationNode MakeBrokenLink(int nextIdNum, int nextOrderNum)
        {
            ConversationNode tempNode = new ConversationNode();
            ContentNode tempContentNode = ContentNode.NewContentNode(nextIdNum, nextOrderNum);
            tempNode.myNode = tempContentNode;
            tempNode.Name = "";
            tempContentNode.conversationText = "[Broken Link]";
            tempContentNode.linkTo = 0;
            return tempNode;
        }

        #endregion

        #region Search and Hierarchy
        public bool IsChildOf(ConversationNode parent) // for the sake of some functions, a node checked to see if it is a subnode of itself returns true
        {
            if (this == parent)
            {
                return true;
            }
            else if (nodeType == ConversationNodeType.Root)
            {
                return false;
            }
            else if ((parent.nodeType == ConversationNodeType.Root) && this.myTreeConversation == parent.myTreeConversation)
            {
                return true;
            }
            else if (parent.nodeType != ConversationNodeType.Root && myParent.nodeType == ConversationNodeType.Root)
            {
                return false;
            }

            return (parent.SearchConversationNodeById(this.myParent.myNode.idNum) == myParent);
        }

        public ConversationNode GetConversationNodeById(int idNum)
        {
            ConversationNode tempNode = null;
            foreach (ConversationNode subNode in this.Nodes)
            {
                tempNode = subNode.SearchConversationNodeById(idNum);
                if (tempNode != null)
                {
                    return tempNode;
                }
            }
            return null;
        }

        public ConversationNode SearchConversationNodeById(int checkIdNum)
        {
            ConversationNode tempNode = null;
            if (this.nodeType != ConversationNodeType.Root && myNode.idNum == checkIdNum)
            {
                return this;
            }
            foreach (ConversationNode subNode in Nodes)
            {
                tempNode = subNode.SearchConversationNodeById(checkIdNum);
                if (tempNode != null)
                {
                    return tempNode;
                }
            }
            return null;
        }

        public ConversationNode GetNodeByOrderNum(int checkOrder) // function is called on the parent
        {
            foreach (ConversationNode subNode in Nodes)
            {
                if (subNode.myNode.orderNum == checkOrder)
                {
                    return subNode;
                }
            }
            return null;
        }

        public ConversationNode CopyOutLinks()
        {
            ConversationNode outsideLink = null;
            outsideLink = this.IsAnyChildLinkedExternally(this);
            UndoState undo = UndoState.CaptureCurrentState(myTreeConversation.myTree);
            this.myTreeConversation.UpdateTab();
            if (this.myTreeConversation.GetConversationNodeById(outsideLink.linkTo) == this)
            {
                outsideLink.MakeLinkReal();
                myTreeConversation.myTree.myParentTab.AddUndo(undo);
                return outsideLink;
            }
            else
            {
                while (outsideLink != null)
                {
                    outsideLink.MakeLinkReal();
                    outsideLink = this.IsAnyChildLinkedExternally(this);
                }
                myTreeConversation.myTree.myParentTab.AddUndo(undo);
                return this;
            }
        }

        #endregion

        #region idNum and orderNum
        public int GetNextIdNum()
        {
            int max = 0;
            foreach (ConversationNode subNode in this.Nodes)
            {
                max = subNode.SearchNextIdNum(max);
            }
            return max + 1;
        }

        public int SearchNextIdNum(int max)
        {
            if (myNode.idNum > max)
            {
                max = myNode.idNum;
            }
            foreach (ConversationNode subNode in Nodes)
            {
                max = subNode.SearchNextIdNum(max);
            }
            return max;
        }

        public int GetNextOrderNum() // function is called on a parent to see what the next order number among its children is
        {
            AlignOrderNumbers();
            int max = 0;
            foreach (ConversationNode subNode in Nodes)
            {
                if (subNode.myNode.orderNum > max)
                {
                    max = subNode.myNode.orderNum;
                }
            }
            return max + 1;
        }
        #endregion

        #region Rearranging
        public void ChangeMyParent(ConversationNode newParent)
        {
            if (newParent != null)
            {
                if (myParent.nodeType == ConversationNodeType.Root)
                {
                    myParent.myTreeConversation.RemoveContentNodeFromRoot(myNode);
                }
                else
                {
                    myParent.myNode.subNodes.Remove(myNode);
                }

                myParent.Nodes.Remove(this);

                newParent.AlignOrderNumbers();

                if (newParent.nodeType == ConversationNodeType.Root)
                {
                    newParent.myTreeConversation.AddContentNodeToRoot(myNode);
                }
                else
                {
                    newParent.myNode.subNodes.Add(myNode);
                }
                newParent.Nodes.Add(this);
                myNode.orderNum = newParent.GetNextOrderNum();
            }
        }

        public void AlignOrderNumbers()
        {
            // this gets rid of gaps in the order numbers
            int lastNumber = 0;
            for (int i = 1; i < (this.Nodes.Count + 1); i++)
            {
                while (this.GetNodeByOrderNum(lastNumber) == null)
                {
                    lastNumber++;
                }
                this.GetNodeByOrderNum(lastNumber).myNode.orderNum = i;
                lastNumber++;
            }
        }

        public void MoveNodeUp()
        {
            if (nodeType != ConversationNodeType.Root)
            {
                myParent.AlignOrderNumbers();
                if (myNode.orderNum > 1)
                {
                    ConversationNode previousNode = myParent.GetNodeByOrderNum(myNode.orderNum - 1);
                    previousNode.myNode.orderNum++;
                    myNode.orderNum--;
                    myTreeConversation.myTree.SortTree();
                }
            }
        }

        public void MoveNodeDown()
        {
            if (nodeType != ConversationNodeType.Root)
            {
                myParent.AlignOrderNumbers();
                if (myNode.orderNum < (myParent.Nodes.Count))
                {
                    ConversationNode nextNode = myParent.GetNodeByOrderNum(myNode.orderNum + 1);
                    nextNode.myNode.orderNum--;
                    myNode.orderNum++;
                    myTreeConversation.myTree.SortTree();
                }
            }
        }
        #endregion

        public ConversationNode GetOriginal()
        {
            if (isLink)
            {
                return myTreeConversation.GetConversationNodeById(linkTo);
            }
            else return null;
        }

        public void SetExpandState(ConversationNode original)
        {
            if (original.shouldBeExpanded)
            {
                this.shouldBeExpanded = true;
            }
            else
            {
                this.shouldBeExpanded = false;
            }
            if (this.Nodes.Count > 0)
            {
                for (int i = 0; i < this.Nodes.Count; i++)
                {
                    ((ConversationNode)this.Nodes[i]).SetExpandState((ConversationNode)original.Nodes[i]);
                }
            }
        }

        public void StrrefToggle()
        {
            if (_isTlk.HasValue && _isTlk.Value)
            {
                //remove
                Info node = myNode.additionalData.Find(x => x.variableName == "str_ref");
                myNode.additionalData.Remove(node);
                _isTlk = false;
            }
            else
            {
                //Add
                myNode.additionalData.Add(new Info("str_ref",""));
                _tlkRef = "";
                _isTlk = true;
            }
        }
    }
}
