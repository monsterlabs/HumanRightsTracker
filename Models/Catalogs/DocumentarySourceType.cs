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
    [ActiveRecord("documentary_source_types")]
    public class DocumentarySourceType : ActiveRecordValidationBase<DocumentarySourceType>
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

        [HasMany(typeof(DocumentarySourceType),  Table="DocumentarySourceTypes", ColumnKey="parent_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true, OrderBy="Name Asc")]
        public IList Children { get; set; }

        public static IList Parents()
        {
            return (IList)DocumentarySourceType.FindAll (new Order[] { Order.Asc ("Name") },
                                                                new ICriterion[] { Restrictions.Or (Restrictions.IsNull("ParentId"),
                                                                                                     Restrictions.Eq("ParentId",0)) });
        }

        public string ParentName () {
            return DocumentarySourceType.Find(this.ParentId).Name;
        }

        public string ParentModel () {
            return "DocumentarySourceType";
        }
    }
}