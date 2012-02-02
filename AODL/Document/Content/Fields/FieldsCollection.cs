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
using AODL.Document.Collections;
using System.Xml;

namespace AODL.Document.Content.Fields
{
	/// <summary>
	/// A typed IContent Collection.
	/// </summary>
	public class FieldsCollection : CollectionWithEvents<Field>
	{
		public FieldsCollection()
		{
			this.Clearing += OnClear;
		}
		
		
		public new FieldsCollection Add(Field value)
		{
			if (value.ContentCollection == null)
				throw new Exceptions.AODLException("Could not add a field directly to TextDocument.Fields." +
					"\r\nAdd the field to the content collection instead!");
			base.Add(value);
			return this;
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public new void Remove(Field value)
		{
			if (value != null)
			{
				if (value.ContentCollection !=null)
				{
					value.ContentCollection.RemoveOnlyHere(value);
					value.ContentCollection = null;
				}
			}
			base.Remove(value);
		}

		/// <summary>
		/// Removes a field at the specified position.
		/// </summary>
		/// <param name="pos">position.</param>
		public new void RemoveAt(int pos)
		{
			if ((base[pos] as Field) != null)
			{
				Field f = base[pos] as Field;
				if (f.ContentCollection !=null)
				{
					f.ContentCollection.RemoveOnlyHere(f);
					f.ContentCollection = null;
				}
			}
			base.RemoveAt(pos);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public new void Insert(int index, Field value)
		{
			if (value.ContentCollection == null)
				throw new Exceptions.AODLException("Could not add a field directly to TextDocument.Fields." +
					"\r\nAdd the field to the content collection instead!");
			base.Insert(index, value);
		}

		/// <summary>
		/// Looks for a specific field by its value
		/// </summary>
		/// <param name="val">value of the field</param>
		/// <returns>The specific field if found, null otherwise</returns>
		public Field FindFieldByValue (string val)
		{
			foreach (Field f in this)
			{
				if (f.Value == val)
				{
					return f;
				}
			}
			return null;
		}

		private void OnClear() 
		{ 
			for (int i=0; i<= Count-1; i++)
			{
				Field f = base[i] as Field;
				if (f !=null)
				{
					if (f.ContentCollection != null)
					{
						f.ContentCollection.Remove(f);
					}
				}
			}
		}		
	}
}


