using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseAndPeopleRow : Gtk.Bin
    {
        Case c;
        public CaseAndPeopleRow (Case c)
        {
            this.Build ();
            this.c = c;
            set_widgets();
        }


        protected void set_widgets() {
            case_row.Case = c;
        }
    }
}

