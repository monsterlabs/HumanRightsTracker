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
using System.Xml ;
using AODL.Document .Styles.Properties ;
using AODL.Document .Collections;

namespace AODL.Document.Styles
{
	/// <summary>
	/// Summary description for DataPointStyle.
	/// </summary>
	public class DataPointStyle :IStyle
	{
		


		/// <summary>
		/// gets and sets the chart graphic properties
		/// </summary>
		public ChartGraphicProperties ChartGraphicProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
					if (property is ChartGraphicProperties)
						return (ChartGraphicProperties)property;
				ChartGraphicProperties chartgraphicProperties	= new ChartGraphicProperties(this);
				this.PropertyCollection.Add((IProperty)chartgraphicProperties);
				return ChartGraphicProperties;
			}
			set
			{
				if (this.PropertyCollection.Contains((IProperty)value))
					this.PropertyCollection.Remove((IProperty)value);
				this.PropertyCollection.Add(value);
			}
		}

		/// <summary>
		/// gets and sets text properties
		/// </summary>

		public TextProperties TextProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
					if (property is TextProperties)
						return (TextProperties)property;
				TextProperties textProperties	= new TextProperties(this);
				this.PropertyCollection.Add((IProperty)textProperties);
				return TextProperties;
			}
			set
			{
				if (this.PropertyCollection.Contains((IProperty)value))
					this.PropertyCollection.Remove((IProperty)value);
				this.PropertyCollection.Add(value);
			}
		}

		/// <summary>
		/// gets and sets the family style
		/// </summary>

		public string FamilyStyle
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:family",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:family",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("family", value, "style");
				this._node.SelectSingleNode("@style:family",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// the constructor of dataPoint style
		/// </summary>
		/// <param name="document"></param>
		/// <param name="styleName"></param>

		public DataPointStyle(IDocument document, string styleName)
		{
			this.Document					= document;
			this.InitStandards();
			this.StyleName					= styleName;
		}

		public DataPointStyle(IDocument document)
		{
			this.Document			= document;
			this.InitStandards();
			this.NewXmlNode();
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.NewXmlNode();
			this.PropertyCollection				= new IPropertyCollection();
			this.PropertyCollection.Inserted	+= PropertyCollection_Inserted;
			this.PropertyCollection.Removed		+= PropertyCollection_Removed;
			this.FamilyStyle					= "chart";
			//			this.Document.Styles.Add(this);
		}

		/// <summary>
		/// Create a new Xml node.
		/// </summary>
		private void NewXmlNode()
		{
			this.Node		= this.Document.CreateNode("style", "style");
		}
		
		public DataPointStyle(IDocument document,XmlNode node)
		{
			this.Document =document;
			this.Node =node;
		}


		#region IContent Member

		/// <summary>
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
		public string StyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("style-name", value, "table");
				this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
		/// </summary>
		/// <value></value>
		public IDocument Document
		{
			get
			{
				return this._document;
			}
			set
			{
				this._document = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this.StyleName	= value.StyleName;
				this._style = value;
			}
		}

		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		private IPropertyCollection _propertyCollection;
		/// <summary>
		/// Collection of properties.
		/// </summary>
		/// <value></value>
		public IPropertyCollection PropertyCollection
		{
			get { return this._propertyCollection; }
			set { this._propertyCollection = value; }
		}
		#endregion

		/// <summary>
		/// Properties the collection_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void PropertyCollection_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IProperty)value).Node);
		}

		/// <summary>
		/// Properties the collection_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void PropertyCollection_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IProperty)value).Node);
		}



		
	}
}
