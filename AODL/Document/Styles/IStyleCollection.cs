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
using AODL.Document.Collections;
using System.Collections.Generic;

namespace AODL.Document.Styles
{
	public class StyleFactory
	{
		IDocument m_document = null;

		public StyleFactory(IDocument document)
		{
			this.m_document = document;
		}
		
		public T Request<T>(string styleName) where T : IStyle
		{
			if (styleName == null)
				styleName = string.Empty;
			IStyle style = m_document.Styles.GetStyleByName(styleName);
			if (style != null)
				return (T)style;
			style = Create(typeof(T), styleName);
			m_document.Styles.Add(style);
			return (T)style ;
		}
		
		private IStyle Create(Type styleType, string styleName)
		{
			return Activator.CreateInstance(styleType, m_document, styleName) as IStyle;
		}
	}
	
	/*public class IStyleComparer :Comparer<IStyle>
	{
		public override int Compare(IStyle x, IStyle y)
		{
			if ((x == null) && (y == null))
				return 0;
			if (x == null)
				return -1;
			if (y == null)
				return -1;
			string xname = x.StyleName;
			string yname = y.StyleName;
			if (xname == null)
			{
				if (yname == null)
					return 0; else
					return -1;
			}
			
			return xname.CompareTo(yname);
		}
		
	}
	
	public class DummyStyle : IStyle
	{
		private string m_styleName = string.Empty;
		
		public string StyleName {
			get { return m_styleName; }
			set { m_styleName = value; }
		}
		
		
		public System.Xml.XmlNode Node {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public IDocument Document {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public AODL.Document.Styles.Properties.IPropertyCollection PropertyCollection {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
	}*/
	
	/// <summary>
	/// IStyleCollection
	/// </summary>
	public class StyleCollection : CollectionWithEvents<IStyle>
	{
		/*private bool m_needSort = true;
		private IComparer<IStyle> m_comparer = new IStyleComparer();
		private DummyStyle m_dummyStyle = new DummyStyle();
		
		private void ReSort()
		{
			if (m_needSort == false)
				return;
			this.Sort(m_comparer);
			m_needSort = false;
		}
		
		private void OnCollectionChange()
		{
			m_needSort = true;
		}*/
		
		public StyleCollection() : base()
		{
			/*this.Clearing += delegate {OnCollectionChange();};
			this.Cleared += delegate {OnCollectionChange();};
			this.Inserted += delegate {OnCollectionChange();};
			this.Inserting += delegate {OnCollectionChange();};
			this.Removed += delegate {OnCollectionChange();};
			this.Removing += delegate {OnCollectionChange();};
			 */
		}
		
		/// <summary>
		/// Gets the name of the style by.
		/// </summary>
		/// <param name="styleName">Name of the style.</param>
		/// <returns></returns>
		public IStyle GetStyleByName(string styleName)
		{
			if (styleName == null)
				styleName = string.Empty;
			/*ReSort();
			m_dummyStyle.StyleName = styleName;
			int index = this.BinarySearch(m_dummyStyle, m_comparer);
			if (index < 0)
				return null;
			return this[index];
			 */
			foreach (IStyle style in this)
			{
				if (styleName == null)
				{
					if (string.IsNullOrEmpty(style.StyleName))
						return style;
					continue;
				}
				if (style.StyleName != null && styleName != null)
				{
					if (style.StyleName.Equals(styleName))
						return style;
				}
			}

			return null;
		}
		
		public List<IStyle> ToValueList()
		{
			return this;
		}
	}
}

/*
 * $Log: IStyleCollection.cs,v $
 * Revision 1.2  2008/04/29 15:39:54  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:48  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */