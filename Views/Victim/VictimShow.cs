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
        private EditableHelper editable_helper;

        public VictimShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
            this.IsEditable = false;
            this.ConnectPerpetratorHandlers ();
        }

        public Victim Victim
        {
            get { return victim; }
            set {
                victim = value;
                victimSelector.Person = victim.Person;
                status.Active = victim.VictimStatus;
                characteristics.Text = victim.Characteristics ?? "";

                if (victim.Perpetrators != null) {
                    perpetratorslist.Records = victim.Perpetrators.Cast<ListableRecord>().ToList ();
                } else {
                    victim.Perpetrators = new ArrayList ();
                    perpetratorslist.Records = new List<ListableRecord>();
                }

                IsEditable = false;
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
            IsEditable= !IsEditable;
        }


        public bool IsEditable
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

        public void ReadOnlyMode(bool mode)
        {
            if (mode) {
                IsEditable = false;
                editButton.Visible = false;
            } else {
                IsEditable = true;
                editButton.Visible = true;
            }
        }

        protected virtual void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = victim.Id < 1 ? true : false;
            victim.Person = victimSelector.Person;
            victim.VictimStatus = status.Active as VictimStatus;
            victim.Characteristics = characteristics.Text;

            if (victim.IsValid ()) {
                if (newRow) {
                    victim.Act.Victims.Add (victim);
                }

                if (victim.Act.Id > 0) {
                    victim.Act.SaveAndFlush ();
                }

                this.IsEditable = false;
                if (Saved != null)
                    Saved (this.Victim, e);
            } else {
                Console.WriteLine( String.Join(",", victim.ValidationErrorMessages) );
                new ValidationErrorsDialog (victim.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }
    }
}

