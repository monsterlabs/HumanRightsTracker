using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ImageSelector : Gtk.Bin, IEditable
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
            deleteButton.Visible = false;
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                filechooserbutton2.Visible = value;
                if (image != null)
                    deleteButton.Visible = value;
                else {
                    deleteButton.Visible = false;
                }
            }
        }

        protected void OnFileSelected (object sender, System.EventArgs e)
        {
            String filename = filechooserbutton2.Filename;
            Gdk.Pixbuf source = new Gdk.Pixbuf (filename);
            if (image == null)
                image = new Image ();
            Gdk.Pixbuf original = GetThumbnail (source, 500);
            Gdk.Pixbuf thumbnail = GetThumbnail (source, 100);
            Gdk.Pixbuf icon = GetThumbnail (source, 48);

            image.Original = original.SaveToBuffer ("jpeg");
            image.Thumbnail = thumbnail.SaveToBuffer ("jpeg");
            image.Icon = icon.SaveToBuffer ("jpeg");
            image8.Pixbuf = thumbnail;
            deleteButton.Visible = true;
        }

        public Image Image
        {
            set
            {
                image = value;
                if (value != null)
                {
                    Gdk.Pixbuf buffer = new Gdk.Pixbuf (value.Thumbnail);
                    image8.Pixbuf = buffer;
                    deleteButton.Visible = true;
                } else
                {
                    image8.Pixbuf = null;
                    deleteButton.Visible = false;
                }
            }

            get
            {
                return image;
            }
        }

        public Gdk.Pixbuf GetThumbnail (Gdk.Pixbuf original, int squareDim)
        {
            int height = original.Height;
            int width = original.Width;

            if (height > width)
            {
                int newWidth = (width * squareDim)/height;
                return original.ScaleSimple (newWidth, squareDim, Gdk.InterpType.Bilinear);
            } else
            {
                int newHeight = (height * squareDim)/width;
                return original.ScaleSimple (squareDim, newHeight, Gdk.InterpType.Bilinear);
            }
        }

        protected void OnDelete (object sender, System.EventArgs e)
        {
            if (image.Id > 0)
                image.DeleteAndFlush ();
            Image = null;
        }
    }
}

