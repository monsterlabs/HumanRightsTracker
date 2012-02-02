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
    public enum PlaceholderType { Text, Table, TextBox, Image, Object, NotSet };

    public class Placeholder : Field
    {
        public Placeholder(IDocument document, PlaceholderType placeholderType)
        {
            _document = document;
            _node = document.CreateNode("placeholder", "text");
            PlaceholderType = placeholderType;
        }

        public Placeholder(IDocument document, PlaceholderType placeholderType, string description)
        {
            _document = document;
            _node = document.CreateNode("placeholder", "text");
            PlaceholderType = placeholderType;
            Description = description;
        }

        private Placeholder(IDocument document, XmlNode node)
        {
            _document = document;
            _node = node;
        }

        /// <summary>
        /// This attribute is mandatory and it indicates which type of text content the placeholder represents
        /// </summary>
        public PlaceholderType PlaceholderType
        {
            get
            {
                XmlNode xn = this._node.SelectSingleNode("@text:placeholder-type",
                    this._document.NamespaceManager);
                if (xn == null) return PlaceholderType.NotSet;
                string s = xn.InnerText;
                PlaceholderType at;
                switch (s)
                {
                    case "text": at = PlaceholderType.Text; break;
                    case "table": at = PlaceholderType.Table; break;
                    case "text-box": at = PlaceholderType.TextBox; break;
                    case "image": at = PlaceholderType.Image; break;
                    case "object": at = PlaceholderType.Object; break;
                    default: at = PlaceholderType.NotSet; break;
                }
                return at;
            }
            set
            {
                string s;
                switch (value)
                {
                    case PlaceholderType.Text: s = "text"; break;
                    case PlaceholderType.Table: s = "table"; break;
                    case PlaceholderType.TextBox: s = "text-box"; break;
                    case PlaceholderType.Image: s = "image"; break;
                    case PlaceholderType.Object: s = "object"; break;
                    default: return;;
                }
                XmlNode nd = this._node.SelectSingleNode("@text:placeholder-type",
                    this._document.NamespaceManager);
                if (nd == null)
                    nd = this._node.Attributes.Append(this._document.CreateAttribute("placeholder-type", "text"));
                nd.InnerText = s;
            }
        }

        /// <summary>
        /// Provides additional description for the placeholder
        /// </summary>
        public string Description
        {
            get
            {
                XmlNode xn = this._node.SelectSingleNode("@text:description",
                    this._document.NamespaceManager);
                if (xn == null) return null;
                return xn.InnerText;
            }
            set
            {
                XmlNode nd = this._node.SelectSingleNode("@text:description",
                    this._document.NamespaceManager);
                if (nd == null)
                    nd = this.Node.Attributes.Append(this._document.CreateAttribute("description", "text"));
                nd.InnerText = value;
            }
        }

    }
}
