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
    [ActiveRecord("involvement_degrees")]
    public class InvolvementDegree : ActiveRecordValidationBase<InvolvementDegree>
    {

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property]
        public String Notes { get; set; }

        [Property("parent_id")]
        public int ParentId { get; set; }

        [HasMany(typeof(InvolvementDegree),  Table="InvolvementDegrees", ColumnKey="parent_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true, OrderBy="Name Asc")]
        public IList Children { get; set; }

        public static IList Parents()
        {
            return (IList)InvolvementDegree.FindAll (new Order[] { Order.Asc ("Name") },
                                                   new ICriterion[] { Restrictions.Or (Restrictions.IsNull("ParentId"),
                                                                                       Restrictions.Eq("ParentId",0)) });
        }

        public string ParentName () {
            return InvolvementDegree.Find(this.ParentId).Name;
        }

        public string ParentModel () {
            return "InvolvementDegree";
        }
    }
}