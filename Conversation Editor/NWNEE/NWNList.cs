using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Editor.NWNEE
{
    public class NWNList
    {
        public string type = "list";

        public List<NWNStruct> value = new List<NWNStruct>();

        public NWNList()
        {
        }
    }
}
