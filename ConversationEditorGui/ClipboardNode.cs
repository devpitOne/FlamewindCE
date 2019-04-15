using System;
using System.Collections.Generic;
using System.Text;
using Editor;
using System.Windows.Forms;

namespace ConversationEditorGui
{
    [Serializable]
    public class ClipboardNode
    {
        [NonSerialized]
        public static DataFormats.Format format = DataFormats.GetFormat(typeof(ClipboardNode).FullName);

        public ContentNode myNode;

        public int linkTo = 0;

        public string myTreeConversationName = "";

        public ConversationNodeType nodeType;

        public string myLinkText = "";

        public List<ClipboardNode> subNodes = new List<ClipboardNode>();

        private ClipboardNode()
        {

        }

        public static ClipboardNode NewClipboardNode(ConversationNode baseNode)
        {
            ClipboardNode newNode = new ClipboardNode();
            newNode.myNode = baseNode.myNode;
            newNode.linkTo = baseNode.linkTo;
            if (newNode.linkTo > 0)
            {
                newNode.myLinkText = baseNode.baseText;
            }
            newNode.myTreeConversationName = baseNode.myTreeConversationName;
            newNode.nodeType = baseNode.nodeType;
            foreach (ConversationNode subNode in baseNode.Nodes)
            {
                newNode.subNodes.Add(ClipboardNode.NewClipboardNode(subNode));
            }
            return newNode;
        }

        public static ClipboardNode GetNodeFromClipboard()
        {
            ClipboardNode clipboardNode = null;
            IDataObject dataObj = Clipboard.GetDataObject();

            if (dataObj.GetDataPresent(ClipboardNode.format.Name))
            {
                clipboardNode = (ClipboardNode)dataObj.GetData(ClipboardNode.format.Name);
                return clipboardNode;
            }
            return null;
        }

        public ContentNode DuplicateMyContentNode(int nextIdNum, int nextOrderNum)
        {
            return myNode.DuplicateContentNode(nextIdNum, nextOrderNum);
        }
    }
}
