using System;
using HumanRightsTracker.Models;

namespace Views
{
    public class PersonEventArgs : EventArgs
    {
        private Person person;

        public Person Person {
            get { return person; }
        }

        public PersonEventArgs (Person person)
        {
            this.person = person;
        }
    }

    public partial class PeopleSelectorWindow : Gtk.Window
    {

        public delegate void PersonEventHandler (object sender, PersonEventArgs args);

        public event PersonEventHandler OnSelect = null;

        public PeopleSelectorWindow (PersonEventHandler handler) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            OnSelect = handler;
        }

        protected void OnSelection (object sender, System.EventArgs e)
        {
            Person p = sender as Person;
            OnSelect (this, new PersonEventArgs(p));
            this.Destroy ();
        }
    }
}

