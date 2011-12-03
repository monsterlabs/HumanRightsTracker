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
        bool isEditing;
        Intervention intervention;

        public event EventHandler InterventionSaved;
        public event EventHandler Cancel;

        public InterventionShow ()
        {
            this.Build ();
        }

        public Intervention Intervention {
            get { return this.intervention; }
            set {
                intervention = value;
                if (intervention != null) {
                    interventionType.Active = intervention.InterventionType;
                    dateSelector.CurrentDate = intervention.Date;

                    interventorSelect.Person = intervention.Interventor;
                    interventorSelect.Institution = intervention.InterventorInstitution;
                    interventorSelect.Job = intervention.InterventorJob;

                    supporterSelect.Person = intervention.Supporter;
                    supporterSelect.Institution = intervention.SupporterInstitution;
                    supporterSelect.Job = intervention.SupporterJob;

                    //interventorSelect.Person = intervention.Interventor;
                    //supporterSelect.Person = intervention.Supporter;
                    impactView.Buffer.Text = intervention.Impact;
                    responseView.Buffer.Text = intervention.Response;

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
                IsEditing = false;
            }
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

                this.IsEditing = false;

                if (intervention.Id < 1 || intervention.Case.Id < 1)
                {
                    if (InterventionSaved != null)
                        InterventionSaved (this.Intervention, e);
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

        protected void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
            if (!isEditing && Cancel != null)
                Cancel (sender, e);
        }

        public bool IsEditing
        {
            get { return this.isEditing; }
            set
            {
                isEditing = value;
                interventionType.IsEditable = value;
                dateSelector.IsEditable = value;
                //interventorSelect.IsEditable = value;
                //supporterSelect.IsEditable = value;

                affectedPeople.IsEditing = value;

                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
            }
        }
    }
}

