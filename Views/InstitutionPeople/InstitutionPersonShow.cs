using System;
using Mono.Unix;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionPersonShow : Gtk.Bin
    {
        InstitutionPerson institution_person;
        bool isEditable;
        bool usingInstitution = false;
        bool usingPerson = true;

        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;


        public InstitutionPersonShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }


        public InstitutionPerson InstitutionPerson {
            get { return this.institution_person; }
            set {
                this.institution_person = value;
                affiliation_type.Active = institution_person.AffiliationType;
                personSelector.Person = institution_person.Person;
                institutionSelector.Institution = institution_person.Institution;
                startDateSelector.setDate(institution_person.start_date, institution_person.StartDateType);
                endDateSelector.setDate(institution_person.end_date, institution_person.EndDateType);
                comments.Text = institution_person.Comments;
            }
        }

        public bool IsEditable
        {
            get { return this.isEditable; }
            set {
                this.isEditable = value;
                this.editable_helper.SetAllEditable(value);

                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }

                if (usingInstitution == true) {
                   personSelector.IsEditable = value;
                   institutionSelector.IsEditable = false;
                }

                if (usingPerson == true) {
                   institutionSelector.IsEditable = value;
                   personSelector.IsEditable = false;
                }

            }
        }

       public void InstitutionReadOnly () {
            institutionSelector.IsEditable = false;
            usingInstitution = true;
            usingPerson = false;
       }

       public void PersonReadOnly () {
            personSelector.IsEditable = false;
            usingPerson = true;
            usingInstitution = false;
       }

       public void HideActionButtons () {
            editButton.Visible = false;
            saveButton.Visible = false;
       }

        protected void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = false;
            if (institution_person.Id < 1) {
                newRow = true;
            }

            institution_person.AffiliationType = affiliation_type.Active as AffiliationType;
            institution_person.Institution = institutionSelector.Institution;
            institution_person.Person = personSelector.Person;
            institution_person.start_date = startDateSelector.SelectedDate ();
            institution_person.StartDateType = startDateSelector.SelectedDateType ();
            institution_person.end_date = endDateSelector.SelectedDate ();
            institution_person.EndDateType = endDateSelector.SelectedDateType ();
            institution_person.Comments = comments.Text;

            if (institution_person.IsValid ()) {
                institution_person.SaveAndFlush ();
                if (newRow) {
                    if ( usingInstitution ) {
                        institution_person.Institution.InstitutionPeople.Add (InstitutionPerson);
                        institution_person.Institution.SaveAndFlush ();
                    }
                    /*
                     else {
                        institution_person.Person.InstitutionPeople.Add (InstitutionPerson);
                        institution_person.Person.SaveAndFlush ();
                    }
                    */
                }
                this.IsEditable = false;

                if (Saved != null)
                   Saved (this.InstitutionPerson, e);
            } else {
                Console.WriteLine( String.Join(",", institution_person.ValidationErrorMessages) );
                new ValidationErrorsDialog (institution_person.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }

        protected void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!IsEditable && Canceled != null)
                Canceled (sender, e);
        }
    }
}

