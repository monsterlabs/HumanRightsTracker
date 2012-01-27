using System;
using Gtk;
using Gdk;

namespace Views
{
 public class CellRendererCatalogSelector: CellRendererText
 {
     string path;
     int rowHeight;
     String model;
     CatalogSelector combo;

     public CellRendererCatalogSelector (String model)
     {
         Mode |= Gtk.CellRendererMode.Editable;
         Entry dummyEntry = new Gtk.Entry ();
         rowHeight = dummyEntry.SizeRequest ().Height;
         this.model = model;

         combo = new CatalogSelector ();
         combo.Model = model;
         combo.Changed += SelectionChanged;
     }
     
     public override void GetSize (Widget widget, ref Rectangle cell_area, out int x_offset, out int y_offset, out int width, out int height)
     {
         base.GetSize (widget, ref cell_area, out x_offset, out y_offset, out width, out height);
         if (height < rowHeight)
             height = rowHeight;
     }
     
     public override CellEditable StartEditing (Gdk.Event ev, Widget widget, string path, Gdk.Rectangle background_area, Gdk.Rectangle cell_area, CellRendererState flags)
     {
        this.path = path;

        //combo.Combobox.Entry.Text = Text;

        return new TreeViewCellContainer (combo);
     }

     void SelectionChanged (object s, EventArgs a)
     {
         //Gtk.ComboBox combo = (Gtk.ComboBox) s;
         if (Changed != null)
             Changed (s, new ComboSelectionChangedArgs (path, combo.Combobox.Active, combo.Combobox.Entry.Text));
     }

     // Fired when the selection changes
     public event ComboSelectionChangedHandler Changed;
 }


 public delegate void ComboSelectionChangedHandler (object sender, ComboSelectionChangedArgs args);

 public class ComboSelectionChangedArgs: EventArgs
 {
     string path;
     int active;
     string activeText;
     
     internal ComboSelectionChangedArgs (string path, int active, string activeText)
     {
         this.path = path;
         this.active = active;
         this.activeText = activeText;
     }
     
     public string Path {
         get { return path; }
     }
     
     public int Active {
         get { return active; }
     }
     
     public string ActiveText {
         get { return activeText; }
     }
 }
}