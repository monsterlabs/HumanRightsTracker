using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonRow : Gtk.Bin
    {
        Person person;

        public PersonRow (Person person)
        {
            this.Build ();
            this.Person = person;
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
    }
}

