using System;
using System.Collections.Generic;
using System.Text;

namespace ConversationEditorGui
{
    public class UndoState
    {
        public TreeConversation treeConversation;
        public SelectedSection selectedSection;
        public ConversationNode selectedNode;

        private UndoState()
        {
        }

        public static UndoState CaptureCurrentState(LinkedTree linkedTree)
        {
            UndoState undoState = new UndoState();
            undoState.treeConversation = linkedTree.myTreeConversation.DuplicateTreeConversation();
            undoState.selectedSection = linkedTree.GetSelectedSection();
            int idNum = 0;
            if (linkedTree.selectedNode != null)
            {
                idNum = linkedTree.selectedNode.myId;
            }
            if (idNum != 0)
            {
                undoState.selectedNode = undoState.treeConversation.GetConversationNodeById(idNum);
            }
            else
                undoState.selectedNode = null;

            return undoState;
        }
    }
}
