using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;
using System.Collections;
using Mono.Unix;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("perpetrators")]
    public class Perpetrator : ActiveRecordValidationBase<Perpetrator>, ListableRecord, AffiliableRecord, AffiliatedRecord
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("victim_id"), ValidateNonEmpty]
        public Victim Victim { get; set; }
        [BelongsTo("person_id"), ValidateNonEmpty]
        public Person Person { get; set; }
        [BelongsTo("institution_id")]
        public Institution Institution { get; set; }
        [BelongsTo("perpetrator_type_id")]
        public PerpetratorType PerpetratorType { get; set; }
        [BelongsTo("perpetrator_status_id"), ValidateNonEmpty]
        public PerpetratorStatus PerpetratorStatus { get; set; }
        [BelongsTo("involvement_degree_id"), ValidateNonEmpty]
        public InvolvementDegree InvolvementDegree { get; set; }
        [BelongsTo("affiliation_type_id")]
        public AffiliationType AffiliationType { get; set; }

        [HasMany(typeof(PerpetratorAct), Cascade=ManyRelationCascadeEnum.All)]
        public IList PerpetratorActs { get; set; }

        public string[] ColumnData ()
        {
            string[] data = {
                this.Person.Lastname,
                this.Person.Firstname,
                "",
                ""
            };

            if (this.Institution != null)
            {
                data[2] = this.Institution.Name;
            }

            if (this.PerpetratorType != null) {
                data[3] = this.PerpetratorType.Name;
            }

            return data;
        }

        public string[] AffiliationColumnData ()
        {
            string personName = "";
            string roleName = Catalog.GetString("Perpetrator");
            string affiliationName = Catalog.GetString("Not defined affiiliation type in perpetrator record");
            string institutionName = "";

            if (this.Person != null) {
                personName = this.Person.Fullname;
            }

            if (this.AffiliationType != null)
            {
                affiliationName = AffiliationType.Name;
            }

            if (this.Institution != null)
            {
                institutionName = this.Institution.Name;
            }

            string[] data = {
                personName,
                affiliationName,
                institutionName,
                roleName,
                this.Victim.Act.Case.Name,
                "",
            };

            if (this.Victim.Act.start_date != null)
                data[5] = this.Victim.Act.start_date.Value.ToShortDateString ();
            return data;
        }


        public string[] AffiliatedColumnData ()
        {
            string personName = "";
            string roleName = Catalog.GetString("Perpetrator");
            string affiliationName = Catalog.GetString("Not defined affiiliation type in perpetrator record");
            string institutionName = "";

            if (this.Person != null) {
                personName = this.Person.Fullname;
            }

            if (this.AffiliationType != null)
            {
                affiliationName = AffiliationType.Name;
            }

            if (this.Institution != null)
            {
                institutionName = this.Institution.Name;
            }

            string[] data = {
                personName,
                affiliationName,
                institutionName,
                roleName,
                this.Victim.Act.Case.Name,
                "",
            };

            if (this.Victim.Act.start_date != null)
                data[5] = this.Victim.Act.start_date.Value.ToShortDateString ();

            return data;
        }

    }
}