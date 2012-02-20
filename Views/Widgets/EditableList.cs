using System;
using Mono.Unix;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using Gtk;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EditableList : Gtk.Bin, IEditable
    {
        string[] headers;
        string[] columnHeaders;

        List<ListableRecord> records;
        List<AffiliableRecord> affiliable_records;
        List<AffiliatedRecord> affiliated_records;

        public event EventHandler NewButtonPressed;
        public event EventHandler DeleteButtonPressed;
        public event EventHandler DetailButtonPressed;

        private EditableHelper editable_helper;
        private bool isEditable;

        public EditableList ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(table);
            this.IsEditable = false;
        }

        public string[] Headers {
            get {
                return this.headers;
            }
            set {
                headers = value;
            }
        }

        public void AddActionColumnToHeaders (){
            Array.Resize(ref this.columnHeaders, this.columnHeaders.Length + 1);
            this.columnHeaders[this.columnHeaders.Length -1] = Catalog.GetString("Action(s)");
        }

        public List<ListableRecord> Records {
            get {
                return this.records;
            }
            set {
                records = value;

                this.DestroyTableChildren ();
                this.columnHeaders = (string[])headers.Clone ();
                this.AddActionColumnToHeaders();
                this.BuildTableHeaders ();

                table.Resize ((uint) (records.Count + 1), (uint) (columnHeaders.Length));
                for (uint i = 0; i < records.Count; i++) {
                    string[] data = records[(int) i].ColumnData ();
                    uint j = 0;
                    for (; j < (columnHeaders.Length -1); j++) {
                        Label l = new Label (data[j]);
                        //l.MaxWidthChars = 20;
                        l.LineWrap = true;
                        l.Wrap = true;
                        l.Justify = Justification.Fill;
                        l.SingleLineMode = false;

                        Gtk.Frame f = new Gtk.Frame ();
                        f.Add(l);
                        f.ShadowType = Gtk.ShadowType.In;

                        table.Attach (f, j, j+1, i+1,i+2);
                        table.SetColSpacing(j, 0);
                    }
                    EditableListButtons buttons = new EditableListButtons (records[(int) i]);
                    buttons.DeletePressed += OnDelete;
                    buttons.DetailPressed += OnDetail;
                    table.Attach (buttons, j, j+1, i+1,i+2);
                    table.SetRowSpacing(i, 0);
                }
                table.ShowAll ();
                this.editable_helper.UpdateEditableWidgets ();
                this.editable_helper.SetAllEditable (isEditable);
            }
        }

        public List<AffiliableRecord> AffiliableRecords {
            get {
                return this.affiliable_records;
            }
            set {
                affiliable_records = value;

                this.DestroyTableChildren ();
                this.columnHeaders = (string[])headers.Clone ();
                this.BuildTableHeaders ();

                table.Resize ((uint) (affiliable_records.Count + 1), (uint) (headers.Length+1));
                for (uint i = 0; i < affiliable_records.Count; i++) {
                    string[] data = affiliable_records[(int) i].AffiliationColumnData ();
                    uint j = 0;
                    for (; j < (headers.Length); j++) {
                        Label l = new Label (data[j]);
                        //l.MaxWidthChars = 20;
                        l.LineWrap = true;
                        l.Wrap = true;
                        l.Justify = Justification.Fill;
                        l.SingleLineMode = false;

                        Gtk.Frame f = new Gtk.Frame ();
                        f.Add(l);
                        f.ShadowType = Gtk.ShadowType.In;

                        table.Attach (f, j, j+1, i+1,i+2);
                        table.SetColSpacing(j, 0);
                    }
                    table.SetRowSpacing(i, 0);
                }
                table.ShowAll ();
                this.editable_helper.UpdateEditableWidgets ();
                this.editable_helper.SetAllEditable (false);
                newButton.Visible = false;
            }
        }

        public List<AffiliatedRecord> AffiliatedRecords {
            get {
                return this.affiliated_records;
            }
            set {
                affiliated_records = value;

                this.DestroyTableChildren ();
                this.columnHeaders = (string[])headers.Clone ();
                this.BuildTableHeaders ();

                table.Resize ((uint) (affiliated_records.Count + 1), (uint) (headers.Length+1));
                for (uint i = 0; i < affiliated_records.Count; i++) {
                    string[] data = affiliated_records[(int) i].AffiliatedColumnData ();
                    uint j = 0;
                    for (; j < (headers.Length); j++) {
                        Label l = new Label (data[j]);
                        //l.MaxWidthChars = 20;
                        l.LineWrap = true;
                        l.Wrap = true;
                        l.Justify = Justification.Fill;
                        l.SingleLineMode = false;

                        Gtk.Frame f = new Gtk.Frame ();
                        f.Add(l);
                        f.ShadowType = Gtk.ShadowType.In;

                        table.Attach (f, j, j+1, i+1,i+2);
                        table.SetColSpacing(j, 0);
                    }
                    table.SetRowSpacing(i, 0);
                }
                table.ShowAll ();
                this.editable_helper.UpdateEditableWidgets ();
                this.editable_helper.SetAllEditable (false);
                newButton.Visible = false;
            }
        }

        protected void BuildTableHeaders () {
            table.Resize (1, (uint) (columnHeaders.Length));
            for (uint i = 0; i < columnHeaders.Length; i++) {
                Label l = new Label ("<b>" + Catalog.GetString (columnHeaders[i]) + "</b>");
                l.UseMarkup = true;
                l.Justify = Justification.Fill;

                Gtk.Frame f = new Gtk.Frame ();
                f.ShadowType = Gtk.ShadowType.In;
                f.Add(l);

                table.Attach (f, i, i+1, 0,1);
                table.SetColSpacing(i, 0);
            }

            table.SetRowSpacing(0, 0);
        }

        protected void DestroyTableChildren () {
            foreach (Gtk.Widget w in table.AllChildren)
                w.Destroy();
        }

        public bool IsEditable
        {
            get { return this.isEditable; }
            set
            {
                isEditable = value;
                newButton.Visible = value;
                this.editable_helper.SetAllEditable (value);
            }
        }

        protected void OnDelete (object sender, System.EventArgs e)
        {
            if (DeleteButtonPressed != null)
                DeleteButtonPressed (sender, e);
        }

        protected void OnDetail (object sender, System.EventArgs e)
        {
            if (DetailButtonPressed != null)
                DetailButtonPressed (sender, e);
        }

        protected void OnNew (object sender, System.EventArgs e)
        {
            if (NewButtonPressed != null)
                NewButtonPressed (sender, e);
        }
    }
}

