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
    public class CaseReportGenerator : ReportGenerator
    {
        public CaseReportGenerator (Case acase)
        {
            addBold ("Reporte narrativo de caso");
            addField("Fecha de expedición", DateTime.Now.ToShortDateString ());

            addTitle ("DATOS GENERALES");

            addField ("Nombre del caso", acase.Name);
            addField ("Fecha de inicio", acase.start_date.Value.ToShortDateString ());
            foreach (Place place in acase.Places)
            {

                addField ("Localidades", String.Format ("{0}, {1}, {2}",
                                                      place.City.Name, place.State.Name, place.Country.Name));
            }
            addField ("No. personas afectadas", acase.AffectedPeople.ToString ());

            addTitle ("SEGUIMIENTO DEL CASO");

            addField ("Resumen", acase.Summary);
            addNewline ();
            addField ("Observaciones", acase.Observations);

            addNewline();

            foreach (TrackingInformation info in acase.TrackingInformation)
            {
                addBold (info.Title);
                addField ("Fecha", info.DateOfReceipt.Value.ToShortDateString ());
                addField ("Comentarios", info.Comments);
                addNewline();
            }

            //save
            //document.SaveTo ("Letter.odt");
            //document.SaveTo ("Letter.pdf", new PDFExporter ());
        }
    }
}

