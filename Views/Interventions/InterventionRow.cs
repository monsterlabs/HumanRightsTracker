using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InterventionRow : Gtk.Bin
    {
        Intervention intervention;
        bool isEditable;
        public new event EventHandler Removed;

        public InterventionRow ()
        {
            this.Build ();
        }

        public InterventionRow (Intervention intervention, EventHandler removed)
        {
            this.Build ();
            Intervention = intervention;
            this.Removed = removed;
        }

        public Intervention Intervention
        {
            get { return intervention; }
            set
            {
                intervention = value;
                interventor.Text = value.Interventor.Fullname;
                supporter.Text = value.Supporter.Fullname;
                if (value.Date.HasValue)
                    date.Text = value.Date.Value.ToShortDateString ();
            }
        }

        protected void OnDelete (object sender, System.EventArgs e)
        {
            if (Removed != null)
                Removed (this, e);
        }

        protected void OnDetail (object sender, System.EventArgs e)
        {
            new InterventionDetailWindow (this.Intervention, OnDetailReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnDetailReturned (object sender, System.EventArgs e)
        {
            this.Intervention = sender as Intervention;
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                hbuttonbox3.Visible = value;
            }
        }
    }
}

