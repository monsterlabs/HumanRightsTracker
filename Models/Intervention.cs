using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;
using Mono.Unix;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("interventions")]
    public class Intervention : ActiveRecordValidationBase<Intervention>, ListableRecord, AffiliableRecord
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("case_id")]
        [ValidateNonEmpty]
        public Case Case { get; set;}

        [BelongsTo("intervention_type_id")]
        [ValidateNonEmpty]
        public InterventionType InterventionType { get; set; }

        [BelongsTo("interventor_id")]
        [ValidateNonEmpty]
        public Person Interventor { get; set; }

        [BelongsTo("interventor_institution_id")]
        [ValidateNonEmpty]
        public Institution InterventorInstitution { get; set; }

        [BelongsTo("interventor_affiliation_type_id")]
        [ValidateNonEmpty]
        public AffiliationType InterventorAffiliationType { get; set; }

        [BelongsTo("supporter_id")]
        [ValidateNonEmpty]
        public Person Supporter { get; set; }

        [BelongsTo("supporter_institution_id")]
        [ValidateNonEmpty]
        public Institution SupporterInstitution { get; set; }

        [BelongsTo("supporter_affiliation_type_id")]
        [ValidateNonEmpty]
        public AffiliationType SupporterAffiliationType { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime? Date { get; set; }

        [Property]
        public String Impact { get; set; }

        [Property]
        public String Response { get; set; }

        [HasMany(typeof(InterventionAffectedPeople), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList AffectedPeople { get; set; }

        public String InterventorName () {
            String name = "";

            if (Interventor != null)
                name = Interventor.Fullname;

            if (InterventorAffiliationType != null)
                name = " " + name + "(" + InterventorAffiliationType.Name + ")";

            if (InterventorInstitution != null)
                name = ", " + name + InterventorInstitution.Name;

            return name;
        }

        public String SupporterName () {
            String name = "";

            if (Supporter != null)
                name = Supporter.Fullname;

            if (SupporterAffiliationType != null)
                name = " " + name + "(" + SupporterAffiliationType.Name + ")";

            if (SupporterInstitution != null)
                name = ", " + name + SupporterInstitution.Name;

            return name;
        }

        public string[] ColumnData ()
        {
            string[] data = {
                this.SupporterName (),
                this.InterventorName (),
                this.InterventionType.Name,
                "",
            };

            if (this.Date.HasValue)
                data[3] = this.Date.Value.ToShortDateString ();

            return data;
        }

        public string[] AffiliationColumnData ()
        {
            string roleName = Catalog.GetString("Not defined role in intervention record");
            string affiliationTypeName = Catalog.GetString("Not defined affiliation type in intervention record");
            string institutionName = "";

            if (this.Supporter != null) {
                roleName = Catalog.GetString("Supporter");
                affiliationTypeName = this.SupporterAffiliationType.Name;
                institutionName = this.SupporterInstitution.Name;
            }
            else if (this.Interventor != null)
            {
                roleName = Catalog.GetString("Interventor");
                affiliationTypeName = this.InterventorAffiliationType.Name;
                institutionName = this.InterventorInstitution.Name;
            }

            string[] data = {
                roleName,
                affiliationTypeName,
                institutionName,
                this.Case.Name,
                "",
            };

            if (this.Date.HasValue)
                data[4] = this.Date.Value.ToShortDateString ();

            return data;
        }
    }
}

