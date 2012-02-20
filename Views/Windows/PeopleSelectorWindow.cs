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

        protected void OnPersonCreated (object sender, EventArgs args)
        {
            // Fix it: Reload only one list
            peoplelist1.ReloadStore();
            peoplelist2.ReloadStore();
        }

        protected void OnChangeTab (object o, Gtk.SwitchPageArgs args)
        {
            int pageNum = (int) args.PageNum;
            if (pageNum == 0) {
                peoplelist1.ReloadStore();
            } else {
                peoplelist2.ReloadStore();
            }

        }

        protected void OnAdd (object sender, System.EventArgs e)
        {
            new PersonCreateWindow (OnPersonCreated, false, (Gtk.Window)this.Toplevel);
        }

        protected void OnAddImmigrant (object sender, System.EventArgs e)
        {
            new PersonCreateWindow (OnPersonCreated, true, (Gtk.Window)this.Toplevel);
        }
    }
}

