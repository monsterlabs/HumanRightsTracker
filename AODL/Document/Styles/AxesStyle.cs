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
using AODL.Document .Styles .Properties ;
using AODL.Document .Collections ;

namespace AODL.Document.Styles
{
	/// <summary>
	/// Summary description for AxesStyle.
	/// </summary>
	public class AxesStyle : AbstractStyle
	{

		/// <summary>
		/// gets and sets the chart graphic property
		/// </summary>
		public ChartGraphicProperties ChartGraphicProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
					if (property is ChartGraphicProperties)
						return (ChartGraphicProperties)property;
				ChartGraphicProperties chartGraphicProperties	= new ChartGraphicProperties(this);
				this.PropertyCollection.Add((IProperty)chartGraphicProperties);
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
		/// gets and sets the text properties
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
		/// gets and sets the axes properties
		/// </summary>

		public AxesProperties AxesProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
				{
					if (property is AxesProperties)
						return (AxesProperties)property;
				}
				AxesProperties axesProperties	= new AxesProperties(this);
				this.PropertyCollection.Add((IProperty)axesProperties);
				return axesProperties;
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
		/// the constructor of the axesStyle
		/// </summary>
		/// <param name="document"></param>

		public AxesStyle(IDocument document)
		{
			this.Document			= document;
			this.InitStandards();
			this.NewXmlNode();
			this.AxesProperties .Origin ="0";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TableStyle"/> class.
		/// </summary>
		/// <param name="document">The spreadsheet document.</param>
		/// <param name="styleName">Name of the style.</param>
		public AxesStyle(IDocument document, string styleName)
		{
			this.Document					= document;
			this.InitStandards();
			this.StyleName					= styleName;
			this.AxesProperties .Origin ="0";
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

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

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
