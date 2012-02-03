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
using System.Xml;
using AODL.Document.TextDocuments;
using AODL.Document.Content;
using AODL.Document.Styles;
using AODL.Document.Import.OpenDocument;

namespace AODL.Document
{
	/// <summary>
	/// IDocument.
	/// </summary>
	public interface IDocument
	{
		/// <summary>
		/// Every document must have a XmlNamespaceManager
		/// </summary>
		XmlNamespaceManager NamespaceManager {get; set;}
		/// <summary>
		/// Every document must have a XmlDocument that
		/// represent the content.
		/// </summary>
		XmlDocument XmlDoc {get; set;}
		/// <summary>
		/// Every document must give access to his meta data
		/// </summary>
		DocumentMetadata DocumentMetadata {get; set;}
		/// <summary>
		/// Every document must give access to his document configurations
		/// </summary>
		DocumentConfiguration2 DocumentConfigurations2 {get; set; }
		/// <summary>
		/// Every document must give access to his pictures
		/// </summary>
		DocumentPictureCollection DocumentPictures {get; set;}
		/// <summary>
		/// Every document must give access to his thumbnails
		/// </summary>
		DocumentPictureCollection DocumentThumbnails {get; set;}
		/// <summary>
		/// The font list
		/// </summary>
		ArrayList FontList {get; set;}
		/// <summary>
		/// Graphics used within the document.
		/// </summary>
		ArrayList Graphics {get;}
		/// <summary>
		/// EmbedObject used within the document.
		/// </summary>
		ArrayList EmbedObjects {get;}

		DirInfo DirInfo {get;}
		
		
		StyleFactory StyleFactory{get;}
		/// <summary>
		/// Collection of local styles used with this document.
		/// </summary>
		StyleCollection Styles {get; set;}
		/// <summary>
		/// Collection of common styles used with this document.
		/// </summary>
		StyleCollection CommonStyles {get; set;}
		/// <summary>
		/// Collection of contents used by this document.
		/// </summary>
		ContentCollection Content {get; set;}
		/// <summary>
		/// Every document must offer CreateNode for creating
		/// new nodes
		/// </summary>
		/// <param name="name">The name of the node</param>
		/// <param name="prefix">The prefix of the node</param>
		/// <returns>The created node</returns>
		XmlNode CreateNode(string name, string prefix);
		/// <summary>
		/// Every document must offer CreateAttribute for creating
		/// new attributes
		/// </summary>
		/// <param name="name">The name of the attribute</param>
		/// <param name="prefix">The prefix of the attribute</param>
		/// <returns>The created attribute</returns>
		XmlAttribute CreateAttribute(string name, string prefix);
		/// <summary>
		/// If this file was loaded
		/// </summary>
		bool IsLoadedFile {get;}
		/// <summary>
		/// Load the given file.
		/// </summary>
		/// <param name="file"></param>
		void Load(string file);
		/// <summary>
		/// Save the document at the given file position.
		/// </summary>
		/// <param name="filename">Path and file name.</param>
		void SaveTo(string filename);
		/// <summary>
		/// Save the document by using the passed IExporter
		/// with the passed file name.
		/// </summary>
		/// <param name="filename">The name of the new file.</param>
		void SaveTo(string filename, AODL.Document.Export.IExporter iExporter);
	}

	public enum DocumentTypes
	{
		/// <summary>
		/// OpenDocument Text document
		/// </summary>
		TextDocument,
		/// <summary>
		/// OpenDocument Spreadsheet document
		/// </summary>
		SpreadsheetDocument
	}
}

/*
 * $Log: IDocument.cs,v $
 * Revision 1.3  2008/04/29 15:39:42  mt
 * new copyright header
 *
 * Revision 1.2  2008/02/08 07:12:20  larsbehr
 * - added initial chart support
 * - several bug fixes
 *
 * Revision 1.1  2007/02/25 08:58:44  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.4  2007/02/04 22:52:57  larsbm
 * - fixed bug in resize algorithm for rows and cells
 * - extending IDocument, overload SaveTo to accept external exporter impl.
 * - initial version of AODL PDF exporter add on
 *
 * Revision 1.3  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */