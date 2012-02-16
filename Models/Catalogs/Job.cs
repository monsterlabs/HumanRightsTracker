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
    [ActiveRecord("jobs")]
    public class Job : ActiveRecordValidationBase<Job>
    {

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property]
        public String Notes { get; set; }

        private IList children = new ArrayList();
        [HasMany(typeof(Job),  Table="Jobs", ColumnKey="parent_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true, OrderBy="Name Asc")]
        public IList Children
        {
            get { return children;}
            set { children = value; }
        }

        public static IList Parents()
        {
            return (IList)Job.FindAll (new Order[] { Order.Asc ("Name") },
                                       new ICriterion[] { Restrictions.Or (Restrictions.IsNull("ParentId"),
                                                                           Restrictions.Eq("ParentId",0)) });
        }
    }
}
