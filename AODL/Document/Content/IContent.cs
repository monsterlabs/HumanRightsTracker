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
using AODL.Document.Styles;
using AODL.Document;

namespace AODL.Document.Content
{
	/// <summary>
	/// All classes that will act as content document
	/// must implement this interface.
	/// </summary>
	public interface IContent
	{
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
		/// </summary>
		IDocument Document {get; set;}
		/// <summary>
		/// Represents the XmlNode within the content.xml from the odt file.
		/// </summary>
		XmlNode Node {get; set;}
		/// <summary>
		/// The stylename wihich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		string StyleName {get; set;}
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		IStyle Style {get; set;}
	}
}

/*
 * $Log: IContent.cs,v $
 * Revision 1.2  2008/04/29 15:39:43  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:33  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:29:46  larsbm
 * *** empty log message ***
 *
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */