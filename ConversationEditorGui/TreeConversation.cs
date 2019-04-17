using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Editor;

namespace ConversationEditorGui
{
    public class TreeConversation : ConversationNode
    {
        public Conversation myConversation = null;

        public LinkedTree myTree = null;

        public string myName
        {
            get
            {
                return myTree.myParentTab.Text;
            }
        }

        private TreeConversation()
        {
        }

        public static TreeConversation GetTree(string xmlFileName)
        {
            TreeConversation newTreeConversation = new TreeConversation();
            Conversation thisConvo = Conversation.GetConversation(xmlFileName);
            newTreeConversation.myConversation = thisConvo;
            newTreeConversation.myTreeConversation = newTreeConversation;
            newTreeConversation.Text = "Root";
            foreach (ContentNode subNode in thisConvo.subNodes)
            {
                newTreeConversation.Nodes.Add(ConversationNode.GetConversationNode(subNode, newTreeConversation));
            }
            newTreeConversation.BuildLinks();
            return newTreeConversation;
        }

        public static TreeConversation NewEmptyTree()
        {
            TreeConversation newTreeConversation = new TreeConversation
            {
                myConversation = Conversation.NewEmptyConversation()
            };
            newTreeConversation.myTreeConversation = newTreeConversation;
            newTreeConversation.Text = "Root";
            return newTreeConversation;
        }

        public TreeConversation DuplicateTreeConversation()
        {
            Conversation.SortConversation(myConversation);
            TreeConversation copy = new TreeConversation();
            Conversation conversation = myConversation.DuplicateConversation();
            copy.myConversation = conversation;
            copy.myTreeConversation = copy;
            copy.Text = "Root";
            for (int i = 0; i < conversation.subNodes.Count; i++)
            { // GetConversationNode creates a new conversation node with all appropriate children
                ContentNode subNode = conversation.subNodes[i];
                copy.Nodes.Add(ConversationNode.GetConversationNode(subNode, copy));
            }
            copy.BuildLinks();
            if (copy.Nodes.Count > 0)
            {
                for (int i = 0; i < copy.Nodes.Count; i++)
                {
                    ((ConversationNode)(copy.Nodes[i])).SetExpandState(((ConversationNode)Nodes[i]));
                }
            }
            return copy;
        }
        
        public void SaveConversation(string fileName, int versionNumber)
        {
            myConversation.SaveContentConversation(fileName, versionNumber);
        }

        public void AddContentNodeToRoot(ContentNode contentNode)
        {
            myConversation.AddNodeToRoot(contentNode);
        }

        public void RemoveContentNodeFromRoot(ContentNode contentNode)
        {
            myConversation.RemoveNodeFromRoot(contentNode);
        }

        public override SelectedSection GetSelectedSection()
        {
            return myTree.GetSelectedSection();
        }

        public void SelectThisNode(ConversationNode conversationNode)
        {
            myTree.SelectedNode = conversationNode;
        }

        public void FocusTree()
        {
            myTree.Focus();
        }

        public void FocusTextBox()
        {
            myTree.FocusTextBox();
        }

        public void FocusCommentsBox()
        {
            myTree.FocusCommentsBox();
        }

        public string GetTextById(int idNum)
        {
            return myConversation.GetTextById(idNum);
        }

        public void UpdateTab()
        {
            myTree.UpdateTab();
        }

        public void SortTree()
        {
            myTree.SortTree();
        }

        public void SaveContentConversation(string fileName, int versionNumber)
        {
            myConversation.SaveContentConversation(fileName, versionNumber);
        }

        public int CheckVersion()
        {
            return myConversation.VersionNumber;
        }
    }
}
