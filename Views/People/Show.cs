using System;
using HumanRightsTracker.Models;
namespace Views.People
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class Show : Gtk.Bin
    {
        public Person person;
        protected bool isEditing;

        public Show ()
        {
            this.Build ();
            this.IsEditing = false;
        }

        public Person Person {
            get { return this.person; }
            set {
                person = value;
                if (person != null) {
                    lastname.Text = person.Lastname;
                    firstname.Text = person.Firstname;
                    birthday.CurrentDate = person.Birthday;
                    sex.Active = person.Gender ? 1 : 0;
                    marital_status.Active = person.MaritalStatus;
                    birthplace.SetPlace(person.Country, person.State, person.City);

                    sexText.Text = sex.ActiveText;
                    fullnameText.Text = person.Fullname;
                }
                IsEditing = false;
            }
        }

        protected virtual void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
        }

        protected bool IsEditing
        {
            get { return this.isEditing; }
            set
            {
                isEditing = value;
                if (value) {
                    editButton.Label = "Cancel";
                } else {
                    editButton.Label = "Edit";
                }
                firstname.Visible = value;
                lastname.Visible = value;
                sex.Visible = value;
                sexText.Visible = !value;
                fullnameText.Visible = !value;

                marital_status.IsEditable = value;
                birthday.IsEditable = value;
                birthplace.IsEditable = value;
            }
        }

    }
}
        
        

