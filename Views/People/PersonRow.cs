using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonRow : Gtk.Bin
    {
        Person person;
        public event EventHandler Removed;

        public PersonRow (Person person, EventHandler removed)
        {
            this.Build ();
            this.Person = person;
            this.Removed = removed;
        }

        public Person Person
        {
            get {return person;}
            set
            {
                person = value;
                if (person != null)
                {
                    if (person.Photo != null)
                    {
                        photo.Pixbuf = new Gdk.Pixbuf (person.Photo.Icon);
                    }
                    fullname.Text = person.Fullname;
                    photo.Show ();
                    fullname.Show ();
                } else {
                    photo.Hide ();
                    fullname.Hide ();
                }
            }
        }

        protected void OnRemove (object sender, System.EventArgs e)
        {
            if (Removed != null)
                Removed (this, e);
        }
    }
}

