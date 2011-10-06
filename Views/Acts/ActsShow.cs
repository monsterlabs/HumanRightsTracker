using System;
using Mono.Unix;
using HumanRightsTracker.Models;
using System.Collections.Generic;
using System.Collections;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ActsShow : Gtk.Bin
    {
        bool isEditing;
        Act act;

        public event EventHandler ActSaved;
        public event EventHandler Cancel;

        public ActsShow ()
        {
            this.Build ();
        }

        public Act Act {
            get { return this.act; }
            set {
                act = value;
                if (act != null) {
                    humanRightsViolation.Active = act.HumanRightsViolation;
                    initialDate.setDate (act.start_date);
                    initialDate.setDateType (act.StartDateType);
                    finalDate.setDate (act.end_date);
                    finalDate.setDateType (act.EndDateType);

                    // person-acts
                    HashSet<Person> victims = new HashSet<Person>(new ARComparer<Person>());
                    HashSet<Person> perpetrators = new HashSet<Person>(new ARComparer<Person>());

                    IList personActs = act.Victims;
                    if (personActs != null) {

                    }
                    VictimSelector.People = victims;
                    perpetratorsSelector.People = perpetrators;
                }
                IsEditing = false;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            act.HumanRightsViolation = humanRightsViolation.Active as HumanRightsViolation;
            act.end_date = finalDate.SelectedDate ();
            act.EndDateType = finalDate.SelectedDateType ();
            act.start_date = initialDate.SelectedDate ();
            act.StartDateType = initialDate.SelectedDateType ();

            if (act.IsValid())
            {
                //act.Save ();
                // TODO: Save victims and perpetrators
                List<Victim> personActs = new List<Victim>();

                foreach (Person person in VictimSelector.People)
                {
                    Victim personAct = new Victim();
                    personAct.Act = act;
                    personAct.Person = person;
                    personActs.Add(personAct);
                }
                foreach (Person person in perpetratorsSelector.People)
                {
                    Victim personAct = new Victim();
                    personAct.Act = act;
                    personAct.Person = person;
                    personActs.Add(personAct);
                }
                act.Victims = personActs;

                this.IsEditing = false;

                if (act.Id < 1 || act.Case.Id < 1)
                {
                    if (ActSaved != null)
                        ActSaved (this.Act, e);
                    return;
                } else {
                    act.Save();
                }
            } else
            {
                Console.WriteLine( String.Join(",", act.ValidationErrorMessages) );
                new ValidationErrorsDialog (act.PropertiesValidationErrorMessages);
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
                humanRightsViolation.IsEditable = value;
                catalogselector1.IsEditable = value;
                initialDate.IsEditable = value;
                finalDate.IsEditable = value;
                editablelabel1.IsEditable = value;
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

