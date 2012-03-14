using System;

namespace Views
{
    public partial class ShowNoteWindow : Gtk.Window
    {
        public ShowNoteWindow (int x, int y, String note_text,  Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.TransientFor = parent;
            this.Modal = true;
            this.Move (x, y);
            this.WindowPosition = Gtk.WindowPosition.None;
            note.Text = note_text;
        }

        protected void OnCloseButtonClicked (object sender, System.EventArgs e)
        {
          this.Destroy ();
       }
    }
}

