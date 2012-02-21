using System;

namespace Views
{
    public partial class InstitutionCreateWindow : Gtk.Window
    {
        public event EventHandler OnRecordSaved = null;

        public InstitutionCreateWindow (EventHandler OnSaveButtonClicked, Gtk.Window parent) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnRecordSaved = OnSaveButtonClicked;
            this.TransientFor = parent;
            HumanRightsTracker.Models.Institution institution = new HumanRightsTracker.Models.Institution ();
            institutionshow.Institution = institution;
            institutionshow.IsEditing = true;
        }


        protected void OnSave (object sender, System.EventArgs e)
        {
            if (OnRecordSaved != null)
            {
                OnRecordSaved (sender, null);
                this.Destroy ();
            }
        }
    }
}

