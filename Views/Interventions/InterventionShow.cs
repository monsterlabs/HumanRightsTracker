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
                    interventorSelect.Job = intervention.InterventorJob;

                    supporterSelect.Person = intervention.Supporter;
                    supporterSelect.Institution = intervention.SupporterInstitution;
                    supporterSelect.Job = intervention.SupporterJob;

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

        protected void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!IsEditable && Canceled != null)
                Canceled (sender, e);
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            intervention.InterventionType = interventionType.Active as InterventionType;
            intervention.Date = dateSelector.CurrentDate;
            intervention.Interventor = interventorSelect.Person;
            intervention.InterventorInstitution = interventorSelect.Institution;
            intervention.InterventorJob = interventorSelect.Job;

            intervention.Supporter = supporterSelect.Person;
            intervention.SupporterInstitution = supporterSelect.Institution;
            intervention.SupporterJob = supporterSelect.Job;

            if (intervention.IsValid())
            {
                List<InterventionAffectedPeople> affectedPeopleList = new List<InterventionAffectedPeople>();

                foreach (Person person in affectedPeople.People)
                {
                    InterventionAffectedPeople affectedPerson = new InterventionAffectedPeople();
                    affectedPerson.Intervention = intervention;
                    affectedPerson.Person = person;

                    affectedPeopleList.Add(affectedPerson);
                }
                intervention.AffectedPeople = affectedPeopleList;

                this.IsEditable = false;

                if (intervention.Id < 1 || intervention.Case.Id < 1)
                {
                    if (Saved != null)
                        Saved (this.Intervention, e);
                    return;
                } else {
                    intervention.Save();
                }
            } else
            {
                Console.WriteLine( String.Join(",", intervention.ValidationErrorMessages) );
                new ValidationErrorsDialog (intervention.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }
    }
}

