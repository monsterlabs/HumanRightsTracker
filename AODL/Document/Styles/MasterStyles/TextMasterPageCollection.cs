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
using AODL.Document.Collections;

namespace AODL.Document.Styles.MasterStyles
{
	/// <summary>
	/// Summary for TextMasterPageCollection.
	/// </summary>
	public class TextMasterPageCollection : CollectionWithEvents<TextMasterPage>
	{
		/// <summary>
		/// Get a text master page by his style name.
		/// </summary>
		/// <returns>The TextMasterPage or null if no master page was found for this name.</returns>
		public AODL.Document.Styles.MasterStyles.TextMasterPage GetByStyleName(string styleName)
		{
			foreach(AODL.Document.Styles.MasterStyles.TextMasterPage txtMP in this)
			{
				if (txtMP.StyleName.ToLower().Equals(styleName.ToLower()))
					return txtMP;
			}
			return null;
		}

		/// <summary>
		/// Gets the default master page for this text document.
		/// </summary>
		/// <returns>The default master page or null if no one was found.</returns>
		public AODL.Document.Styles.MasterStyles.TextMasterPage GetDefaultMasterPage()
		{
			return this.GetByStyleName("Standard");
		}
	}
}
