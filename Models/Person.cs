using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Queries;
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
        public DateTime Birthday { get; set; }

        [Property]
        public String Settlement { get; set; }

        [Property("is_immigrant")]
        public Boolean IsImmigrant { get; set; }

        [Property]
        public String Email { get; set; }

        [BelongsTo("country_id")]
        public Country Country { get; set; }
        [BelongsTo("state_id")]
        public State State { get; set; }
        [BelongsTo("city_id")]
        public City City { get; set; }
        [BelongsTo("marital_status_id")]
        public MaritalStatus MaritalStatus { get; set; }
        [BelongsTo("citizen_id")]
        public Country Citizen { get; set; }

        [HasMany(typeof(PersonDetail), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList PersonDetails { get; set; }

        [HasMany(typeof(ImmigrationAttempt), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList ImmigrationAttempts { get; set; }

        [HasMany(typeof(Address), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Addresses { get; set; }

        [HasMany(typeof(Identification), Table="Identifications", ColumnKey="person_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Identifications { get; set;}

        [HasMany(typeof(Victim), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Victims { get; set; }

        [HasMany(typeof(Perpetrator),  Table="Perpetrators", ColumnKey="person_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Perpetrators { get; set; }

        [HasMany(typeof(Intervention),  Table="Interventions", ColumnKey="interventor_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Interventors { get; set; }

        [HasMany(typeof(Intervention),  Table="Interventions", ColumnKey="supporter_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Supporters { get; set; }

        [HasMany(typeof(Perpetrator), Table="Perpetrators", ColumnKey="person_id", Where = "institution_id IS NOT NULL", Lazy=true)]
        public IList InstitutionAndJobInPerpetrations { get; set; }

        [HasMany(typeof(Intervention), Table="Interventions", ColumnKey="interventor_id", Where = "interventor_institution_id IS NOT NULL", Lazy=true)]
        public IList InstitutionAndJobAsInterventors { get; set; }

        [HasMany(typeof(Intervention), Table="Interventions", ColumnKey="supporter_id", Where = "supporter_institution_id IS NOT NULL", Lazy=true)]
        public IList InstitutionAndJobAsSupporters { get; set; }

        [HasMany(typeof(PersonRelationship), Table="PersonRelationships", ColumnKey="person_id", Cascade=ManyRelationCascadeEnum.All, Lazy=true)]
        public IList PersonRelationships { get; set; }

        [HasMany(typeof(PersonRelationship), Table="PersonRelationships", ColumnKey="related_person_id", Cascade=ManyRelationCascadeEnum.None, Lazy=true)]
        public IList PersonRelationshipsAsRelatedPerson { get; set; }

        [HasMany(typeof(DocumentarySource), Table="DocumentarySources", ColumnKey="reported_person_id", Cascade=ManyRelationCascadeEnum.None, Lazy=true)]
        public IList AsReportedPersonInDocumentarySources { get; set; }

        [HasMany(typeof(InformationSource), Table="InformationSources", ColumnKey="source_person_id", Cascade=ManyRelationCascadeEnum.None, Lazy=true)]
        public IList AsSourceInInformationSources { get; set; }

        [HasMany(typeof(InformationSource), Table="InformationSources", ColumnKey="reported_person_id", Cascade=ManyRelationCascadeEnum.None, Lazy=true)]
        public IList AsReportedPersonInInformationSources { get; set; }

        [HasMany(typeof(InstitutionPerson), Table="InstitutionPeople", ColumnKey="person_id", Cascade=ManyRelationCascadeEnum.All, Lazy=true)]
        public IList InstitutionPeople { get; set; }

        public String Fullname
        {
            get
            {
                return Lastname + " " +  Firstname;
            }
        }

        public Photo Photo
        {
            get
            {
                return new Photo (this.Id, "Person");
            }

        }

        public IList AffiliationList() {
            IList affiliations = new ArrayList ();

             foreach (Perpetrator p in Perpetrators)
               affiliations.Add (p);

            foreach (Intervention i in InstitutionAndJobAsInterventors)
                affiliations.Add (i);

            foreach (Intervention i in InstitutionAndJobAsSupporters)
                affiliations.Add (i);

            foreach (DocumentarySource ds in AsReportedPersonInDocumentarySources)
                affiliations.Add (ds);

            foreach (InformationSource infs in AsSourceInInformationSources)
                affiliations.Add (infs);

            foreach (InformationSource infs in AsReportedPersonInInformationSources)
                affiliations.Add (infs);

            return affiliations;
        }

        public Boolean HasRelateRecords {
            get {
                IList affiliations = AffiliationList ();
                IList relationshipsAsRelatedPerson = PersonRelationshipsAsRelatedPerson;
                return (affiliations.Count > 0 ||relationshipsAsRelatedPerson.Count > 0);
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
                institution_and_job.Add (null);
                institutions_and_jobs.Add (institution_and_job);
            }

            foreach (Intervention i in InstitutionAndJobAsInterventors) {
                ArrayList institution_and_job = new ArrayList();
                institution_and_job.Add (i.InterventorInstitution as  Institution);
                institution_and_job.Add (null);
                institutions_and_jobs.Add (institution_and_job);
            }

            foreach (Intervention s in InstitutionAndJobAsSupporters) {
                ArrayList institution_and_job = new ArrayList();
                institution_and_job.Add (s.SupporterInstitution as  Institution);
                institution_and_job.Add (null);
                institutions_and_jobs.Add (institution_and_job);
            }
            return institutions_and_jobs;
        }

        public static ArrayList FindVictims(Boolean IsImmigrant, String searchString) {
            String hql = "select p from Person p inner join p.Victims as v where p.IsImmigrant = :IsImmigrant and p.Id in v.Person.Id";
            return (ArrayList)ExecuteFilter (hql,IsImmigrant, searchString);
        }

        public static ArrayList FindPerpetrators(Boolean IsImmigrant, String searchString) {
            String hql = "select p from Person p inner join p.Perpetrators as pp where p.IsImmigrant = :IsImmigrant and p.Id in pp.Person.Id";
            return (ArrayList)ExecuteFilter (hql, IsImmigrant, searchString);
        }

        public static ArrayList FindInterventors(Boolean IsImmigrant, String searchString) {
            String hql = "select p from Person p inner join p.Interventors as i where p.IsImmigrant = :IsImmigrant and p.Id in i.Interventor.Id";
            return (ArrayList)ExecuteFilter (hql, IsImmigrant, searchString);
        }

        public static ArrayList FindSupporters(Boolean IsImmigrant, String searchString) {
            String hql = "select p from Person p inner join p.Interventors as i where p.IsImmigrant = :IsImmigrant and p.Id in i.Supporter.Id";
            return (ArrayList)ExecuteFilter (hql, IsImmigrant, searchString);
        }

        protected static ArrayList ExecuteFilter(String hql, Boolean IsImmigrant, String searchString) {
            if (searchString != null) {
                hql += " and (lower(p.Firstname) like lower(:SearchString) or lower(p.Lastname) like lower(:SearchString))";
            }
            hql += " order by p.Lastname asc, p.Firstname asc";
            HqlBasedQuery query = new HqlBasedQuery(typeof(Person), hql);
            query.SetParameter("IsImmigrant", IsImmigrant);
            if (searchString != null)
                query.SetParameter("SearchString", '%' + searchString + '%');

           return (ArrayList)ActiveRecordMediator.ExecuteQuery(query);
        }

        public static Person[] FindAllByPersonType(bool isImmigrant) {
            return Person.FindAll(new Order[] { Order.Asc ("Lastname"), Order.Asc("Firstname") }, isImmigrantCriterion (isImmigrant));
        }

        public static Person[] SimpleSearch(String searchString, bool isImmigrant) {
            return Person.FindAll (new Order[] { Order.Asc ("Lastname"), Order.Asc("Firstname") },
                                   new ICriterion[] {
                                       Restrictions.Or (
                                        Restrictions.InsensitiveLike("Firstname", searchString, MatchMode.Anywhere),
                                        Restrictions.InsensitiveLike("Lastname", searchString, MatchMode.Anywhere)
                                        ), isImmigrantCriterion (isImmigrant)
                                   });
        }

        protected static ICriterion isImmigrantCriterion (Boolean isImmigrant) {
            ICriterion criterion = Restrictions.Eq("IsImmigrant", isImmigrant);
            return criterion;
        }
    }
}
