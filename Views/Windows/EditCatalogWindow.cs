using System;
using System.Reflection;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Internal;

namespace Views
{
    public partial class EditCatalogWindow : Gtk.Window
    {
        public event EventHandler OnRecordSaved = null;
        protected ActiveRecordBase record;
        protected string model;
        protected ActiveRecordModel mod;

        public EditCatalogWindow (string model, object record,
            ActiveRecordModel mod, EventHandler OnSaveButtonClicked, Gtk.Window parent) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.model = model;
            this.mod = mod;
            this.record = (ActiveRecordBase)record;
            this.OnRecordSaved = OnSaveButtonClicked;
            this.TransientFor = parent;
            modelLabel.Text = model;
            if (!mod.PropertyDictionary.Keys.Contains("Notes"))
                editRecord.HideNotesEntry ();

            if (mod.PropertyDictionary.Keys.Contains("ParentId")) {
                PropertyInfo parentNameProp =  mod.PropertyDictionary["ParentName"].Property;
                editRecord.ParentValue = parentNameProp.GetValue(record, null) as String;
                PropertyInfo parentModelProp =  mod.PropertyDictionary["ParentModel"].Property;
                editRecord.ParentName = parentModelProp.GetValue(record, null) as String;


            } else {
                editRecord.HideParentEntry ();
            }
        }

        protected void OnSaveButtonClicked (object sender, System.EventArgs e)
        {
            if (mod.PropertyDictionary.Keys.Contains("Name")) {
                PropertyInfo nameProp =  mod.PropertyDictionary["Name"].Property;
                nameProp.SetValue (record, editRecord.NameEntry, null);
            }

            if (mod.PropertyDictionary.Keys.Contains("Notes")) {
                PropertyInfo notesProp =  mod.PropertyDictionary["Notes"].Property;
                notesProp.SetValue (record, editRecord.NotesEntry, null);
            }

            record.Save ();
            this.Destroy ();
        }

        protected void OnCancelButtonClicked (object sender, System.EventArgs e)
        {
            this.Destroy ();
        }
    }
}

