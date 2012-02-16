using System;
using Mono.Unix;
using HumanRightsTracker.Models;
using System.Collections;
using System.Collections.Generic;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InterventionShow : Gtk.Bin
    {
        bool isEditable;
        Intervention intervention;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public InterventionShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }

        public Intervention Intervention {
            get { return this.intervention; }
            set {
                intervention = value;
                if (intervention != null) {
                    interventionType.Active = intervention.InterventionType;
                    dateSelector.CurrentDate = intervention.Date;
                    impact.Text = intervention.Impact;
                    response.Text = intervention.Response;

                    interventorSelect.Person = intervention.Interventor;
                    interventorSelect.Institution = intervention.InterventorInstitution;
                    interventorSelect.AffiliationType = intervention.InterventorAffiliationType;

                    supporterSelect.Person = intervention.Supporter;
                    supporterSelect.Institution = intervention.SupporterInstitution;
                    supporterSelect.AffiliationType = intervention.SupporterAffiliationType;

                    // intervention affected people
                    HashSet<Person> affected = new HashSet<Person>(new ARComparer<Person>());
                    IList affectedPeopleList = intervention.AffectedPeople;
                    if (affectedPeopleList != null) {
                        foreach (InterventionAffectedPeople affectedPerson in affectedPeopleList)
                        {
                            affected.Add(affectedPerson.Person);
                        }
                    }
                    affectedPeople.People = affected;
                }
                IsEditable = false;
            }
        }

        public bool IsEditable {
            get { return this.isEditable; }
            set
            {
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
        
        protected void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!IsEditable && Canceled != null)
                Canceled (sender, e);
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = false;
            if (intervention.Id < 1) {
                newRow = true;
            }
            intervention.InterventionType = interventionType.Active as InterventionType;
            intervention.Date = dateSelector.CurrentDate;
            intervention.Impact = impact.Text;
            intervention.Response = response.Text;

            intervention.Interventor = interventorSelect.Person;
            intervention.InterventorInstitution = interventorSelect.Institution;
            intervention.InterventorAffiliationType = interventorSelect.AffiliationType;

            intervention.Supporter = supporterSelect.Person;
            intervention.SupporterInstitution = supporterSelect.Institution;
            intervention.SupporterAffiliationType = supporterSelect.AffiliationType;

            if (intervention.IsValid())
            {
                if (intervention.AffectedPeople == null) {
                    intervention.AffectedPeople = new List<InterventionAffectedPeople> ();
                } else {
                    intervention.AffectedPeople.Clear ();
                }

                foreach (Person person in affectedPeople.People) {
                    InterventionAffectedPeople affected = new InterventionAffectedPeople ();
                    affected.Intervention = intervention;
                    affected.Person = person;
                    intervention.AffectedPeople.Add (affected);
                }

                intervention.SaveAndFlush ();

                if(newRow) {
                    intervention.Case.Interventions.Add (Intervention);
                    intervention.Case.SaveAndFlush ();
                }

                this.IsEditable = false;

                if (Saved != null)
                    Saved (this.Intervention, e);
            } else {
                Console.WriteLine( String.Join(",", intervention.ValidationErrorMessages) );
                new ValidationErrorsDialog (intervention.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }
    }
}

