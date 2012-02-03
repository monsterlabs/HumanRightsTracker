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
using System.Diagnostics;
using System.IO;
using System.Xml;
using AODL;
using AODL.Document.TextDocuments;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Exceptions;
using AODL.Document.Forms;
using AODL.Document.Forms.Controls;
using AODL.Document.Content.Tables;

namespace AODL.Document.Import.OpenDocument.NodeProcessors
{
	public class FormsProcessor
	{
		/// <summary>
		/// If set to true all node content would be directed
		/// to Console.Out
		/// </summary>
		private bool _debugMode			= false;
		/// <summary>
		/// The textdocument
		/// </summary>
		private IDocument _document;
		/// <summary>
		/// Warning delegate
		/// </summary>
		public delegate void Warning(AODLWarning warning);
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="FormsProcessor"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		public FormsProcessor(IDocument document)
		{
			this._document			= document;
		}
		/// <summary>
		/// Reads the content nodes.
		/// </summary>
		public void ReadFormNodes()
		{
			try
			{

				XmlNode node				= null;
				if (this._document is TextDocument)
				{
					node	= this._document.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, this._document.NamespaceManager);
					(this._document as TextDocument).Forms = new ODFFormCollection();
				}
				else if (this._document is SpreadsheetDocument)
					node	= this._document.XmlDoc.SelectSingleNode(
						"/office:document-content/office:body/office:spreadsheet", this._document.NamespaceManager);

				if (node != null)
				{
					this.CreateDocumentForms(node);
				}
				else
				{
					throw new AODLException("Unknown content type.");
				}
				
			}
			catch(Exception ex)
			{
				throw new AODLException("Error while trying to load the content file!", ex);
			}
		}

		/// <summary>
		/// Creates the document forms.
		/// </summary>
		/// <param name="node">The node.</param>
		public void CreateCellForms(Table table)
		{
			/// TODO: ADD IMPLEMENTATION!
				
			try
			{
				XmlNode nodeOfficeForms;
				nodeOfficeForms = table.Node.SelectSingleNode("office:forms", _document.NamespaceManager);
					
				if (nodeOfficeForms != null)
				{
					foreach(XmlNode nodeChild in nodeOfficeForms)
					{
						if (this._document is SpreadsheetDocument)
						{
							ODFForm f = CreateForm(nodeChild);
							table.Forms.Add(f);
						}
					}
					nodeOfficeForms.RemoveAll();
				}
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while processing forms.", ex);
			}
		}

		/// <summary>
		/// Creates the document forms.
		/// </summary>
		/// <param name="node">The node.</param>
		private void CreateDocumentForms(XmlNode node)
		{
			/// TODO: ADD IMPLEMENTATION!
				
				try
				{
					XmlNode nodeOfficeForms;
					nodeOfficeForms = node.SelectSingleNode("office:forms", _document.NamespaceManager);
					
					if (nodeOfficeForms != null)
					{
						foreach(XmlNode nodeChild in nodeOfficeForms)
						{
							if (this._document is TextDocument)
							{
								ODFForm f = CreateForm(nodeChild);
								(this._document as TextDocument).Forms.Add(f);
							}
						}
						nodeOfficeForms.RemoveAll();
					}
				}
				catch(Exception ex)
				{
					throw new AODLException("Exception while processing forms.", ex);
				}
		}

		/// <summary>
		/// Gets the form.
		/// </summary>
		/// <param name="formnode">The node of the form.</param>
		/// <returns></returns>
		private ODFForm CreateForm(XmlNode formnode)
		{
			ODFForm form = null;
			try
			{
				if (formnode.Name == "form:form")
				{
					if (this._debugMode)
						this.LogNode(formnode, "Log form node before");
						
					//Create a new ODFForm
					
					///////////TODO. Fix for child forms!
					form = new ODFForm(formnode.CloneNode(true), this._document);
						
					form.SuppressControlEvents();
					foreach(XmlNode nodeChild in form.Node.ChildNodes)
					{
						
						switch (nodeChild.Name)
						{
							case "form:form":	
								if (nodeChild.ParentNode==form.Node)
								{
									ODFForm frmchild = CreateForm(nodeChild);
									if (frmchild != null)
									{
										form.ChildForms.Add(frmchild);
									}
									form.Node.RemoveChild(nodeChild);
								}
								break;
							case "form:properties": break;
							
							case "form:button":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFButton button = new ODFButton(form, nodeChild);
									button.FixPropertyCollection();
									form.Controls.Add(button);
								}
								break;
							
							case "form:listbox":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFListBox listbox = new ODFListBox(form, nodeChild);
									listbox.FixPropertyCollection();
									listbox.FixOptionCollection();
									form.Controls.Add(listbox);
								}
								break;
							
							case "form:combobox":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFComboBox combobox = new ODFComboBox(form, nodeChild);
									combobox.FixPropertyCollection();
									combobox.FixItemCollection();
									form.Controls.Add(combobox);
								}
								break;

							case "form:textarea":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFTextArea text = new ODFTextArea(form, nodeChild);
									text.FixPropertyCollection();
									form.Controls.Add(text);
								}
								break;

							case "form:frame":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFFrame frm = new ODFFrame(form, nodeChild);
									frm.FixPropertyCollection();
									form.Controls.Add(frm);
								}
								break;

							case "form:file":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFFile file = new ODFFile(form, nodeChild);
									file.FixPropertyCollection();
									form.Controls.Add(file);
								}
								break;
							
							case "form:hidden":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFHidden hidden = new ODFHidden(form, nodeChild);
									hidden.FixPropertyCollection();
									form.Controls.Add(hidden);
								}
								break;

							case "form:checkbox":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFCheckBox cb = new ODFCheckBox(form, nodeChild);
									cb.FixPropertyCollection();
									form.Controls.Add(cb);
								}
								break;

							case "form:radio":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFRadioButton rb = new ODFRadioButton(form, nodeChild);
									rb.FixPropertyCollection();
									form.Controls.Add(rb);
								}
								break;

							case "form:formatted-text":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFFormattedText text = new ODFFormattedText(form, nodeChild);
									text.FixPropertyCollection();
									form.Controls.Add(text);
								}
								break;
							case "form:value-range":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFValueRange vr = new ODFValueRange(form, nodeChild);
									vr.FixPropertyCollection();
									form.Controls.Add(vr);
								}
								break;
							case "form:image":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFImage img = new ODFImage(form, nodeChild);
									img.FixPropertyCollection();
									form.Controls.Add(img);
								}
								break;
							case "form:image-frame":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFImageFrame imgf = new ODFImageFrame(form, nodeChild);
									imgf.FixPropertyCollection();
									form.Controls.Add(imgf);
								}
								break;
							case "form:grid":
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFGrid gr = new ODFGrid(form, nodeChild);
									gr.FixPropertyCollection();
									gr.FixColumnCollection();
									form.Controls.Add(gr);
								}
								break;
							default: 
								if (nodeChild.ParentNode==form.Node) 
								{
									ODFGenericControl gc = new ODFGenericControl(form, nodeChild);
									gc.FixPropertyCollection();
									form.Controls.Add(gc);
								}
								break;
						}
					}
					form.RestoreControlEvents();
					form.FixPropertyCollection();
				//	formnode.RemoveAll();
				//	formnode.InnerText = "";
				}
			}
				

			catch(Exception ex)
			{
				throw new AODLException("Exception while processing a form:form node.", ex);
			}
			return form;
		}


		/// <summary>
		/// Logs the node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="msg">The MSG.</param>
		private void LogNode(XmlNode node, string msg)
		{
			Console.WriteLine("\n#############################\n{0}", msg);
			XmlTextWriter writer	= new XmlTextWriter(Console.Out);
			writer.Formatting		= Formatting.Indented;
			node.WriteTo(writer);
		}

	}


}
