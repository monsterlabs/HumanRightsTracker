using System;
using System.Collections;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("cases")]
    public class Case : ActiveRecordValidationBase<Case>
    {
        protected IList acts;

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property("affected_persons")]
        public int AffectedPeople { get; set; }

        [Property("record_count")]
        public int RecordCount { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime? start_date { get; set; }

        [Property]
        public DateTime? end_date { get; set; }

        [Property("narrative_description")]
        public String NarrativeDescription { get; set; }

        [Property("summary")]
        public String Summary { get; set; }

        [Property("observations")]
        public String Observations { get; set; }

        [BelongsTo("start_date_type_id")]
        [ValidateNonEmpty]
        public DateType StartDateType { get; set; }

        [BelongsTo("end_date_type_id")]
        public DateType EndDateType { get; set; }

        [HasMany(typeof(TrackingInformation), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList TrackingInformation { get; set; }

        [HasMany(typeof(Place), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Places { get; set; }

        [HasMany(typeof(Act), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Acts { get; set; }

        [HasMany(typeof(Intervention), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Interventions { get; set; }

        [HasMany(typeof(InformationSource), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList InformationSources { get; set; }

        [HasMany(typeof(DocumentarySource), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList DocumentarySources { get; set; }

        [HasMany(typeof(CaseRelationship), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList CaseRelationships { get; set; }

        public IList victimList () {
             IList victim_list = new ArrayList();
             foreach (Act act in Acts)
                foreach (Victim victim in act.Victims)
                    victim_list.Add (victim.Person);

            return victim_list;
        }

    }
}

