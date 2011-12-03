using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonOrInstitutionSelector : Gtk.Bin
    {
        public PersonOrInstitutionSelector ()
        {
            this.Build ();
        }

        protected void OnComboboxChanged (object sender, System.EventArgs e)
        {
            int i = 0;
            foreach (Gtk.Widget w in vbox.AllChildren) {
                if (i == 1)
                    w.Destroy();
                i++;
            }

            if (combobox.ActiveText == "Person")
                vbox.PackEnd (new PersonAndInstitutionSelect());
            else if (combobox.ActiveText == "Institution")
                vbox.PackEnd (new InstitutionSelect());

            vbox.ShowAll ();
        }
    }
}

