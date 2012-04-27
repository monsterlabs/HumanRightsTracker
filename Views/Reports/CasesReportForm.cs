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
            fromDate.CurrentDateForCalendar = DateTime.Now;
            toDate.CurrentDateForCalendar = DateTime.Now;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            DetachedCriteria dC = DetachedCriteria.For<Case> ();

            if (caseName.Text != null && caseName.Text.Length > 0)
                dC.Add(Restrictions.InsensitiveLike("Name", caseName.Text, MatchMode.Anywhere));

            if (fromDate.CurrentDate > DateTime.MinValue) {
                dC.Add (Restrictions.Ge ("start_date", fromDate.CurrentDate));
            }

            if (toDate.CurrentDate > DateTime.MinValue) {
                dC.Add (Restrictions.Le ("start_date", toDate.CurrentDate));
            }

            if (victimName.Text != null && victimName.Text.Length > 0) {
                DetachedCriteria dA = dC.CreateCriteria("Acts");
                DetachedCriteria dV = dA.CreateCriteria("Victims");
                DetachedCriteria dP = dV.CreateCriteria("Person");
                dP.Add (Restrictions.Or (
                    Restrictions.InsensitiveLike("Firstname", victimName.Text, MatchMode.Anywhere),
                    Restrictions.InsensitiveLike("Lastname", victimName.Text, MatchMode.Anywhere)
                ));
            }

            if (humanRight.Active != null) {
                DetachedCriteria dA = dC.CreateCriteria("Acts");
                HumanRightsViolation hrv = humanRight.Active as HumanRightsViolation;

                HumanRightsViolationCategory hr = HumanRightsViolationCategory.FindOne(new ICriterion[] { Restrictions.Eq ("Id", hrv.CategoryId)});
                dA.Add (Restrictions.Eq ("HumanRightsViolationCategory", hr));
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
            victimName.Text = "";
            humanRight.Active = null;
            AffectedRight.Active = null;
            fileName.Text = "";
            folderChooser.SetCurrentFolder ("");
        }

        protected void OnFromNameDelete (object sender, System.EventArgs e)
        {
            fromDate.CurrentDate = null;
        }

        protected void OnVictimNameDelete (object sender, System.EventArgs e)
        {
            victimName.Text = "";
        }

        protected void OnFileNameDelete (object sender, System.EventArgs e)
        {
            fileName.Text = "";
        }
    }
}