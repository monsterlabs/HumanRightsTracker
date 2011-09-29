using System;
using HumanRightsTracker.Models;
using Mono.Unix;

namespace Views.People
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class Show : Gtk.Bin
    {
        public Person person;
        public PersonDetail person_details;
        public ImmigrationAttempt immigration_attempt;
        public Identification identification;
        public Address address;

        protected bool isEditing;
        public bool personDetailsExist;
        public bool immigrationAttemptExist;
        public bool identificationExist;
        public bool addressExist;
        public bool isImmigrant = false;

        public event EventHandler PersonSaved;

        public Show ()
        {
            this.Build ();
            this.IsEditing = false;
        }

        public Person Person {
            get { return this.person; }
            set {
                this.person = value;
                if (person != null) {
                     set_person_widgets ();
                     set_person_details_widgets ();
                     set_immigration_details_widgets ();
                     set_identification_widgets();
                     set_address_widgets();
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

                editable_person (value);
                editable_person_details (value);
                editable_immigration_attempts (value);
                editable_identification (value);
                editable_address (value);
            }
        }

        public bool IsImmigrant {
            get {
                return this.isImmigrant;
            }
            set {
                this.isImmigrant = value;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            person_detail_record ();
            if (person_details.IsValid()) {
                person_details.Save ();
            }

            immigration_attempt_record ();
            if (immigration_attempt.IsValid()) {
                immigration_attempt.Save ();
            }

            identification_record ();
            if (identification.IsValid()) {
                identification.Save();
            }

            address_record ();
            if (address.IsValid()) {
                address.Save();
            }

            person_record ();
            if (person.IsValid())
            {
                person.Save ();

                if (personDetailsExist == false) {
                    person.PersonDetails.Add(person_details);
                    person.Update ();
                }
                if (immigrationAttemptExist == false) {
                    person.ImmigrationAttempts.Add(immigration_attempt);
                    person.Update ();
                }

                if (identificationExist == false) {
                    person.Identifications.Add(identification);
                    person.Update ();
                }

                if (addressExist == false) {
                    person.Addresses.Add(address);
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


        protected void set_person_widgets ()
        {
            lastname.Text = person.Lastname == null ? "" : person.Lastname;
            firstname.Text = person.Firstname == null ? "" : person.Firstname;
            alias.Text = person.Alias == null ? "" : person.Alias;
            email.Text = person.Email == null ? "" : person.Email;
            birthday.CurrentDate = person.Birthday;
            gender.Activate = person.Gender;
            marital_status.Active = person.MaritalStatus;
            birthplace.SetPlace(person.Country, person.State, person.City);
            settlement.Text = person.Settlement == null ? "" : person.Settlement;
            imageselector1.Image = person.Photo;
            age.Text = "" + DateTime.Now.Subtract(person.Birthday).Days/365;
        }

        protected void set_person_details_widgets ()
        {
            if (person.PersonDetails.Count == 0) {
                person_details = new PersonDetail ();
                personDetailsExist = false;
            } else {
                person_details = (PersonDetail)person.PersonDetails[0];
                personDetailsExist = true;
                number_of_sons.Text = person_details.NumberOfSons.ToString();
                religion.Active = person_details.Religion;
                scholarity_level.Active = person_details.ScholarityLevel;
                most_recent_job.Active = person_details.MostRecentJob;
                ethnic_group.Active = person_details.EthnicGroup as EthnicGroup;
                indigenous_group.Text = person_details.IndigenousGroup == null ? "" : person_details.IndigenousGroup;
                is_spanish_speaker.Activate = person_details.IsSpanishSpeaker;
            }

        }

        protected void set_immigration_details_widgets ()
        {
            if (person.ImmigrationAttempts.Count == 0) {
                immigration_attempt = new ImmigrationAttempt();
                immigrationAttemptExist = false;
            } else {
                immigration_attempt = (ImmigrationAttempt)person.ImmigrationAttempts[0];
                immigrationAttemptExist = true;
                traveling_reason.Active = immigration_attempt.TravelingReason;
                destination_country.Active = immigration_attempt.DestinationCountry as Country;
                expulsions_from_destination_country.Text = immigration_attempt.ExpulsionsFromDestinationCountry.ToString();
                transit_country.Active = immigration_attempt.TransitCountry as Country;
                expulsions_from_transit_country.Text = immigration_attempt.ExpulsionsFromTransitCountry.ToString();
                cross_border_attempts_transit_country.Text = immigration_attempt.CrossBorderAttemptsTransitCountry.ToString();
           }
        }

        protected void set_identification_widgets()
        {
            if (person.Identifications.Count == 0) {
                identification = new Identification ();
                identificationExist = false;
            } else {
                identification = (Identification)person.Identifications[0];
                identificationExist = true;
                identification_type.Active = identification.IdentificationType;
                identification_number.Text = identification.IdentificationNumber;
            }
        }

        protected void set_address_widgets ()
        {
            if (person.Addresses.Count == 0) {
                address = new Address ();
                addressExist = false;
            } else {
                address = (Address)person.Addresses[0];
                addressExist = true;
                location.Text = address.Location;
                address_place.SetPlace(address.Country, address.State, address.City);
                zipcode.Text = address.ZipCode  == null ? "" : address.ZipCode;
                phone.Text = address.Phone == null ? "" : address.Phone;
                mobile.Text = address.Mobile  == null ? "" : address.Mobile;
            }
        }

        protected void editable_person (bool value)
        {
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
            email.IsEditable = value;
        }

        protected void editable_person_details (bool value)
        {
             number_of_sons.IsEditable = value;
             scholarity_level.IsEditable = value;
             most_recent_job.IsEditable = value;
             religion.IsEditable = value;
             ethnic_group.IsEditable = value;
             indigenous_group.IsEditable = value;
             is_spanish_speaker.IsEditable = value;
        }

        protected void editable_immigration_attempts (bool value)
        {
             traveling_reason.IsEditable = value;
             destination_country.IsEditable = value;
             expulsions_from_destination_country.IsEditable = value;
             transit_country.IsEditable = value;
             expulsions_from_transit_country.IsEditable = value;
             cross_border_attempts_transit_country.IsEditable = value;
        }

        protected void editable_identification (bool value)
        {
             identification_type.IsEditable = value;
             identification_number.IsEditable = value;
        }

        protected void editable_address (bool value)
        {
            location.IsEditable  = value;
            address_place.IsEditable  = value;
            zipcode.IsEditable  = value;
            phone.IsEditable  = value;
            mobile.IsEditable  = value;
        }

        protected void person_detail_record ()
        {
            person_details.NumberOfSons = int.Parse(number_of_sons.Text);
            person_details.ScholarityLevel = scholarity_level.Active as ScholarityLevel;
            person_details.Religion = religion.Active as Religion;
            person_details.EthnicGroup = ethnic_group.Active as EthnicGroup;
            person_details.MostRecentJob = most_recent_job.Active as Job;
            person_details.IndigenousGroup = indigenous_group.Text;
            person_details.IsSpanishSpeaker = is_spanish_speaker.Value ();
        }

        protected void immigration_attempt_record ()
        {
            immigration_attempt.TravelingReason = traveling_reason.Active as TravelingReason;
            immigration_attempt.DestinationCountry = destination_country.Active as Country;
            immigration_attempt.ExpulsionsFromDestinationCountry = int.Parse(expulsions_from_destination_country.Text);
            immigration_attempt.TransitCountry = transit_country.Active as Country;
            immigration_attempt.CrossBorderAttemptsTransitCountry = int.Parse(cross_border_attempts_transit_country.Text);
            immigration_attempt.ExpulsionsFromTransitCountry = int.Parse(expulsions_from_transit_country.Text);
        }

        protected void identification_record ()
        {
           identification.IdentificationType = identification_type.Active as IdentificationType;
           identification.IdentificationNumber = identification_number.Text;
        }

        protected void address_record ()
        {
            address.Location = location.Text;
            address.Phone = phone.Text;
            address.Mobile = mobile.Text;
            address.ZipCode = zipcode.Text;
            address.Country = address_place.Country;
            address.State = address_place.State;
            address.City = address_place.City;
        }

        protected void person_record ()
        {
            person.Lastname = lastname.Text;
            person.Firstname = firstname.Text;
            person.Alias = alias.Text;
            person.Email = email.Text;
            person.IsImmigrant = this.isImmigrant;

            if (birthday.CurrentDate.Year == 1)
            {
                person.Birthday = new DateTime(DateTime.Now.Subtract(new TimeSpan(Convert.ToInt32(age.Text)*365, 0, 0, 0)).Year, 1, 1);
            } else
            {
                person.Birthday = DateTime.Now;
            }

            person.Country = birthplace.Country as Country;
            person.State = birthplace.State as State;
            person.City = birthplace.City as City;

            person.MaritalStatus = marital_status.Active as MaritalStatus;
            person.Gender = gender.Value ();
            person.Settlement = settlement.Text;
        }
    }
}
