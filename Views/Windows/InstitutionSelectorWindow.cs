using System;
using HumanRightsTracker.Models;


namespace Views
{
    public class InstitutionEventArgs : EventArgs
    {
        private Institution institution;

        public Institution Institution {
            get { return institution; }
        }

        public InstitutionEventArgs (Institution institution)
        {
            this.institution = institution;
        }
    }

    public partial class InstitutionSelectorWindow : Gtk.Window
    {

        public delegate void InstitutionEventHandler (object sender, InstitutionEventArgs args);

        public event InstitutionEventHandler OnSelect = null;

        public InstitutionSelectorWindow (InstitutionEventHandler handler) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            OnSelect = handler;
        }

        protected void OnSelection (object sender, System.EventArgs e)
        {
            Institution i = sender as Institution;
            OnSelect (this, new InstitutionEventArgs(i));
            this.Modal = false;
            this.TransientFor = null;
            this.Hide ();
        }

        protected void OnAdd (object sender, System.EventArgs e)
        {
            new InstitutionCreateWindow (OnInstitutionCreated, (Gtk.Window)this.Toplevel);
        }

        protected void OnInstitutionCreated (object sender, EventArgs args)
        {
            // Fix it: Reload only one list
            Institution i = sender as Institution;
            OnSelect (this, new InstitutionEventArgs(i));
            this.Modal = false;
            this.TransientFor = null;
            this.Hide ();
        }

    }
}

