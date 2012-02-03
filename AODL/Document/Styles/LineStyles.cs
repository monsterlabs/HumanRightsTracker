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

namespace AODL.Document.Styles
{
	/// <summary>
	/// Represents the possible line styles used in OpenDocument.
	/// e.g for the FormatedText.Underline
	/// </summary>
	public class LineStyles
	{
		/// <summary>
		/// long-dash
		/// </summary>
		public static readonly string longdash	= "long-dash";
		/// <summary>
		/// dot-dash
		/// </summary>
		public static readonly string dotdash	= "dot-dash";
		/// <summary>
		/// dot-dot-dash
		/// </summary>
		public static readonly string dotdotdash	= "dot-dot-dash";
		/// <summary>
		/// No style
		/// </summary>
		public static readonly string none	= "none";
		/// <summary>
		/// solid
		/// </summary>
		public static readonly string solid	= "solid";
		/// <summary>
		/// dotted
		/// </summary>
		public static readonly string dotted	= "dotted";
		/// <summary>
		/// dash
		/// </summary>
		public static readonly string dash	= "dash";
		/// <summary>
		/// wave
		/// </summary>
		public static readonly string wave	= "wave";
	}

	/// <summary>
	/// Border helper class
	/// </summary>
	public class Border
	{
		/// <summary>
		/// Normal solid
		/// </summary>
		public static readonly string NormalSolid	= "0.002cm solid #000000";
		/// <summary>
		/// Middlle solid
		/// </summary>
		public static readonly string MiddleSolid	= "0.004cm solid #000000";
		/// <summary>
		/// Heavy solid
		/// </summary>
		public static readonly string HeavySolid	= "0.008cm solid #000000";
	}
}

/*
 * $Log: LineStyles.cs,v $
 * Revision 1.2  2008/04/29 15:39:54  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:48  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.4  2005/11/23 19:18:17  larsbm
 * - New Textproperties
 * - New Paragraphproperties
 * - New Border Helper
 * - Textproprtie helper
 *
 * Revision 1.3  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */