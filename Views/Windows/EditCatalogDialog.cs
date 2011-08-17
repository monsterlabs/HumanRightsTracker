using System;

namespace Views
{
    public partial class EditCatalogDialog : Gtk.Dialog
    {
        String model;
        Type t;

        public EditCatalogDialog (string model)
        {
            this.Build ();
            this.model = model;
        }

        protected void OnButtonOkClicked (object sender, System.EventArgs e)
        {
        }
    }
}

