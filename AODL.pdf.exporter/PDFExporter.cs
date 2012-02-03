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
using AODL.Document.Export;
using AODL.ExternalExporter.PDF.Document;

namespace AODL.ExternalExporter.PDF
{
	/// <summary>
	/// Summary for PDFExporter
	/// </summary>
	public class PDFExporter : IExporter
	{
		/// <summary>
		/// Invoked when the document has been exported.
		/// </summary>
		public delegate void ExportFinished();
		/// <summary>
		/// Invoked when the document has been exported.
		/// </summary>
		public event ExportFinished OnExportFinished;

		/// <summary>
		/// Initializes a new instance of the <see cref="PDFExporter"/> class.
		/// </summary>
		public PDFExporter()
		{
		}

		#region IExporter Member

		public System.Collections.ArrayList DocumentSupportInfos
		{
			get
			{
				// TODO:  Getter-Implementierung für PDFExporter.DocumentSupportInfos hinzufügen
				return null;
			}
		}

		public System.Collections.ArrayList ExportError
		{
			get
			{
				// TODO:  Getter-Implementierung für PDFExporter.ExportError hinzufügen
				return null;
			}
		}

		/// <summary>
		/// Exports the specified document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		public void Export(AODL.Document.IDocument document, string filename)
		{
			try
			{
				PDFDocument pdfDocument = new PDFDocument();
				pdfDocument.DoExport(document, filename);
				if (this.OnExportFinished != null)
				{
					this.OnExportFinished();
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion
	}
}
