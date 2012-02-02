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
	public class ODFValueRange: ODFFormControl
	{
		public override string Type
		{
			get
			{
				return "value-range";
			}
		}

		/// <summary>
		/// Specifies the current status of an input control
		/// </summary>
		public string CurrentValue
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:current-value", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText; 
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:current-value", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("current-value", "form"));
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
		/// Specifies whether or not a user can modify the value of a control
		/// </summary>
		public XmlBoolean ReadOnly
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:readonly", 
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
				XmlNode nd = this._node.SelectSingleNode("@form:readonly", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("readonly", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies the minimum value that a user can enter.
		/// </summary>
		public int MinValue
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:min-value", 
					this._document.NamespaceManager);
				if (xn == null) return 0;
				return int.Parse(xn.InnerText);
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:min-value", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("min-value", "form"));
				nd.InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Specifies the maximum value that a user can enter.
		/// </summary>
		public int MaxValue
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:max-value", 
					this._document.NamespaceManager);
				if (xn == null) return 0;
				return int.Parse(xn.InnerText);
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:max-value", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("max-value", "form"));
				nd.InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Specifies the increment to be used for a control representing a value
		/// </summary>
		public int StepSize
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:step-size", 
					this._document.NamespaceManager);
				if (xn == null) return -1;
				return int.Parse(xn.InnerText);
			}
			set
			{
				if (value >0)
				{
					XmlNode nd = this._node.SelectSingleNode("@form:step-size", 
						this._document.NamespaceManager);
					if (nd == null)
						nd = this.Node.Attributes.Append(this._document.CreateAttribute("step-size", "form"));
					nd.InnerText = value.ToString();
				}
				else
				{
					
				}
			}
		}

		/// <summary>
		/// Specifies a second-level increment to be used for a control representing a value
		/// </summary>
		public int PageStepSize
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:page-step-size", 
					this._document.NamespaceManager);
				if (xn == null) return -1;
				return int.Parse(xn.InnerText);
			}
			set
			{
				if (value >0)
				{
					XmlNode nd = this._node.SelectSingleNode("@form:page-step-size", 
						this._document.NamespaceManager);
					if (nd == null)
						nd = this.Node.Attributes.Append(this._document.CreateAttribute("page-step-size", "form"));
					nd.InnerText = value.ToString();
				}
				else
				{
					
				}
			}
		}


		/// <summary>
		/// Specifies a time-out to be used  before a pressed mouse button results in repeating an action
		/// </summary>
		public string RepeatDelay
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:delay-for-repeat", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:delay-for-repeat", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("delay-for-repeat", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Specifies the orientation of the control, which could be either 
		/// horizontal or vertical
		/// </summary>
		public Orientation Orientation
		{	
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:orientation", 
					this._document.NamespaceManager);
				if (xn == null) return Orientation.NotSet;

				string s = xn.InnerText;	
				Orientation at;
				switch (s)
				{
					case "horizontal": at = Orientation.Horizontal; break;
					case "vertical": at = Orientation.Vertical; break;
					default: at = Orientation.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case Orientation.Horizontal: s = "horizontal"; break;
					case Orientation.Vertical: s = "vertical"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:orientation", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this._node.Attributes.Append(this._document.CreateAttribute("orientation", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Creates an ODFValueRange
		/// </summary>
		/// <param name="ParentForm">Parent form that the control belongs to</param>
		/// <param name="contentCollection">Collection of content where the control will be referenced</param>
		/// <param name="id">Control ID. Please specify a unique ID!</param>
		public ODFValueRange(ODFForm ParentForm, ContentCollection contentCollection, string id): base (ParentForm, contentCollection, id)
		{}

		/// <summary>
		/// Creates an ODFValueRange
		/// </summary>
		/// <param name="ParentForm">Parent form that the control belongs to</param>
		/// <param name="contentCollection">Collection of content where the control will be referenced</param>
		/// <param name="id">Control ID. Please specify a unique ID!</param>
		/// <param name="x">X coordinate of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="y">Y coordinate of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="width">Width of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="height">Height of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		public ODFValueRange(ODFForm ParentForm, ContentCollection contentCollection, string id, string x, string y, string width, string height): base (ParentForm, contentCollection, id, x, y, width, height)
		{}

		public ODFValueRange(ODFForm ParentForm, XmlNode node): base(ParentForm, node)
		{}

		protected override void CreateBasicNode()
		{
			XmlNode xe = this._document.CreateNode("value-range", "form");
			Node.AppendChild(xe);
			Node = xe;
			this.ControlImplementation = "ooo:com.sun.star.form.component.SpinButton";
		}
	}
}
