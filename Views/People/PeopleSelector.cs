using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PeopleSelector : Gtk.Bin
    {
        List<Person> people = new List<Person>(3);

        public PeopleSelector ()
        {
            this.Build ();
        }

        protected void OnPersonSelected (object sender, PersonEventArgs args)
        {
            peopleList.PackStart (new PersonRow(args.Person));
            peopleList.ShowAll ();
            people.Add (args.Person);
            return;
        }

        protected void OnAddClicked (object sender, System.EventArgs e)
        {
            new PeopleSelectorWindow (OnPersonSelected);
        }
    }
}

