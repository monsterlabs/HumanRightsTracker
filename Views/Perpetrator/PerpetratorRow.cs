using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PerpetratorRow : Gtk.Bin, IEditable
    {
        protected Perpetrator perpetrator;
        protected bool isEditable;

        public event EventHandler OnRowRemoved;

        public PerpetratorRow ()
        {
            this.Build ();
            this.IsEditable = false;
        }

        public PerpetratorRow (Perpetrator perpetrator, EventHandler removed)
        {
            this.Build ();
            this.Perpetrator = perpetrator;
            this.OnRowRemoved = removed;
        }

        public virtual bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                button8.Visible = value;
            }
        }
        public virtual Perpetrator Perpetrator
        {
            get {return perpetrator;}
            set
            {
                perpetrator = value;
                if (perpetrator != null)
                {
                    if (perpetrator.Person.Photo != null)
                    {
                        photo.Pixbuf = new Gdk.Pixbuf (perpetrator.Person.Photo.Icon);
                    }

                    fullname.Text = perpetrator.Person.Fullname;
                    institution.Text = perpetrator.Institution.Name;
                    job.Text = perpetrator.Job.Name;
                    photo.Show ();
                    fullname.Show ();
                    institution.Show ();
                    job.Show ();
                } else {
                    photo.Hide ();
                    fullname.Hide ();
                    institution.Hide ();
                    job.Hide ();
                }
            }
        }

        protected void OnRemove (object sender, System.EventArgs e)
        {
            if (OnRowRemoved != null)
                OnRowRemoved (this, e);
        }

        protected void OnInfo (object sender, System.EventArgs e)
        {
            new PerpetratorWindow (perpetrator, OnPerpetratorUpdated, (Gtk.Window)this.Toplevel);
        }

        protected void OnPerpetratorUpdated (object sender, PerpetratorEventArgs args)
        {
            Perpetrator p = args.Perpetrator;
            if (perpetrator.Victim.Id > 0) {
                this.Perpetrator = p;
            }

            return;
        }
    }
}

