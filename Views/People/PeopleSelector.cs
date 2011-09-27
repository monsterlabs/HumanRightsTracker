using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PeopleSelector : Gtk.Bin
    {
        HashSet<Person> people = new HashSet<Person>(new ARComparer<Person>());

        public PeopleSelector ()
        {
            this.Build ();
            row.Destroy ();
        }

        public HashSet<Person> People {
            get {
                return this.people;
            }
            set {
                people = value;
                foreach (Gtk.Widget person in peopleList.Children)
                {
                    person.Destroy();
                }
                foreach (Person person in people)
                {
                    peopleList.PackStart (new PersonRow(person, OnRemoved));
                }
                peopleList.ShowAll ();
            }
        }
        protected void OnPersonSelected (object sender, PersonEventArgs args)
        {
            if (people.Add (args.Person))
            {
                peopleList.PackStart (new PersonRow(args.Person, OnRemoved));
                peopleList.ShowAll ();
            }

            return;
        }

        protected void OnAddClicked (object sender, System.EventArgs e)
        {
            new PeopleSelectorWindow (OnPersonSelected);
        }

        protected void OnRemoved (object sender, System.EventArgs e)
        {
            PersonRow row = sender as PersonRow;
            people.Remove (row.Person);
            peopleList.Remove (row);

            return;
        }
    }
}

