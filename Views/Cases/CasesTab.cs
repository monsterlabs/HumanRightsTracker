using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CasesTab : Gtk.Bin, TabWithDefaultButton
    {
        public CasesTab ()
        {
            this.Build ();
        }

        public Gtk.Button DefaultButton ()
        {
            return caselist.SearchButton;
        }

    }
}

