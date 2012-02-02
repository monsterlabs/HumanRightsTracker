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
using AODL.Document.Styles;

namespace AODL.Document.Content.Tables
{
	/// <summary>
	/// TableBuilder offer static methode to build table for different
	/// document types.
	/// </summary>
	public class TableBuilder
	{
		/// <summary>
		/// Create a spreadsheet table.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="styleName">Name of the style.</param>
		/// <returns></returns>
		public static Table CreateSpreadsheetTable(AODL.Document.SpreadsheetDocuments.SpreadsheetDocument document, string tableName, string styleName)
		{
			return new Table(document, tableName, styleName);
		}
		
		/// <summary>
		/// Creates the text document table.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="styleName">Name of the style.</param>
		/// <param name="rows">The rows.</param>
		/// <param name="columns">The columns.</param>
		/// <param name="width">The width.</param>
		/// <param name="useTableRowHeader">if set to <c>true</c> [use table row header].</param>
		/// <param name="useBorder">The useBorder.</param>
		/// <returns></returns>
		public static Table CreateTextDocumentTable(
			AODL.Document.TextDocuments.TextDocument document, 
			string tableName, 
			string styleName, 
			int rows, 
			int columns, 
			double width, 
			bool useTableRowHeader, 
			bool useBorder)
		{
			string tableCnt							= document.DocumentMetadata.TableCount.ToString();
			Table table								= new Table(document, tableName, styleName);
			table.TableStyle.TableProperties.Width	= width.ToString().Replace(",",".")+"cm";

			for(int i=0; i<columns; i++)
			{
				Column column						= new Column(table, "co"+tableCnt+i.ToString());
				column.ColumnStyle.ColumnProperties.Width = GetColumnCellWidth(columns, width);
				table.ColumnCollection.Add(column);
			}

			if (useTableRowHeader)
			{
				rows--;
				RowHeader rowHeader					= new RowHeader(table);
				Row row								= new Row(table, "roh1"+tableCnt);
				for(int i=0; i<columns; i++)
				{
					Cell cell						= new Cell(table.Document, "rohce"+tableCnt+i.ToString());
					if (useBorder)
						cell.CellStyle.CellProperties.Border = Border.NormalSolid;
					row.Cells.Add(cell);
				}
				rowHeader.RowCollection.Add(row);
				table.RowHeader						= rowHeader;
			}

			for(int ir=0; ir<rows; ir++)
			{
				Row row								= new Row(table, "ro"+tableCnt+ir.ToString());
				
				for(int ic=0; ic<columns; ic++)
				{
					Cell cell						= new Cell(table.Document, "ce"+tableCnt+ir.ToString()+ic.ToString());
					if (useBorder)
						cell.CellStyle.CellProperties.Border = Border.NormalSolid;
					row.Cells.Add(cell);
				}

				table.Rows.Add(row);
			}

			return table;
		}

		/// <summary>
		/// Gets the width of the column cell.
		/// </summary>
		/// <param name="columns">The columns.</param>
		/// <param name="tableWith">The table with.</param>
		/// <returns></returns>
		private static string GetColumnCellWidth(int columns, double tableWith)
		{
			double ccWidth							= (double)((tableWith/(double)columns));
			return ccWidth.ToString("F2").Replace(",",".");
		}
	}
}
