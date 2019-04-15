using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ConversationEditorGui
{
    [Serializable]
    public class ExpandSettings
    {
        [XmlAttribute]
        public string fileName;

        [XmlArrayItem("expandList")]
        public List<int> expandList;

        public ExpandSettings()
        {

        }

        public ExpandSettings(string fn, List<int> el)
        {
            this.fileName = fn;
            this.expandList = el;
        }
    }
}
