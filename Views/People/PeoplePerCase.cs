using System;
using System.Collections;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using System.Diagnostics;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PeoplePerCase : Gtk.Bin
    {
        Case c;
        Institution i;

        public PeoplePerCase ()
        {
            this.Build ();
        }

        public Case Case
        {
            get { return c; }
            set
            {
                c = value;
                ReloadVictimList ();
                ReloadPerpetratorList ();
                ReloadInterventorList ();
                ReloadSupporterList ();
                ReloadInformationSourceList ();
                ReloadDocumentarySourceList();
            }
        }

        public Institution Institution
        {
            get { return i; }
            set
            {
                i = value;
                ReloadVictimList ();
                ReloadPerpetratorList ();
                ReloadInterventorList ();
                ReloadSupporterList ();
                ReloadInformationSourceList ();
                ReloadDocumentarySourceList();

            }
        }

        public void ReloadVictimList ()
        {
           FillVbox(victims_vbox, c.victimList());
        }


        public void ReloadPerpetratorList ()
        {
           FillVbox(perpetrators_vbox, c.perpetratorList ());
        }

        public void ReloadInterventorList ()
        {
           FillVbox(interventors_vbox, c.interventorList());
        }

        public void ReloadSupporterList ()
        {
            FillVbox(supporters_vbox, c.supporterList());
        }

        public void ReloadInformationSourceList() {
            FillVbox(supporters_vbox1, c.informationSourceList());
        }

        public void ReloadDocumentarySourceList() {
            FillVbox(supporters_vbox2, c.documentarySourceList ());
        }

        public void FillVbox(Gtk.VBox vbox, IList people) {
            foreach (Person p in people)
            {
                vbox.PackStart (new PersonAndJobRow(p, null));
            }
            vbox.ShowAll ();
        }
    }
}

