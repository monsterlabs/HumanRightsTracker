using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class AlphabetList : Gtk.Bin
    {
        public AlphabetList ()
        {
            this.Build ();
            Gtk.CellRendererText letterCell = new Gtk.CellRendererText ();

            Gtk.TreeViewColumn letterColumn = new Gtk.TreeViewColumn ();
            letterColumn.Title = "Letter";
            letterColumn.PackStart (letterCell, true);

            alphabetList.AppendColumn(letterColumn);
            letterColumn.AddAttribute (letterCell, "text", 0);

            Gtk.ListStore letterListStore = new Gtk.ListStore (typeof (string));
            string alpha = "ABCDEFGHIJKLMNÑOPQRSTUVQXYZ";
            foreach (char c in alpha) {
                letterListStore.AppendValues(c.ToString());
            }
            alphabetList.Model = letterListStore;
        }
    }
}