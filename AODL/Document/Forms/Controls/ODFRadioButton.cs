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

namespace AODL.Document.Forms.Controls
{
	public class ODFRadioButton: ODFFormControl
	{
		public override string Type
		{
			get
			{
				return "radio";
			}
		}

		/// <summary>
		/// Specifies the default value of the control
		/// </summary>
		public string Value
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:value", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText; 
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:value", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("value", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Contains a label for the control
		/// </summary>
		public string Label
		{
			get
			{
				XmlNode xn;
				xn = this._node.SelectSingleNode("@form:label", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:label", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("label", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Specifies whether or not a control can accept user input
		/// </summary>
		public XmlBoolean Disabled
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:disabled", 
					this._document.NamespaceManager);
				if (xn == null) return XmlBoolean.NotSet;

				string s = xn.InnerText;	
				XmlBoolean at;
				switch (s)
				{
					case "true": at = XmlBoolean.True; break;
					case "false": at = XmlBoolean.False; break;
					default: at = XmlBoolean.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case XmlBoolean.True: s = "true"; break;
					case XmlBoolean.False: s = "false"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:disabled", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("disabled", "form"));
				nd.InnerText = s;
			}
		}


		/// <summary>
		/// Contains additional information about a control.
		/// </summary>
		public string Title
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:title", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:title", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("title", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Specifies the name of a result set column. The result set is 
		/// determined by the form which the control belongs to
		/// </summary>
		public string DataField
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:data-field", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:data-field", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("title", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Specifies the tabbing navigation order of a control within a form
		/// </summary>
		public int TabIndex
		{
			get
			{
				return int.Parse(this._node.SelectSingleNode("@form:tab-index", 
					this._document.NamespaceManager).InnerText);
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:tab-index", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("tab-index", "form"));
				nd.InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Specifies whether or not a control is included in the tabbing 
		/// navigation order
		/// </summary>
		public XmlBoolean TabStop
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:tab-stop", 
					this._document.NamespaceManager);
				if (xn == null) return XmlBoolean.NotSet;

				string s = xn.InnerText;	
				XmlBoolean at;
				switch (s)
				{
					case "true": at = XmlBoolean.True; break;
					case "false": at = XmlBoolean.False; break;
					default: at = XmlBoolean.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case XmlBoolean.True: s = "true"; break;
					case XmlBoolean.False: s = "false"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:tab-stop", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("tab-stop", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies whether or not a control is printed when a user prints 
		/// the document in which the control is contained
		/// </summary>
		public XmlBoolean Printable
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:printable", 
					this._document.NamespaceManager);
				if (xn == null) return XmlBoolean.NotSet;

				string s = xn.InnerText;	
				XmlBoolean at;
				switch (s)
				{
					case "true": at = XmlBoolean.True; break;
					case "false": at = XmlBoolean.False; break;
					default: at = XmlBoolean.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case XmlBoolean.True: s = "true"; break;
					case XmlBoolean.False: s = "false"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:printable", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("printable", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies the default state of a radio button or option
		/// </summary>
		public XmlBoolean Selected
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:selected", 
					this._document.NamespaceManager);
				if (xn == null) return XmlBoolean.NotSet;

				string s = xn.InnerText;	
				XmlBoolean at;
				switch (s)
				{
					case "true": at = XmlBoolean.True; break;
					case "false": at = XmlBoolean.False; break;
					default: at = XmlBoolean.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case XmlBoolean.True: s = "true"; break;
					case XmlBoolean.False: s = "false"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:selected", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("selected", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies if the radio button is currently selected
		/// </summary>
		public XmlBoolean CurrentSelected
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:current-selected", 
					this._document.NamespaceManager);
				if (xn == null) return XmlBoolean.NotSet;

				string s = xn.InnerText;	
				XmlBoolean at;
				switch (s)
				{
					case "true": at = XmlBoolean.True; break;
					case "false": at = XmlBoolean.False; break;
					default: at = XmlBoolean.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case XmlBoolean.True: s = "true"; break;
					case XmlBoolean.False: s = "false"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:current-selected", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("current-selected", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies a visual affect to apply to a control
		/// </summary>
		public VisualEffect VisualEffect
		{	
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:visual-effect", 
					this._document.NamespaceManager);
				if (xn == null) return VisualEffect.NotSet;

				string s = xn.InnerText;	
				VisualEffect at;
				switch (s)
				{
					case "flat": at = VisualEffect.Flat; break;
					case "3d": at = VisualEffect.ThreeD; break;
					default: at = VisualEffect.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case VisualEffect.Flat: s = "flat"; break;
					case VisualEffect.ThreeD: s = "3d"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:visual-effect", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this._node.Attributes.Append(this._document.CreateAttribute("visual-effect", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies the position of an image to be displayed in a form control, relative to the label text
		/// </summary>
		public ImagePosition ImagePosition
		{	
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:image-position", 
					this._document.NamespaceManager);
				if (xn == null) return ImagePosition.NotSet;

				string s = xn.InnerText;	
				ImagePosition at;
				switch (s)
				{
					case "start": at = ImagePosition.Start; break;
					case "end": at = ImagePosition.End; break;
					case "top": at = ImagePosition.Top; break;
					case "bottom": at = ImagePosition.Bottom; break;
					default: at = ImagePosition.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case ImagePosition.Start: s = "start"; break;
					case ImagePosition.End: s = "end"; break;
					case ImagePosition.Top: s = "top"; break;
					case ImagePosition.Bottom: s = "bottom"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:image-position", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this._node.Attributes.Append(this._document.CreateAttribute("image-position", "form"));
				nd.InnerText = s;
			}
		}


		/// <summary>
		/// Specifies the position of an image to be displayed in a form control, relative to the label text
		/// </summary>
		public ImageAlign ImageAlign
		{	
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:image-align", 
					this._document.NamespaceManager);
				if (xn == null) return ImageAlign.NotSet;

				string s = xn.InnerText;	
				ImageAlign at;
				switch (s)
				{
					case "start": at = ImageAlign.Start; break;
					case "end": at = ImageAlign.End; break;
					case "center": at = ImageAlign.Center; break;
					default: at = ImageAlign.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case ImageAlign.Start: s = "start"; break;
					case ImageAlign.End: s = "end"; break;
					case ImageAlign.Center: s = "center"; break;
					default: return; 
				}
				XmlNode nd = this._node.SelectSingleNode("@form:image-align", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this._node.Attributes.Append(this._document.CreateAttribute("image-align", "form"));
				nd.InnerText = s;
			}
		}

		
		/// <summary>
		/// Creates an ODFRadioButton
		/// </summary>
		/// <param name="ParentForm">Parent form that the control belongs to</param>
		/// <param name="contentCollection">Collection of content where the control will be referenced</param>
		/// <param name="id">Control ID. Please specify a unique ID!</param>
		public ODFRadioButton(ODFForm ParentForm, ContentCollection contentCollection, string id): base (ParentForm, contentCollection, id)
		{}

		/// <summary>
		/// Creates an ODFRadioButton
		/// </summary>
		/// <param name="ParentForm">Parent form that the control belongs to</param>
		/// <param name="contentCollection">Collection of content where the control will be referenced</param>
		/// <param name="id">Control ID. Please specify a unique ID!</param>
		/// <param name="x">X coordinate of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="y">Y coordinate of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="width">Width of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="height">Height of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		public ODFRadioButton(ODFForm ParentForm, ContentCollection contentCollection, string id, string x, string y, string width, string height): base (ParentForm, contentCollection, id, x, y, width, height)
		{}

		public ODFRadioButton(ODFForm ParentForm, XmlNode node): base(ParentForm, node)
		{}

		protected override void CreateBasicNode()
		{
			XmlNode xe = this._document.CreateNode("radio", "form");
			Node.AppendChild(xe);
			Node = xe;
			this.ControlImplementation = "ooo:com.sun.star.form.component.RadioButton";
		}
	}
}
