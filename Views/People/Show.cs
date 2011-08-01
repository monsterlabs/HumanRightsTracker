using System;
using HumanRightsTracker.Models;
using Mono.Unix;

namespace Views.People
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class Show : Gtk.Bin
    {
        public Person person;
        protected bool isEditing;

        public event EventHandler PersonSaved;

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
                    lastname.Text = person.Lastname == null ? "" : person.Lastname;
                    firstname.Text = person.Firstname == null ? "" : person.Firstname;
                    birthday.CurrentDate = person.Birthday;
                    sex.Active = person.Gender ? 1 : 0;
                    marital_status.Active = person.MaritalStatus;
                    birthplace.SetPlace(person.Country, person.State, person.City);
                    imageselector1.Image = person.Photo;

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

        public bool IsEditing
        {
            get { return this.isEditing; }
            set
            {
                isEditing = value;
                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
                firstname.Visible = value;
                lastname.Visible = value;
                sex.Visible = value;
                sexText.Visible = !value;
                fullnameText.Visible = !value;

                marital_status.IsEditable = value;
                birthday.IsEditable = value;
                birthplace.IsEditable = value;
                imageselector1.IsEditable = value;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            person.Lastname = lastname.Text;
            person.Firstname = firstname.Text;
            person.Birthday = birthday.CurrentDate;
            person.Country = birthplace.Country;
            person.MaritalStatus = marital_status.Active as MaritalStatus;
            person.Gender = sex.Active == 1;

            if (person.IsValid())
            {
                person.Save ();
                Image photo = imageselector1.Image;
                if (photo != null)
                {
                    photo.ImageableId = person.Id;
                    photo.ImageableType = "People";
                    photo.Save ();
                }

                this.IsEditing = false;
                if (PersonSaved != null)
                    PersonSaved (person, e);
            } else
            {
                Console.WriteLine( String.Join(",", person.ValidationErrorMessages) );
                new ValidationErrorsDialog (person.PropertiesValidationErrorMessages);
            }

        }
    }
}
        
        

