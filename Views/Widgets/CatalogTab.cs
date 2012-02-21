using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CatalogTab : Gtk.Bin
    {
        public CatalogTab ()
        {
            this.Build ();

            String[] catalogs = {
                Mono.Unix.Catalog.GetString("ActPlace"),
                Mono.Unix.Catalog.GetString("Language"),
                Mono.Unix.Catalog.GetString("HumanRightsViolationCategory"),
                Mono.Unix.Catalog.GetString("HumanRightsViolation"),
                Mono.Unix.Catalog.GetString("LocationType"),
                Mono.Unix.Catalog.GetString("City"),
                Mono.Unix.Catalog.GetString("State"),
                Mono.Unix.Catalog.GetString("Country"),
                Mono.Unix.Catalog.GetString("AffiliationType"),
                Mono.Unix.Catalog.GetString("IdentificationType"),
                Mono.Unix.Catalog.GetString("SourceInformationType"),
                Mono.Unix.Catalog.GetString("MaritalStatus"),
                Mono.Unix.Catalog.GetString("TravelCompanion"),
                Mono.Unix.Catalog.GetString("CaseStatus"),
                Mono.Unix.Catalog.GetString("IndigenousLanguage"),
                Mono.Unix.Catalog.GetString("RelationshipType"),
                Mono.Unix.Catalog.GetString("TravelingReason"),
                Mono.Unix.Catalog.GetString("InstitutionCategory"),
                Mono.Unix.Catalog.GetString("ReliabilityLevel"),
                Mono.Unix.Catalog.GetString("VictimStatus"),
                Mono.Unix.Catalog.GetString("InstitutionType"),
                Mono.Unix.Catalog.GetString("Religion"),
                Mono.Unix.Catalog.GetString("InterventionType"),
                Mono.Unix.Catalog.GetString("EthnicGroup"),
                Mono.Unix.Catalog.GetString("Job"),
                Mono.Unix.Catalog.GetString("ScholarityLevel"),
                Mono.Unix.Catalog.GetString("AddressType")
            };



            notebook1.Remove (notebook1.Children[0]);

            foreach (String catalog in catalogs) {
                CatalogCRUD catalogcrud = new CatalogCRUD ();
                catalogcrud.Model = catalog;

                notebook1.Add (catalogcrud);

                // Notebook tab
                Gtk.Label label = new Gtk.Label ();
                label.LabelProp = catalog;
                notebook1.SetTabLabel (catalogcrud, label);
                label.ShowAll ();
            }


        }
    }
}

