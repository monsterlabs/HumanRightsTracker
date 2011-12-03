using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;
using NHibernate.Criterion;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InformationSourceList : Gtk.Bin
    {
        List<InformationSource> information_sources;
        Case c;
        bool isEditable;

        public InformationSourceList ()
        {
            this.Build ();
            row.Destroy ();
            information_sources = new List<InformationSource>();
        }

        public List<InformationSource> InformationSources
        {
            get {return information_sources;}
        }

        public Case Case
        {
            get { return c; }
            set
            {
                c = value;
                ReloadList ();
            }
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                newButton.Visible = value;
                foreach (Gtk.Widget row in informationSourceList.AllChildren) {
                    ((InformationSourceRow) row).IsEditable = value;
                }
            }
        }

        public void ReloadList ()
        {
            foreach (Gtk.Widget w in informationSourceList.AllChildren)
            {
                w.Destroy();
            }
            if (c.Id < 1) {
                return;
            }
            information_sources = new List<InformationSource> (InformationSource.FindAll (new ICriterion[] { Restrictions.Eq("Case", c) }));
            foreach (InformationSource i in information_sources)
            {
                informationSourceList.PackStart (new InformationSourceRow (i, OnInformationSourceRowRemoved));
            }
            informationSourceList.ShowAll ();
        }

        protected void OnInformationSourceRowRemoved (object sender, EventArgs e)
        {
            InformationSourceRow informationSourceRow = sender as InformationSourceRow;
            InformationSource i = informationSourceRow.InformationSource;
            information_sources.Remove(i);
            informationSourceList.Remove(informationSourceRow);

            if (i.Id >= 1)
            {
                // TODO: Confirmation.
                i.Delete ();
            }

            return;
        }


        protected void OnNewButtonClicked (object sender, System.EventArgs e)
        {
            new InformationSourceWindow (c, OnNewInformationSourceReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnNewInformationSourceReturned (object sender, EventArgs args)
        {
            InformationSource i = sender as InformationSource;
            informationSourceList.PackStart (new InformationSourceRow (i, OnInformationSourceRowRemoved));
            informationSourceList.ShowAll ();
            information_sources.Add (i);
            return;
        }

    }
}

