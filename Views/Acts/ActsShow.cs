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
        private EditableHelper editable_helper;

        public ActsShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
            this.IsEditing = false;
        }

        public Act Act {
            get { return this.act; }
            set {
                act = value;
                if (act != null) {

                    humanrightsviolationcategory.Active = act.HumanRightsViolationCategory;
                    humanRightsViolation.Active = act.HumanRightsViolation;
                    initialDate.setDate (act.start_date);
                    initialDate.setDateType (act.StartDateType);
                    finalDate.setDate (act.end_date);
                    finalDate.setDateType (act.EndDateType);

                    affected.Text = act.AffectedPeopleNumber.ToString ();
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

                    victimlist.Act = act;
                }
                IsEditing = false;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            act.HumanRightsViolationCategory = humanrightsviolationcategory.Active as HumanRightsViolationCategory;
            act.HumanRightsViolation = humanRightsViolation.Active as HumanRightsViolation;
            act.end_date = finalDate.SelectedDate ();
            act.EndDateType = finalDate.SelectedDateType ();
            act.start_date = initialDate.SelectedDate ();
            act.StartDateType = initialDate.SelectedDateType ();

            act.AffectedPeopleNumber = Convert.ToInt32(affected.Text);
            act.Country = placeselector1.Country;
            act.State = placeselector1.State;
            act.City = placeselector1.City;

            if (act.IsValid())
            {
                act.Save ();
                List<Victim> victims = new List<Victim>();

                foreach (Victim victim in victimlist.Victims)
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
                new ValidationErrorsDialog (act.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
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
                this.editable_helper.SetAllEditable(value);
                if (value) {
                    editButton1.Label = Catalog.GetString("Cancel");
                    saveButton1.Visible = true;
                } else {
                    editButton1.Label = Catalog.GetString("Edit");
                    saveButton1.Visible = false;
                }
            }
        }

        protected void OnVictimSelected (object sender, System.EventArgs e)
        {
            Victim v = sender as Victim;
            if (v != null) {
                victimshow1.Victim = v;
            }
        }

        protected void OnVictimSaved (object sender, System.EventArgs e)
        {
            victimlist.ReloadStore ();
        }

        protected void OnAddVictim (object sender, System.EventArgs e)
        {
            Victim v = new Victim ();
            v.Act = act;
            victimlist.UnselectAll ();

            victimshow1.Victim = v;
            victimshow1.IsEditing = true;
            victimshow1.Show ();
            return;
        }
    }
}

