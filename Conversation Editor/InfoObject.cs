using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace Editor
{
    [Serializable]
    public class InfoObject
    {
        [XmlAttribute]
        public string objectName;

        [XmlArrayItem("InfoObject")]
        public List<InfoObject> subObjects = new List<InfoObject>();

        [XmlArrayItem("Info")]
        public List<Info> info = new List<Info>();

        public InfoObject()
        {

        }

        public InfoObject(string name)
        {
            this.objectName = name;
        }

        public void AddItem(string name, string value)
        {
            this.info.Add(new Info(name, value));
        }

        public void AddSubObject(InfoObject obj)
        {
            this.subObjects.Add(obj);
        }

        public string GetInfoByName(string name)
        {
            foreach (Info subInfo in info)
            {
                if (subInfo.variableName == name)
                {
                    return subInfo.variableValue;
                }
            }
            return "";
        }
    }
}
