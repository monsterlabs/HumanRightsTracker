using System;
using AODL.Document.TextDocuments;
using AODL.Document.Content.Tables;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.Content.Text;
using AODL.ExternalExporter.PDF;

using HumanRightsTracker.Models;

namespace Reports
{
    public class CaseReportGenerator
    {
        public CaseReportGenerator ()//(Case acase)
        {
            iTextSharp.text.Document.Compress = false;
            TextDocument document = new TextDocument();
            document.New();

            Paragraph paragraph;
            FormatedText formText;

            TextStyle boldStyle = new TextStyle (document, "boldML");
            boldStyle.TextProperties.Bold = "bold";
            boldStyle.TextProperties.FontName = "Times-Roman";
            document.Styles.Add (boldStyle);

            TextStyle boldUnderlineStyle = new TextStyle (document, "boldUndelineML");
            boldUnderlineStyle.TextProperties.Bold = "bold";
            boldUnderlineStyle.TextProperties.FontName = "Times-Roman";
            boldUnderlineStyle.TextProperties.Underline = "solid";
            document.Styles.Add (boldUnderlineStyle);

            paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);
            formText = new FormatedText(document, "T1", "Reporte narrativo de caso");
            formText.TextStyle = boldStyle;
            paragraph.TextContent.Add(formText);
            document.Content.Add(paragraph);

            paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);
            formText = new FormatedText(document, "T1", "Fecha de expedición");
            formText.TextStyle = boldStyle;
            paragraph.TextContent.Add(formText);
            document.Content.Add(paragraph);

            paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);
            paragraph.TextContent.Add(new SimpleText(document, "01/01/01"));
            document.Content.Add(paragraph);

            paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);
            formText = new FormatedText(document, "T1", "DATOS GENERALES");
            formText.TextStyle = boldUnderlineStyle;
            paragraph.TextContent.Add(formText);
            //paragraph.ParagraphStyle = new ParagraphStyle (document, "foo");
            //paragraph.ParagraphStyle.ParagraphProperties.Alignment = TextAlignments.center.ToString ();
            document.Content.Add(paragraph);


            /*
            paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);

            paragraph.TextContent.Add(new SimpleText(document, "DATOS GENERALES"));
            document.Content.Add(paragraph);
            */
            //save
            document.SaveTo ("Letter.odt");
            document.SaveTo ("Letter.pdf", new PDFExporter ());
        }
    }
}

