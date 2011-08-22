using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EditableLabel : Gtk.Bin
    {
        protected bool isEditable;
        protected String text_string;

        public EditableLabel ()
        {
            this.Build ();
            this.isEditable = false;
            label.Visible = isEditable;
            entry.Visible = !isEditable;
        }

        public int WidthChars {
            get { return entry.WidthChars; }
            set { entry.WidthChars = value; }
        }

        public int MaxLength {
            get { return entry.MaxLength; }
            set { entry.MaxLength = value; }
        }

        public bool IsEditable {
            set {
                this.isEditable = value;
                if (this.isEditable) {
                    label.Visible = false;
                    entry.Visible = true;
                } else {
                    label.Visible = true;
                    entry.Visible = false;
                }
            }
            get {
                return this.isEditable;
            }
        }

        public String Text {
            set {
                this.text_string = value;
                label.Text = this.text_string;
                entry.Text = this.text_string;
            }
            get {
                return entry.Text;
            }
        }

    }
}

