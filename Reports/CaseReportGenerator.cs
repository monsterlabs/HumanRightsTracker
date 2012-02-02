using System;
using AODL.Document.TextDocuments;
using AODL.Document.Content.Text;
using AODL.ExternalExporter.PDF;

namespace Reports
{
    public class CaseReportGenerator
    {
        public CaseReportGenerator ()
        {
            //Create a new text document
            string someText     = "Max Mustermann\nMustermann Str. 300\n22222 Hamburg\n\n\n\n"
                                 +"Heinz Willi\nDorfstr. 1\n22225 Hamburg\n\n\n\n"
                                 +"Offer for 200 Intel Pentium 4 CPU's\n\n\n\n"
                                 +"Dear Mr. Willi,\n\n\n\n"
                                 +"thank you for your request. \tWe can     offer you the 200 Intel Pentium IV 3 Ghz CPU's for a price of 79,80 Ä per unit."
                                 +"This special offer is valid to 31.10.2005. If you accept, we can deliver within 24 hours.\n\n\n\n"
                                 +"Best regards \nMax Mustermann";

         //Create new TextDocument
         iTextSharp.text.Document.Compress = false;

         TextDocument document               = new TextDocument();
         document.New();
         //Use the ParagraphBuilder to split the string into ParagraphCollection
         ParagraphCollection pCollection     = ParagraphBuilder.CreateParagraphCollection(
                                                 document,
                                                 someText,
                                                 true,
                                                 ParagraphBuilder.ParagraphSeperator);
         //Add the paragraph collection
         foreach(Paragraph paragraph in pCollection)
             document.Content.Add(paragraph);

            //save
            document.SaveTo ("Letter.odt");
            document.SaveTo ("Letter.pdf", new PDFExporter ());
        }
    }
}

