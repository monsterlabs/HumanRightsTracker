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
        bool isEditing;

        public event EventHandler Unsaved;
        public event EventHandler Saved;
        public event EventHandler Cancel;

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
                institutionselect1.Institution = perpetrator.Institution;
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
                IsEditing = false;
            }
        }

        protected virtual void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
        }

        public bool IsEditing
        {
            get { return this.isEditing; }
            set
            {
                isEditing = value;
                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
                institutionselect1.IsEditable = value;
                job.IsEditable = value;
                perpetratorSelector.IsEditable = value;
                //perpetratoractsselector2.IsEditing = value;
            }
        }

        protected virtual void OnSave (object sender, System.EventArgs e)
        {
            perpetrator.Person = perpetratorSelector.Person;
            perpetrator.Job = job.Active as Job;

            if (perpetrator.Id < 1 || perpetrator.Victim.Id < 1)
            {
                if (Unsaved != null)
                    Unsaved (this.Perpetrator, e);
                return;
            } else {
                perpetrator.Save();
                if (Saved != null)
                    Saved (this.Perpetrator, e);
            }
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

