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
        public ImmigrationAttempt immigration_attempt;
        public Identification identification;
        public Address address;

        protected bool isEditing;
        public bool personDetailExist;
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
                person = value;
                if (person != null) {
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

                    if (person.Identifications.Count == 0) {
                        identification = new Identification ();
                        identificationExist = false;
                    } else {
                        identification = (Identification)person.Identifications[0];
                        identificationExist = true;
                        identification_type.Active = identification.IdentificationType;
                        identification_number.Text = identification.IdentificationNumber;
                    }

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
                email.IsEditable = value;

                //Person Details
                number_of_sons.IsEditable = value;
                scholarity_level.IsEditable = value;
                most_recent_job.IsEditable = value;
                religion.IsEditable = value;
                ethnic_group.IsEditable = value;
                indigenous_group.IsEditable = value;
                is_spanish_speaker.IsEditable = value;

                //Immigration Attempts
                traveling_reason.IsEditable = value;
                destination_country.IsEditable = value;
                expulsions_from_destination_country.IsEditable = value;
                transit_country.IsEditable = value;
                expulsions_from_transit_country.IsEditable = value;
                cross_border_attempts_transit_country.IsEditable = value;

                //Identification
                identification_type.IsEditable = value;
                identification_number.IsEditable = value;

                //Address
                location.IsEditable  = value;
                address_place.IsEditable  = value;
                zipcode.IsEditable  = value;
                phone.IsEditable  = value;
                mobile.IsEditable  = value;
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
            immigration_attempt.TravelingReason = traveling_reason.Active as TravelingReason;
            immigration_attempt.DestinationCountry = destination_country.Active as Country;
            immigration_attempt.ExpulsionsFromDestinationCountry = int.Parse(expulsions_from_destination_country.Text);
            immigration_attempt.TransitCountry = transit_country.Active as Country;
            immigration_attempt.CrossBorderAttemptsTransitCountry = int.Parse(cross_border_attempts_transit_country.Text);
            immigration_attempt.ExpulsionsFromTransitCountry = int.Parse(expulsions_from_transit_country.Text);
            if (immigration_attempt.IsValid()) {
                immigration_attempt.Save ();
            }

            identification.IdentificationType = identification_type.Active as IdentificationType;
            identification.IdentificationNumber = identification_number.Text;
            if (identification.IsValid()) {
                identification.Save();
            }

            address.Location = location.Text;
            address.Phone = phone.Text;
            address.Mobile = mobile.Text;
            address.ZipCode = zipcode.Text;
            address.Country = address_place.Country;
            address.State = address_place.State;
            address.City = address_place.City;
            if (address.IsValid()) {
                address.Save();
            }

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

            if (person.IsValid())
            {
                person.Save ();

                if (personDetailExist == false) {
                    person.PersonDetails.Add(person_detail);
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
    }
}
