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
using System.Collections;
using System.Text;
using AODL.Document.Content;
using AODL.Document;
using System.Xml;
using AODL.Document.Forms;
using AODL.Document.Styles;
using AODL.Document.Helper;

namespace AODL.Document.Content.Fields
{
	public enum Display {None, Formula, Value, NotSet};
	
	public class VariableSet : Field
    {
        public VariableSet(IDocument document, VariableDecl variableDeclaration)
        {
            _document = document;
            _node = document.CreateNode("variable-set", "text");
            VariableDeclaration = variableDeclaration;
        }

        public VariableSet(IDocument document, VariableDecl variableDeclaration, string value)
        {
            _document = document;
            _node = document.CreateNode("variable-set", "text");
            VariableDeclaration = variableDeclaration;
            Value = value;
        }

        private VariableSet(IDocument document, XmlNode node)
        {
            _document = document;
            _node = node;
        }

        /// <summary>
        /// The formula to compute the value of the variable field
        /// </summary>
        public string Formula
        {
            get
            {
                XmlNode xn = this._node.SelectSingleNode("@text:formula",
                    this._document.NamespaceManager);
                if (xn == null) return null;
                return xn.InnerText;
            }
            set
            {
                XmlNode nd = this._node.SelectSingleNode("@text:formula",
                    this._document.NamespaceManager);
                if (nd == null)
                    nd = this.Node.Attributes.Append(this._document.CreateAttribute("formula", "text"));
                nd.InnerText = value;
            }
        }

        /// <summary>
        /// The formula to compute the value of the variable field
        /// </summary>
        public VariableDecl VariableDeclaration
        {
            get
            {
                XmlNode xn = this._node.SelectSingleNode("@text:name",
                    this._document.NamespaceManager);
                if (xn == null) return null;
                return (this.Document as TextDocuments.TextDocument).VariableDeclarations.GetVariableDeclByName(xn.InnerText);
                
            }
            set
            {
                if (value != null)
                {
                    XmlNode nd = this._node.SelectSingleNode("@text:name",
                        this._document.NamespaceManager);
                    if (nd == null)
                        nd = this.Node.Attributes.Append(this._document.CreateAttribute("name", "text"));
                    nd.InnerText = value.Name;
                }
            }
        }

        /// <summary>
        /// Defines the way the variable is displayed
        /// </summary>
        public Display Display
        {

            get
            {
                XmlNode xn = this._node.SelectSingleNode("@text:display",
                    this._document.NamespaceManager);
                if (xn == null) return Display.NotSet;

                string s = xn.InnerText;

                Display vt;
                switch (s)
                {
                    case "value": vt = Display.Value; break;
                    case "formula": vt = Display.Formula; break;
                    case "none": vt = Display.None; break;
                    default: vt = Display.NotSet; break;
                }
                return vt;
            }
            set
            {
                string s = "";
                switch (value)
                {
                    case Display.Value: s = "value"; break;
                    case Display.Formula: s = "formula"; break;
                    case Display.None: s = "none"; break;
                }

                XmlNode nd = this._node.SelectSingleNode("@text:display",
                    this._document.NamespaceManager);
                if (nd == null)
                    nd = this.Node.Attributes.Append(this._document.CreateAttribute("display", "text"));
                nd.InnerText = s;
            }
        }

		/// <summary>
		/// Defines the type of the variable
		/// </summary>
		public VariableValueType VariableValueType
		{

			get
			{
				XmlNode xn = this._node.SelectSingleNode("@office:value-type",
					this._document.NamespaceManager);
				if (xn == null) return VariableValueType.NotSet;

				string s = xn.InnerText;

				VariableValueType vt;
				switch (s)
				{
					case "float": vt = VariableValueType.Float; break;
					case "percentage": vt = VariableValueType.Percentage; break;
					case "currency": vt = VariableValueType.Currency; break;
					case "date": vt = VariableValueType.Date; break;
					case "time": vt = VariableValueType.Time; break;
					case "boolean": vt = VariableValueType.Boolean; break;
					case "string": vt = VariableValueType.String; break;
					default: vt = VariableValueType.String; break;
				}
				return vt;
			}
			set
			{
				string s = "";
				switch (value)
				{
					case VariableValueType.Float: s = "float"; break;
					case VariableValueType.Percentage: s = "percentage"; break;
					case VariableValueType.Currency: s = "currency"; break;
					case VariableValueType.Date: s = "date"; break;
					case VariableValueType.Time: s = "time"; break;
					case VariableValueType.Boolean: s = "boolean"; break;
					case VariableValueType.String: s = "string"; break;
				}

				XmlNode nd = this._node.SelectSingleNode("@office:value-type",
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("value-type", "office"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Refers to the data style used to format the numeric value.
		/// If no style is available this is null.
		/// </summary>
		public string DataStyleName
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@style:data-style-name",
					this.Document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@style:data-style-name",
					this.Document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this.Document.CreateAttribute("data-style-name", "style"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Refers to the data style used to format the numeric value.
		/// If no style is available this is null.
		/// </summary>
		public IStyle DataStyle
		{
			get
			{
				return this._document.Styles.GetStyleByName(DataStyleName);
			}
			set
			{
				DataStyleName = value.StyleName;
			}
		}

    }
}
