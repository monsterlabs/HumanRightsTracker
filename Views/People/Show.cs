using System;
using HumanRightsTracker.Models;
using Mono.Unix;

namespace Views.People
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class Show : Gtk.Bin
    {
        public Person person;
        public PersonDetail person_detail;
        protected bool isEditing;
        public bool personDetailExist;

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
                    alias.Text = person.Alias == null ? "" : person.Alias;
                    birthday.CurrentDate = person.Birthday;
                    gender.Activate = person.Gender;
                    marital_status.Active = person.MaritalStatus;
                    birthplace.SetPlace(person.Country, person.State, person.City);
                    settlement.Text = person.Settlement == null ? "" : person.Settlement;
                    imageselector1.Image = person.Photo;
                    age.Text = "" + DateTime.Now.Subtract(person.Birthday).Days/365;

                    if (person.PersonDetails.Count == 0) {
                        person_detail = new PersonDetail ();
                        personDetailExist = false;
                    } else {
                        person_detail = (PersonDetail)person.PersonDetails[0];
                        personDetailExist = true;
                        number_of_sons.Text = person_detail.NumberOfSons.ToString();
                        religion.Active = person_detail.Religion;
                        scholarity_level.Active = person_detail.ScholarityLevel;
                        most_recent_job.Active = person_detail.MostRecentJob;
                        ethnic_group.Active = person_detail.EthnicGroup as EthnicGroup;
                        indigenous_group.Text = person_detail.IndigenousGroup == null ? "" : person_detail.IndigenousGroup;
                        is_spanish_speaker.Activate = person_detail.IsSpanishSpeaker;
                    }
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

                // Person
                lastname.IsEditable = value;
                firstname.IsEditable = value;
                alias.IsEditable = value;
                settlement.IsEditable = value;
                gender.IsEditable = value;
                marital_status.IsEditable = value;
                birthday.IsEditable = value;
                birthplace.IsEditable = value;
                imageselector1.IsEditable = value;
                age.IsEditable = value;

                //Person Details
                number_of_sons.IsEditable = value;
                scholarity_level.IsEditable = value;
                most_recent_job.IsEditable = value;
                religion.IsEditable = value;
                ethnic_group.IsEditable = value;
                indigenous_group.IsEditable = value;
                is_spanish_speaker.IsEditable = value;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            person_detail.NumberOfSons = int.Parse(number_of_sons.Text);
            person_detail.ScholarityLevel = scholarity_level.Active as ScholarityLevel;
            person_detail.Religion = religion.Active as Religion;
            person_detail.EthnicGroup = ethnic_group.Active as EthnicGroup;
            person_detail.MostRecentJob = most_recent_job.Active as Job;
            person_detail.IndigenousGroup = indigenous_group.Text;
            person_detail.IsSpanishSpeaker = is_spanish_speaker.Value ();

            if (person_detail.IsValid()) {
                person_detail.Save ();
            }

            person.Lastname = lastname.Text;
            person.Firstname = firstname.Text;
            person.Alias = alias.Text;
            if (birthday.CurrentDate.Year == 1)
            {
                person.Birthday = new DateTime(DateTime.Now.Subtract(new TimeSpan(Convert.ToInt32(age.Text)*365, 0, 0, 0)).Year, 1, 1);
            } else
            {
                person.Birthday = birthday.CurrentDate;
            }

            person.Country = birthplace.Country;
            person.MaritalStatus = marital_status.Active as MaritalStatus;
            person.Gender = gender.Value ();
            person.Settlement = settlement.Text;

            if (person.IsValid())
            {
                person.Save ();

                if (personDetailExist == false) {
                    person.PersonDetails.Add(person_detail);
                    person.Update ();
                }

                Image photo = imageselector1.Image;
                if (photo != null)
                {
                    photo.ImageableId = person.Id;
                    photo.ImageableType = "Person";
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

        protected void OnBirthdayChanged (object sender, System.EventArgs e)
        {
            DateTime selectedBD = (DateTime)sender;
            age.Text = "" + DateTime.Now.Subtract(selectedBD).Days/365;
        }
    }
}
        
        

