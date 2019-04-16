using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ConversationEditorGui
{
    public static class LinkedTextBoxExtensions
    {
        private static Type _editorContextMenuType;
        private static MethodInfo _editorContextMenuAddMenuItemsMethod;
        private static Type _textBoxBaseType;
        private static FieldInfo _textBoxBaseTextEditorField;
        private static PropertyInfo _contextMenuEventArgsUserInitiatedProperty;

        static LinkedTextBoxExtensions()
        {
            _editorContextMenuType = typeof(TextBox).Assembly.GetType("System.Windows.Documents.TextEditorContextMenu+EditorContextMenu");
            _editorContextMenuAddMenuItemsMethod = _editorContextMenuType.GetMethod("AddMenuItems",
            BindingFlags.NonPublic |
            BindingFlags.Instance);
            _textBoxBaseType = typeof(TextBoxBase);
            _textBoxBaseTextEditorField = _textBoxBaseType
                .GetField("_textEditor", BindingFlags.NonPublic | BindingFlags.Instance);

            _contextMenuEventArgsUserInitiatedProperty = typeof(ContextMenuEventArgs).GetProperty("UserInitiated",
                              BindingFlags.NonPublic |
                              BindingFlags.Instance);
        }

        public static void InjectIntoDefaultMenu(this TextBoxBase textBoxBase, ContextMenuEventArgs e, Action<ContextMenuEventArgs> callBaseContextMenuOpening, params MenuItem[] items)
        {
            var contextMenu = (ContextMenu)Activator.CreateInstance(_editorContextMenuType, true);
            textBoxBase.ContextMenu = contextMenu;

            callBaseContextMenuOpening(e);

            _editorContextMenuAddMenuItemsMethod.Invoke(contextMenu, new[]
            {
                _textBoxBaseTextEditorField.GetValue(textBoxBase),
                _contextMenuEventArgsUserInitiatedProperty.GetValue(e, null)
            });

            if (contextMenu.Items.Count > 0 && items.Length > 0)
                contextMenu.Items.Add(new Separator());

            foreach (var item in items)
                contextMenu.Items.Add(item);
        }
    }
}
