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
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Draw;

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary for MixedContentConverter.
	/// </summary>
	public class MixedContentConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MixedContentConverter"/> class.
		/// </summary>
		public MixedContentConverter()
		{
		}

		/// <summary>
		/// Convert a AODL IContentCollection into an ArrayList of IElement iText objects.
		/// </summary>
		/// <param name="iContentCollection">The i content collection.</param>
		/// <returns>An ArrayList of iText IElement objects.</returns>
		public static ArrayList GetMixedPdfContent(ContentCollection iContentCollection)
		{
			ArrayList mixedPdfContent = new ArrayList();
			foreach(IContent content in iContentCollection)
			{
				if (content is AODL.Document.Content.Text.Paragraph)
				{
					if (((AODL.Document.Content.Text.Paragraph)content).MixedContent != null
					    && ((AODL.Document.Content.Text.Paragraph)content).MixedContent.Count > 0)
						mixedPdfContent.Add(ParagraphConverter.Convert(
							content as AODL.Document.Content.Text.Paragraph));
					else
						mixedPdfContent.Add(iTextSharp.text.Chunk.NEWLINE);
				}
				else if (content is AODL.Document.Content.Text.Header)
				{
					mixedPdfContent.Add(HeadingConverter.Convert(
						content as AODL.Document.Content.Text.Header));
				}
				else if (content is AODL.Document.Content.Tables.Table)
				{
					TableConverter tc = new TableConverter();
					mixedPdfContent.Add(tc.Convert(
						content as AODL.Document.Content.Tables.Table));
				}
				else if (content is AODL.Document.Content.Draw.Frame)
				{
					DrawFrameConverter dfc = new DrawFrameConverter();
					mixedPdfContent.Add(dfc.Convert(
						content as AODL.Document.Content.Draw.Frame));
				}
				else if (content is AODL.Document.Content.Draw.Graphic)
				{
					ImageConverter ic = new ImageConverter();
					mixedPdfContent.Add(ic.Convert(
						content as AODL.Document.Content.Draw.Graphic));
				}
			}
			return mixedPdfContent;
		}
	}
}
