using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using Gtk;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EditableList : Gtk.Bin
    {
        string[] headers;

        List<ListableRecord> records;

        public event EventHandler NewButtonPressed;
        public event EventHandler DeleteButtonPressed;
        public event EventHandler DetailButtonPressed;

        public EditableList ()
        {
            this.Build ();
        }

        public string[] Headers {
            get {
                return this.headers;
            }
            set {
                headers = value;

                table.Resize (1, (uint) (headers.Length + 1));
                for (uint i = 0; i < headers.Length; i++) {
                    Label l = new Label ("<b>" + headers[i] + "</b>");
                    l.UseMarkup = true;
                    table.Attach (l, i, i+1, 0,1);
                }
            }
        }

        public List<ListableRecord> Records {
            get {
                return this.records;
            }
            set {
                records = value;

                foreach (Gtk.Widget w in table.AllChildren) {
                    w.Destroy();
                }

                table.Resize ((uint) (records.Count + 1), (uint) (headers.Length + 1));
                for (uint i = 0; i < records.Count; i++) {
                    string[] data = records[(int) i].ColumnData ();
                    uint j = 0;
                    for (; j < headers.Length; j++) {
                        Label l = new Label (data[j]);
                        //l.MaxWidthChars = 20;
                        l.LineWrap = true;
                        l.Wrap = true;
                        l.SingleLineMode = false;
                        table.Attach (l, j, j+1, i+1,i+2);
                    }
                    EditableListButtons buttons = new EditableListButtons (records[(int) i]);
                    buttons.DeletePressed += OnDelete;
                    buttons.DetailPressed += OnDetail;
                    table.Attach (buttons, j, j+1, i+1,i+2);
                }
                table.ShowAll ();
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

