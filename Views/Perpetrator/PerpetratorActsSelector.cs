using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PerpetratorActsSelector : Gtk.Bin
    {
        HashSet<PerpetratorAct> perpetratorActs = new HashSet<PerpetratorAct>(new ARComparer<PerpetratorAct>());
        bool isEditing;

        Perpetrator perpetrator;

        public PerpetratorActsSelector ()
        {
            this.Build ();
            //row.Destroy ();
        }


        public Perpetrator Perpetrator
        {
            get {return perpetrator;}
            set {perpetrator = value;}
        }

        public bool IsEditing {
            get {
                return this.isEditing;
            }
            set {
                isEditing = value;
                addButton.Visible = value;
                foreach (Gtk.Widget row in peopleList.AllChildren) {
                    ((PerpetratorActRow) row).IsEditable = value;
                }
            }
        }
        public HashSet<PerpetratorAct> PerpetratorActs {
            get {
                return this.perpetratorActs;
            }
            set {
                perpetratorActs = value;
                foreach (Gtk.Widget perpetratorRow in peopleList.Children)
                {
                    perpetratorRow.Destroy();
                }
                foreach (PerpetratorAct perpetratorAct in perpetratorActs)
                {
                    peopleList.PackStart (new PerpetratorActRow(perpetratorAct, OnRemoved));
                }
                peopleList.ShowAll ();
            }
        }
        protected void OnPerpetratorActSelected (object sender, PerpetratorActEventArgs args)
        {
            if (perpetratorActs.Add (args.PerpetratorAct))
            {
                peopleList.PackStart (new PerpetratorActRow(args.PerpetratorAct, OnRemoved));
                peopleList.ShowAll ();
            }

            return;
        }

        protected void OnAddClicked (object sender, System.EventArgs e)
        {
            PerpetratorAct newPerpetratorAct = new PerpetratorAct();
            newPerpetratorAct.Perpetrator = perpetrator;
            new PerpetratorActWindow (newPerpetratorAct, OnPerpetratorActSelected);
        }

        protected void OnRemoved (object sender, System.EventArgs e)
        {
            PerpetratorActRow row = sender as PerpetratorActRow;
            perpetratorActs.Remove (row.PerpetratorAct);
            peopleList.Remove (row);

            return;
        }
    }
}

