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

                    if (act.Id < 1) {
                        victims_frame.Hide ();
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
            act.Country = placeselector1.Country;
            act.State = placeselector1.State;
            act.City = placeselector1.City;
            int affectedPeopleNumber;
            bool isNum = int.TryParse(affected.Text, out affectedPeopleNumber);
            if (isNum) {
                act.AffectedPeopleNumber = Convert.ToInt32(affectedPeopleNumber);
            }

            if (act.IsValid())
            {
                act.SaveAndFlush ();

                initialDate.setDate (act.start_date, act.StartDateType);
                finalDate.setDate (act.end_date, act.EndDateType);
                affected.Text = act.AffectedPeopleNumber.ToString ();

                if (newRow) {
                    act.Case.Acts.Add (Act);
                    act.Case.SaveAndFlush ();
                }

                if (Saved != null) {
                    Saved (this.Act, e);
                }

                this.IsEditable = false;
                victims_frame.Show ();
            } else {
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
                } else {
                    editButton1.Label = Catalog.GetString("Edit");
                    saveButton1.Visible = false;
                }
            }
        }

        public void HideActionButtons () {
            editButton1.Visible = false;
            saveButton1.Visible = false;
            button74.Visible = false;
            victims_frame.Hide ();
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

        protected void OnVictimsClicked (object sender, System.EventArgs e)
        {
            new VictimsWindow (act, (Gtk.Window) this.Toplevel);
        }
    }
}

