using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;
using System.Collections;
using Mono.Unix;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PerpetratorShow : Gtk.Bin
    {
        Perpetrator perpetrator;
        bool isEditable;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public PerpetratorShow ()
        {
            this.Build ();
        }

        public Perpetrator Perpetrator
        {
            get {return perpetrator;}
            set {
                perpetrator = value;
                perpetratorSelector.Person = perpetrator.Person;
                institution.Institution = perpetrator.Institution;
                job.Active = perpetrator.Job;

                HashSet<PerpetratorAct> perpetratorActs = new HashSet<PerpetratorAct>(new ARComparer<PerpetratorAct>());
                IList acts = perpetrator.PerpetratorActs;
                if (acts != null) {
                    foreach (PerpetratorAct perpetratorAct in acts)
                    {
                        perpetratorActs.Add(perpetratorAct);
                    }
                }
                //perpetratoractsselector.PerpetratorActs = perpetratorActs;
                perpetratoractlist.Perpetrator = perpetrator;
                IsEditable = false;
            }
        }

        public bool IsEditable
        {
            get { return this.isEditable; }
            set
            {
                isEditable = value;
                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
                institution.IsEditable = value;
                job.IsEditable = value;
                perpetratorSelector.IsEditable = value;
                //perpetratoractsselector2.IsEditing = value;
            }
        }

        protected virtual void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!isEditable && Canceled != null) {
                Canceled (sender, e);
            }
        }

        protected virtual void OnSave (object sender, System.EventArgs e)
        {
            Console.WriteLine("On PerpetratorShow Save.");
            bool newRow = false;
            if (perpetrator.Id < 1) {
                newRow = true;
            }

            perpetrator.Person = perpetratorSelector.Person;
            perpetrator.Institution = institution.Institution;
            perpetrator.Job = job.Active as Job;

            if (perpetrator.IsValid()) {
                perpetrator.SaveAndFlush ();

                if (newRow) {
                    perpetrator.Victim.Perpetrators.Add (Perpetrator);
                    perpetrator.Victim.SaveAndFlush ();
                }
                this.IsEditable = false;

                if (Saved != null)
                        Saved (this.Perpetrator, e);
            } else {
                Console.WriteLine( String.Join(",", perpetrator.ValidationErrorMessages) );
                new ValidationErrorsDialog (perpetrator.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
//            perpetrator.Person = perpetratorSelector.Person;
//            perpetrator.Job = job.Active as Job;
//
//            if (perpetrator.Id < 1 || perpetrator.Victim.Id < 1)
//            {
//                return;
//            } else {
//                perpetrator.Save();
//                if (Saved != null)
//                    Saved (this.Perpetrator, e);
//            }
        }

        protected void OnPerpetratorActSelected (object sender, System.EventArgs e)
        {
            PerpetratorAct a = sender as PerpetratorAct;
            if (a != null) {
                perpetratoractshow.PerpetratorAct = a;
            }
        }

        protected void OnPerpetratorActSaved (object sender, System.EventArgs e)
        {
            perpetratoractlist.ReloadStore ();
        }

        protected void OnAddPerpetratorAct (object sender, System.EventArgs e)
        {
            PerpetratorAct a = new PerpetratorAct ();
            a.Perpetrator = perpetrator;
            perpetratoractlist.UnselectAll ();

            perpetratoractshow.PerpetratorAct = a;
            perpetratoractshow.IsEditing = true;
            perpetratoractshow.Show ();
            return;
        }
    }
}

