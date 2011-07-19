using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("ethnic_groups")]
	public class EthnicGroup : ActiveRecordLinqBase<EthnicGroup>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public String Name { get; set; }
	}
}
