using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ConversationEditorGui
{
    public class TabControlDragDrop : TabControl
    {
        private TabPage GetPageByTab(Point pt)
        {
            for(int i = 0; i < TabPages.Count; i++)
            {
                if (GetTabRect(i).Contains(pt))
                {
                    return TabPages[i];
                }
            }
            return null;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Point pt = new Point(e.X, e.Y);
            TabPage tab = GetPageByTab(pt);

            if (tab != null)
            {
                DoDragDrop(tab, DragDropEffects.All);
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            Point pt = new Point(e.X, e.Y);
            pt = PointToClient(pt);

            TabPage overTab = GetPageByTab(pt);

            if (overTab != null)
            {
                Rectangle overRectangle = GetTabRect(TabPages.IndexOf(overTab));
                int overIndex = TabPages.IndexOf(overTab);
                if (e.Data.GetDataPresent(typeof(TabPage)) || e.Data.GetDataPresent(typeof(TreeTab)))
                {
                    e.Effect = DragDropEffects.Move;
                    TabPage draggedTab = (TabPage)e.Data.GetData(typeof(TabPage));
                    if (draggedTab == null)
                    {
                        draggedTab = (TabPage)e.Data.GetData(typeof(TreeTab));
                    }

                    if (overTab != draggedTab)
                    {
                        if (pt.X < ((overRectangle.Left + overRectangle.Right) / 2))
                        {
                            // Dragging to left side of rectangle
                                TabPages.Remove(draggedTab);
                                TabPages.Insert(overIndex, draggedTab);
                                SelectedTab = draggedTab;
                        }
                        else
                        {
                            // Dragging to right side of rectangle
                            if (TabPages.IndexOf(overTab) != TabPages.IndexOf(draggedTab) - 1)
                            {
                                TabPages.Remove(draggedTab);
                                TabPages.Insert(overIndex + 1, draggedTab);
                                SelectedTab = draggedTab;
                            }
                        }
                    }
                }
            }
        }
    }
}
