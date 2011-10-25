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

                    affected.Text = act.AffectedPeopleNumber.ToString ();
                    actStatus.Active = act.ActStatus;
                    victimStatus.Active = act.VictimStatus;
                    placeselector1.SetPlace (act.Country, act.State, act.City);
                    // person-acts
                    HashSet<Victim> victims = new HashSet<Victim>(new ARComparer<Victim>());

                    IList actVictims = act.Victims;
                    if (actVictims != null) {
                        foreach (Victim victim in actVictims)
                        {
                            victims.Add(victim);
                        }
                    }
                    VictimSelector.Victims = victims;
                    VictimSelector.Act = act;
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

            act.AffectedPeopleNumber = Convert.ToInt32(affected.Text);
            act.ActStatus = actStatus.Active as ActStatus;
            act.VictimStatus = victimStatus.Active as VictimStatus;
            act.Country = placeselector1.Country;
            act.State = placeselector1.State;
            act.City = placeselector1.City;

            if (act.IsValid())
            {
                //act.Save ();
                // TODO: Save victims and perpetrators
                List<Victim> victims = new List<Victim>();

                foreach (Victim victim in VictimSelector.Victims)
                {
                    victims.Add(victim);
                }
                act.Victims = victims;

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
                affected.IsEditable = value;
                actStatus.IsEditable = value;
                victimStatus.IsEditable = value;
                VictimSelector.IsEditing = value;

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

