using System;
using System.Collections.Generic;
using System.Text;

namespace ConversationEditorGui
{
    public class RedoCollection
    {
        private UndoCollection myUndos;

        private Stack<UndoState> collection = new Stack<UndoState>();

        public RedoCollection(UndoCollection sender)
        {
            this.myUndos = sender;
        }

        public void Clear()
        {
            collection = new Stack<UndoState>();
        }

        public void Add(UndoState newRedo)
        {
            collection.Push(newRedo);
        }

        public UndoState Redo()
        {
            return collection.Pop();
        }

        public bool HasRedos()
        {
            return (collection.Count > 0);
        }

        public UndoState Peek()
        {
            return collection.Peek();
        }
    }
}
