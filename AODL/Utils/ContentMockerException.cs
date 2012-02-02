/*
 * Created by SharpDevelop.
 * User: darius.damalakas
 * Date: 2009.04.30
 * Time: 08:56
 * 
 */
using System;

namespace AODL.Utils
{
	public class ContentMockerException : Exception
	{
		public ContentMockerException(string message) : base(message)
		{
		}
		
		public ContentMockerException(string message, Exception innerException) : base(message, innerException)
		{
			
		}
	}
}
