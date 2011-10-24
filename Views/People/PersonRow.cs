using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonRow : Gtk.Bin
    {
        protected Person person;
        protected bool isEditable;

        public event EventHandler OnRowRemoved;

        public PersonRow ()
        {
            this.Build ();
        }

        public PersonRow (Person person, EventHandler removed)
        {
            this.Build ();
            this.Person = person;
            this.OnRowRemoved = removed;
        }

        public virtual bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                button8.Visible = value;
            }
        }
        public virtual Person Person
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
            if (OnRowRemoved != null)
                OnRowRemoved (this, e);
        }
    }
}

