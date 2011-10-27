using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseRow : Gtk.Bin
    {
        Case c;

        public CaseRow (Case c)
        {
            this.Build ();
            this.c = c;
            set_widgets ();
        }
        
        public CaseRow ()
        {
            this.Build ();
        }

        public Case Case
        {
            get { return c; }
            set {
                c = value;
                set_widgets();
            }
        }

        public void set_widgets() {
                   if (c != null) {
                    name.Text = c.Name;
                    start_date.Text = String.Format("{0:MM/dd/yyyy}", c.start_date);
                    end_date.Text = String.Format("{0:MM/dd/yyyy}", c.end_date);
                }
        }

        protected void OnButtonClicked (object sender, System.EventArgs e)
        {
            new  CaseDetailWindow (this.c,(Gtk.Window)this.Toplevel);
        }
    }
}

