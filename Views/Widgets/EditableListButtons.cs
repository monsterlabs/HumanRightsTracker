using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EditableListButtons : Gtk.Bin
    {

        int recordId;

        public EditableListButtons (int recordId)
        {
            this.Build ();
            this.recordId = recordId;
        }

        public event EventHandler DeletePressed;
        public event EventHandler DetailPressed;

        protected void OnDelete (object sender, System.EventArgs e)
        {
            if (DeletePressed != null)
                DeletePressed (recordId, e);
        }

        protected void OnDetail (object sender, System.EventArgs e)
        {
            if (DetailPressed != null)
                DetailPressed (recordId, e);
        }
    }
}

