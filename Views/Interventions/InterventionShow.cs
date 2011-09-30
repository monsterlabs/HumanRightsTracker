using System;
using Mono.Unix;
using HumanRightsTracker.Models;

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
                    supporterSelect.Person = intervention.Supporter;
                    impactView.Buffer.Text = intervention.Impact;
                    responseView.Buffer.Text = intervention.Response;

                    // person-acts
//                    HashSet<Person> victims = new HashSet<Person>(new ARComparer<Person>());
//                    HashSet<Person> perpetrators = new HashSet<Person>(new ARComparer<Person>());
//
//                    IList personActs = intervention.PersonActs;
//                    if (personActs != null) {
//                        foreach (PersonAct personAct in personActs)
//                        {
//                            switch (personAct.Role.Name)
//                            {
//                            case "Perpetrador":
//                                perpetrators.Add(personAct.Person);
//                                break;
//                            case "Víctima":
//                                victims.Add(personAct.Person);
//                                break;
//                            default:
//                                break;
//                            }
//                        }
//                    }
//                    VictimSelector.People = victims;
//                    perpetratorsSelector.People = perpetrators;
                }
                IsEditing = false;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            intervention.InterventionType = interventionType.Active as InterventionType;
            intervention.Date = dateSelector.CurrentDate;
            intervention.Interventor = interventorSelect.Person;
            intervention.Supporter = supporterSelect.Person;

            if (intervention.IsValid())
            {
//                List<PersonAct> personActs = new List<PersonAct>();
//
//                foreach (Person person in VictimSelector.People)
//                {
//                    PersonAct personAct = new PersonAct();
//                    personAct.Act = intervention;
//                    personAct.Person = person;
//                    personAct.Role = Role.Find(1);
//                    personActs.Add(personAct);
//                }
//                foreach (Person person in perpetratorsSelector.People)
//                {
//                    PersonAct personAct = new PersonAct();
//                    personAct.Act = intervention;
//                    personAct.Person = person;
//                    personAct.Role = Role.Find(2);
//                    personActs.Add(personAct);
//                }
//                intervention.PersonActs = personActs;

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
                new ValidationErrorsDialog (intervention.PropertiesValidationErrorMessages);
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
                interventorSelect.IsEditable = value;
                supporterSelect.IsEditable = value;

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

