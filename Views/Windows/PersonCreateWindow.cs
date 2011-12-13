using System;

namespace Views
{
    public partial class PersonCreateWindow : Gtk.Window
    {
        public event EventHandler OnRecordSaved = null;

        public PersonCreateWindow (EventHandler OnSaveButtonClicked, Gtk.Window parent) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnRecordSaved = OnSaveButtonClicked;
            this.TransientFor = parent;
            show1.Person = new HumanRightsTracker.Models.Person ();
            show1.IsEditing = true;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            if (OnRecordSaved != null)
            {
                OnRecordSaved (this, null);
                this.Destroy ();
            }
        }
    }
}

