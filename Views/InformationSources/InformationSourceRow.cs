using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InformationSourceRow : Gtk.Bin
    {
        InformationSource information_source;
        bool isEditable;
        public new event EventHandler Removed;


        public InformationSourceRow ()
        {
            this.Build ();
            
        }

        public InformationSourceRow (InformationSource information_source, EventHandler removed)
        {
            this.Build ();
            InformationSource = information_source;
            this.Removed = removed;
        }

        public InformationSource InformationSource
        {
            get { return information_source; }
            set
            {
                information_source = value;
                source.Text = information_source.sourceName ();
                reported.Text = information_source.reportedName ();
                if (value.Date.HasValue)
                    date.Text = information_source.Date.Value.ToShortDateString ();
            }
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                removeButton.Visible = value;
            }
        }

        protected void OnRemoveButtonClicked (object sender, System.EventArgs e)
        {
            if (Removed != null)
                Removed (this, e);
        }

        protected void OnDetailButtonClicked (object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException ();
        }

        protected void OnDetailReturned (object sender, System.EventArgs e)
        {
            this.InformationSource = sender as InformationSource;
        }
    }
}

