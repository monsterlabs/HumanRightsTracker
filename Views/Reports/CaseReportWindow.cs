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
            generator.OverrideConfirmation = HandleOverrideConfirmation;
            generator.SaveTo (System.IO.Path.Combine (folderchooserbutton.CurrentFolder, filename.Text));
            this.Destroy ();
        }

        protected void OnCancel (object sender, System.EventArgs e)
        {
            this.Destroy ();
        }

        bool HandleOverrideConfirmation (String name)
        {
            Gtk.MessageDialog dialog = new Gtk.MessageDialog(this.Toplevel as Gtk.Window,
                Gtk.DialogFlags.DestroyWithParent,
                Gtk.MessageType.Question,
                Gtk.ButtonsType.YesNo,
                name + Mono.Unix.Catalog.GetString(" already exists.\n Do you want to override it?"));
            dialog.Modal = true;
            Gtk.ResponseType result = (Gtk.ResponseType)dialog.Run();
            dialog.Destroy ();

            return result == Gtk.ResponseType.Yes;
        }
    }
}

