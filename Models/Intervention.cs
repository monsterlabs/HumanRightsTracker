using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;
using Mono.Unix;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("interventions")]
    public class Intervention : ActiveRecordValidationBase<Intervention>, ListableRecord, AffiliableRecord, AffiliatedRecord
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
        public Institution InterventorInstitution { get; set; }

        [BelongsTo("interventor_affiliation_type_id")]
        public AffiliationType InterventorAffiliationType { get; set; }

        [BelongsTo("supporter_id")]
        public Person Supporter { get; set; }

        [BelongsTo("supporter_institution_id")]
        public Institution SupporterInstitution { get; set; }

        [BelongsTo("supporter_affiliation_type_id")]
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
                name = name + " (" + InterventorAffiliationType.Name + ")";

            if (InterventorInstitution != null)
                name =  name + ", " + InterventorInstitution.Name;

            return name;
        }

        public String SupporterName () {
            String name = "";

            if (Supporter != null)
                name = Supporter.Fullname;

            if (SupporterAffiliationType != null)
                name = name + " (" + SupporterAffiliationType.Name + ")";

            if (SupporterInstitution != null)
                name = name + ", " +  SupporterInstitution.Name;

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
            string personName = "";
            string roleName = Catalog.GetString("Not defined role in intervention record");
            string affiliationTypeName = Catalog.GetString("Not defined affiliation type in intervention record");
            string institutionName = "";

            if (this.Supporter != null) {
                roleName = Catalog.GetString("Supporter");

                if (this.SupporterAffiliationType != null) {
                    affiliationTypeName = this.SupporterAffiliationType.Name;
                }

                if (this.SupporterInstitution != null) {
                    institutionName = this.SupporterInstitution.Name;
                }

                personName = this.Supporter.Fullname;
            }

            if (this.Interventor != null)
            {
                roleName = Catalog.GetString("Interventor");
                if (this.InterventorAffiliationType != null) {
                    affiliationTypeName = this.InterventorAffiliationType.Name;
                }

                if (this.InterventorInstitution != null) {
                    institutionName = this.InterventorInstitution.Name;
                }

                personName = this.Interventor.Fullname;
            }

            string[] data = {
                personName,
                affiliationTypeName,
                institutionName,
                roleName,
                this.Case.Name,
                "",
            };

            if (this.Date.HasValue)
                data[5] = this.Date.Value.ToShortDateString ();

            return data;
        }


        public string[] AffiliatedColumnData ()
        {
            string personName = "";
            string roleName = Catalog.GetString("Not defined role in intervention record");
            string affiliationTypeName = Catalog.GetString("Not defined affiliation type in intervention record");
            string institutionName = "";

            if (this.Supporter != null) {
                personName = this.Supporter.Fullname;
                roleName = Catalog.GetString("Supporter");

                if (this.SupporterAffiliationType != null) {
                    affiliationTypeName = this.SupporterAffiliationType.Name;
                }

                if (this.SupporterInstitution != null) {
                    institutionName = this.SupporterInstitution.Name;
                }
            }

            if (this.Interventor != null)
            {
                personName = this.Interventor.Fullname;
                roleName = Catalog.GetString("Interventor");

                if (this.InterventorAffiliationType != null) {
                    affiliationTypeName = this.InterventorAffiliationType.Name;
                }

                if (this.InterventorInstitution != null) {
                    institutionName = this.InterventorInstitution.Name;
                }
            }

            string[] data = {
                personName,
                affiliationTypeName,
                institutionName,
                roleName,
                this.Case.Name,
                "",
            };

            if (this.Date.HasValue)
                data[5] = this.Date.Value.ToShortDateString ();

            return data;
        }
    }
}

