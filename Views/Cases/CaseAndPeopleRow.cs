using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseAndPeopleRow : Gtk.Bin
    {
        Case c;
        Institution i;
        public CaseAndPeopleRow (Case c, Institution i)
        {
            this.Build ();
            this.c = c;
            this.i = i;
            set_widgets();
        }


        protected void set_widgets() {
            case_row.Case = c;
        }
    }
}

