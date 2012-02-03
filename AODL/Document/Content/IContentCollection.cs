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

using System.Collections;
using System.Collections.Generic;
using System.Xml;

using AODL.Document.Collections;
using AODL.Document.Content.Fields;
using AODL.Document.TextDocuments;

namespace AODL.Document.Content
{
	/// <summary>
	/// A typed IContent Collection.
	/// </summary>
	public class ContentCollection : CollectionWithEvents<IContent>
	{
		public ContentCollection()
		{
			this.Clearing += OnClear;
		}
		
		public ContentCollection(params IContent[] contents)
		{
			AddRange(contents);
		}
		
		public ContentCollection(ContentCollection contents)
		{
			AddRange(contents);
		}
		
		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public override CollectionWithEvents<IContent> Add(AODL.Document.Content.IContent value)
		{
			if (value is Field)
			{
				Field f = value as Field;
				if (f != null)
				{
					if (f.Document is TextDocument)
					{
						TextDocument td = f.Document as TextDocument;
						f.ContentCollection = this;
						td.Fields.Add(f);
					}
				}
			}
			base.Add(value);
			return this;
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public new void Remove(AODL.Document.Content.IContent value)
		{
			if (value is Field)
			{
				Field f = value as Field;
				if (f != null)
				{
					if (f.Document is TextDocument)
					{
						TextDocument td = f.Document as TextDocument;
                        f.ContentCollection = null;
						td.Fields.Remove(f);
					}
				}
			}
            base.Remove(value);
		}

		/// <summary>
		/// Removes an element at the specified position.
		/// </summary>
		/// <param name="pos">position.</param>
		public new void RemoveAt(int pos)
		{
			if (this[pos] is Field)
			{
				Field f = this[pos] as Field;
				if (f != null)
				{
					if (f.Document is TextDocument)
					{
						TextDocument td = f.Document as TextDocument;
						f.ContentCollection = null;
						td.Fields.Remove(f);
					}
				}
			}
			base.RemoveAt(pos);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void RemoveOnlyHere(AODL.Document.Content.IContent value)
		{
			base.Remove(value);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public new void Insert(int index, AODL.Document.Content.IContent value)
		{
			if (value is Field)
			{
				Field f = value as Field;
				if (f != null)
				{
					if (f.Document is TextDocument)
					{
						TextDocument td = f.Document as TextDocument;
						f.ContentCollection = this;
						td.Fields.Add(f);
					}
				}
			}
			base.Insert(index, value);
		}

		private void OnClear() 
		{ 
			for (int i=0; i< base.Count; i++)
			{
				IContent c = base[i];
				if (c is Field)
				{
					Field f = c as Field;
					if (f != null)
					{
						if (f.Document is TextDocument)
						{
							TextDocument td = f.Document as TextDocument;
							td.Fields.Remove(f);
						}
					}
				}
			}
		}	
	}
}

/*
 * $Log: IContentCollection.cs,v $
 * Revision 1.5  2008/04/29 15:39:43  mt
 * new copyright header
 *
 * Revision 1.4  2007/07/15 09:29:55  yegorov
 * Issue number:
 * Submitted by:
 * Reviewed by:
 *
 * Revision 1.2  2007/04/08 16:51:22  larsbehr
 * - finished master pages and styles for text documents
 * - several bug fixes
 *
 * Revision 1.1  2007/02/25 08:58:33  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:29:46  larsbm
 * *** empty log message ***
 *
 * Revision 1.3  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */