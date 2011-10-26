using System;
using HumanRightsTracker.Models;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionAndJobRow : Gtk.Bin
    {
        Institution institution;
        Job job;
        public InstitutionAndJobRow (Institution i, Job j)
        {
            this.Build ();
            this.institution = i;
            this.job = j;
            set_widgets();
        }


         public void set_widgets() {
            if (institution != null ) {
               institution_name.Text = institution.Name;

               image.Pixbuf = new Gdk.Pixbuf (institution.Photo.Thumbnail);
            }

            if (job != null) {
                job_name.Text = job.Name;
            }
        }

    }
}

