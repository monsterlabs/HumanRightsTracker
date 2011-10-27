using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class VictimRow : Gtk.Bin
    {
        protected Victim victim;
        protected bool isEditable;

        public event EventHandler OnRowRemoved;

        public VictimRow ()
        {
            this.Build ();
        }

        public VictimRow (Victim victim, EventHandler removed)
        {
            this.Build ();
            this.Victim = victim;
            this.OnRowRemoved = removed;
        }

        public virtual bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                removeButton.Visible = value;
            }
        }
        public virtual Victim Victim
        {
            get {return victim;}
            set
            {
                victim = value;
                if (victim != null)
                {
                    if (victim.Person.Photo != null)
                    {
                        photo.Pixbuf = new Gdk.Pixbuf (victim.Person.Photo.Icon);
                    }
                    status.Text = victim.VictimStatus.Name;
                    fullname.Text = victim.Person.Fullname;
                    photo.Show ();
                    fullname.Show ();
                } else {
                    photo.Hide ();
                    fullname.Hide ();
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
            new VictimWindow (victim, OnVictimUpdated, (Gtk.Window)this.Toplevel);
        }

        protected void OnVictimUpdated (object sender, VictimEventArgs args)
        {
            Victim v = args.Victim;
            if (victim.Act.Id > 0) {
                this.Victim = v;
            }

            return;
        }
    }
}

