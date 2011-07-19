using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("marital_statuses")]
	public class MaritalStatus : ActiveRecordLinqBase<MaritalStatus>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public String Name { get; set; }
	}
}
