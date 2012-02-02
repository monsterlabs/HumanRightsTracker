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

namespace AODL.Document.SpreadsheetDocuments.Tables.Style
{
	/// <summary>
	/// OfficeValueTypes represent possible values
	/// for office values that are used within
	/// spreadsheet table cells
	/// </summary>
	public class OfficeValueTypes
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OfficeValueTypes"/> class.
		/// </summary>
		public OfficeValueTypes()
		{		
		}

		/// <summary>
		/// Type float (floating point number)
		/// </summary>
		public static string Float			= "float";
		/// <summary>
		/// Type string (text)
		/// </summary>
		public static string String			= "string";
	}
}

/*
 * $Log: OfficeValueTypes.cs,v $
 * Revision 1.2  2008/04/29 15:39:54  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:49  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */