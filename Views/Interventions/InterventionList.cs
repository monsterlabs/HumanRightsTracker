using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InterventionList : Gtk.Bin
    {
        List<Intervention> interventions;
        Case c;
        bool isEditable;

        public InterventionList ()
        {
            this.Build ();
            row.Destroy ();
            interventions = new List<Intervention>();
        }

        public List<Intervention> Interventions
        {
            get {return interventions;}
        }

        public Case Case
        {
            get { return c; }
            set
            {
                c = value;
                ReloadList ();
            }
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                newButton.Visible = value;
                foreach (Gtk.Widget row in interventionsList.AllChildren) {
                    ((InterventionRow) row).IsEditable = value;
                }
            }
        }

        public void ReloadList ()
        {
            foreach (Gtk.Widget w in interventionsList.AllChildren)
            {
                w.Destroy();
            }
            if (c.Id < 1) {
                return;
            }
            interventions = new List<Intervention> (Intervention.FindAll (new ICriterion[] { Restrictions.Eq("Case", c) }));
            foreach (Intervention i in interventions)
            {
                interventionsList.PackStart (new InterventionRow (i, OnInterventionRowRemoved));
            }
            interventionsList.ShowAll ();
        }

        protected void OnNewIntervention (object sender, System.EventArgs e)
        {
            new InterventionDetailWindow (c, OnNewInterventionReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnNewInterventionReturned (object sender, EventArgs args)
        {
            Intervention a = sender as Intervention;
            interventionsList.PackStart (new InterventionRow (a, OnInterventionRowRemoved));
            interventionsList.ShowAll ();
            interventions.Add (a);
            return;
        }

        protected void OnInterventionRowRemoved (object sender, EventArgs e)
        {
            InterventionRow interventionRow = sender as InterventionRow;
            Intervention i = interventionRow.Intervention;
            interventions.Remove(i);
            interventionsList.Remove(interventionRow);

            if (i.Id >= 1)
            {
                // TODO: Confirmation.
                i.Delete ();
            }

            return;
        }
    }
}

