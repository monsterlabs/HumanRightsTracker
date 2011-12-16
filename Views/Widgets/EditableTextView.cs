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
            label.Visible = isEditable;
            textview.Visible = !isEditable;
        }

        public bool IsEditable {
            set {
                this.isEditable = value;
                if (this.isEditable) {
                    label.Visible = false;
                    textview.Visible = true;
                } else {
                    label.Visible = true;
                    textview.Visible = false;
                }
            }
            get {
                return this.isEditable;
            }
        }

        public String Text {
            set {
                this.text = value;
                label.Text = this.text;
                textview.Buffer.Text = this.text;
            }
            get {
                return textview.Buffer.Text;
            }
        }
    }
}

