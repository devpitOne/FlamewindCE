using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Editor.NWNEE
{
    /// <summary>
    /// Used to hold the conversation text
    /// </summary>
    public class NWNText
    {
        public string type;

        /// <summary>
        /// These are references to lines in the dialog.tlk. Not sure how they are added to conversations.
        /// TODO: Make them a doable feature?
        /// </summary>
        public string str_ref;

        /// <summary>
        /// The actual text is kept in a key/value of the value variable, For English the key is "0"
        /// </summary>
        public Dictionary<string, string> value { get; set; }

        public NWNText()
        {

        }
    }
}
