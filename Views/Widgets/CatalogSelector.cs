using System;
using System.Reflection;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Internal;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CatalogSelector : Gtk.Bin, IEditable
    {
        String model;
        ActiveRecordModel mod;
        Array collection;
        Type t;
        bool isEditable;
        bool matched;
        bool hideAddButton;
        bool hideNoteButton;
        bool orderById;
        int parent_id;
        string attribute;

        public event EventHandler Changed;

        public CatalogSelector ()
        {
            this.Build ();
            this.hideAddButton = false;
            this.hideNoteButton = true;
            this.parent_id = 0;
            combobox.Entry.Completion = new Gtk.EntryCompletion();
            combobox.Entry.Completion.Model = combobox.Model;
            combobox.Entry.Completion.TextColumn = 0;
            combobox.Entry.Completion.InlineCompletion = false;
            combobox.Entry.Completion.MatchSelected += OnMatchSelected;
            combobox.Entry.FocusOutEvent += OnFocusOutEvent;
        }

        public Gtk.ComboBoxEntry Combobox {
            get {
                return this.combobox;
            }
        }

        public bool OrderById {
            get {return orderById;}
            set {orderById = value;}
        }

        public string Attribute {
            get { return attribute; }
            set { attribute = value; }
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

            PropertyInfo nameProp =  mod.PropertyDictionary[AttributeName()].Property;
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

        private void OnFocusOutEvent (object sender, Gtk.FocusOutEventArgs args)
        {
             PropertyInfo nameProp =  mod.PropertyDictionary[AttributeName()].Property;
             foreach (Object o in collection)
             {
                String oName = nameProp.GetValue(o, null) as String;
                if (oName == combobox.Entry.Text)
                {
                    matched = true;
                    break;
                } else {
                    matched = false;
                }
            }

            if (matched != true)  {
                combobox.Entry.Text = "";
                combobox.Active = -1;
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
            if (mod.PropertyDictionary.ContainsKey("Notes"))
            {
                string note = NoteString ();

                if ((note != null) && (note.Trim().Length >0)) {
                    combobox.TooltipText = note;
                    HideNoteButton = false;
                } else {
                    HideNoteButton = true;
                }
            }
            if (Changed != null)
                Changed (this, e);
        }

        protected String NoteString () {
            PropertyInfo notesProp = mod.PropertyDictionary["Notes"].Property;
            string note = notesProp.GetValue(collection.GetValue(combobox.Active), null) as String;
            return note;
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
                    combobox.Entry.Text = "";
                    combobox.Active = -1;
                    return;
                }

                MethodInfo nameMethod = t.GetMethod ("get_"+AttributeName());
                String name = nameMethod.Invoke (value, null) as String;
                int i = 0;
                if (collection == null)
                {
                    Populate();
                }
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
                noteButton.Visible = (this.hideNoteButton && isEditable);
            }
        }

        public void FilterBy (ICriterion[] criteria, int parent_id)
        {
            if (t != null)
            {
                Array options = ActiveRecordMetaBase.Where(t, criteria, new Order(AttributeName(), true));
                DeleteAndSetOptions (options);
                combobox.Sensitive = (collection.Length > 0);
            }

            if (parent_id != 0)
                this.parent_id = parent_id;
        }

        private void DeleteAndSetOptions (Array options)
        {
            collection = options;
            ((Gtk.ListStore)combobox.Model).Clear ();
            PropertyInfo nameProp =  mod.PropertyDictionary[AttributeName()].Property;
            foreach (Object o in collection) {
                String name = nameProp.GetValue(o, null) as String;
                combobox.AppendText (name);
            }
            combobox.Entry.Text = "";
        }

        public bool HideAddButton {
            get {
                return this.hideAddButton;
            }
            set {
                this.hideAddButton = value;
                addButton.Visible = !value;
            }
        }

       public bool HideNoteButton {
            get {
                return this.hideNoteButton;
            }
            set {
                this.hideNoteButton = value;
                noteButton.Visible = !value;
            }
        }
        protected void OnAddButtonClicked (object sender, System.EventArgs e)
        {
            object record = Activator.CreateInstance(t);

            if (this.parent_id != 0) {
                PropertyInfo parentIdProp = record.GetType().GetProperty("ParentId");
                parentIdProp.SetValue (record, this.parent_id, null);
            }

            new EditCatalogWindow(model, record, mod, OnNewRecordReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnNewRecordReturned (object record, EventArgs args)
        {
           if (record != null) {
                if (this.parent_id == 0 ) {
                    Populate();
                } else {
                    MethodInfo modelMethod = record.GetType().GetMethod("ParentModel");
                    String parentName = modelMethod.Invoke (record, null) as String;
                    FilterBy (new ICriterion[] { Restrictions.Eq ((parentName+"Id"), parent_id) }, parent_id);
                }
                Active = record;
           }
           return;
        }

        protected void OnFocusChildSet (object o, Gtk.FocusChildSetArgs args)
        {
            if (collection == null)
            {
                Populate();
            }
        }

        protected void OnEditingDone (object sender, System.EventArgs e)
        {
            Console.WriteLine("EditingDone");
        }

        private void Populate()
        {
            Array options;
            if (OrderById) {
                options = ActiveRecordMetaBase.All(t, new Order("Id", true));
            } else {
                options = ActiveRecordMetaBase.All(t, new Order(AttributeName (), true));
            }
            DeleteAndSetOptions (options);
        }

        private string AttributeName () {
            return (attribute == null ? "Name" : attribute);
        }

        protected void OnNoteButtonClicked (object sender, System.EventArgs e)
        {
            int x, y;
            this.TranslateCoordinates(this.Toplevel, 0, 0, out x, out y);
            ShowNoteWindow note_window = new ShowNoteWindow(x, y, NoteString(), (Gtk.Window)this.Toplevel);
            //note_window.TransientFor = (Gtk.Window)this.Toplevel;
            //note_window.Modal = true;
        }
    }
}