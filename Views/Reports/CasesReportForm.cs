using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using Reports;
using NHibernate.Criterion;


namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CasesReportForm : Gtk.Bin
    {
        public CasesReportForm ()
        {
            this.Build ();
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            DetachedCriteria dC = DetachedCriteria.For<Case> ();

            if (caseName.Text != null && caseName.Text.Length > 0)
                dC.Add(Restrictions.InsensitiveLike("Name", caseName.Text, MatchMode.Anywhere));

            if (fromDate.CurrentDate.HasValue) {
                dC.Add (Restrictions.Ge ("start_date", fromDate.CurrentDate));
            }

            if (toDate.CurrentDate.HasValue) {
                dC.Add (Restrictions.Le ("start_date", toDate.CurrentDate));
            }

            if (humanRight.Active != null) {
                // Only filter by act when both right and act are selected
                // because otherwise the right (HumanRightsViolationCategory)
                // may hide cases with that act selected but not the same right.
                DetachedCriteria dA = dC.CreateCriteria("Acts");
                HumanRightsViolation hrv = humanRight.Active as HumanRightsViolation;


                dA.Add (Restrictions.Eq ("HumanRightsViolation", hrv));


            } else {
                // Only a right is selected, filter by it.
                if (AffectedRight.Active != null) {
                    DetachedCriteria dA = dC.CreateCriteria("Acts");
                    HumanRightsViolationCategory hr = AffectedRight.Active as HumanRightsViolationCategory;
                    dA.Add (Restrictions.Eq ("HumanRightsViolationCategory", hr));
                }
            }

            Case[] cases = Case.FindAll (dC);
            CasesReportGenerator report = new CasesReportGenerator (cases);
            report.OverrideConfirmation = HandleOverrideConfirmation;
            report.SaveTo (System.IO.Path.Combine(folderChooser.CurrentFolder, fileName.Text));
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

        protected void OnAffectedRightCategorySelected (object sender, System.EventArgs e)
        {
            HumanRightsViolationCategory category = sender as HumanRightsViolationCategory;
            humanRight.FilterByCategoryId(category.Id);
        }

        protected void OnCaseNameDelete (object sender, System.EventArgs e)
        {
            caseName.Text = "";
        }

        protected void OnToDateDelete (object sender, System.EventArgs e)
        {
            toDate.CurrentDate = null;
        }

        protected void OnClear (object sender, System.EventArgs e)
        {
            fromDate.CurrentDate = null;
            toDate.CurrentDate = null;
            caseName.Text = "";
            humanRight.Active = null;
            AffectedRight.Active = null;
            fileName.Text = "";
            folderChooser.SetCurrentFolder ("");
        }

        protected void OnFromNameDelete (object sender, System.EventArgs e)
        {
            fromDate.CurrentDate = null;
        }

        protected void OnFileNameDelete (object sender, System.EventArgs e)
        {
            fileName.Text = "";
        }

        protected void OnClearActAndRight (object sender, System.EventArgs e)
        {
            humanRight.Active = null;
            AffectedRight.Active = null;
        }
    }
}