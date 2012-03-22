using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;
namespace HumanRightsTracker.Models
{
    [ActiveRecord("source_information_types")]
    public class SourceInformationType : ActiveRecordValidationBase<SourceInformationType>
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

        [HasMany(typeof(SourceInformationType),  Table="SourceInformationTypes", ColumnKey="parent_id", Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true, OrderBy="Name Asc")]
        public IList Children { get; set; }
    }
}