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
    public class TextInput : Field
    {
        public TextInput(IDocument document, string value)
        {
            _document = document;
            _node = document.CreateNode("text-input", "text");
            Value = value;
        }

        public TextInput(IDocument document, string value, string description)
        {
            _document = document;
            _node = document.CreateNode("text-input", "text");
            Value = value;
            Description = description;
        }

        private TextInput(IDocument document, XmlNode node)
        {
            _document = document;
            _node = node;
        }

      
        /// <summary>
        /// Provides additional description for the text input field
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
