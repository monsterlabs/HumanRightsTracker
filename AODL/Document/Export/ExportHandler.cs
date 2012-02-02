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
using AODL.Document.Export.Html;
using AODL.Document.Export.OpenDocument;

namespace AODL.Document.Export
{
	/// <summary>
	/// ExportHandler class to get the right IExporter implementations
	/// for the document to export.
	/// </summary>
	public class ExportHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExportHandler"/> class.
		/// </summary>
		public ExportHandler()
		{
		
		}

		/// <summary>
		/// Gets the first exporter that match the parameter criteria.
		/// </summary>
		/// <param name="documentType">Type of the document.</param>
		/// <param name="savePath">The save path.</param>
		/// <returns></returns>
		public IExporter GetFirstExporter(DocumentTypes documentType, string savePath)
		{
			string targetExtension			= GetExtension(savePath);

			foreach(IExporter iExporter in this.LoadExporter())
			{
				foreach(DocumentSupportInfo documentSupportInfo in iExporter.DocumentSupportInfos)
					if (documentSupportInfo.Extension.ToLower().Equals(targetExtension.ToLower()))
						if (documentSupportInfo.DocumentType == documentType)
							return iExporter;
			}

			throw new AODLException("No exporter available for type "+documentType.ToString()+" and extension "+targetExtension);
		}

		/// <summary>
		/// Load exporters.
		/// </summary>
		/// <returns></returns>
		private ArrayList LoadExporter()
		{
			try
			{
				ArrayList alExporter			= new ArrayList();
				
				alExporter.Add(new OpenDocumentTextExporter());
				alExporter.Add(new OpenDocumentHtmlExporter());

				return alExporter;
			}
			catch(Exception ex)
			{	
				throw new AODLException("Error while trying to load the exporter.", ex);
			}
		}

		/// <summary>
		/// Gets the extension.
		/// </summary>
		/// <param name="aFullPathOrFileName">Name of a full path or file.</param>
		/// <returns></returns>
		public static string GetExtension(string aFullPathOrFileName)
		{
			int point				= aFullPathOrFileName.LastIndexOf(".");

			return aFullPathOrFileName.Substring(point);
		}
	}
}

/*
 * $Log: ExportHandler.cs,v $
 * Revision 1.2  2008/04/29 15:39:48  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:42  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */