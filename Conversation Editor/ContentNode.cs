using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Editor
{
    [Serializable]
    public class ContentNode
    {
        [XmlAttribute]
        public int idNum;

        [XmlAttribute]
        public int orderNum;

        [XmlElement]
        public string conversationText = "";

        [XmlElement]
        public string conversationComments = "";

        [XmlArrayItem("ContentNode")]
        public List<ContentNode> subNodes = new List<ContentNode>();

        [XmlAttribute]
        public int linkTo;

        [XmlArrayItem("Info")]
        public List<Info> additionalData = new List<Info>();

        [XmlArrayItem("InfoObject")]
        public List<InfoObject> infoObjects = new List<InfoObject>();

        [XmlAttribute(AttributeName = "nodeType")]
        public string nodeTypeString
        {
            get
            {
                if (myNodeTypeEnum == ConversationNodeType.NPC)
                {
                    return "npc";
                }
                else if (myNodeTypeEnum == ConversationNodeType.PC)
                {
                    return "pc";
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (value == "pc")
                {
                    myNodeTypeEnum = ConversationNodeType.PC;
                }
                else if (value == "npc")
                {
                    myNodeTypeEnum = ConversationNodeType.NPC;
                }
                else
                {
                    throw new Exception("This GUI node does not have a proper nodeType or is a root node and should not have a content node.");
                }
            }
        }

        private ConversationNodeType myNodeTypeEnum;

        [XmlIgnore]
        public ConversationNodeType nodeType
        {
            get
            {
                return myNodeTypeEnum;
            }
            set
            {
                myNodeTypeEnum = value;
            }
        }

        [XmlIgnore]
        public string myGuid;

        [XmlIgnore]
        public string myConnectorID = "";

        [XmlIgnore]
        public bool isLink
        {
            get
            {
                return (linkTo > 0);
            }
        }

        [XmlIgnore]
        public Conversation myConversation;

        public static ContentNode NewContentNode(int nextIdNum, int nextOrderNum)
        {
            ContentNode newNode = new ContentNode();
            newNode.idNum = nextIdNum;
            newNode.orderNum = nextOrderNum;
            return newNode;
        }

        public static ContentNode NewContentNodeLink(int nextOrderNum)
        {
            ContentNode newNode = new ContentNode();
            newNode.orderNum = nextOrderNum;
            return newNode;
        }

        public ContentNode SearchContentNodeById(int checkIdNum)
        {
            ContentNode tempNode = null;
            if (idNum == checkIdNum)
            {
                return this;
            }
            foreach (ContentNode subNode in subNodes)
            {
                tempNode = subNode.SearchContentNodeById(checkIdNum);
                if (tempNode != null)
                {
                    return tempNode;
                }
            }
            return null;
        }

        public ContentNode DuplicateContentNode(int nextIdNum, int nextOrderNum)
        {
            ContentNode newNode = new ContentNode();
            newNode.conversationComments = this.conversationComments;
            newNode.conversationText = this.conversationText;
            newNode.idNum = nextIdNum;
            newNode.linkTo = this.linkTo;
            newNode.nodeType = this.nodeType;
            newNode.orderNum = nextOrderNum;
            return newNode;
        }

        public ContentNode Duplicate()
        {
            ContentNode copy = new ContentNode();
            
            copy.additionalData = this.additionalData;
            copy.conversationComments = this.conversationComments;
            copy.conversationText = this.conversationText;
            copy.idNum = this.idNum;
            copy.linkTo = this.linkTo;
            copy.nodeType = this.nodeType;
            copy.orderNum = this.orderNum;
            for (int i = 0; i < this.subNodes.Count; i++)
            {
                ContentNode node = this.subNodes[i];
                copy.subNodes.Add(node.Duplicate());
            }
            return copy;
        }

        public string GetInfoValueByName(string name)
        {
            foreach (Info info in additionalData)
            {
                if (info.variableName == name)
                {
                    return info.variableValue;
                }                
            }
            return "";
        }
        public InfoObject GetInfoObjectByName(string name)
        {
            foreach (InfoObject info in infoObjects)
            {
                if (info.objectName == name)
                    return info;
            }
            return new InfoObject();
        }
        public static bool ArrayContains(string[] array, string contains)
        {
            foreach (string content in array)
            {
                if (content == contains)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ConvertStringToBool(string text)
        {
            if (text == "True" || text == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AlignOrderNumbers()
        {
            // this gets rid of gaps in the order numbers
            // it ASSUMES that they are sorted in the correct order
            int lastNumber = 0;
            for (int i = 1; i < (this.subNodes.Count + 1); i++)
            {
                while (this.GetNodeByOrderNum(lastNumber) == null)
                {
                    lastNumber++;
                }
                this.GetNodeByOrderNum(lastNumber).orderNum = i;
                lastNumber++;
            }
        }

        public ContentNode GetNodeByOrderNum(int checkOrder)
        {
            foreach (ContentNode subNode in subNodes)
            {
                if (subNode.orderNum == checkOrder)
                {
                    return subNode;
                }
            }
            return null;
        }

    }
}