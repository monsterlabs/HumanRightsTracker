using System;
using HumanRightsTracker.Models;
namespace Views.People
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class Show : Gtk.Bin
    {
        public Person person;

        public Show ()
        {
            this.Build ();
        }

        public Person Person {
            get { return this.person; }
            set {
                person = value;
                if (person != null) {
                    lastname.Text = person.Lastname;
                    firstname.Text = person.Firstname;
                    birthday.CurrentDate = person.Birthday;
                }
            }
        }
    }
}

