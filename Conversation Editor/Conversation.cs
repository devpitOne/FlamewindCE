using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using Editor.NWNEE;
using YamlDotNet.Serialization;

namespace Editor
{
    [Serializable]
    public class Conversation
    {
        #region Properties
        [XmlArrayItem("ContentNode"), YamlIgnore]
        public List<ContentNode> subNodes = new List<ContentNode>();

        [XmlArrayItem("Info"), YamlIgnore]
        public List<Info> additionalData = new List<Info>();

        [XmlArrayItem("InfoObject"), YamlIgnore]
        public List<InfoObject> infoObjects = new List<InfoObject>();

        [XmlElement, YamlIgnore]
        public int VersionNumber = 0;

        #endregion

        protected Conversation()
        {
        }

        public static Conversation GetConversation(string fileName)
        {
            Conversation toReturn = null;
            FileStream myFileStream = null;
            try
            {
                myFileStream = new FileStream(fileName, FileMode.Open);
                if (fileName.EndsWith("yml"))
                {
                    var messyObject = NWNEE.YAMLFactory.Deserialize(myFileStream);
                    toReturn = YAMLFactory.ConvertToConv(messyObject);
                }
                else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Conversation));
                    toReturn = (Conversation)serializer.Deserialize(myFileStream);
                }
            }
            catch (Exception ex)
            {
                string strErr = String.Format("Unable to open file. Error:\n{0}", ex.Message);
                MessageBox.Show(strErr, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (myFileStream != null)
                {
                    myFileStream.Close();
                }
                SortConversation(toReturn);
            }
            return toReturn;
        }

        public static Conversation NewEmptyConversation()
        {
            Conversation toReturn = new Conversation();
            return toReturn;
        }

        public static void SortList(List<ContentNode> thisList)
        {
            ContentNode tempNode;
            // sorts lists of ContentNodes according to the "orderNum" value of each node
            for (int i = 0; i < thisList.Count; i++)
            {
                for (int j = i + 1; j < thisList.Count; j++)
                {
                    if (thisList[j].orderNum < thisList[i].orderNum)
                    {
                        tempNode = thisList[j];
                        thisList[j] = thisList[i];
                        thisList[i] = tempNode;
                    }
                }
            }
        }

        public static void SortSubNodes(ContentNode myNode)
        {
            foreach (ContentNode subNode in myNode.subNodes)
            {
                SortSubNodes(subNode);
            }
            SortList(myNode.subNodes);
        }

        public static void SortConversation(Conversation toSort)
        {
            foreach (ContentNode subNode in toSort.subNodes)
            {
                SortSubNodes(subNode);
            }
            SortList(toSort.subNodes);
        }

        public virtual void SaveContentConversation(string fileName, int versionNumber)
        {
            StreamWriter writer = null;
            this.VersionNumber = versionNumber;
            try
            {
                if (fileName.EndsWith("yml"))
                {
                    NWNStruct toBeSerial = YAMLFactory.ConvertToStruct(this);
                    var serialized = YAMLFactory.Serialize(toBeSerial);
                    writer = new StreamWriter(fileName);
                    writer.Write(serialized);
                }
                else
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Conversation));
                    writer = new StreamWriter(fileName);
                    ser.Serialize(writer, this);
                }
            }
            catch (Exception ex)
            {
                string strErr = "Unable to save conversation. Error: " + ex.Message;
                MessageBox.Show(strErr, "Save File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                writer = null;
            }
        }

        public ContentNode GetContentNodeById(int idNum)
        {
            ContentNode tempNode = null;
            foreach (ContentNode subNode in subNodes)
            {
                tempNode = subNode.SearchContentNodeById(idNum);
                if (tempNode != null)
                {
                    return tempNode;
                }
            }
            return null;
        }

        public string GetTextById(int idNum)
        {
            ContentNode tempNode = GetContentNodeById(idNum);
            return tempNode.conversationText;
        }

        public void AddNodeToRoot(ContentNode contentNode)
        {
            subNodes.Add(contentNode);
        }

        public void RemoveNodeFromRoot(ContentNode contentNode)
        {
            subNodes.Remove(contentNode);
        }

        public Conversation DuplicateConversation()
        {
            Conversation copy = new Conversation();
            for (int i = 0; i < this.subNodes.Count; i++)
            {
                ContentNode node = this.subNodes[i];
                copy.subNodes.Add(node.Duplicate());
            }
            return copy;
        }

        public bool IsSubnode(ContentNode node)
        {
            foreach (ContentNode subNode in subNodes)
            {
                if (subNode == node)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
