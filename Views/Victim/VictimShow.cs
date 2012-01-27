using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;
using System.Collections;
using Mono.Unix;
using System.Linq;

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
        private EditableHelper editable_helper;

        public VictimShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
            this.IsEditing = false;
            this.ConnectPerpetratorHandlers ();
        }

        public Victim Victim
        {
            get {return victim;}
            set {
                victim = value;
                victimSelector.Person = victim.Person;
                status1.Active = victim.VictimStatus;
                characteristics.Text = victim.Characteristics;

                HashSet<Perpetrator> perpetrators = new HashSet<Perpetrator>(new ARComparer<Perpetrator>());
                IList victimPerpetrators = victim.Perpetrators;
                if (victimPerpetrators != null) {
                    foreach (Perpetrator perpetrator in victimPerpetrators)
                    {
                        perpetrators.Add(perpetrator);
                    }
                }

                perpetratorslist.Records = victimPerpetrators.Cast<ListableRecord>().ToList ();

                IsEditing = false;
            }
        }

        private void ReloadPerpetrators () {
            List<ListableRecord> perpetrators = this.Victim.Perpetrators.Cast<ListableRecord>().ToList ();
            perpetratorslist.Records = perpetrators;
        }

        private void ConnectPerpetratorHandlers() {
            perpetratorslist.NewButtonPressed += (sender, e) => {
                new PerpetratorDetailWindow (this.Victim, (obj, args) => {
                    this.ReloadPerpetrators ();
                }, (Gtk.Window) this.Toplevel);
            };
            perpetratorslist.DeleteButtonPressed += (sender, e) => {
                Perpetrator p = sender as Perpetrator;
                this.Victim.Perpetrators.Remove (p);
                if (p.Id >= 1) {
                    p.DeleteAndFlush ();
                }
                this.ReloadPerpetrators ();
            };
            perpetratorslist.DetailButtonPressed  += (sender, e) => {
                Perpetrator p = sender as Perpetrator;
                new PerpetratorDetailWindow(p, (obj, args) => {
                    this.ReloadPerpetrators ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

        protected virtual void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditing= !IsEditing;
        }


        public bool IsEditing
        {
            get { return this.isEditing; }
            set
            {
                isEditing = value;
                this.editable_helper.SetAllEditable(value);
                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
            }

        }

        protected virtual void OnSave (object sender, System.EventArgs e)
        {
            victim.Person = victimSelector.Person;
            victim.VictimStatus = status1.Active as VictimStatus;
            victim.Characteristics = characteristics.Text;

            if (victim.Act.Id < 1)
            {
                if (Unsaved != null)
                    Unsaved (this.Victim, e);
                return;
            } else {
                victim.Save();
                Victim = victim;
                if (Saved != null)
                    Saved (this.Victim, e);
            }
        }
    }
}

