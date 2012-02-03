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
	public class ODFListBox: ODFFormControl
	{
		private ODFOptionCollection _options;

		/// <summary>
		/// The collection of the ODFOptions (each option stand for a list box element)
		/// </summary>
		public ODFOptionCollection Options
		{
			get
			{
				return _options;
			}
			set
			{
				_options = value;
			}
		}

		public override string Type
		{
			get
			{
				return "listbox";
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
		/// Specifies the source used to populate the list in a list box or
		/// combo box. The first column of the list source result set populates the list.
		/// </summary>
		public string ListSource
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:list-source",
				                                         this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:list-source",
				                                         this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("list-source", "form"));
				nd.InnerText = value;
			}
		}
		
		/// <summary>
		/// Specifies the column values of the list source result set that
		/// are used to fill the data field values
		/// </summary>
		public string BoundColumn
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:bound-column",
				                                         this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:bound-column",
				                                         this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("bound-column", "form"));
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
		/// Specifies the type of data source that is used to
		/// populate the list data in a list box or combo box
		/// </summary>
		public ListSourceType ListSourceType
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:list-source-type",
				                                         this._document.NamespaceManager);
				if (xn == null) return ListSourceType.NotSet;

				string s = xn.InnerText;
				ListSourceType at;
				switch (s)
				{
						case "table": at = ListSourceType.Table; break;
						case "query": at = ListSourceType.Query; break;
						case "sql": at = ListSourceType.Sql; break;
						case "sql-pass-through": at = ListSourceType.SqlPassThrough; break;
						case "value-list": at = ListSourceType.ValueList; break;
						case "table-fields": at = ListSourceType.TableFields; break;
						default: at = ListSourceType.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
						case ListSourceType.Table: s = "table"; break;
						case ListSourceType.Query: s = "query"; break;
						case ListSourceType.Sql: s = "sql"; break;
						case ListSourceType.SqlPassThrough: s = "sql-pass-through"; break;
						case ListSourceType.ValueList: s = "value-list"; break;
						case ListSourceType.TableFields: s = "table-fields"; break;
						default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:list-source-type",
				                                         this._document.NamespaceManager);
				if (nd == null)
					nd = this._node.Attributes.Append(this._document.CreateAttribute("list-source-type", "form"));
				nd.InnerText = s;
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
		/// Specifies whether the list in a combo box or list box is always
		/// visible or is only visible when the user clicks the drop-down button
		/// </summary>
		public XmlBoolean DropDown
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:dropdown",
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
				XmlNode nd = this._node.SelectSingleNode("@form:dropdown",
				                                         this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("dropdown", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// specifies the number of rows that are visible at a time in a combo box
		/// list or a list box list
		/// </summary>
		public int Size
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:size",
				                                         this._document.NamespaceManager);
				if (xn == null) return -1;
				return int.Parse(xn.InnerText);
			}
			set
			{
				if (value >0)
				{
					XmlNode nd = this._node.SelectSingleNode("@form:size",
					                                         this._document.NamespaceManager);
					if (nd == null)
						nd = this.Node.Attributes.Append(this._document.CreateAttribute("size", "form"));
					nd.InnerText = value.ToString();
				}
				else
				{
					
				}
			}
		}

		/// <summary>
		/// specifies whether or not empty current values are regarded as NULL
		/// </summary>
		public XmlBoolean ConvertEmptyToNull
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:convert-empty-to-null",
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
				XmlNode nd = this._node.SelectSingleNode("@form:convert-empty-to-null",
				                                         this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("convert-empty-to-null", "form"));
				nd.InnerText = s;
			}
		}

		public void SuppressOptionEvents()
		{
			_options.Inserted -= OptionCollection_Inserted;
			_options.Removed -= OptionCollection_Removed;
		}

		public void RestoreOptionEvents()
		{
			_options.Inserted += OptionCollection_Inserted;
			_options.Removed += OptionCollection_Removed;
		}

		private void OptionCollection_Inserted(int index, object value)
		{
			ODFOption opt = value as ODFOption;
			Node.AppendChild(opt.Node);
		}

		private void OptionCollection_Removed(int index, object value)
		{
			ODFOption opt = value as ODFOption;
			if (opt !=null)
				Node.RemoveChild(opt.Node);
		}

		/// <summary>
		/// Looks for a specified option by its value
		/// </summary>
		/// <param name="val">Option value</param>
		/// <returns></returns>
		public ODFOption GetOptionByValue(string val)
		{
			foreach (ODFOption opt in _options)
			{
				if (opt.Value == val)
				{
					return opt;
				}
			}
			return null;
		}

		/// <summary>
		/// Looks for a specified option by its label
		/// </summary>
		/// <param name="lbl">Option label</param>
		/// <returns></returns>
		public ODFOption GetOptionByLabel(string lbl)
		{
			foreach (ODFOption opt in _options)
			{
				if (opt.Label == lbl)
				{
					return opt;
				}
			}
			return null;
		}

		public void FixOptionCollection()
		{
			_options.Clear();
			SuppressOptionEvents();
			foreach (XmlNode nodeChild in Node)
			{
				if (nodeChild.Name == "form:option" && nodeChild.ParentNode == Node)
				{
					ODFOption sp = new ODFOption(_document, nodeChild);
					_options.Add(sp);
				}
			}
			RestoreOptionEvents();
		}


		/// <summary>
		/// Creates an ODFListBox
		/// </summary>
		/// <param name="ParentForm">Parent form that the control belongs to</param>
		/// <param name="contentCollection">Collection of content where the control will be referenced</param>
		/// <param name="id">Control ID. Please specify a unique ID!</param>
		public ODFListBox(ODFForm ParentForm, ContentCollection contentCollection, string id): base (ParentForm, contentCollection, id)
		{
			_options = new ODFOptionCollection();
			RestoreOptionEvents();
		}

		/// <summary>
		/// Creates an ODFListBox
		/// </summary>
		/// <param name="ParentForm">Parent form that the control belongs to</param>
		/// <param name="contentCollection">Collection of content where the control will be referenced</param>
		/// <param name="id">Control ID. Please specify a unique ID!</param>
		/// <param name="x">X coordinate of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="y">Y coordinate of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="width">Width of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="height">Height of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		public ODFListBox(ODFForm ParentForm, ContentCollection contentCollection, string id, string x, string y, string width, string height): base (ParentForm, contentCollection, id, x, y, width, height)
		{
			_options = new ODFOptionCollection();
			RestoreOptionEvents();
		}

		public ODFListBox(ODFForm ParentForm, XmlNode node): base(ParentForm, node)
		{
			_options = new ODFOptionCollection();
			RestoreOptionEvents();
		}

		protected override void CreateBasicNode()
		{
			XmlNode xe = this._document.CreateNode("listbox", "form");
			Node.AppendChild(xe);
			Node = xe;
			this.ControlImplementation = "ooo:com.sun.star.form.component.ListBox";
		}
	}
}
