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
using AODL.Document;
using AODL.Document.Forms;
using System.Xml;
using AODL.Document.Content;
using AODL.Document.Styles;

namespace AODL.Document.Forms.Controls
{
	
	/// <summary>
	/// Summary description for IODFFormControl.
	/// </summary>
	public abstract class ODFFormControl
	{
		protected XmlNode _node;
		protected ODFForm _parentForm;
		protected int _refCounter = 0;
		protected IDocument _document;
		private ContentCollection _contentCollection;
		private ODFControlRef _controlRef;

		public ContentCollection ContentCollection {
			get { return _contentCollection; }
			set { _contentCollection = value; }
		}
		
		public ODFControlRef ControlRef {
			get { return _controlRef; }
			set { _controlRef = value; }
		}
		

		private FormPropertyCollection _properties;

		/// <summary>
		/// Collection of generic form properties (form:property in ODF)
		/// </summary>
		public FormPropertyCollection Properties
		{
			get
			{
				return _properties;
			}
			set
			{
				_properties = value;
			}	
		}

		/// <summary>
		/// Parent form
		/// </summary>
		public ODFForm ParentForm
		{
			get { return this._parentForm; }
			set { this._parentForm = value; }
		}

		/// <summary>
		/// XML node that represents the control
		/// </summary>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		/// <summary>
		/// Control implementation. Don't change it unless required
		/// </summary>
		public string ControlImplementation
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:control-implementation", 
					this._document.NamespaceManager);
				if (xn == null) return null;
                return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:control-implementation", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("control-implementation", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// The name of the control
		/// </summary>
		public string Name
		{
			get
			{
				XmlNode xn = this.Node.SelectSingleNode("@form:name", 
					this._document.NamespaceManager);
				if (xn == null) return null;
                return xn.InnerText;
			}
			set
			{
				XmlNode nd = this.Node.SelectSingleNode("@form:name", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("name", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Control ID
		/// </summary>
		public string ID
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:id", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:id", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("id", "form"));
				nd.InnerText = value;
				if (_controlRef !=null) _controlRef.DrawControl = value;
			}
		}

		/// <summary>
		/// Returns the type of the control
		/// </summary>
		public abstract string Type
		{
			get;
		}

		protected ODFFormControl(ODFForm ParentForm, XmlNode node)
		{
			_document = ParentForm.Document;
			_parentForm = ParentForm;
			Node = node;

			_contentCollection = null; 
			_controlRef = null;

			_properties = new FormPropertyCollection();
			_properties.Inserted += PropertyCollection_Inserted;
			_properties.Removed += PropertyCollection_Removed;
		}

		protected ODFFormControl(ODFForm ParentForm, ContentCollection contentCollection, string id)
		{
			_document = ParentForm.Document;
			_parentForm = ParentForm;
			Node = ParentForm.Node;
			_contentCollection = contentCollection;
			CreateBasicNode();
			ID = id;
			this.ControlImplementation = "ooo:com.sun.star.form.component.TextField";
			_controlRef = new ODFControlRef(_document, id);

			_properties = new FormPropertyCollection();
			_properties.Inserted += PropertyCollection_Inserted;
			_properties.Removed += PropertyCollection_Removed;
			
		}

		protected ODFFormControl(ODFForm ParentForm, ContentCollection contentCollection, string id, string x, string y, string width, string height) //: base (document, ContentCollection, id, x, y, width, height)
		{
			_document = ParentForm.Document;
			_parentForm = ParentForm;
			Node = ParentForm.Node;
			_contentCollection = contentCollection;
			CreateBasicNode();
			ID = id;
			_controlRef = new ODFControlRef(_document, id, x, y, width, height);

			_properties = new FormPropertyCollection();
			_properties.Inserted += PropertyCollection_Inserted;
			_properties.Removed += PropertyCollection_Removed;
		}

		protected virtual void CreateBasicNode()
		{

		}

		public virtual void AddToContentCollection()
		{
			if (_contentCollection !=null)
			{
				if (_refCounter == 0)
				{
					_contentCollection.Add(_controlRef);
					_refCounter++;
				}
				else
				{
					throw new Exception("Cannot add control to form: it already belongs to another form!");
				}
			}
		}

		public virtual void RemoveFromContentCollection()
		{
			if (_contentCollection != null && _refCounter > 0)
			{
				_contentCollection.Remove(_controlRef);
				_refCounter--;
			}	
		}
	
		public void SuppressPropertyEvents()
		{
			_properties.Inserted -= PropertyCollection_Inserted;
			_properties.Removed -= PropertyCollection_Removed;
		}

		public void RestorePropertyEvents()
		{
			_properties.Inserted += PropertyCollection_Inserted;
			_properties.Removed += PropertyCollection_Removed;
		}

		private void PropertyCollection_Inserted(int index, object value)
		{
			XmlNode form_prop;
			form_prop = this.Node.SelectSingleNode("form:properties", this._document.NamespaceManager);
			
			if (form_prop == null)
			{
				form_prop = _document.CreateNode("properties", "form");
			}
				
			FormProperty prop = value as FormProperty;
			form_prop.AppendChild(prop.Node);
			this.Node.AppendChild(form_prop);
		}

		private void PropertyCollection_Removed(int index, object value)
		{
			XmlNode form_prop;
			form_prop = this.Node.SelectSingleNode("form:properties", this._document.NamespaceManager);
			
			if (form_prop != null)
			{
				FormProperty prop = value as FormProperty;
				form_prop.RemoveChild(prop.Node);
				if (index == 0)
				{
					Node.RemoveChild(form_prop);
				}
			}
		}

		/// <summary>
		/// Look for a control generic property by its name
		/// </summary>
		/// <param name="name">Property name</param>
		/// <returns></returns>
		public FormProperty GetFormProperty(string name)
		{
			foreach (FormProperty fp in _properties)
			{
				if (fp.Name == name)
				{
					return fp;
				}
			}
			return null;
		}

		public void FixPropertyCollection()
		{
			_properties.Clear();
			SuppressPropertyEvents();
			XmlNode form_prop = this.Node.SelectSingleNode("form:properties", this._document.NamespaceManager);
			if (form_prop == null) return;

			foreach (XmlNode nodeChild in form_prop)
			{
				if (nodeChild.Name == "form:property" && nodeChild.ParentNode == form_prop)
				{
					SingleFormProperty sp = new SingleFormProperty(_document, nodeChild);
					_properties.Add(sp);
				}
				if (nodeChild.Name == "form:list-property" && nodeChild.ParentNode == form_prop)
				{
					ListFormProperty lp = new ListFormProperty(_document, nodeChild);
					_properties.Add(lp);
				}
			}
			RestorePropertyEvents();
		}

		#region ODFControlRef properties
		
		/// <summary>
		/// X coordinate of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)
		/// </summary>
		public string X
		{
			get
			{
				return _controlRef.X;
			}
			set
			{
				_controlRef.X = value;
			}
		}

		/// <summary>
		/// Y coordinate of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)
		/// </summary>
		public string Y
		{
			get
			{
				return _controlRef.Y;
			}
			set
			{
				_controlRef.Y = value;
			}
		}

		/// <summary>
		/// Width of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)
		/// </summary>
		public string Width
		{
			get
			{
				return _controlRef.Width;
			}
			set
			{
				_controlRef.Width = value;
			}
		}

		/// <summary>
		/// Height of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)
		/// </summary>
		public string Height
		{
			get
			{
				return _controlRef.Height;
			}
			set
			{
				_controlRef.Height = value;
			}
		}

		/// <summary>
		/// Control's Z index
		/// </summary>
		public int ZIndex
		{
			get
			{
				return _controlRef.ZIndex;
			}
			set
			{
				_controlRef.ZIndex = value;
			}
		}

		
		/// <summary>
		/// A Style class which is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		public IStyle Style 
		{
			get
			{
				return _controlRef.Style;
			}
			set
			{
				_controlRef.Style = value;
			}
		}

		/// <summary>
		/// Style name
		/// </summary>
		public string StyleName
		{
			get
			{
				return _controlRef.StyleName;
			}
			set
			{
				_controlRef.StyleName = value;
			}
		}

		/// <summary>
		/// Text style name
		/// </summary>
		public string TextStyleName
		{
			get
			{
				return _controlRef.TextStyleName;
			}
			set
			{
				_controlRef.TextStyleName = value;
			}
		}

		/// <summary>
		/// A text style.
		/// If no style is available this is null.
		/// </summary>
		public IStyle TextStyle 
		{
			get
			{
				return _controlRef.TextStyle;
			}
			set
			{
				_controlRef.TextStyle = value;
			}
		}

		/// <summary>
		/// Number of page to use as an anchor
		/// </summary>
		public int AnchorPageNumber
		{
			get
			{
				return _controlRef.AnchorPageNumber;
			}
			set
			{
				_controlRef.AnchorPageNumber = value;
			}
		}

		/// <summary>
		/// What to use as an anchor
		/// </summary>
		public AnchorType AnchorType
		{
			get
			{
				return _controlRef.AnchorType;
			}
			set
			{
				_controlRef.AnchorType = value;
			}
		}

		/// <summary>
		/// Table background
		/// </summary>
		public XmlBoolean TableBackground
		{
			get
			{
				return _controlRef.TableBackground;
			}
			set
			{
				_controlRef.TableBackground = value;
			}
		}

		/// <summary>
		/// Transform. For the details, see ODF v1.0 specification
		/// </summary>
		public string Transform
		{
			get
			{
				return _controlRef.Transform;
			}
			set
			{
				_controlRef.Transform = value;
			}
		}

		~ODFFormControl()
		{
			RemoveFromContentCollection();
		}

		#endregion


	}
}
