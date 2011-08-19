using System;
using System.Reflection;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Internal;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CatalogSelector : Gtk.Bin
    {
        String model;
        ActiveRecordModel mod;
        Array collection;
        Type t;
        bool isEditable;
        bool hideAddButton;

        int parent_id;

        public event EventHandler Changed;

        public CatalogSelector ()
        {
            this.Build ();
            this.hideAddButton = false;
            combobox.Entry.Completion = new Gtk.EntryCompletion();
            combobox.Entry.Completion.Model = combobox.Model;
            combobox.Entry.Completion.TextColumn = 0;
            combobox.Entry.Completion.InlineCompletion = false;
            combobox.Entry.Completion.MatchSelected += OnMatchSelected;
        }

        public String Model {
            get { return this.model; }
            set {
                model = value;
                Assembly asm = Assembly.Load ("Models");
                t = asm.GetType ("HumanRightsTracker.Models." + model);
                mod = ActiveRecordModel.GetModel(t);
            }
        }

        [GLib.ConnectBefore]
        private void OnMatchSelected (object sender, Gtk.MatchSelectedArgs args)
        {
            String name = args.Model.GetValue (args.Iter, 0) as String;
            PropertyInfo nameProp =  mod.PropertyDictionary["Name"].Property;
            int i = 0;
            foreach (Object o in collection)
            {
                String oName = nameProp.GetValue(o, null) as String;
                if (oName == name)
                {
                    combobox.Active = i;
                    break;
                }
                ++i;
            }
        }

        protected virtual void onChanged (object sender, System.EventArgs e)
        {
            if (combobox.Active < 0)
            {
                // TODO: check if entrycompletion model has available options
                //       if not, delete the text.
                //combobox.Entry.Text = "";
                return;
            }
            if (Changed != null)
                Changed (this, e);
        }

        public Object Active {
            get
            {
                if (combobox.Active < 0)
                    return null;
                return collection.GetValue(combobox.Active);
            }
            set
            {
                if (value == null)
                {
                    combobox.Active = -1;
                    return;
                }

                MethodInfo nameMethod = t.GetMethod ("get_Name");
                String name = nameMethod.Invoke (value, null) as String;
                int i = 0;
                foreach (Object o in collection)
                {
                    String oName = nameMethod.Invoke (o, null) as String;
                    if (oName == name)
                    {
                        combobox.Active = i;
                        break;
                    }
                    ++i;
                }


            }
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                combobox.Visible = value;
                text.Visible = !value;
                text.Text = combobox.ActiveText;
                addButton.Visible = (!this.hideAddButton && isEditable);
            }
        }

        public void FilterBy (ICriterion[] criteria, int parent_id)
        {
            if (t != null)
            {
                Array options = ActiveRecordMetaBase.Where(t, criteria, new Order("Name", true));
                DeleteAndSetOptions (options);
            }

            if (parent_id != 0)
                this.parent_id = parent_id;
        }

        private void DeleteAndSetOptions (Array options)
        {
            collection = options;
            ((Gtk.ListStore)combobox.Model).Clear ();
            PropertyInfo nameProp =  mod.PropertyDictionary["Name"].Property;
            foreach (Object o in collection) {
                String name = nameProp.GetValue(o, null) as String;
                combobox.AppendText (name);
            }
        }

        public bool HideAddButton {
            get {
                return this.hideAddButton;
            }
            set {
                this.hideAddButton = value;
            }
        }

        protected void OnAddButtonClicked (object sender, System.EventArgs e)
        {
            object record = Activator.CreateInstance(t);

            if ((mod.PropertyDictionary.Keys.Contains("ParentId")) && (this.parent_id != 0)) {
                PropertyInfo parentIdProp =  mod.PropertyDictionary["ParentId"].Property;
                parentIdProp.SetValue (record, this.parent_id, null);
            }

            new EditCatalogWindow(model, record, mod, OnNewRecordReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnNewRecordReturned (object sender, EventArgs args)
        {
           // object record = sender;
           return;
        }

        protected void OnFocusChildSet (object o, Gtk.FocusChildSetArgs args)
        {
            if (collection == null)
            {
                Array options = ActiveRecordMetaBase.All(t, new Order("Name", true));
                DeleteAndSetOptions (options);
            }
        }

        protected void OnEditingDone (object sender, System.EventArgs e)
        {
            Console.WriteLine("EditingDone");
        }
    }
}

