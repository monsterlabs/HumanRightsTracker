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
    [ActiveRecord("human_rights_violations")]
    public class HumanRightsViolation : ActiveRecordValidationBase<HumanRightsViolation>
    {
        public HumanRightsViolation ()
        {
        }

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property("category_id")]
        public int CategoryId { get; set; }

        [Property("parent_id")]
        public int ParentId { get; set; }

        [Property]
        public String Notes { get; set; }

        private IList children = new ArrayList();
        [HasMany(typeof(HumanRightsViolation),  Table="HumanRightsViolations", ColumnKey="parent_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true, OrderBy="Name Asc")]
        public IList Children
        {
            get { return children;}
            set { children = value; }
        }

        public static IList Parents()
        {
            return (IList)HumanRightsViolation.FindAll (new Order[] { Order.Asc ("Name") },
                                                        new ICriterion[] { Restrictions.Or (Restrictions.IsNull("ParentId"),
                                                                                            Restrictions.Eq("ParentId",0)) });
        }

        public static HumanRightsViolation[] FindAllByCategoryId (int category_id) {
            return HumanRightsViolation.FindAll(new Order[] { Order.Asc ("Name") }, new ICriterion[] { Restrictions.Eq ("CategoryId", category_id) });
        }

        public string ParentName () {
            return HumanRightsViolation.Find(this.ParentId).Name;
        }

        public string ParentModel () {
            return "HumanRightsViolation";
        }

        public string CategoryName () {
            return HumanRightsViolationCategory.Find(this.CategoryId).Name;
        }

        public string CategoryModel () {
            return "HumanRightsViolationCategory";
        }
    }
}

