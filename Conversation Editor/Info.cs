using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Editor
{
    [Serializable]
    public class Info
    {
        [XmlAttribute]
        public string variableName;

        [XmlAttribute]
        public string variableValue;

        public Info()
        {

        }

        public Info(string name, string value)
        {
            this.variableName = name;
            this.variableValue = value;
        }
    }
}
