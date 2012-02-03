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
using AODL.Document.Content.Text;
using AODL.Document.Styles;
using AODL.ExternalExporter.PDF.Document.StyleConverter;

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary HeadingConverter.
	/// </summary>
	public class HeadingConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HeadingConverter"/> class.
		/// </summary>
		public HeadingConverter()
		{
		}

		/// <summary>
		/// Converts the specified heading.
		/// </summary>
		/// <param name="heading">The heading.</param>
		/// <returns>A PDF paragraph representing the ODF heading</returns>
		public static iTextSharp.text.Paragraph Convert(Header heading)
		{
			iTextSharp.text.Font font = DefaultDocumentStyles.Instance().DefaultTextFont;
			IStyle style = heading.Document.CommonStyles.GetStyleByName(heading.StyleName);
			if (style != null && style is ParagraphStyle)
			{
				if ((ParagraphStyle)style != null)
				{
					if (((ParagraphStyle)style).ParentStyle != null)
					{
						IStyle parentStyle = heading.Document.CommonStyles.GetStyleByName(
							((ParagraphStyle)style).ParentStyle);
						if (parentStyle != null
						    && parentStyle is ParagraphStyle
						    && ((ParagraphStyle)parentStyle).TextProperties != null
						    && ((ParagraphStyle)style).TextProperties != null)
						{
							// get parent style first
							font = TextPropertyConverter.GetFont(((ParagraphStyle)parentStyle).TextProperties);
							// now use the orignal style as multiplier
							font = TextPropertyConverter.FontMultiplier(((ParagraphStyle)style).TextProperties, font);
						}
						else
						{
							font = TextPropertyConverter.GetFont(((ParagraphStyle)style).TextProperties);
						}
					}
					else
					{
						font = TextPropertyConverter.GetFont(((ParagraphStyle)style).TextProperties);
					}
				}
			}
			iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph("", font); // default ctor protected - why ??
			paragraph.AddRange(FormatedTextConverter.GetTextContents(heading.TextContent, font));
			return paragraph;
		}
	}
}
