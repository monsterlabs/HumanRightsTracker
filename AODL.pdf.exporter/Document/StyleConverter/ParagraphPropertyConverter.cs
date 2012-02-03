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
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using iTextSharp.text;

namespace AODL.ExternalExporter.PDF.Document.StyleConverter
{
	/// <summary>
	/// Zusammenfassung für ParagraphPropertyConverter.
	/// </summary>
	public class ParagraphPropertyConverter
	{
		public ParagraphPropertyConverter()
		{
		}

		/// <summary>
		/// Gets the align ment.
		/// </summary>
		/// <param name="paragraphProperties">The ODF align ment.</param>
		/// <returns>The align ment</returns>
		public static int GetAlignMent(string odfAlignMent)
		{
			try
			{
				switch(odfAlignMent)
				{
					case "right":
						return Element.ALIGN_RIGHT;
					case "justify":
						return Element.ALIGN_JUSTIFIED;
					case "start":
						return Element.ALIGN_LEFT;
					case "end":
						return Element.ALIGN_RIGHT;
					default:
						return Element.ALIGN_LEFT;
				}
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
