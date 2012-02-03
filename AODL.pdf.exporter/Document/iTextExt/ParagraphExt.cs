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
using iTextSharp.text;

namespace AODL.ExternalExporter.PDF.Document.iTextExt
{
	/// <summary>
	/// Summary for ParagraphExt.
	/// iText paragraph extension for making it ODF compliant.
	/// </summary>
	public class ParagraphExt : Paragraph
	{
		private bool _pageBreakBefore;
		/// <summary>
		/// Gets a value indicating whether [page break before].
		/// </summary>
		/// <value><c>true</c> if [page break before]; otherwise, <c>false</c>.</value>
		public bool PageBreakBefore
		{
			get { return this._pageBreakBefore; }
			set { this._pageBreakBefore = value; }
		}

		private bool _pageBreakAfter;
		/// <summary>
		/// Gets a value indicating whether [page break after].
		/// </summary>
		/// <value><c>true</c> if [page break after]; otherwise, <c>false</c>.</value>
		public bool PageBreakAfter
		{
			get { return this._pageBreakBefore; }
			set { this._pageBreakAfter = value; }
		}

		public ParagraphExt(string text, Font font) : base(text, font)
		{
		}
	}
}
