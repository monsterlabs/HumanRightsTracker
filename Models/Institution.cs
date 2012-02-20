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
    [ActiveRecord("institutions")]
    public class Institution : ActiveRecordValidationBase<Institution>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property]
        public String Abbrev { get; set; }

        [Property]
        public String Location { get; set; }

        [Property]
        public String Phone { get; set; }

        [Property]
        public String Fax { get; set; }

        [Property]
        public String Email { get; set; }

        [Property]
        public String Url { get; set; }

        [Property("zipcode")]
        public int ZipCode { get; set; }

        [BelongsTo("institution_type_id")]
        public InstitutionType InstitutionType { get; set; }

        [BelongsTo("institution_category_id")]
        public InstitutionCategory InstitutionCategory { get; set; }

        [BelongsTo("country_id"), ValidateNonEmpty]
        public Country Country { get; set; }

        [BelongsTo("state_id")]
        public State State { get; set; }

        [BelongsTo("city_id")]
        public City City { get; set; }

        [HasMany(typeof(Perpetrator),  Table="Perpetrators", ColumnKey="institution_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Perpetrators { get; set; }

        [HasMany(typeof(Intervention),  Table="Interventions", ColumnKey="interventor_institution_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Interventors { get; set; }

        [HasMany(typeof(Intervention),  Table="Interventions", ColumnKey="supporter_institution_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Supporters { get; set; }

        [HasMany(typeof(DocumentarySource),  Table="DocumentarySource", ColumnKey="reported_institution_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList AsReportedPersonInDocumentarySources{ get; set; }

        [HasMany(typeof(InformationSource), Table="InformationSources", ColumnKey="source_institution_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList AsSourceInInformationSources { get; set; }

        [HasMany(typeof(InformationSource), Table="InformationSources", ColumnKey="reported_institution_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList AsReportedPersonInInformationSources { get; set; }

        [HasMany(typeof(InstitutionRelationship), Table="InstitutionRelationships", ColumnKey="institution_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList InstitutionRelationships { get; set; }

        public Image Photo
        {
            get
            {
                Image photo = Image.FindOne (new ICriterion[] { Restrictions.And (
                        Restrictions.Eq ("ImageableId", this.Id),
                        Restrictions.Eq ("ImageableType", "Institution")
                )});
                
                return photo;
            }

        }

        public IList caseList () {
            IList case_list = new ArrayList();

            foreach (Perpetrator p in Perpetrators)
                case_list.Add (p.Victim.Act.Case);

            foreach (Intervention i in Interventors)
                case_list.Add (i.Case);


            foreach (Intervention s in Supporters)
                case_list.Add (s.Case);

            return case_list;
        }

        public IList AffiliatedPersonList ()
        {
            IList affiliated_people = new ArrayList();

            foreach (Perpetrator p in Perpetrators)
                affiliated_people.Add (p);

            foreach (Intervention i in Interventors)
                affiliated_people.Add (i);

            foreach (Intervention i in Supporters)
                affiliated_people.Add (i);

            foreach (DocumentarySource ds in AsReportedPersonInDocumentarySources)
                affiliated_people.Add (ds);

            foreach (InformationSource infsrc in AsSourceInInformationSources)
                affiliated_people.Add (infsrc);

            foreach (InformationSource infsrc in AsReportedPersonInInformationSources)
                affiliated_people.Add (infsrc);

            return affiliated_people;
        }


        public IList perpetratorAndJobPerCase (Case c) {
            IList person_and_job_list = new ArrayList();

            foreach (Perpetrator p in Perpetrators) {
                if (p.Victim.Act.Case.Id == c.Id) {
                    ArrayList person_and_job = new ArrayList();
                    person_and_job.Add (p.Person as Person);
                    person_and_job.Add (null);
                    person_and_job_list.Add (person_and_job);
                }
            }
            return person_and_job_list;
        }

        public IList interventorAndJobPerCase (Case c) {
            IList person_and_job_list = new ArrayList();

            foreach (Intervention i in Interventors) {
                if (i.Case.Id == c.Id) {
                    ArrayList person_and_job = new ArrayList();
                    person_and_job.Add (i.Interventor  as Person);
                    person_and_job.Add (null);
                    person_and_job_list.Add (person_and_job);
                }
            }
            return person_and_job_list;
        }

        public IList supporterAndJobPerCase (Case c) {
            IList person_and_job_list = new ArrayList();

            foreach (Intervention s in Supporters) {
                if (s.Case.Id == c.Id) {
                    ArrayList person_and_job = new ArrayList();
                    person_and_job.Add (s.Supporter  as Person);
                    person_and_job.Add (null);
                    person_and_job_list.Add (person_and_job);
                }
            }
            return person_and_job_list;
        }

        public static Institution[] SimpleSearch(String searchString) {
            return Institution.FindAll (new Order[] { Order.Asc ("Name") },
            new ICriterion[] { Restrictions.Or (
                                        Restrictions.InsensitiveLike("Name", searchString, MatchMode.Anywhere),
                                        Restrictions.InsensitiveLike("Abbrev", searchString, MatchMode.Anywhere)
                                        ) }
            );
        }

        public static Institution[] FindAllOrderedByName() {
            return Institution.FindAll(new Order[] { Order.Asc ("Name") });
        }
    }
}
