using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("scholarity_levels")]
	public class ScholarityLevel : ActiveRecordLinqBase<ScholarityLevel>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public String Name { get; set; }
	}
}
