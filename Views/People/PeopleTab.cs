using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PeopleTab : Gtk.Bin
    {
        public PeopleTab ()
        {
            this.Build ();
            show.Visible = false;
        }

        protected virtual void PersonSelected (object sender, System.EventArgs e)
        {
            show.Visible = true;
            Person person = (Person) sender;
            show.Person = person;
        }
    }
}

