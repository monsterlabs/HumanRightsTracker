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
    public class DateField: Field
    {
        
		public DateField(IDocument document)
        {
            _document = document;
            _node = document.CreateNode("date", "text");
        }

		public DateField(IDocument document, XmlNode node)
		{
			_document = document;
			_node = node;
		}
        /// <summary>
        /// Specifies whether or not the value of a field element is fixed
        /// </summary>
        public XmlBoolean Fixed
        {
            get
            {
                XmlNode xn = this._node.SelectSingleNode("@text:fixed",
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
                XmlNode nd = this._node.SelectSingleNode("@text:fixed",
                    this._document.NamespaceManager);
                if (nd == null)
                    nd = this.Node.Attributes.Append(this._document.CreateAttribute("fixed", "text"));
                nd.InnerText = s;
            }
        }

        /// <summary>
        /// Specifies a particular date value.
        /// </summary>
        public DateTime DateValue
        {
            get
            {
                XmlNode xn = this._node.SelectSingleNode("@text:date-value",
                    this._document.NamespaceManager);
                if (xn == null) return new DateTime();
                return DateTimeConverter.GetDateTimeFromString(xn.InnerText);
            }
            set
            {
                XmlNode nd = this._node.SelectSingleNode("@text:date-value",
                    this._document.NamespaceManager);
                if (nd == null)
                    nd = this.Node.Attributes.Append(this._document.CreateAttribute("date-value", "text"));
                nd.InnerText = DateTimeConverter.GetStringFromDateTime(value);
            }
        }

		
		/// <summary>
		/// The adjustment of the date
		/// </summary>
		public string DateAdjust
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@text:date-adjust",
					this.Document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@text:date-adjust",
					this.Document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this.Document.CreateAttribute("date-adjust", "text"));
				nd.InnerText = value;
			}
		}

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
