using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonSelect : Gtk.Bin
    {
        Person person;
        bool isEditable;
        public event EventHandler Changed;

        public PersonSelect ()
        {
            this.Build ();
        }

        public PersonSelect (Person person, EventHandler changed)
        {
            this.Build ();
            this.Person = person;
            this.Changed = changed;
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                changeButton.Visible = value;
            }
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

        protected void OnPersonSelected (object sender, PersonEventArgs args)
        {
            Person = args.Person;

            return;
        }

        protected void OnChangeClicked (object sender, System.EventArgs e)
        {
            new PeopleSelectorWindow (OnPersonSelected);
        }
    }
}

