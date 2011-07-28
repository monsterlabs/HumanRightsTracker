using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ImageSelector : Gtk.Bin
    {
        bool isEditable;
        Image image;

        public ImageSelector ()
        {
            this.Build ();
            Gtk.FileFilter jpgFilter = new Gtk.FileFilter ();
            jpgFilter.Name = "JPEG images";
            jpgFilter.AddMimeType("image/jpeg");
            jpgFilter.AddPattern("*.jpg");
            jpgFilter.AddPattern("*.jpeg");
            filechooserbutton2.AddFilter (jpgFilter);
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                filechooserbutton2.Visible = value;
            }
        }

        protected void OnFileSelected (object sender, System.EventArgs e)
        {
            String filename = filechooserbutton2.Filename;
            String type = filename.Substring (filename.LastIndexOf('.') + 1);
            image8.File = filename;
            if (image == null)
                image = new Image ();
            image.Original = image8.Pixbuf.SaveToBuffer("jpeg");
        }

        public Image Image
        {
            set
            {
                if (value != null)
                {
                    image = value;
                    Gdk.Pixbuf buffer = new Gdk.Pixbuf (value.Original);
                    image8.Pixbuf = buffer;
                } else
                {
                    image8.Pixbuf = null;
                }
            }

            get
            {
                return image;
            }
        }
    }
}

