using System;

namespace Views
{
    public partial class PersonCreateWindow : Gtk.Window
    {
        public event EventHandler OnRecordSaved = null;

        public PersonCreateWindow (EventHandler OnSaveButtonClicked, bool isImmigrant, Gtk.Window parent) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnRecordSaved = OnSaveButtonClicked;
            this.TransientFor = parent;
            HumanRightsTracker.Models.Person person = new HumanRightsTracker.Models.Person ();
            person.IsImmigrant = isImmigrant;
            show1.Person = person;
            show1.isImmigrant = isImmigrant;
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

