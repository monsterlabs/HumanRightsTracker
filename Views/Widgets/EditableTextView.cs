using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EditableTextView : Gtk.Bin, IEditable
    {
        protected bool isEditable;
        protected string text;

        public EditableTextView ()
        {
            this.Build ();
            this.isEditable = false;
//            textview.ModifyBase(Gtk.StateType.Insensitive, new Gdk.Color(0xdf, 0xdf, 0xdf));
//            textview.Sensitive = false;
        }

        public bool IsEditable {
            set {
                this.isEditable = value;
                if (this.isEditable) {
                    textview.Editable = true;
                } else {
                    textview.Editable = false;
                }
            }
            get {
                return this.isEditable;
            }
        }

        public String Text {
            set {
                this.text = value;
                textview.Buffer.Text = this.text;
            }
            get {
                return textview.Buffer.Text;
            }
        }
    }
}

