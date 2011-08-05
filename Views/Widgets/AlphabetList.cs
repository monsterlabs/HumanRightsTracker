using System;

namespace Views
{
    [Gtk.TreeNode (ListOnly=true)]
    public class LetterNode : Gtk.TreeNode {

        public LetterNode (string letter)
        {
          Letter = letter;
        }

        [Gtk.TreeNodeValue (Column=0)]
        public string Letter;
    }

    [System.ComponentModel.ToolboxItem(true)]
    public partial class AlphabetList : Gtk.Bin
    {

        Gtk.NodeStore store;
        Gtk.NodeView alphabetView;

        Gtk.NodeStore Store {
            get {
                if (store == null) {
                    NewStore();
                }
                return store;
            }
        }

        public AlphabetList ()
        {
            this.Build ();
            alphabetNodeView.NodeStore = Store;
            alphabetNodeView.AppendColumn ("-", new Gtk.CellRendererText (), "text", 0);
            this.alphabetView = alphabetNodeView;
        }

        public void NewStore ()
        {
            store = new Gtk.NodeStore (typeof(LetterNode));
            foreach (char c in Alphabet()) {
                store.AddNode(new LetterNode(c.ToString()));
            }
            alphabetNodeView.NodeSelection.SelectPath(new Gtk.TreePath("0"));
        }

        public string Alphabet ()
        {
            return "ABCDEFGHIJKLMNÑOPQRSTUVQXYZ";
        }

        public void SetEventHandlerOnSelectionChanged(EventHandler e) {
            alphabetView.NodeSelection.Changed += new System.EventHandler(e);
        }
    }
}