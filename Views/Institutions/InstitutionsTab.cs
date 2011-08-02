using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionsTab : Gtk.Bin, TabWithDefaultButton
    {
        public InstitutionsTab ()
        {
            this.Build ();
        }

        public Gtk.Button DefaultButton ()
        {
            return institutionlist.SearchButton;
        }
    }
}