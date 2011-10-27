using System;
using System.Collections;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using System.Diagnostics;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionAndJobPerPerson : Gtk.Bin
    {
        Person p;
        public InstitutionAndJobPerPerson ()
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
            foreach (Gtk.Widget w in institution_and_job_vbox.Children)
                w.Destroy ();

            foreach (ArrayList institution_and_job in p.institutionAndJobList ())
            {
                institution_and_job_vbox.PackStart (new InstitutionAndJobRow ((Institution)institution_and_job[0], (Job)institution_and_job[1]));
            }
           institution_and_job_vbox.ShowAll ();
        }

    }
}

