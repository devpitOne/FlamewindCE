using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ConversationEditorGui
{
    public class NodeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            ConversationNode xNode = (ConversationNode)x;
            ConversationNode yNode = (ConversationNode)y;

            return xNode.myNode.orderNum - yNode.myNode.orderNum;
        }
    }
}
