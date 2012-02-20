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
    public class Perpetrator : ActiveRecordValidationBase<Perpetrator>, ListableRecord, AffiliableRecord
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("victim_id"), ValidateNonEmpty]
        public Victim Victim { get; set; }
        [BelongsTo("person_id"), ValidateNonEmpty]
        public Person Person { get; set; }
        [BelongsTo("institution_id"), ValidateNonEmpty]
        public Institution Institution { get; set; }
        [BelongsTo("perpetrator_type_id")]
        public PerpetratorType PerpetratorType { get; set; }
        [BelongsTo("perpetrator_status_id"), ValidateNonEmpty]
        public PerpetratorStatus PerpetratorStatus { get; set; }

        [HasMany(typeof(PerpetratorAct), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList PerpetratorActs { get; set; }

        public string[] ColumnData ()
        {
            string[] data = {
                this.Person.Lastname,
                this.Person.Firstname,
                this.Institution.Name,
                ""
            };

            if (this.PerpetratorType != null) {
                data[3] = this.PerpetratorType.Name;
            }

            return data;
        }

        public string[] AffiliationColumnData ()
        {
            string roleName = Catalog.GetString("Perpetrator");
            string perpetratorTypeName = Catalog.GetString("Not defined perpetrator type in perpetrator record");
            string institutionName = "";

            if (this.PerpetratorType != null)
            {
                perpetratorTypeName = PerpetratorType.Name;
            }

            if (this.Institution != null)
            {
                institutionName = this.Institution.Name;
            }

            string[] data = {
                roleName,
                perpetratorTypeName,
                institutionName,
                this.Victim.Act.Case.Name,
                "",
            };

            if (this.Victim.Act.start_date != null)
                data[4] = this.Victim.Act.start_date.Value.ToShortDateString ();

            return data;
        }


        public string[] AffiliatedColumnData ()
        {
            string personName = "";
            string roleName = Catalog.GetString("Perpetrator");
            string perpetratorTypeName = Catalog.GetString("Not defined perpetrator type in perpetrator record");
            string institutionName = "";

            if (this.Person != null) {
                personName = this.Person.Fullname;
            }

            if (this.PerpetratorType != null)
            {
                perpetratorTypeName = PerpetratorType.Name;
            }

            if (this.Institution != null)
            {
                institutionName = this.Institution.Name;
            }

            string[] data = {
                personName,
                roleName,
                perpetratorTypeName,
                institutionName,
                this.Victim.Act.Case.Name,
                "",
            };

            if (this.Victim.Act.start_date != null)
                data[5] = this.Victim.Act.start_date.Value.ToShortDateString ();

            return data;
        }

    }
}