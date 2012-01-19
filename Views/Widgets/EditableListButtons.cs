using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EditableListButtons : Gtk.Bin, IEditable
    {
        protected bool isEditable;

        ListableRecord record;

        public EditableListButtons (ListableRecord record)
        {
            this.Build ();
            this.record = record;
            this.isEditable = false;
        }

        public event EventHandler DeletePressed;
        public event EventHandler DetailPressed;

        protected void OnDelete (object sender, System.EventArgs e)
        {
            if (DeletePressed != null)
                DeletePressed (record, e);
        }

        protected void OnDetail (object sender, System.EventArgs e)
        {
            if (DetailPressed != null)
                DetailPressed (record, e);
        }

         public bool IsEditable {
            set {
                this.isEditable = value;
                deleteButton.Visible = value;
            }
            get {
                return this.isEditable;
            }
        }
    }
}

