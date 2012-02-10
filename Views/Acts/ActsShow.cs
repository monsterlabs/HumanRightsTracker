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
                    humanRightViolation.Active = act.HumanRightsViolation;
                    initialDate.setDate (act.start_date, act.StartDateType);
                    finalDate.setDate (act.end_date, act.EndDateType);

                    affected.Text = act.AffectedPeopleNumber.ToString ();
                    placeselector1.SetPlace (act.Country, act.State, act.City);

                    victimlist.Act = act;
                    if (act.Victims == null || act.Victims.Count == 0) {
                        Victim victim = new Victim ();
                        victim.Act = act;
                        victim.Perpetrators = new ArrayList ();
                        victim.Act.Victims = victim.Act.Victims ?? new ArrayList ();
                        victimshow.Victim = victim;
                    }
                }
                IsEditable = false;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = act.Id < 1 ? true : false;
            act.HumanRightsViolationCategory = humanrightsviolationcategory.Active as HumanRightsViolationCategory;
            act.HumanRightsViolation = humanRightViolation.Active as HumanRightsViolation;
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
                    victimshow.ReadOnlyMode(false);
                } else {
                    editButton1.Label = Catalog.GetString("Edit");
                    saveButton1.Visible = false;
                    addVictimButton.Visible = false;
                    victimshow.ReadOnlyMode(true);
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
                victimshow.Victim = v;
                victimshow.IsEditable = IsEditable;
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

            victimshow.Victim = v;
            victimshow.Show ();
            victimshow.IsEditable = true;
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
            humanRightViolation.FilterByCategoryId (category.Id);
        }
    }
}

