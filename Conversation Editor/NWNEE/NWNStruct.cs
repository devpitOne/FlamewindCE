using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Editor.NWNEE
{
    [Serializable]
    public class NWNStruct : Conversation
    {
        #region Shared Properties
        [XmlIgnore, YamlDotNet.Serialization.YamlIgnore]
        public int? idNum;
        [XmlIgnore]
        public string __struct_id;
        [XmlIgnore]
        public Dictionary<string, string> AnimLoop;
        [XmlIgnore]
        public Dictionary<string, string> Animation;
        [XmlIgnore]
        public Dictionary<string, string> Delay;
        [XmlIgnore]
        public Dictionary<string, string> Quest;
        [XmlIgnore]
        public Dictionary<string, string> Script;
        [XmlIgnore]
        public Dictionary<string, string> Sound;
        [XmlIgnore]
        public Dictionary<string, string> Active;
        [XmlIgnore]
        public Dictionary<string, string> Index;
        #endregion

        #region Dialog Properties
        [XmlIgnore]
        public string __data_type;

        [XmlIgnore]
        public Dictionary<string, string> DelayEntry;

        [XmlIgnore]
        public Dictionary<string, string> DelayReply;

        [XmlIgnore]
        public Dictionary<string, string> EndConverAbort;

        [XmlIgnore]
        public Dictionary<string, string> EndConversation;

        [XmlIgnore]
        public NWNList EntryList;

        [XmlIgnore]
        public Dictionary<string, string> NumWords;
        [XmlIgnore]
        public Dictionary<string, string> PreventZoomIn;
        [XmlIgnore]
        public NWNList ReplyList;
        [XmlIgnore]
        public NWNList StartingList;
        #endregion

        #region EntryListItem Properties

        [XmlIgnore]
        public Dictionary<string, string> Speaker;
        [XmlIgnore]
        public NWNList RepliesList;
        [XmlIgnore]
        public Dictionary<string, string> Comment;
        [XmlIgnore]
        public NWNText Text;
        #endregion

        #region ReplyListItem Properties
        [XmlIgnore]
        public Dictionary<string, string> IsChild;
        [XmlIgnore]
        public Dictionary<string, string> LinkComment;

        [XmlIgnore]
        public NWNList EntriesList;
        #endregion

        #region StartingListItem Properties
        #endregion
        public NWNStruct()
        {

        }

        //public override void SaveContentConversation(string fileName, int versionNumber)
        //{

        //}
    }
}
