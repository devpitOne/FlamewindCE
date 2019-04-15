using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ConversationEditorGui
{
    internal class Win32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public int fErase;
            public RECT rcPaint;
            public int fRestore;
            public int fIncUpdate;
            public int Reserved1;
            public int Reserved2;
            public int Reserved3;
            public int Reserved4;
            public int Reserved5;
            public int Reserved6;
            public int Reserved7;
            public int Reserved8;
        }


        [DllImport("user32.dll")]
        public static extern int GetUpdateRect(IntPtr hwnd, ref RECT rect, bool erase);
        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(IntPtr hwnd, ref PAINTSTRUCT paintStruct);
        [DllImport("user32.dll")]
        public static extern bool EndPaint(IntPtr hwnd, ref PAINTSTRUCT paintStruct);
        [DllImport("user32.dll")]
        public static extern int SetClipboardViewer(int hWnd);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
    }
}
