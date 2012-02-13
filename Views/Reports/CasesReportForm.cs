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
                HumanRightsViolationCategory hr = humanRight.Active as HumanRightsViolationCategory;
                dA.Add (Restrictions.Eq ("HumanRightsViolationCategory", hr));
            }

            Case[] cases = Case.FindAll (dC);
            CasesReportGenerator report = new CasesReportGenerator (cases);
            report.SaveTo (System.IO.Path.Combine(folderChooser.CurrentFolder, fileName.Text));
        }
    }
}