using System;
using System.Collections;
using System.Collections.Generic;
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

        [HasMany(typeof(CaseRelationship),  Table="CaseRelationships", ColumnKey="related_case_id", Cascade=ManyRelationCascadeEnum.None, Lazy=true)]
        public IList CaseRelationshipsAsRelatedCase { get; set; }

        public IList victimList () {
             IList victim_list = new ArrayList();
             foreach (Act act in Acts)
                foreach (Victim victim in act.Victims)
                    victim_list.Add (victim.Person);

            return victim_list;
        }

        public static Case[] SimpleSearch(String searchString) {
            return Case.FindAll (new Order[] { Order.Asc ("Name") },
            new ICriterion[] { Restrictions.InsensitiveLike("Name", searchString, MatchMode.Anywhere) }
            );
        }

        public static Case[] FindAllOrderedByName() {
            return Case.FindAll(new Order[] { Order.Asc ("Name") });
        }

        public String[] ToReportArray ()
        {
            string AffectedRight = "";
            string Acts = "";
            string Statuses = "";

            foreach (Act act in this.Acts)
            {
                AffectedRight += act.HumanRightsViolationCategory.Name + ", ";
                Acts += act.HumanRightsViolation.Name + ", ";
                if (act.ActStatus != null)
                    Statuses += act.ActStatus.Name + ", ";
                else
                    Statuses +=  "Ninguno, ";
            }

            string startDate = this.start_date == null ? "" : this.start_date.Value.ToShortDateString ();
            string endDate = this.end_date == null ? "" : this.end_date.Value.ToShortDateString ();

            return new String[] {
                this.Name,
                AffectedRight,
                Acts,
                Statuses,
                this.victimList ().Count.ToString (),
                startDate,
                endDate
            };
        }

           public Boolean HasRelateRecords {
            get {
                //IList cases = CaseRelationships;
                IList case_as_related_cases = CaseRelationshipsAsRelatedCase;
                return (case_as_related_cases.Count > 0);
            }
        }

    }
}

