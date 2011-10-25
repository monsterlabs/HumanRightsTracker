using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;
using System.Collections;
using Mono.Unix;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class VictimShow : Gtk.Bin
    {
        Victim victim;
        bool isEditing;

        public event EventHandler Unsaved;
        public event EventHandler Saved;
        public event EventHandler Cancel;

        public VictimShow ()
        {
            this.Build ();
        }

        public Victim Victim
        {
            get {return victim;}
            set {
                victim = value;
                victimSelector.Person = victim.Person;
                status1.Active = victim.VictimStatus;
                characteristics.Buffer.Text = victim.Characteristics;

                HashSet<Perpetrator> perpetrators = new HashSet<Perpetrator>(new ARComparer<Perpetrator>());
                IList victimPerpetrators = victim.Perpetrators;
                if (victimPerpetrators != null) {
                    foreach (Perpetrator perpetrator in victimPerpetrators)
                    {
                        perpetrators.Add(perpetrator);
                    }
                }
                PerpetratorSelector.Perpetrators = perpetrators;
                PerpetratorSelector.Victim = victim;


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
                victimSelector.IsEditable = value;
                status1.IsEditable = value;
                characteristics.Editable = value;
            }
        }

        protected virtual void OnSave (object sender, System.EventArgs e)
        {
            victim.Person = victimSelector.Person;
            victim.VictimStatus = status1.Active as VictimStatus;
            victim.Characteristics = characteristics.Buffer.Text;

            if (victim.Id < 1 || victim.Act.Id < 1)
            {
                if (Unsaved != null)
                    Unsaved (this.Victim, e);
                return;
            } else {
                victim.Save();
                if (Saved != null)
                    Saved (this.Victim, e);
            }
        }
    }
}

