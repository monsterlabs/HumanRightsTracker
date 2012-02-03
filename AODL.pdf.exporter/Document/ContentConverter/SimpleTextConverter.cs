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

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary for SimpleTextConverter.
	/// </summary>
	public class SimpleTextConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleTextConverter"/> class.
		/// </summary>
		public SimpleTextConverter()
		{
		}

		/// <summary>
		/// Converts the specified simple text.
		/// </summary>
		/// <param name="simpleText">The simple text.</param>
		/// <returns></returns>
		public static iTextSharp.text.Chunk Convert(AODL.Document.Content.Text.SimpleText simpleText, iTextSharp.text.Font font)
		{
			try
			{
				return new iTextSharp.text.Chunk(simpleText.Text, font);
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Converts the tabs.
		/// </summary>
		/// <param name="tabStop">The tab stop.</param>
		/// <returns>Chunkck containing whitespace for a tab.</returns>
		public static iTextSharp.text.Phrase ConvertTabs(AODL.Document.Content.Text.TextControl.TabStop tabStop, iTextSharp.text.Font font)
		{
			try
			{
				// Only a trick since PDF doesn't support tab stops as know from other
				// formats, so we only use whitespace character for the beginning
				// TODO: do it better
				iTextSharp.text.Phrase phrase = new iTextSharp.text.Phrase("     ", font);
				return phrase;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Converts the white spaces.
		/// </summary>
		/// <param name="whiteSpace">The white space.</param>
		/// <returns>Chunck containing the whitspaces.</returns>
		public static iTextSharp.text.Phrase ConvertWhiteSpaces(AODL.Document.Content.Text.TextControl.WhiteSpace whiteSpace, iTextSharp.text.Font font)
		{
			try
			{
				string simulatedWhitespaces = "";
				if (whiteSpace.Count != null)
				{
					for(int i=0; i < Int32.Parse(whiteSpace.Count); i++)
					{
						simulatedWhitespaces += " ";
					}
				}
				return new iTextSharp.text.Phrase(simulatedWhitespaces, font);
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
