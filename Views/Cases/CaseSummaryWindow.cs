using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class CaseSummaryWindow : Gtk.Window
    {

         public CaseSummaryWindow (Case c, Gtk.Window parent) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            case_row.Case = c;
        }
    }
}

