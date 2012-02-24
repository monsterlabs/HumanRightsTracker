using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionSelect : Gtk.Bin
    {
        Institution institution;
        bool isEditable;
        public event EventHandler Changed;

        public InstitutionSelect ()
        {
            this.Build ();
        }

        public InstitutionSelect (Institution institution, EventHandler changed)
        {
            this.Build ();
            this.Institution = institution;
            this.Changed = changed;
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                changeButton.Visible = value;
            }
        }
        public Institution Institution
        {
            get {return institution;}
            set
            {
                institution = value;
                if (institution != null)
                {
                    Console.WriteLine(institution.Photo);
                    if (institution.Photo.Icon != null)
                    {
                        photo.Pixbuf = new Gdk.Pixbuf (institution.Photo.Icon);
                    } else {
                        photo.Pixbuf = Gdk.Pixbuf.LoadFromResource ("Views.images.MissingInstitutionIcon.jpg");
                    }
                    fullname.Text = institution.Name;
                    photo.Show ();
                    fullname.Show ();
                } else {
                    photo.Hide ();
                    fullname.Hide ();
                }
            }
        }

        protected void OnInstitutionSelected (object sender, InstitutionEventArgs args)
        {
            Institution = args.Institution;

            return;
        }

        protected void OnChangeClicked (object sender, System.EventArgs e)
        {
            new InstitutionSelectorWindow (OnInstitutionSelected);
        }
    }
}

