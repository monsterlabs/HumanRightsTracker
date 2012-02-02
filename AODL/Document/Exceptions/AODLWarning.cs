/*************************************************************************
 *
 * DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER
 * 
 * Copyright 2008 Sun Microsystems, Inc. All rights reserved.
 * 
 * Use is subject to license terms.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy
 * of the License at http://www.apache.org/licenses/LICENSE-2.0. You can also
 * obtain a copy of the License at http://odftoolkit.org/docs/license.txt
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * 
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 ************************************************************************/

using System;
using System.Xml;

namespace AODL.Document.Exceptions
{
	/// <summary>
	/// You can use an AODLWarning instead of an AODLException
	/// if the whole result isn't really in danger.
	/// </summary>
	public class AODLWarning : AODLException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AODLWarning"/> class.
		/// </summary>
		public AODLWarning() : base()
		{
		}

		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AODLWarning"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public AODLWarning(string message) : this(message, null, null)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AODLWarning"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="e">original exception</param>
		public AODLWarning(string message, XmlNode node) : this(message, node, null)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AODLWarning"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="e">original exception</param>
		public AODLWarning(string message, Exception e) : this(message, null, e)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AODLWarning"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="e">original exception</param>
		public AODLWarning(string message, XmlNode node, Exception e) : base(message, node, e)
		{
		}
	}
}

/*
 * $Log: AODLWarning.cs,v $
 * Revision 1.2  2008/04/29 15:39:47  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:42  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */