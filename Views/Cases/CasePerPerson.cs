using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using System.Diagnostics;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CasePerPerson : Gtk.Bin
    {
        List<Case> case_list;
        Person p;

        public CasePerPerson ()
        {
            this.Build ();
            case_list = new List<Case>();
        }

        public List<Case> CaseList
        {
            get {return case_list;}
        }


        public Person Person
        {
            get { return p; }
            set
            {
                p = value;
                ReloadList ();
            }
        }

        public void ReloadList ()
        {
            foreach (Case c in p.caseList ())
            {
                case_vbox.PackStart (new CaseRow (c));

            }
            case_vbox.ShowAll ();
        }


    }
}

