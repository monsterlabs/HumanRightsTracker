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
        Person p;

        public CasePerPerson ()
        {
            this.Build ();

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
            foreach (Gtk.Widget w in case_vbox.Children)
                w.Destroy ();

            foreach (Case c in p.caseList ())
            {
                case_vbox.PackStart (new CaseRow (c));

            }
            case_vbox.ShowAll ();
        }


    }
}

