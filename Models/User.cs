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

        public static User authenticate (String login, String password)
        {
            User u = User.FindOne (Expression.Eq ("Login", login));
            if (u != null && u.Password.Equals (encrypt (password, u.Salt))) {
                return u;
            }
            return null;
        }

        public static bool ChangePassword(String login, String new_password, String password_confirmation)
        {
            User u = User.FindOne (Expression.Eq ("Login", login));
            if (u != null)
            {
                if ( ( new_password.Trim ().Length >= 6 && password_confirmation.Trim ().Length >= 6 ) &&
                     ( new_password == password_confirmation ))
                {
                    u.Salt = GetSalt(10);
                    u.Password = encrypt (new_password, u.Salt);
                    u.SaveAndFlush ();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        private static String encrypt (String password, String salt)
        {
            byte[] bytes = Encoding.ASCII.GetBytes (password + salt);
            
            SHA1Managed sha1 = new SHA1Managed ();
            
            byte[] hashBytes = sha1.ComputeHash (bytes);
            sha1.Clear ();
            
            string hex = "";
            foreach (byte x in hashBytes) {
                hex += String.Format ("{0:x2}", x);
            }
            
            return hex;
        }

        private static string GetSalt(int length)
        {
            byte[] randomArray = new byte[length];
            string randomString;
            Random rnd = new Random();
            rnd.NextBytes(randomArray);
            randomString = Convert.ToBase64String(randomArray);
            return randomString;
        }
    }
}

