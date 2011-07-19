using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("religions")]
	public class Religion : ActiveRecordLinqBase<Religion>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public String Name { get; set; }
	}
}
