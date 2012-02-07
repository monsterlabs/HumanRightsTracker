using System;
using System.Collections;
using System.Collections.Generic;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class NumberSelector : Gtk.Bin, IEditable
    {
        private int min = 0;
        private int max = 100;
        protected int active_value;
        protected bool isEditable;

        public NumberSelector ()
        {
            this.Build ();
            this.BuildArray ();
            combobox.Entry.Completion = new Gtk.EntryCompletion();
            combobox.Entry.Completion.Model = combobox.Model;
            combobox.Entry.Completion.TextColumn = 0;
            combobox.Entry.Completion.InlineCompletion = false;
            combobox.Entry.Completion.MatchSelected += OnMatchSelected;
            combobox.Entry.FocusOutEvent += OnFocusOutEvent;
        }

        public void BuildArray ()
        {
            Gtk.ListStore listStore = new Gtk.ListStore (typeof (string));
            for (int i = min; i < (max + 1); i++) {
                listStore.AppendValues(i.ToString());
            }
            combobox.Model = listStore;
        }

         public int Min {
            get {
                return this.min;
            }
            set {
                    this.min = value;
            }
         }

         public int Max {
            get {
                return this.max;
            }
            set {
                if (value > this.min) {
                   this.max = value;
                }
            }
         }

        public int Active {
            get { return int.Parse(combobox.Active.ToString()); }
            set {
                this.active_value = value;
                combobox.Active = active_value;
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
            }
        }

        [GLib.ConnectBefore]
        private void OnMatchSelected (object sender, Gtk.MatchSelectedArgs args)
        {
            combobox.Active = int.Parse(args.Model.GetValue(args.Iter, 0).ToString());
        }

        private void OnFocusOutEvent (object sender, Gtk.FocusOutEventArgs args)
        {
            string Str = combobox.Entry.Text.Trim();
            int Num;
            bool isNum = int.TryParse(Str, out Num);
            if ( isNum != true || Num < this.min || Num > this.max) {
                combobox.Entry.Text = "";
                combobox.Active = -1;
            }
        }
    }
}

