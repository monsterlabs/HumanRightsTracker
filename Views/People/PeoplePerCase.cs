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
            }
        }

        public Institution Institution
        {
            get { return i; }
            set
            {
                i = value;
                ReloadPerpetratorList ();
                ReloadInterventorList ();
                ReloadSupporterList ();
            }
        }

        public void ReloadVictimList ()
        {
            foreach (Gtk.Widget w in victims_vbox.Children)
                w.Destroy ();

            foreach (Person person in c.victimList ())
            {
                victims_vbox.PackStart (new PersonAndJobRow (person, null));
            }
           victims_vbox.ShowAll ();
        }


        public void ReloadPerpetratorList ()
        {
            foreach (Gtk.Widget w in perpetrators_vbox.Children)
                w.Destroy ();

            foreach (ArrayList a in i.perpetratorAndJobPerCase (c))
            {
                perpetrators_vbox.PackStart (new PersonAndJobRow((Person)a[0], (Job)a[1]));
            }
           perpetrators_vbox.ShowAll ();
        }

        public void ReloadInterventorList ()
        {
            foreach (Gtk.Widget w in interventors_vbox.Children)
                w.Destroy ();

            foreach (ArrayList a in i.interventorAndJobPerCase (c))
            {
                interventors_vbox.PackStart (new PersonAndJobRow((Person)a[0], (Job)a[1]));
            }
           interventors_vbox.ShowAll ();
        }

        public void ReloadSupporterList ()
        {
            foreach (Gtk.Widget w in supporters_vbox.Children)
                w.Destroy ();

            foreach (ArrayList a in i.supporterAndJobPerCase (c))
            {
                supporters_vbox.PackStart (new PersonAndJobRow((Person)a[0], (Job)a[1]));
            }
           supporters_vbox.ShowAll ();
        }
    }
}

