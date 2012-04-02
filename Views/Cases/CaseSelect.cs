using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseSelect : Gtk.Bin
    {
        Case c;
        bool isEditable;
        public event EventHandler Changed;

        public CaseSelect ()
        {
            this.Build ();
        }

        public CaseSelect (Case c, EventHandler changed)
        {
            this.Build ();
            this.c = c;
            this.Changed = changed;
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                selectButton.Visible = value;
            }
        }

        public Case Case {
            get { return this.c; }
            set {
                this.c = value;
                if (c != null) {
                    name.Text = c.Name;
                    start_date.Text = c.start_date.Value.ToShortDateString ();
                    if (c.end_date != null)
                        end_date.Text = c.end_date.Value.ToShortDateString ();
                    else
                        end_date.Text = "";
                }
            }
        }

        protected void OnSelectButtonClicked (object sender, System.EventArgs e)
        {
            new CaseSelectorWindow (OnCaseSelected, (Gtk.Window) this.Toplevel);
        }

        protected void OnCaseSelected (object sender, CaseEventArgs args)
        {
            Case = args.Case;
            return;
        }
    }
}

