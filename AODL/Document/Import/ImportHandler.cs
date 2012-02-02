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
using System.IO;
using System.Collections;
using System.Diagnostics;
using AODL.Document;
using AODL.Document.Exceptions;
using AODL.Document.Export;
using AODL.Document.Import.OpenDocument;
using AODL.Document.Import.PlainText;

namespace AODL.Document.Import
{
	/// <summary>
	/// ImportHandler class to get the right IImporter implementations
	/// for the document to import.
	/// </summary>
	public class ImportHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ImportHandler"/> class.
		/// </summary>
		public ImportHandler()
		{
		
		}

		/// <summary>
		/// Gets the first importer that match the parameter criteria.
		/// </summary>
		/// <param name="documentType">Type of the document.</param>
		/// <param name="loadPath">The save path.</param>
		/// <returns></returns>
		public IImporter GetFirstImporter(DocumentTypes documentType, string loadPath)
		{
			string targetExtension			= ExportHandler.GetExtension(loadPath);

			foreach(IImporter iImporter in this.LoadImporter())
			{
				foreach(DocumentSupportInfo documentSupportInfo in iImporter.DocumentSupportInfos)
					if (documentSupportInfo.Extension.ToLower().Equals(targetExtension.ToLower()))
						if (documentSupportInfo.DocumentType == documentType)
							return iImporter;
			}

			throw new AODLException("No importer available for type "+documentType.ToString()+" and extension "+targetExtension);
		}

		/// <summary>
		/// Load importers
		/// </summary>
		/// <returns></returns>
		private ArrayList LoadImporter()
		{
			try
			{
				ArrayList alImporter			= new ArrayList();				
				alImporter.Add(new OpenDocumentImporter());
				alImporter.Add(new PlainTextImporter());
				alImporter.Add(new CsvImporter());

				return alImporter;
			}
			catch(Exception ex)
			{	
				throw new AODLException("Error while trying to load the importer.", ex);
			}
		}
	}
}

/*
 * $Log: ImportHandler.cs,v $
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
 */