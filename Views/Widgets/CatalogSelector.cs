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

        public event EventHandler Changed;

        public CatalogSelector ()
        {
            this.Build ();
            this.hideAddButton = false;
        }

        public String Model {
            get { return this.model; }
            set {
                model = value;
                Assembly asm = Assembly.Load ("Models");
                t = asm.GetType ("HumanRightsTracker.Models." + model);
                mod = ActiveRecordModel.GetModel(t);
                Array options = ActiveRecordMetaBase.All(t, new Order("Name", true));
                DeleteAndSetOptions (options);
            }
        }

        protected virtual void onChanged (object sender, System.EventArgs e)
        {
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

        public void FilterBy (ICriterion[] criteria)
        {
            if (t != null)
            {
                Array options = ActiveRecordMetaBase.Where(t, criteria, new Order("Name", true));
                DeleteAndSetOptions (options);
            }
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
            new EditCatalogWindow(model, record, mod, OnNewRecordReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnNewRecordReturned (object sender, EventArgs args)
        {
           // object record = sender;
           return;
        }

    }
}

