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
                "ActPlace", "Language", "HumanRightsViolationCategory", 
                "LocationType", "City", "State", "Country","AffiliationType","IdentificationType",
                "SourceInformationType","MaritalStatus","TravelCompanion","CaseStatus",
                "IndigenousLanguage","RelationshipType","TravelingReason","InstitutionCategory",
                "ReliabilityLevel","VictimStatus","InstitutionType","Religion","InterventionType",
                "EthnicGroup","Job","ScholarityLevel", "AddressType"
            };



            notebook1.Remove (notebook1.Children[0]);

            foreach (String catalog in catalogs) {
                CatalogCRUD catalogcrud = new CatalogCRUD ();
                catalogcrud.Model = catalog;

                notebook1.Add (catalogcrud);

                // Notebook tab
                Gtk.Label label = new Gtk.Label ();
                label.LabelProp = Mono.Unix.Catalog.GetString (catalog);
                notebook1.SetTabLabel (catalogcrud, label);
                label.ShowAll ();
            }


        }
    }
}

