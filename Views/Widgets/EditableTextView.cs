using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EditableTextView : Gtk.Bin, IEditable
    {
        protected bool isEditable;
        protected string text;
        protected Gtk.TextView textview;
        protected Gtk.TextBuffer buffer;

        public EditableTextView ()
        {
            this.Build ();
            buffer = new Gtk.TextBuffer (TrackTagTable.Instance);
            textview = new Gtk.TextView (buffer);
            textview.WrapMode = Gtk.WrapMode.WordChar;
            scrolledwindow.Add (textview);
            this.ShowAll ();
            this.isEditable = false;
        }

        public bool IsEditable {
            set {
                this.isEditable = value;
                if (this.isEditable) {
                    textview.Editable = true;
                    toolbar.Visible = true;
                } else {
                    textview.Editable = false;
                    toolbar.Visible = false;
                }
            }
            get {
                return this.isEditable;
            }
        }

        public String Text {
            set {
                string content = value ?? "";
                TrackBufferArchiver.Deserialize (buffer, content);
                this.text = buffer.Text;
            }
            get {
                Console.WriteLine (TrackBufferArchiver.Serialize (buffer));
                return TrackBufferArchiver.Serialize (buffer);
            }
        }

        protected void OnJustifyLeftActivated (object sender, System.EventArgs e)
        {
            textview.Justification = Gtk.Justification.Left;
        }

        protected void OnJustifyRightActivated (object sender, System.EventArgs e)
        {
            textview.Justification = Gtk.Justification.Right;
        }

        protected void OnJustifyFillActivated (object sender, System.EventArgs e)
        {
            textview.Justification = Gtk.Justification.Fill;
        }

        protected void OnBoldActivated (object sender, System.EventArgs e)
        {
            Gtk.TextTag tag = TrackTagTable.Instance.Lookup ("bold");
            Gtk.TextIter select_start, select_end;

            if (buffer.GetSelectionBounds(out select_start, out select_end)) {
                if (select_start.BeginsTag(tag) || select_start.HasTag(tag)) {
                    buffer.RemoveTag(tag, select_start, select_end);
                } else {
                    buffer.ApplyTag(tag, select_start, select_end);
                }
            }
        }

        protected void OnItalicActivated (object sender, System.EventArgs e)
        {
            Gtk.TextTag tag = TrackTagTable.Instance.Lookup ("italic");
            Gtk.TextIter select_start, select_end;

            if (buffer.GetSelectionBounds(out select_start, out select_end)) {
                if (select_start.BeginsTag(tag) || select_start.HasTag(tag)) {
                    buffer.RemoveTag(tag, select_start, select_end);
                } else {
                    buffer.ApplyTag(tag, select_start, select_end);
                }
            }
        }
    }

    public class TrackTagTable : Gtk.TextTagTable
    {
        static TrackTagTable instance;

        public static TrackTagTable Instance {
            get {
                if (instance == null) {
                    instance = new TrackTagTable ();
                }
                return instance;
            }
        }

        public TrackTagTable () : base ()
        {
            InitCommonTags ();
        }

        void InitCommonTags ()
        {
            Gtk.TextTag tag;
            tag = new Gtk.TextTag ("bold");
            tag.Weight = Pango.Weight.Bold;
            Add (tag);

            tag = new Gtk.TextTag ("italic");
            tag.Style = Pango.Style.Italic;
            Add (tag);
        }
    }

    public class TrackBufferArchiver
    {
        public static string Serialize (Gtk.TextBuffer buffer)
        {
            return Serialize (buffer, buffer.StartIter, buffer.EndIter);
        }

        public static string Serialize (Gtk.TextBuffer buffer, Gtk.TextIter start, Gtk.TextIter end)
        {
            Stack<Gtk.TextTag> tag_stack = new Stack<Gtk.TextTag> ();
            Gtk.TextIter iter = start;
            Gtk.TextIter next_iter = start;
            next_iter.ForwardChar ();
            StringBuilder sb = new StringBuilder ();

            while (!iter.Equal(end) && iter.Char != null) {

                foreach (Gtk.TextTag tag in iter.Tags) {
                    if (iter.BeginsTag (tag)) {
                        WriteTag (tag, sb, true);
                        tag_stack.Push (tag);
                    }
                }

                sb.Append (iter.Char);

                foreach (Gtk.TextTag tag in iter.Tags) {
                    if (TagEndsHere (tag, iter, next_iter)) {
                        WriteTag (tag_stack.Pop (), sb, false);
                    }
                }

                iter.ForwardChar ();
                next_iter.ForwardChar ();
            }

            while (tag_stack.Count > 0) {
                WriteTag (tag_stack.Pop (), sb, false);
            }

            return sb.ToString ();
        }

        class TagStart
        {
            public int Start;
            public Gtk.TextTag Tag;
        }

        public static void Deserialize (Gtk.TextBuffer buffer, string content)
        {
            Deserialize (buffer, buffer.StartIter, content);
        }

        public static void Deserialize (Gtk.TextBuffer buffer, Gtk.TextIter start, string content)
        {
            StringReader reader = new StringReader(content);
            int intCharacter;
            char convertedCharacter;
            int offset = start.Offset;
            string sbuffer = String.Empty;
            Stack<TagStart> stack = new Stack<TagStart> ();
            TagStart tag_start;

            while (true) {
                intCharacter = reader.Read ();

                if (intCharacter == -1) break;

                convertedCharacter = Convert.ToChar (intCharacter);

                if (sbuffer != String.Empty || convertedCharacter == '<') {
                    sbuffer += convertedCharacter;
                    if (sbuffer == "<bold>") {
                        tag_start = new TagStart ();
                        tag_start.Start = offset;
                        tag_start.Tag = buffer.TagTable.Lookup ("bold");
                        stack.Push (tag_start);
                        sbuffer = String.Empty;
                    } else if (sbuffer == "<italic>") {
                        tag_start = new TagStart ();
                        tag_start.Start = offset;
                        tag_start.Tag = buffer.TagTable.Lookup ("italic");
                        stack.Push (tag_start);
                        sbuffer = String.Empty;
                    } else if (sbuffer == "</bold>" || sbuffer == "</italic>") {
                        if (stack.Count > 0) {
                            tag_start = stack.Pop ();
                            Gtk.TextIter apply_start, apply_end;
                            apply_start = buffer.GetIterAtOffset (tag_start.Start);
                            apply_end = buffer.GetIterAtOffset (offset);
                            buffer.ApplyTag (tag_start.Tag, apply_start, apply_end);
                            sbuffer = String.Empty;
                        }
                    } else if (sbuffer.Length > 9) {
                        Gtk.TextIter insert_at = buffer.GetIterAtOffset(offset);
                        buffer.Insert (ref insert_at, sbuffer);
                        offset += sbuffer.Length;
                        sbuffer = String.Empty;
                    }
                } else {
                    Gtk.TextIter insert_at = buffer.GetIterAtOffset(offset);
                    buffer.Insert (ref insert_at, convertedCharacter.ToString ());
                    offset += 1;
                }
            }
        }

        static bool TagEndsHere (Gtk.TextTag tag, Gtk.TextIter iter, Gtk.TextIter next_iter)
        {
                return (iter.HasTag (tag) && !next_iter.HasTag(tag) || next_iter.IsEnd);
        }

        static void WriteTag (Gtk.TextTag tag, StringBuilder sb, bool start)
        {
            if (start) {
                sb.Append ("<" + tag.Name + ">");
            } else {
                sb.Append ("</" + tag.Name + ">");
            }
        }
    }
}


