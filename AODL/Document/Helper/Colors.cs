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
using System.Collections;
using System.Drawing;

namespace AODL.Document.Helper
{
	/// <summary>
	/// Converter class. Convert any enum Color from System.Drawing.Color
	/// into his rgb hex value.
	/// </summary>
	public class Colors
	{

		/// <summary>
		/// Convert any enum Color from System.Drawing.Color
		/// into his rgb hex value.
		/// </summary>
		/// <param name="color">A System.Drawing.Color</param>
		/// <returns>The rgb hex value.</returns>
		public static string GetColor(Color color)
		{
			int argb = color.ToArgb();

			return "#"+argb.ToString("x").Substring(2);
		}
	}
}

/*
 * $Log: Colors.cs,v $
 * Revision 1.2  2008/04/29 15:39:51  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:44  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */