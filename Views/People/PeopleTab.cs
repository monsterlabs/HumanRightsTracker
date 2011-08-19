using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PeopleTab : Gtk.Bin, TabWithDefaultButton
    {
        public PeopleTab ()
        {
            this.Build ();
        }

        protected virtual void PersonSelected (object sender, System.EventArgs e)
        {
            if (sender != null)
            {
                show.Show();
                Person person = (Person) sender;
                show.Person = person;
                removeButton.Sensitive = true;
            } else {
                show.Hide();
                removeButton.Sensitive = false;
            }
        }

        protected void onAdd (object sender, System.EventArgs e)
        {
            Person p = new Person();
            peoplelist.UnselectAll();
            show.Person = p;
            show.IsEditing = true;
            show.Show();
            return;
        }

        protected void onRemove (object sender, System.EventArgs e)
        {
            Person p = show.Person;
            p.Delete();
            peoplelist.ReloadStore();
            return;
        }

        public Gtk.Button DefaultButton ()
        {
            return peoplelist.SearchButton;
        }

        public void InitialSetup ()
        {
            show.Hide ();
        }

        protected void OnPersonSaved (object sender, System.EventArgs e)
        {
            peoplelist.ReloadStore();
        }
    }
}

