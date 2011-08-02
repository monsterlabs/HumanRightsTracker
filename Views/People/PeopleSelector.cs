using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PeopleSelector : Gtk.Bin
    {
        Person person;

        public PeopleSelector ()
        {
            this.Build ();
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
                        photo.Pixbuf = new Gdk.Pixbuf (person.Photo.Thumbnail);
                    fullname.Text = person.Fullname;
                    photo.Show ();
                    fullname.Show ();
                } else {
                    photo.Hide ();
                    fullname.Hide ();
                }
            }
        }

        protected void OnChangeClicked (object sender, System.EventArgs e)
        {
            new PeopleSelectorWindow (OnPersonSelected);
        }

        protected void OnPersonSelected (object sender, PersonEventArgs args)
        {
            if (args.Person != null)
            {
                Person p = args.Person;
                fullname.Text = p.Fullname;
                if (p.Photo != null)
                    photo.Pixbuf = new Gdk.Pixbuf (p.Photo.Thumbnail);
                else
                    photo.Pixbuf = null;
            }
            return;
        }
    }
}

