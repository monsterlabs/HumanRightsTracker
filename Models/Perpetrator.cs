using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("perpetrators")]
    public class Perpetrator : ActiveRecordValidationBase<Perpetrator>, ListableRecord
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("victim_id"), ValidateNonEmpty]
        public Victim Victim { get; set; }
        [BelongsTo("person_id"), ValidateNonEmpty]
        public Person Person { get; set; }
        [BelongsTo("institution_id"), ValidateNonEmpty]
        public Institution Institution { get; set; }
        [BelongsTo("job_id")]
        public Job Job { get; set; }
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

            if (this.Job != null) {
                data[3] = this.Job.Name;
            }

            return data;
        }

    }
}