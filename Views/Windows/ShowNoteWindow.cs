using System;

namespace Views
{
    public partial class ShowNoteWindow : Gtk.Window
    {
        Gtk.Window _parent;

        public ShowNoteWindow (int x, int y, String note_text,  Gtk.Window parent) : base(Gtk.WindowType.Popup)
        {
            this.TransientFor = parent;
            _parent = parent;
            _parent.Modal = false;
            this.Modal = true;
            this.Build ();
            this.Move (x, y);
            this.WindowPosition = Gtk.WindowPosition.None;
            note.Text = note_text;
        }

        protected void OnCloseButtonClickedxx (object sender, System.EventArgs e)
        {
            this.Destroy ();
        }

        protected void OnCloseButtonClicked (object sender, System.EventArgs e)
        {
          this.Destroy ();
       }
    }
}

