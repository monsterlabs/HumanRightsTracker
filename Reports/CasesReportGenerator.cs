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
            /*
            * Nombre del caso, incluir los lugares
            * Derecho afectado
            * Tipo de violación o acto, incluyendo lugar
            * Número de perpetrador, tipos de perpetradores (jobs)
            * Número de víctimas
            * Estado del caso < El último estado en el seguimiento del caso
            * Fechas de cada acto
            * Fecha de inicio y termino del caso
            */

            AddHeader (0, new String[] {"Nombre del caso", "Número de Víctimas"});
            for (int i = 1, max = cases.Length; i < max; i++) {
                Case acase = cases[i];
                AddRow (i, new String[] {acase.Name, acase.victimList ().Count.ToString ()});
            }
        }



    }
}
