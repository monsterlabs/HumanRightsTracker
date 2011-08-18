using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
namespace Views
{
    public partial class EditCatalogWindow : Gtk.Window
    {
        public event EventHandler OnRecordSaved = null;
        protected ActiveRecordBase record;
        protected string model;

        public EditCatalogWindow (string model, ActiveRecordValidationBase record, EventHandler onSave, Gtk.Window parent) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.model = model;
            this.record = record as ActiveRecordValidationBase;
            this.OnRecordSaved = OnSaveButtonClicked;
            this.TransientFor = parent;
            modelLabel.Text = model;
        }

        protected void OnSaveButtonClicked (object sender, System.EventArgs e)
        {
            // record.Save ();
            this.Destroy ();
        }

        protected void OnCancelButtonClicked (object sender, System.EventArgs e)
        {
            this.Destroy ();
        }
    }
}

