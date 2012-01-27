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
        private EditableHelper editable_helper;

        public PerpetratorSelector ()
        {
            this.Build ();
            row.Destroy ();
            this.editable_helper = new EditableHelper(this);
            this.IsEditing = false;
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
                
                this.editable_helper.SetAllEditable(value);
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
        protected void OnPerpetratorSelected (object sender, EventArgs args)
        {
//            if (perpetrators.Add (args.Perpetrator))
//            {
//                peopleList.PackStart (new PerpetratorRow(args.Perpetrator, OnRemoved));
//
//                peopleList.ShowAll ();
//
//            }

            return;
        }

        protected void OnAddClicked (object sender, System.EventArgs e)
        {
            new PerpetratorDetailWindow (victim, OnPerpetratorSelected, (Gtk.Window)this.Toplevel);
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

