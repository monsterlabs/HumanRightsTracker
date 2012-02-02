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

namespace AODL.Document.Export
{
	/// <summary>
	/// IExporter all classes that want to act as exporter
	/// have to implement this interface.
	/// </summary>
	public interface IExporter
	{
		//The export err�rs as string		
		/// <summary>
		/// Gets the export erros.
		/// </summary>
		/// <value>The export erros.</value>
		System.Collections.ArrayList ExportError {get; }
		//Export the document		
		/// <summary>
		/// Exports the specified document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		void Export(AODL.Document.IDocument document, string filename);
		/// <summary>
		/// ArrayList of DocumentSupportInfo objects
		/// </summary>
		/// <value>ArrayList of DocumentSupportInfo objects.</value>
		ArrayList DocumentSupportInfos {get; }		
	}

	/// <summary>
	/// Information about Author, Info url
	/// and description of the importer
	/// resp. exporter.
	/// </summary>
	public interface IPublisherInfo
	{
		/// <summary>
		/// The name the Author
		/// </summary>
		string Author {get;}
		/// <summary>
		/// Url to a info site
		/// </summary>
		string InfoUrl {get; }
		/// <summary>
		/// Description about the exporter resp. importer
		/// </summary>
		string Description {get; }
	}

	/// <summary>
	/// DocumentSupportInfo is used within a implementation
	/// of IImporter and IExporter for determining
	/// which file extension and which DocumentType
	/// will be supported.
	/// </summary>
	public class DocumentSupportInfo
	{
		private string _extension;
		/// <summary>
		/// Gets or sets the extension.
		/// </summary>
		/// <value>The extension.</value>
		public string Extension
		{
			get { return this._extension; }
			set { this._extension = value; }
		}

		private DocumentTypes _documentType;
		/// <summary>
		/// Gets or sets the type of the document.
		/// </summary>
		/// <value>The type of the document.</value>
		public DocumentTypes DocumentType
		{
			get { return this._documentType; }
			set { this._documentType = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentSupportInfo"/> class.
		/// </summary>
		public DocumentSupportInfo()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentSupportInfo"/> class.
		/// </summary>
		/// <param name="extension">The extension.</param>
		/// <param name="documentTyp">The document typ.</param>
		public DocumentSupportInfo(string extension, DocumentTypes documentTyp)
		{
			this.Extension			= extension;
			this.DocumentType		= documentTyp;
		}
	}
}

/*
 * $Log: IExporter.cs,v $
 * Revision 1.2  2008/04/29 15:39:48  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:42  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */