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
using AODL.Document.Styles;
using AODL.Document.Content.Text;
using AODL.ExternalExporter.PDF.Document.StyleConverter;

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary for FormatedTextConverter.
	/// </summary>
	public class FormatedTextConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FormatedTextConverter"/> class.
		/// </summary>
		public FormatedTextConverter()
		{
		}

		/// <summary>
		/// Converts the specified formated text.
		/// </summary>
		/// <param name="formatedText">The formated text.</param>
		/// <returns>The chunck object representing this formated text.</returns>
		public static iTextSharp.text.Phrase Convert(AODL.Document.Content.Text.FormatedText formatedText)
		{
			iTextSharp.text.Font font;
			if ((TextStyle)formatedText.Style != null
			    && ((TextStyle)formatedText.Style).TextProperties != null)
				font = TextPropertyConverter.GetFont(
					((TextStyle)formatedText.Style).TextProperties);
			else
				font = DefaultDocumentStyles.Instance().DefaultTextFont;

			iTextSharp.text.Phrase phrase = new iTextSharp.text.Phrase("", font); // default ctor protected - why ??
			phrase.AddRange(FormatedTextConverter.GetTextContents(formatedText.TextContent, font));

			return phrase;
		}

		/// <summary>
		/// Gets the text contents.
		/// </summary>
		/// <param name="textCollection">The text collection.</param>
		/// <returns>The content. ArrayList of chunks and phrases.</returns>
		public static ICollection GetTextContents(ITextCollection textCollection, iTextSharp.text.Font font)
		{
			ArrayList contents = new ArrayList();
			foreach(object obj in textCollection)
			{
				if (obj is AODL.Document.Content.Text.FormatedText)
				{
					contents.Add(FormatedTextConverter.Convert(
						obj as AODL.Document.Content.Text.FormatedText));
				}
				else if (obj is AODL.Document.Content.Text.SimpleText)
				{
					contents.Add(SimpleTextConverter.Convert(
						obj as AODL.Document.Content.Text.SimpleText, font));
				}
				else if (obj is AODL.Document.Content.Text.TextControl.TabStop)
				{
					contents.Add(SimpleTextConverter.ConvertTabs(
						obj as AODL.Document.Content.Text.TextControl.TabStop, font));
				}
				else if (obj is AODL.Document.Content.Text.TextControl.WhiteSpace)
				{
					contents.Add(SimpleTextConverter.ConvertWhiteSpaces(
						obj as AODL.Document.Content.Text.TextControl.WhiteSpace, font));
				}
			}
			return contents;
		}
	}
}
