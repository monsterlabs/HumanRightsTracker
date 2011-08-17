using System;
using System.Reflection;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EditCatalogRecord : Gtk.Bin
    {
        public EditCatalogRecord ()
        {
            this.Build ();
        }

        public void SetRecord()
        {
            nameEntry.Text = "test";
        }

    }
}

