using System;
using System.IO;
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
            ICriterion[] criteria = new ICriterion[] {
                Restrictions.InsensitiveLike("Name", caseName.Text, MatchMode.Anywhere) };
            Case[] cases = Case.FindAll (criteria);
            CasesReportGenerator report = new CasesReportGenerator (cases);
            report.SaveTo (System.IO.Path.Combine(folderChooser.CurrentFolder, fileName.Text));
        }
    }
}