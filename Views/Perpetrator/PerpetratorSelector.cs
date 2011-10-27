using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;


namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PerpetratorSelector : Gtk.Bin
    {
        HashSet<Perpetrator> perpetrators = new HashSet<Perpetrator>(new ARComparer<Perpetrator>());
        bool isEditing;

        Victim victim;

        public PerpetratorSelector ()
        {
            this.Build ();
            row.Destroy ();
        }


        public Victim Victim
        {
            get {return victim;}
            set {victim = value;}
        }

        public bool IsEditing {
            get {
                return this.isEditing;
            }
            set {
                isEditing = value;
                addButton.Visible = value;
                foreach (Gtk.Widget row in peopleList.AllChildren) {
                    ((PerpetratorRow) row).IsEditable = value;
                }
            }
        }
        public HashSet<Perpetrator> Perpetrators {
            get {
                return this.perpetrators;
            }
            set {
                perpetrators = value;
                foreach (Gtk.Widget perpetratorRow in peopleList.Children)
                {
                    perpetratorRow.Destroy();
                }
                foreach (Perpetrator perpetrator in perpetrators)
                {
                    peopleList.PackStart (new PerpetratorRow(perpetrator, OnRemoved));
                }
                peopleList.ShowAll ();
            }
        }
        protected void OnPerpetratorSelected (object sender, PerpetratorEventArgs args)
        {
            if (perpetrators.Add (args.Perpetrator))
            {
                peopleList.PackStart (new PerpetratorRow(args.Perpetrator, OnRemoved));
                peopleList.ShowAll ();
            }

            return;
        }

        protected void OnAddClicked (object sender, System.EventArgs e)
        {
            new PerpetratorWindow (victim, OnPerpetratorSelected, (Gtk.Window)this.Toplevel);
        }

        protected void OnRemoved (object sender, System.EventArgs e)
        {
            PerpetratorRow row = sender as PerpetratorRow;
            perpetrators.Remove (row.Perpetrator);
            peopleList.Remove (row);

            return;
        }
    }
}

