using System;
using System.Text.RegularExpressions;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EditableLabel : Gtk.Bin, IEditable
    {
        protected bool isEditable;
        protected String text_string;
        protected bool onlyNumbers;

        public EditableLabel ()
        {
            this.Build ();
            this.isEditable = false;
            label.Visible = isEditable;
            entry.Visible = !isEditable;
        }

        void NumericEntryChanged (object sender, EventArgs e)
        {
            if (entry.Text.Length > 0 && !Regex.IsMatch(entry.Text, "^\\d+$"))
            {
                this.ErrorBell();
                entry.Text = entry.Text.Substring(0, entry.Text.Length -1);
            }
        }

        public int WidthChars {
            get { return entry.WidthChars; }
            set
            {
                if (value > 0 )
                    entry.WidthChars = value;
            }
        }

        public int MaxLength {
            get { return entry.MaxLength; }
            set { entry.MaxLength = value; }
        }

        public bool OnlyNumbers {
            set {
                this.onlyNumbers = value;
                if (value)
                {
                    entry.Changed += NumericEntryChanged;
                }
            }
            get {
                return this.onlyNumbers;
            }
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

