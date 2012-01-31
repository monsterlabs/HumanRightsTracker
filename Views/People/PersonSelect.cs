using System;
using HumanRightsTracker.Models;

using Mono.Unix;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonSelect : Gtk.Bin, IEditable
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
                    } else {
                        photo.Pixbuf = Gdk.Pixbuf.LoadFromResource ("Views.images.MissingIcon.jpg");
                    }
                    fullname.Text = person.Fullname;
                    photo.Show ();
                    fullname.Show ();
                    changeButton.Label = Catalog.GetString("Change");
                } else {
                    photo.Hide ();
                    fullname.Hide ();
                    changeButton.Label = Catalog.GetString("New");
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

