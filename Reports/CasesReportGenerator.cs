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
            AddHeader (0, new String[] {"Nombre del caso", "Derecho afectado", "Tipo de actos", "Estado de los actos", "Número de Víctimas", "Fecha de inicio", "Fecha de término"});
            for (int i = 0, max = cases.Length; i < max; i++) {
                Case acase = cases[i];
                AddRow (i+1, acase.ToReportArray ());
            }
        }



    }
}
