using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ActRow : Gtk.Bin
    {
        Act act;
        bool isEditable;
        public new event EventHandler Removed;

        public ActRow ()
        {
            this.Build ();
        }

        public ActRow (Act act, EventHandler removed)
        {
            this.Build ();
            Act = act;
            this.Removed = removed;
        }

        public Act Act
        {
            get { return act; }
            set
            {
                act = value;
                affected_right.Text = value.HumanRightsViolationCategory.Name;
                act_name.Text = value.HumanRightsViolation.Name;
                if (value.start_date.HasValue)
                    startDate.Text = value.start_date.Value.ToShortDateString ();
                if (value.end_date.HasValue)
                    endDate.Text = value.end_date.Value.ToShortDateString ();
            }
        }

        protected void OnDelete (object sender, System.EventArgs e)
        {
            if (Removed != null)
                Removed (this, e);
        }

        protected void OnDetail (object sender, System.EventArgs e)
        {
            new ActDetailWindow (this.Act, OnDetailReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnDetailReturned (object sender, System.EventArgs e)
        {
            this.Act = sender as Act;
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                removeButton.Visible = value;
            }
        }
    }
}

