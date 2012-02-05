using System;
using System.IO;

using HumanRightsTracker.Models;
using Reports;

namespace Views
{
    public partial class CaseReportWindow : Gtk.Window
    {
        Case _case;

        public CaseReportWindow (Case acase) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            _case = acase;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            CaseReportGenerator generator = new CaseReportGenerator (_case);
            generator.SaveTo (System.IO.Path.Combine (folderchooserbutton.CurrentFolder, filename.Text ?? _case.Name));
            this.Destroy ();
        }

        protected void OnCancel (object sender, System.EventArgs e)
        {
            this.Destroy ();
        }
    }
}

