using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Editor;

namespace ConversationEditorGui
{
    public partial class TestConversation : Form
    {
        private TreeConversation treeConversation = null;

        private const string NPC_TAG = "Choose how the NPC would respond:";

        private const string START_TAG = "Choose the NPC's greeting:";

        private const string PC_TAG = "Available PC choices:";

        private const int LEFT_MARGIN = 11;

        private bool showComments = true;

        private List<ConversationLinkLabel> myLinks = new List<ConversationLinkLabel>();

        private List<Label> myComments = new List<Label>();
        
        public TestConversation()
        {
            InitializeComponent();
        }

        public void SetConversation(TreeConversation conversation)
        {
            treeConversation = conversation;
        }

        private void TestConversation_Shown(object sender, System.EventArgs e)
        {
            RestartConversation();
        }

        private void ClearLinksAndComments()
        {
            foreach (ConversationLinkLabel link in myLinks)
            {
                this.Controls.Remove(link);
            }
            foreach (Label comment in myComments)
            {
                this.Controls.Remove(comment);
            }
            myComments = new List<Label>();
            myLinks = new List<ConversationLinkLabel>();
        }

        private void PopulateLinksAndComments()
        {
            foreach (ConversationLinkLabel link in myLinks)
            {
                this.Controls.Add(link);
            }
            foreach (Label comment in myComments)
            {
                this.Controls.Add(comment);
            }
        }

        private void RestartConversation()
        {
            ClearLinksAndComments();
            int controlHeight = 12;
            this.SuspendLayout();
            ConversationLinkLabel tempLink;
            Label tempComments;
            foreach (ConversationNode subNode in treeConversation.Nodes)
            {
                controlHeight += 30;
                tempLink = CreateLink(subNode);
                tempLink.Location = new Point(LEFT_MARGIN, controlHeight);
                tempLink.AutoSize = true;
                myLinks.Add(tempLink);
                if (showComments)
                {
                    if (subNode.myComments != "" && subNode.myComments != null)
                    {
                        controlHeight += 25;
                        tempComments = new Label();
                        tempComments.Text = "(Comments: " + subNode.myComments + ")";
                        tempComments.Location = new Point(LEFT_MARGIN, controlHeight);
                        tempComments.AutoSize = true;
                        myComments.Add(tempComments);
                    }
                }
            }
            PopulateLinksAndComments();
            tag.Text = START_TAG;
            this.ResumeLayout();
        }

        private ConversationLinkLabel CreateLink(ConversationNode node)
        {
            ConversationLinkLabel link = new ConversationLinkLabel();
            if (node.isLink)
            {
                node = node.GetOriginal();
            }
            link.myNode = node;
            link.Text = node.Text;
            if (node.nodeType == ConversationNodeType.NPC)
            {
                link.ActiveLinkColor = Color.Maroon;
                link.LinkColor = Color.Maroon;
                link.VisitedLinkColor = Color.Maroon;
            }
            else
            {
                link.ActiveLinkColor = Color.Navy;
                link.LinkColor = Color.Navy;
                link.VisitedLinkColor = Color.Navy;
            }
            link.Click += new EventHandler(delegate
            {
                Repopulate(link.myNode);
            });
            return link;
        }

        public void Repopulate(ConversationNode node)
        {
            ClearLinksAndComments();
            int controlHeight = 12;
            this.SuspendLayout();
            ConversationLinkLabel tempLink;
            Label tempComments;
            foreach (ConversationNode subNode in node.Nodes)
            {
                controlHeight += 30;
                tempLink = CreateLink(subNode);
                tempLink.Location = new Point(LEFT_MARGIN, controlHeight);
                tempLink.AutoSize = true;
                myLinks.Add(tempLink);
                if (showComments)
                {
                    if (subNode.myComments != "" && subNode.myComments != null)
                    {
                        controlHeight += 25;
                        tempComments = new Label();
                        tempComments.Text = "(Comments: " + subNode.myComments + ")";
                        tempComments.Location = new Point(LEFT_MARGIN, controlHeight);
                        tempComments.AutoSize = true;
                        myComments.Add(tempComments);
                    }
                }
            }
            if (myLinks.Count == 0)
            {
                tag.Text = "";
                this.ResumeLayout();
                MessageBox.Show("Conversation Has Ended.");
                this.Hide();
            }
            else
            {
                PopulateLinksAndComments();
                if (node.nodeType == ConversationNodeType.NPC)
                {
                    tag.Text = PC_TAG;
                }
                else
                {
                    tag.Text = NPC_TAG;
                }
                this.ResumeLayout();
            }
        }

        private void SelectedNode(ConversationNode node)
        {

        }
    }
}