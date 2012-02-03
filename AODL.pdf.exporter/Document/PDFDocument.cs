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
using System.IO;
using iTextSharp.text.pdf;
using AODL.Document;
using AODL.Document.Content;
using AODL.ExternalExporter.PDF.Document.ContentConverter;
using AODL.ExternalExporter.PDF.Document.StyleConverter;

namespace AODL.ExternalExporter.PDF.Document
{
	/// <summary>
	/// Zusammenfassung f�r PDFDocument.
	/// </summary>
	public class PDFDocument
	{
		/// <summary>
		/// The new pdf document.
		/// </summary>
		private iTextSharp.text.Document _document;
		/// <summary>
		/// Gets the document.
		/// </summary>
		/// <value>The document.</value>
		public iTextSharp.text.Document Document
		{
			get { return this._document; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PDFDocument"/> class.
		/// </summary>
		public PDFDocument()
		{
		}

		/// <summary>
		/// Does the export.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="fileName">Name of the file.</param>
		public void DoExport(IDocument document, string fileName)
		{
			try
			{
				this.LoadDefaultStyles(document);
				this.CreatePDFDocument(fileName);
				ArrayList pdfElements = MixedContentConverter.GetMixedPdfContent(document.Content);
				foreach(object pdfElement in pdfElements)
				{
					if (pdfElement is AODL.ExternalExporter.PDF.Document.iTextExt.ParagraphExt
						&& ((AODL.ExternalExporter.PDF.Document.iTextExt.ParagraphExt)pdfElement).PageBreakBefore)
							this._document.NewPage();
					this._document.Add(pdfElement as iTextSharp.text.IElement);
					if (pdfElement is AODL.ExternalExporter.PDF.Document.iTextExt.ParagraphExt
						&& ((AODL.ExternalExporter.PDF.Document.iTextExt.ParagraphExt)pdfElement).PageBreakAfter)
						this._document.NewPage();
				}

				this._document.Close();
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Loads the default styles.
		/// </summary>
		private void LoadDefaultStyles(IDocument document)
		{
			try
			{
				DefaultDocumentStyles defaultDocumentStyle;
				if (document is AODL.Document.TextDocuments.TextDocument)
					defaultDocumentStyle = DefaultDocumentStyles.Instance(
						((AODL.Document.TextDocuments.TextDocument)document).DocumentStyles, document);
				else if (document is AODL.Document.SpreadsheetDocuments.SpreadsheetDocument)
					defaultDocumentStyle = DefaultDocumentStyles.Instance(
						((AODL.Document.SpreadsheetDocuments.SpreadsheetDocument)document).DocumentStyles, document);
				else 
					throw new Exception("Unknown IDocument implementation.");
				defaultDocumentStyle.Init();
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Creates the PDF document.
		/// </summary>
		/// <param name="filename">The filename.</param>
		private void CreatePDFDocument(string filename)
		{
			try
			{
				this._document = new iTextSharp.text.Document();				
				PdfWriter pdfWriter = PdfWriter.GetInstance(this._document, new FileStream(filename, FileMode.Create));
				this._document.Open();
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}

