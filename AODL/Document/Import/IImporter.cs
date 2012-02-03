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
using AODL.Document.TextDocuments;
using System.Collections;
using AODL.Document;
using AODL.Document.Import.OpenDocument;
using AODL.Document.Exceptions;

namespace AODL.Document.Import
{
	public class ImporterException : AODLException
	{
		public ImporterException(string message, Exception e)
			: base(message, e)
		{
		}
	}
	
	/// <summary>
	/// All classes that want to act as an importer have to
	/// to implement this interface.
	/// </summary>
	public interface IImporter
	{
		/// <summary>
		/// Imports the specified document.
		/// A Importer class have to return a TextDocument object
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		void Import(IDocument document,string filename);

		/// <summary>
		/// Deletes temporary files created while importing
		/// </summary>
		void DeleteUnpackedFiles();
		
		
		/// <summary>
		/// Returns directory information, where everythingis extracted,
		/// graphics stored, etc
		/// </summary>
		DirInfo DirInfo{get;}
		
		/// <summary>
		/// Gets the import error.
		/// </summary>
		/// <value>The import error.</value>
		ArrayList ImportError {get; }
		/// <summary>
		/// ArrayList of DocumentSupportInfo objects
		/// </summary>
		/// <value>ArrayList of DocumentSupportInfo objects.</value>
		ArrayList DocumentSupportInfos {get; }
		/// <summary>
		/// If the import file format isn't any OpenDocument
		/// format you have to return true and AODL will
		/// create a new one.
		/// </summary>
		bool NeedNewOpenDocument {get;}
	}
}

/*
 * $Log: IImporter.cs,v $
 * Revision 1.2  2008/04/29 15:39:52  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:44  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.2  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
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