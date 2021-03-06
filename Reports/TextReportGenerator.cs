using System;
using System.IO;
using AODL.Document.TextDocuments;
using AODL.Document.Content.Tables;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.Content.Text;
using AODL.ExternalExporter.PDF;


namespace Reports
{
    public class TextReportGenerator
    {
        protected TextDocument document;
        protected TextStyle BOLD_STYLE;
        protected TextStyle BOLD_UNDERLINE_STYLE;

        //public event EventHandler OverrideConfirmation;
        public delegate bool OverrideConfirmationDelegate (String name);

        public OverrideConfirmationDelegate OverrideConfirmation;

        public TextReportGenerator ()
        {
            iTextSharp.text.Document.Compress = false;
            document = new TextDocument();
            document.New();

            BOLD_STYLE = new TextStyle (document, "boldML");
            BOLD_STYLE.TextProperties.Bold = "bold";
            BOLD_STYLE.TextProperties.FontName = "Times-Roman";
            document.Styles.Add (BOLD_STYLE);

            BOLD_UNDERLINE_STYLE = new TextStyle (document, "boldUndelineML");
            BOLD_UNDERLINE_STYLE.TextProperties.Bold = "bold";
            BOLD_UNDERLINE_STYLE.TextProperties.FontName = "Times-Roman";
            BOLD_UNDERLINE_STYLE.TextProperties.Underline = "solid";
            document.Styles.Add (BOLD_UNDERLINE_STYLE);

        }

        public TextDocument Document {
            get {
                return this.document;
            }
        }
        protected Paragraph addBold (String text)
        {
            Paragraph paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);
            FormatedText formText = new FormatedText(document, "T1", text);
            formText.TextStyle = BOLD_STYLE;
            paragraph.TextContent.Add(formText);
            document.Content.Add(paragraph);
            return paragraph;
        }

        protected Paragraph addTitle (String text)
        {
            addNewline ();
            Paragraph paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);
            FormatedText formText = new FormatedText(document, "T1", text);
            formText.TextStyle = BOLD_UNDERLINE_STYLE;
            paragraph.TextContent.Add(formText);
            //paragraph.ParagraphStyle = new ParagraphStyle (document, "foo");
            //paragraph.ParagraphStyle.ParagraphProperties.Alignment = TextAlignments.center.ToString ();
            document.Content.Add(paragraph);
            addNewline ();
            return paragraph;
        }

        protected Paragraph addField (String name, String text)
        {
            Paragraph paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);
            FormatedText formText = new FormatedText(document, "T1", name + " ");
            formText.TextStyle = BOLD_STYLE;
            paragraph.TextContent.Add(formText);
            paragraph.TextContent.Add(new SimpleText(document, text));
            document.Content.Add(paragraph);

            return paragraph;
        }

        protected void addNewline ()
        {
            Paragraph paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);
            paragraph.TextContent.Add(new SimpleText(document, ""));
            document.Content.Add(paragraph);
        }

        protected bool Save(String name) {
            if (!File.Exists (name) || (OverrideConfirmation != null && OverrideConfirmation (name))) {
                return true;
            }
            return false;
        }

        public void SaveTo (String name)
        {
            if (Save (name + ".odt"))
                document.SaveTo (name + ".odt");
            if (Save (name + ".pdf"))
                document.SaveTo (name + ".pdf", new PDFExporter ());
        }
    }
}

