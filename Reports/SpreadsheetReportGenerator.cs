using System;
using System.IO;
using AODL.Document.TextDocuments;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Content.Tables;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.Content.Text;
using AODL.ExternalExporter.PDF;

namespace Reports
{
    public class SpreadsheetReportGenerator : TextReportGenerator
    {
        protected new SpreadsheetDocument document;
        protected Table table;

        public SpreadsheetReportGenerator ()
        {
            //Create new spreadsheet document
            document = new SpreadsheetDocument();
            document.New();
            table = TableBuilder.CreateSpreadsheetTable (document, "First", "");
        }

        public void AddHeader (int rowIdx, String[] values)
        {
            for (int i = 0, max = values.Length; i < max; i++) {
                Cell cell = table.CreateCell ();
                cell.OfficeValueType = "string";

                Paragraph paragraph = ParagraphBuilder.CreateSpreadsheetParagraph(document);
                FormatedText formText = new FormatedText(document, "T1", values[i]);
                formText.TextStyle = BOLD_STYLE;
                paragraph.TextContent.Add(formText);
                cell.Content.Add(paragraph);

                table.InsertCellAt(rowIdx, i, cell);
            }
        }

        public void AddRow (int rowIdx, String[] values)
        {
            for (int i = 0, max = values.Length; i < max; i++) {
                Cell cell = table.CreateCell ();
                cell.OfficeValueType = "string";

                Paragraph paragraph = ParagraphBuilder.CreateSpreadsheetParagraph(document);
                paragraph.TextContent.Add(new SimpleText(document, values[i]));
                cell.Content.Add(paragraph);

                table.InsertCellAt(rowIdx, i, cell);
            }
        }

        public new void SaveTo (String name)
        {
            document.TableCollection.Add(table);
            document.SaveTo (name);
        }

    }
}

