using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonAndJobRow : Gtk.Bin
    {

        Person person;
        Job job;
        public PersonAndJobRow (Person p, Job j)
        {
            this.Build ();
            this.person = p;
            this.job = j;
            set_widgets();
        }

        public void set_widgets() {
            if (person != null ) {
               person_fullname.Text = person.Fullname;
               if (person.Photo != null)
                image.Pixbuf = new Gdk.Pixbuf (person.Photo.Icon);
               else
                image.Pixbuf = Gdk.Pixbuf.LoadFromResource ("Views.images.Missing.jpg");
            }

            if (job != null)
                job_name.Text = job.Name;
            else
                job_name.Destroy();
        }

    }
}

