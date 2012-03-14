using System;
using Mono.Unix;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class BooleanRadioButton : Gtk.Bin, IEditable
    {
        string[] labels;
        bool isEditable;

        public BooleanRadioButton ()
        {
            this.Build ();
        }

        public string[] Labels {
            get { return this.labels; }

            set {
                labels = value;
                radiobutton_true.Label = Catalog.GetString(labels[0]);
                radiobutton_false.Label = Catalog.GetString(labels[1]);
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
                text.Text = radiobutton_true.Active ? Catalog.GetString(labels[0]) : Catalog.GetString(labels[1]);
            }
        }

        public new bool Activate {
            get {
                return Value ();
            }
            set {
               bool state = value;
               // Console.WriteLine("Radio button value {0} for {1}", state, this.Name);
               radiobutton_true.Active = value;
               radiobutton_false.Active = !value;
            }
        }
    }
}