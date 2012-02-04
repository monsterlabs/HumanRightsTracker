using System;
using AODL.Document.TextDocuments;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Content.Tables;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.Content.Text;
using AODL.ExternalExporter.PDF;

using HumanRightsTracker.Models;

namespace Reports
{
    public class CasesReportGenerator : SpreadsheetReportGenerator
    {
        public CasesReportGenerator (Case[] cases)
        {
            // AddHeader (0, new String[] {"A", "B"});
            for (int i = 1, max = cases.Length; i < max; i++) {
                AddRow (i, new String[] {"a", "b"});
            }
        }



    }
}

