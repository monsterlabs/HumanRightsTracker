using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class VictimSelector : Gtk.Bin
    {
        HashSet<Person> victims = new HashSet<Person>(new ARComparer<Person>());
        bool isEditing;

        public VictimSelector ()
        {
            this.Build ();
            row.Destroy ();
        }

        public bool IsEditing {
            get {
                return this.isEditing;
            }
            set {
                isEditing = value;
                addButton.Visible = value;
                foreach (Gtk.Widget row in peopleList.AllChildren) {
                    ((VictimRow) row).IsEditable = value;
                }
            }
        }
        public HashSet<Person> People {
            get {
                return this.victims;
            }
            set {
                victims = value;
                foreach (Gtk.Widget person in peopleList.Children)
                {
                    person.Destroy();
                }
                foreach (Person person in victims)
                {
                    peopleList.PackStart (new VictimRow(person, OnRemoved));
                }
                peopleList.ShowAll ();
            }
        }
        protected void OnPersonSelected (object sender, PersonEventArgs args)
        {
            if (victims.Add (args.Person))
            {
                peopleList.PackStart (new VictimRow(args.Person, OnRemoved));
                peopleList.ShowAll ();
            }

            return;
        }

        protected void OnAddClicked (object sender, System.EventArgs e)
        {
            // TODO VictimSelectorWindow.
            new PeopleSelectorWindow (OnPersonSelected);
        }

        protected void OnRemoved (object sender, System.EventArgs e)
        {
            VictimRow row = sender as VictimRow;
            victims.Remove (row.Person);
            peopleList.Remove (row);

            return;
        }
    }
}

