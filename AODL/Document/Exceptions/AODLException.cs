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
using System.Collections;
using System.Diagnostics;

namespace AODL.Document.Exceptions
{
	/// <summary>
	/// AODLException is a special exception which will let you also
	/// access possible broken XmlNodes.
	/// </summary>
	public class AODLException : Exception
	{
		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		public AODLException()
			: base(string.Empty, null)
		{
			
		}
		
		public AODLException(string message)
			: base(message, null)
		{
			
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AODLException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public AODLException(string message, Exception e)
			: base(message, e)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AODLException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public AODLException(string message, XmlNode node, Exception e)
			: base(message, e)
		{
			Node = node;
		}
	}
}

/*
 * $Log: AODLException.cs,v $
 * Revision 1.2  2008/04/29 15:39:47  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:42  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.2  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */