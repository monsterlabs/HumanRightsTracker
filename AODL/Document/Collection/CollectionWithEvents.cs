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


using AODL.Document.Content;
using System.Collections;
using System.Collections.Generic;

namespace AODL.Document.Collections
{
	// Declare the event signatures
	/// <summary>
	/// The collection clear delegate
	/// </summary>
	public delegate void CollectionClear();
	/// <summary>
	/// The collection change event.
	/// </summary>
	public delegate void CollectionChange<T>(int index, T value);

	
	/// <summary>
	/// The events for all collections used within AODL
	/// </summary>
	public class CollectionWithEvents<T> : List<T>
	{
		
		// Collection change events
		/// <summary>
		/// The clearing event
		/// </summary>
		public event CollectionClear Clearing;
		/// <summary>
		/// The cleared event
		/// </summary>
		public event CollectionClear Cleared;
		/// <summary>
		/// The inserting event
		/// </summary>
		public event CollectionChange<T> Inserting;
		/// <summary>
		/// The inserted event
		/// </summary>
		public event CollectionChange<T> Inserted;
		/// <summary>
		/// The removing event
		/// </summary>
		public event CollectionChange<T> Removing;
		/// <summary>
		/// The removed event
		/// </summary>
		public event CollectionChange<T> Removed;

		
		protected virtual void OnClearing()
		{
			if (Clearing != null) {
				Clearing();
			}
		}
		
		protected virtual void OnCleared()
		{
			if (Cleared != null) {
				Cleared();
			}
		}
		
		protected virtual void OnInserting(int index, T value)
		{
			if (Inserting != null) {
				Inserting(index, value);
			}
		}
		
		protected virtual void OnInserted(int index, T value)
		{
			if (Inserted != null) {
				Inserted(index, value);
			}
		}
		
		protected virtual void OnRemoving(int index, T value)
		{
			if (Removing != null) {
				Removing(index, value);
			}
		}
		
		protected virtual void OnRemoved(int index, T value)
		{
			if (Removed != null) {
				Removed(index, value);
			}
		}
		
		public virtual new CollectionWithEvents<T> Add(T value)
		{
			int index = this.Count - 1;
			OnInserting(index, value);
			base.Add(value);
			OnInserted(index, value);
			return this;
		}
		
		/// <summary>
		/// Executes additional processes while deleting <see cref="T:System.Collections.CollectionBase"/>
		/// </summary>
		public virtual new CollectionWithEvents<T> Clear()
		{
			OnClearing();
			base.Clear();
			OnCleared();
			return this;
		}

		
		
		/// <summary>
		/// Executes additional processes before deleting an object.<see cref="T:System.Collections.CollectionBase"/>
		/// </summary>
		/// <param name="index">Zero based index <paramref name="value"/> which should be inserted</param>
		/// <param name="value">The new value at <paramref name="index"/>.</param>
		public virtual new CollectionWithEvents<T> Insert(int index, T value)
		{
			OnInserting(index, value);
			base.Insert(index, value);
			OnInserted(index, value);
			return this;
		}


		/// <summary>
		/// Executes additional processes while deleting an object<see cref="T:System.Collections.CollectionBase"/>
		/// </summary>
		/// <param name="index">The zero based index of the object <paramref name="value"/> to delete</param>
		/// <param name="value">The object to <paramref name="index"/> delete.</param>
		public virtual new CollectionWithEvents<T> Remove(T value)
		{
			int index = this.IndexOf(value);
			OnRemoving(index, value);
			base.Remove(value);
			OnRemoved(index, value);
			return this;
		}

	}

}
/*
 * $Log: CollectionWithEvents.cs,v $
 * Revision 1.2  2008/04/29 15:39:42  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:32  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:29:45  larsbm
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
