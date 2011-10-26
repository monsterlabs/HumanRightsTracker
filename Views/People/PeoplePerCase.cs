using System;
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

        public void ReloadVictimList ()
        {
            foreach (Person person in c.victimList ())
            {
                victims_vbox.PackStart (new PersonAndJobRow (person, null));
            }
           victims_vbox.ShowAll ();
        }
    }
}

