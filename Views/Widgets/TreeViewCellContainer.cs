using System;
using Gtk;
using Gdk;

namespace Views
{
 class TreeViewCellContainer: Entry
 {
     EventBox box;
     
     public TreeViewCellContainer (Gtk.Widget child)
     {
         box = new EventBox ();
         box.ButtonPressEvent += new ButtonPressEventHandler (OnClickBox);
         box.ModifyBg (StateType.Normal, Style.White);
         //box.Add (child);
         child.Reparent (box);
         box.Add (child);
         child.Show ();
         Show ();
     }
     
     [GLib.ConnectBefore]
     void OnClickBox (object s, ButtonPressEventArgs args)
     {
         // Avoid forwarding the button press event to the
         // tree, since it would hide the cell editor.
         args.RetVal = true;
     }
     
     protected override void OnParentSet (Gtk.Widget parent)
     {
         base.OnParentSet (parent);

         //if (Parent != null) {
         //    if (ParentWindow != null)
             box.ParentWindow = ParentWindow;
             box.Parent = Parent;
             box.Show ();

         //}
         //else
         if (Parent == null)
             box.Unparent ();
     }
     
     protected override void OnShown ()
     {
         // Do nothing.
     }
     
     protected override void OnSizeAllocated (Gdk.Rectangle allocation)
     {
         base.OnSizeAllocated (allocation);
         box.SizeRequest ();
         box.Allocation = allocation;
     }
 }
}