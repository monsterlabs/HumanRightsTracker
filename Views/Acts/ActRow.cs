using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ActRow : Gtk.Bin
    {
        Act act;

        public ActRow ()
        {
            this.Build ();
        }

        public ActRow (Act act)
        {
            this.Build ();
            Act = act;
        }

        public Act Act
        {
            get { return act; }
            set
            {
                act = value;
                title.Text = value.HumanRightsViolation.Name;
            }
        }

    }
}

