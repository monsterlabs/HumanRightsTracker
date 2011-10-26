using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using System.Diagnostics;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CasePerInstitution : Gtk.Bin
    {

        Institution i;
        public CasePerInstitution ()
        {
            this.Build ();
        }

        public Institution Institution
        {
            get { return i; }
            set
            {
                i = value;
                ReloadList ();
            }
        }

        public void ReloadList ()
        {
            foreach (Case c in i.caseList ())
            {
                case_vbox.PackStart (new CaseAndPeopleRow (c));

            }
            case_vbox.ShowAll ();
        }


    }
}

















