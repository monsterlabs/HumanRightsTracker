using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("users")]
	public class User : ActiveRecordLinqBase<User>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public String Login { get; set; }
		
		[Property]
        public String Password { get; set; }
		
		[Property]
        public String Salt { get; set; }
	}
}

