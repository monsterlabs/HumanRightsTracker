using System;
using Mono.Unix;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonRelationshipShow : Gtk.Bin
    {
        PersonRelationship person_relationship;
        bool isEditable;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public PersonRelationshipShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }

        public PersonRelationship PersonRelationship {
            get { return this.person_relationship; }
            set {
                this.person_relationship = value;
                personRelationshipTypeSelector.Active = person_relationship.PersonRelationshipType;
                personSelector.Person = person_relationship.RelatedPerson as Person;
                startDateSelector.setDate(person_relationship.start_date, person_relationship.StartDateType);
                endDateSelector.setDate(person_relationship.end_date, person_relationship.EndDateType);
                comments.Text = person_relationship.Comments;
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

       public void HideActionButtons () {
            editButton.Visible = false;
            saveButton.Visible = false;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = false;
            if (person_relationship.Id < 1) {
                newRow = true;
            }

            person_relationship.PersonRelationshipType = personRelationshipTypeSelector.Active as PersonRelationshipType;
            person_relationship.RelatedPerson = personSelector.Person;
            person_relationship.start_date = startDateSelector.SelectedDate ();
            person_relationship.StartDateType = startDateSelector.SelectedDateType ();
            person_relationship.end_date = endDateSelector.SelectedDate ();
            person_relationship.EndDateType = endDateSelector.SelectedDateType ();
            person_relationship.Comments = comments.Text;

            if (person_relationship.IsValid ()) {
                person_relationship.SaveAndFlush ();
                if (newRow) {
                    person_relationship.Person.PersonRelationships.Add (PersonRelationship);
                    person_relationship.Person.SaveAndFlush ();
                }
                this.IsEditable = false;

                if (Saved != null)
                   Saved (this.PersonRelationship, e);
            } else {
                Console.WriteLine( String.Join(",", person_relationship.ValidationErrorMessages) );
                new ValidationErrorsDialog (person_relationship.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }

        protected void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!isEditable && Canceled != null) {
                Canceled (sender, e);
            }
        }
    }
}

