using System;
using System.Collections.Generic;

namespace Views
{
    public class EditableHelper
    {
        private Gtk.Widget root_widget;
        private List<IEditable> editable_widgets;

        public IEditable[] EditableWidgets {
            get { return this.editable_widgets.ToArray(); }
        }

        public EditableHelper (Gtk.Widget root)
        {
            this.root_widget = root;
            this.editable_widgets = new List<IEditable>();
            this.GetAllEditableWidgets();
        }

        public void SetAllEditable(Boolean value)
        {
            foreach(IEditable e in this.EditableWidgets) {
                e.IsEditable = value;
            }
        }

        public void UpdateEditableWidgets()
        {
            editable_widgets.Clear ();
            GetAllEditableWidgets ();
        }

        private void GetAllEditableWidgets()
        {
            Queue<Gtk.Widget> queue = new Queue<Gtk.Widget>();
            queue.Enqueue(this.root_widget);

            while(queue.Count != 0) {
                Gtk.Widget currentNode = queue.Dequeue();

                if (currentNode is Gtk.Container) {
                    Gtk.Container container = (Gtk.Container) currentNode;

                    foreach(Gtk.Widget w in container.Children) {
                        queue.Enqueue(w);
                    }
                }

                if (currentNode is IEditable) {
                    this.editable_widgets.Add((IEditable) currentNode);
                }
            }
        }
    }
}