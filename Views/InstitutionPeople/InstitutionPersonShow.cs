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
            }
        }

       public void InstitutionReadOnly () {
            institutionSelector.IsEditable = false;
            usingInstitution = true;
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

