using System;
using HumanRightsTracker.Models;
using Mono.Unix;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionShow : Gtk.Bin
    {
        public Institution institution;
        protected bool isEditing;

        public event EventHandler InstitutionSaved;

        public InstitutionShow ()
        {
            this.Build ();
            this.isEditing = false;
        }

        public Institution Institution {
            get { return this.institution; }
            set {
                institution = value;
                if (institution != null) {
                    nameEntry.Text = institution.Name;
                    abbrevEntry.Text = institution.Abbrev;
                    institutionTypeSelector.Active = institution.InstitutionType;

                    locationEntry.Text = institution.Location;
                    countrySelector.Active = institution.Country;
                    stateSelector.Active = institution.State;
                    citySelector.Active = institution.City;

                    phoneEntry.Text = institution.Phone;
                    faxEntry.Text = institution.Fax;
                    emailEntry.Text = institution.Email;
                    urlEntry.Text = institution.Url;
                }
                isEditing = false;
            }
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
                nameEntry.Visible = value;
                abbrevEntry.Visible = value;
                institutionTypeSelector.IsEditable = value;

                locationEntry.Visible = value;
                countrySelector.IsEditable = value;
                stateSelector.IsEditable = value;
                citySelector.IsEditable = value;

                phoneEntry.Visible = value;
                faxEntry.Visible = value;
                emailEntry.Visible = value;
                urlEntry.Visible = value;
                imageSelector.IsEditable = value;
            }
        }


        protected void OnSaveButtonClicked (object sender, System.EventArgs e)
        {
            institution.Name = nameEntry.Text;
            institution.Abbrev = abbrevEntry.Text;
            institution.InstitutionType = institutionTypeSelector.Active as InstitutionType;

            institution.Location = locationEntry.Text;
            institution.Country = countrySelector.Active as Country;
            institution.State = stateSelector.Active as State;
            institution.City = citySelector.Active as City;

            institution.Phone = phoneEntry.Text;
            institution.Fax = faxEntry.Text;
            institution.Email = emailEntry.Text;
            institution.Url = urlEntry.Text;

            if (institution.IsValid())
            {
                institution.Save ();
                Image photo = imageSelector.Image;
                if (photo != null)
                {
                    photo.ImageableId = institution.Id;
                    photo.ImageableType = "Institution";
                    photo.Save ();
                }

                this.IsEditing = false;
                if (InstitutionSaved != null)
                    InstitutionSaved (institution, e);
            } else
            {
                Console.WriteLine( String.Join(",", institution.ValidationErrorMessages) );
                new ValidationErrorsDialog (institution.PropertiesValidationErrorMessages);
            }

        }

        protected void OnEditButtonClicked (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
        }
    }
}

