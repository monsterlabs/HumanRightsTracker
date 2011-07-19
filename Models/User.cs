using System;
using System.Text;
using System.Security.Cryptography;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("users")]
	public class User : ActiveRecordValidationBase<User>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property, ValidateNonEmpty, ValidateIsUnique]
        public String Login { get; set; }
		
		[Property, ValidateNonEmpty]
        public String Password { get; set; }
		
		[Property, ValidateNonEmpty]
        public String Salt { get; set; }
		
		public static User authenticate(String login, String password)
		{
			User u = User.FindOne(Expression.Eq("Login", login));
			if (u != null && u.Password.Equals(encrypt(password, u.Salt)))
			{
				return u;	
			}
			return null;
		}
		
		private static String encrypt(String password, String salt)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(password + salt);
			
			SHA1Managed sha1 = new SHA1Managed();
			
			byte[] hashBytes = sha1.ComputeHash(bytes);
			sha1.Clear();
			
			string hex = "";
			foreach (byte x in hashBytes)
			{
				hex += String.Format("{0:x2}", x);
			}
			
			return hex;
		}
	}
}

