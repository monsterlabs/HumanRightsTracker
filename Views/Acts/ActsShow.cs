using System;
using System.Reflection;
using Mono.Unix;
using HumanRightsTracker.Models;
using System.Collections.Generic;
using System.Collections;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ActsShow : Gtk.Bin
    {
        bool isEditable;
        Act act;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public ActsShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
            this.isEditable = false;
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
                IsEditable = false;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = false;
            if (act.Id < 1) {
                newRow = true;
            }

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
                List<Victim> victims = new List<Victim>();
                foreach (Victim victim in victimlist.Victims)
                {
                    victims.Add(victim);
                }
                act.Victims = victims;
                act.SaveAndFlush ();

                if (newRow) {
                    act.Case.Acts.Add (Act);
                    act.Case.SaveAndFlush ();
                }

                if (Saved != null) {
                    Saved (this.Act, e);
                }

                this.IsEditable = false;
            } else
            {
                Console.WriteLine( String.Join(",", act.ValidationErrorMessages) );
                new ValidationErrorsDialog (act.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }

        protected void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!IsEditable && Canceled != null)
                Canceled (sender, e);
        }

        public bool IsEditable
        {
            get { return this.isEditable; }
            set
            {
                isEditable = value;
                this.editable_helper.SetAllEditable(value);

                if (value) {
                    editButton1.Label = Catalog.GetString("Cancel");
                    saveButton1.Visible = true;
                    addVictimButton.Visible = true;
                    victimshow1.ReadOnlyMode(false);
                } else {
                    editButton1.Label = Catalog.GetString("Edit");
                    saveButton1.Visible = false;
                    addVictimButton.Visible = false;
                    victimshow1.ReadOnlyMode(true);
                }
            }
        }

        public void HideActionButtons () {
            editButton1.Visible = false;
            saveButton1.Visible = false;
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
            victimshow1.Show ();
            victimshow1.IsEditable = true;
            return;
        }

        protected void OnHumanrightsviolationcategoryCategorySelected (object sender, System.EventArgs e)
        {
            HumanRightsViolationCategory category = sender as HumanRightsViolationCategory;
            Console.WriteLine(category.Name);
            Assembly asm = Assembly.Load ("Models");
            Type t = asm.GetType ("HumanRightsTracker.Models.HumanRightsViolation");
            Array array = ActiveRecordMetaBase.Where(t, new ICriterion[] { Restrictions.Eq ("CategoryId", category.Id) }, new Order("Id", true));
            foreach (Object o in array) {
                HumanRightsViolation h = o as HumanRightsViolation;
                Console.WriteLine("Name: " + h.Name);
            }
            humanRightsViolation.FilterBy (new ICriterion[] { Restrictions.Eq ("CategoryId", category.Id) }, category.Id);
        }
    }
}

