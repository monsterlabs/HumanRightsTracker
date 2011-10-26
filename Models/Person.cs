using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;
using System.Collections;
using System.Collections.Generic;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("people")]
    public class Person : ActiveRecordValidationBase<Person>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Firstname { get; set; }

        [Property]
        [ValidateNonEmpty]
        public String Lastname { get; set; }

        [Property]
        [ValidateNonEmpty]
        public Boolean Gender { get; set; }

        [Property]
        public String Alias { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime Birthday { get; set; }

        [Property]
        public String Settlement { get; set; }

        [Property("is_immigrant")]
        [ValidateNonEmpty]
        public Boolean IsImmigrant { get; set; }

        [Property]
        public String Email { get; set; }

        [BelongsTo("country_id"), ValidateNonEmpty]
        public Country Country { get; set; }
        [BelongsTo("state_id")]
        public State State { get; set; }
        [BelongsTo("city_id")]
        public City City { get; set; }
        [BelongsTo("marital_status_id"), ValidateNonEmpty]
        public MaritalStatus MaritalStatus { get; set; }


        private IList details = new ArrayList();
        [HasMany(typeof(PersonDetail), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList PersonDetails
        {
            get { return details; }
            set { details = value; }
        }

        private IList immigration_attempts = new ArrayList();
        [HasMany(typeof(ImmigrationAttempt), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList ImmigrationAttempts
        {
            get { return immigration_attempts; }
            set { immigration_attempts = value; }
        }

        private IList addresses = new ArrayList();
        [HasMany(typeof(Address), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList Addresses
        {
            get { return addresses; }
            set { addresses = value; }
        }

        private IList identifications = new ArrayList();
        [HasMany(typeof(Identification), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList Identifications
        {
            get { return identifications; }
            set { identifications = value; }
        }

        private IList victims = new ArrayList();
        [HasMany(typeof(Victim), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList Victims
        {
            get { return victims; }
            set { victims = value; }
        }

        private IList perpetrators = new ArrayList();
        [HasMany(typeof(Perpetrator),  Table="Perpetrators", ColumnKey="person_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList Perpetrators
        {
            get { return perpetrators;}
            set { perpetrators = value; }
        }

        private IList interventors = new ArrayList();
        [HasMany(typeof(Intervention),  Table="Interventions", ColumnKey="interventor_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList Interventors
        {
            get { return interventors; }
            set { interventors = value; }
        }

        private IList supporters = new ArrayList();
        [HasMany(typeof(Intervention),  Table="Interventions", ColumnKey="supporter_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList Supporters
        {
            get { return supporters; }
            set { supporters = value; }
        }

        private  IList institution_and_job_in_perpetrations = new ArrayList();
        [HasMany(typeof(Perpetrator), Table="Perpetrators", ColumnKey="person_id", Where = "institution_id IS NOT NULL")]
        public IList InstitutionAndJobInPerpetrations
        {
            get { return institution_and_job_in_perpetrations; }
            set { institution_and_job_in_perpetrations = value; }
        }

        private  IList institution_and_job_as_interventors = new ArrayList();
        [HasMany(typeof(Intervention), Table="Interventions", ColumnKey="interventor_id", Where = "interventor_institution_id IS NOT NULL")]
        public IList InstitutionAndJobAsInterventors
        {
            get { return institution_and_job_as_interventors; }
            set { institution_and_job_as_interventors = value; }
        }

        private  IList institution_and_job_as_supporters = new ArrayList();
        [HasMany(typeof(Intervention), Table="Interventions", ColumnKey="supporter_id", Where = "supporter_institution_id IS NOT NULL")]
        public IList InstitutionAndJobAsSupporters
        {
            get { return institution_and_job_as_supporters; }
            set { institution_and_job_as_supporters = value; }
        }

        public String Fullname
        {
            get
            {
                return Lastname + " " +  Firstname;
            }
        }

        public Image Photo
        {
            get
            {
                Image photo = Image.FindOne (new ICriterion[] { Restrictions.And (
                        Restrictions.Eq ("ImageableId", this.Id),
                        Restrictions.Eq ("ImageableType", "Person")
                )});
                return photo;
            }

        }

        public IList caseList () {
            IList case_list = new ArrayList();

            foreach (Victim v in Victims)
                case_list.Add (v.Act.Case);

            foreach (Perpetrator p in Perpetrators)
                case_list.Add (p.Victim.Act.Case);

            foreach (Intervention i in Interventors)
                case_list.Add (i.Case);

            foreach (Intervention s in Supporters)
                case_list.Add (s.Case);

            return case_list;
        }

        public IList institutionAndJobList () {
            IList institutions_and_jobs = new ArrayList();

            foreach (Perpetrator p in InstitutionAndJobInPerpetrations) {
                ArrayList institution_and_job = new ArrayList();
                institution_and_job.Add (p.Institution as Institution);
                institution_and_job.Add (p.Job as Job);
                institutions_and_jobs.Add (institution_and_job);
            }

            foreach (Intervention i in InstitutionAndJobAsInterventors) {
                ArrayList institution_and_job = new ArrayList();
                institution_and_job.Add (i.InterventorInstitution as  Institution);
                institution_and_job.Add (i.InterventorJob as Job);
                institutions_and_jobs.Add (institution_and_job);
            }

            foreach (Intervention s in InstitutionAndJobAsSupporters) {
                ArrayList institution_and_job = new ArrayList();
                institution_and_job.Add (s.SupporterInstitution as  Institution);
                institution_and_job.Add (s.SupporterJob as Job);
                institutions_and_jobs.Add (institution_and_job);
            }
            return institutions_and_jobs;
        }
    }
}
