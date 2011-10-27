using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class VictimSelector : Gtk.Bin
    {
        HashSet<Victim> victims = new HashSet<Victim>(new ARComparer<Victim>());
        bool isEditing;

        Act act;

        public VictimSelector ()
        {
            this.Build ();
            row.Destroy ();
        }


        public Act Act
        {
            get {return act;}
            set {act = value;}
        }

        public bool IsEditing {
            get {
                return this.isEditing;
            }
            set {
                isEditing = value;
                addButton.Visible = value;
                foreach (Gtk.Widget row in peopleList.AllChildren) {
                    ((VictimRow) row).IsEditable = value;
                }
            }
        }
        public HashSet<Victim> Victims {
            get {
                return this.victims;
            }
            set {
                victims = value;
                foreach (Gtk.Widget victimRow in peopleList.Children)
                {
                    victimRow.Destroy();
                }
                foreach (Victim victim in victims)
                {
                    peopleList.PackStart (new VictimRow(victim, OnRemoved));
                }
                peopleList.ShowAll ();
            }
        }
        protected void OnVictimSelected (object sender, VictimEventArgs args)
        {
            if (victims.Add (args.Victim))
            {
                peopleList.PackStart (new VictimRow(args.Victim, OnRemoved));
                peopleList.ShowAll ();
            }

            return;
        }

        protected void OnAddClicked (object sender, System.EventArgs e)
        {
            new VictimWindow (act, OnVictimSelected, (Gtk.Window)this.Toplevel);
        }

        protected void OnRemoved (object sender, System.EventArgs e)
        {
            VictimRow row = sender as VictimRow;
            victims.Remove (row.Victim);
            peopleList.Remove (row);

            return;
        }
    }
}

