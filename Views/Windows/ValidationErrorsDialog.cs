using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Views
{
    public partial class ValidationErrorsDialog : Gtk.Dialog
    {
        public ValidationErrorsDialog (IDictionary propertiesValidationErrorMessages, Gtk.Window parent)
        {
            this.Build ();
            foreach (PropertyInfo p in propertiesValidationErrorMessages.Keys)
            {
                String messages = "";
                foreach (String m in propertiesValidationErrorMessages[p] as ArrayList)
                {
                    messages += m;
                }
                Gtk.Label label = new Gtk.Label (p.Name + ": " + messages);
                vbox3.PackEnd (label);
                vbox3.ShowAll ();
                this.Modal = true;
                this.TransientFor = parent;
            }
        }

         public ValidationErrorsDialog (String ErrorMessage, Gtk.Window parent)
        {
            this.Build ();
            Gtk.Label label = new Gtk.Label (ErrorMessage);
            vbox3.PackEnd (label);
            vbox3.ShowAll ();
            this.Modal = true;
            this.TransientFor = parent;
        }

        protected void OnOk (object sender, System.EventArgs e)
        {
            this.Destroy ();
        }
    }
}

