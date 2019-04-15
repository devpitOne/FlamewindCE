using System;
using System.Collections.Generic;
using System.Text;

namespace ConversationEditorGui
{
    public class UndoCollection
    {
        private TreeTab myParentTab;

        private RedoCollection myRedos;

        private Stack<UndoState> collection = new Stack<UndoState>();

        public UndoCollection(TreeTab sender)
        {
            this.myParentTab = sender;
            this.myRedos = new RedoCollection(this);
        }

        public void Update(UndoState undo)
        {
            collection.Push(undo);
            myRedos.Clear();
        }

        public UndoState Undo()
        {
            myRedos.Add(UndoState.CaptureCurrentState(myParentTab.myTree));
            return collection.Pop();
        }

        public UndoState Redo()
        {
            collection.Push(UndoState.CaptureCurrentState(myParentTab.myTree));
            return myRedos.Redo();
        }

        public bool HasUndos()
        {
            return (collection.Count > 0);
        }

        public bool HasRedos()
        {
            return myRedos.HasRedos();
        }
    }
}
