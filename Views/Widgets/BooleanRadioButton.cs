using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class BooleanRadioButton : Gtk.Bin
    {
        String[] labels;
        bool isEditable;

        public BooleanRadioButton ()
        {
            this.Build ();
        }

        public String[] Labels {
            get { return this.labels; }

            set {
                labels = value;
                radiobutton_true.Label = labels[0];
                radiobutton_false.Label = labels[1];
            }
        }

        public bool Value () {
            if (radiobutton_true.Active)
               return true;
            else
               return false;
        }

         public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                radiobutton_true.Visible = value;
                radiobutton_false.Visible = value;
                text.Visible = !value;
                text.Text = radiobutton_true.Active ? labels[0] : labels[1];
            }
        }

        public bool Activate {
            get {
                return Value ();
            }
            set {
                bool state = value;
                if (state)
                    radiobutton_true.Activate ();
                else
                    radiobutton_false.Activate ();
            }
        }
    }
}