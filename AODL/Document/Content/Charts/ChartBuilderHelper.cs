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
using AODL.Document.Content.Tables;
using AODL.Document.Content.Text;

namespace AODL.Document.Content.Charts
{
	public class ChartBuilderHelper
	{
		IDocument m_document = null;
		ChartPlotArea ChartPlotArea = null;
		CellRanges m_tableData;
		int startRowIndex = 0;
		int endRowIndex   = 0;
		int startColumnIndex = 0;
		int endColumnIndex   = 0;
		Table table   = null;
		Table DataTable = null;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns>the null string cell</returns>
		/// <example ><texp/></example>
		private Cell  CreateNullStringCell(Table table)
		{
			Cell  cell  = new Cell (table.Document);
			Paragraph   paragraph  = new Paragraph (m_document );
			cell.Content .Add (paragraph);
			
			return cell;
		}
		
		/// <summary>
		/// create the row header of the data table of the chart
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		private RowHeader CreateRowHeader(Table table)
		{
			RowHeader   rowheader = new RowHeader (table);
			int startColumnIndex = m_tableData.startcell .columnIndex ;
			int endColumnIndex   = m_tableData.endcell .columnIndex ;
			Row  row              = new Row (table);
			Cell cell             = this.CreateNullStringCell (table);
			row.Cells .Add (cell);
			
			for(int i=startColumnIndex; i<=endColumnIndex; i++)
			{
				Cell  tempCell        = new Cell (table.Document);
				tempCell.OfficeValueType ="string";
				Paragraph   paragraph = new Paragraph (m_document);
				string  content       =((char)('A'+i-1)).ToString ()+"ĮŠ";
				paragraph.TextContent .Add (new SimpleText (m_document ,content));
				tempCell.Content .Add (paragraph);
				row.Cells .Add (tempCell);
	
			}
	
			rowheader.RowCollection .Add (row);
	
			return rowheader;
		}
		
		public ChartBuilderHelper(IDocument document, ChartPlotArea chartPlotArea, CellRanges tableData)
		{
			this.m_document = document;
			this.ChartPlotArea = chartPlotArea;
			this.m_tableData = tableData;
			
			startRowIndex = m_tableData.startcell.rowIndex ;
			endRowIndex   = m_tableData.endcell .rowIndex ;
			startColumnIndex = m_tableData.startcell .columnIndex ;
			endColumnIndex   = m_tableData.endcell .columnIndex ;
			
			table   = new Table (m_document ,"local-table",null);
			DataTable = m_tableData.table ;
	
			
		}
		
		private void BothHasLabels()
		{
			Row    row  = new Row (table);
			Cell   cell = new Cell (table.Document);
			Paragraph  paragraph  = new Paragraph (m_document );
			cell.Content .Add (paragraph);
			row.Cells .Add (cell);
	
			RowHeader   rowheader = new RowHeader (table);
			
			for(int i=startColumnIndex; i<endColumnIndex ; i++)
			{
				Cell cellTemp = m_tableData.table .Rows [startRowIndex-1].Cells[i];
				
				string cellRepeating = cellTemp.ColumnRepeating ;
				
				int  cellRepeated=0;
	
				if (cellRepeating!=null)
					cellRepeated= Int32.Parse (cellTemp.ColumnRepeating);
	
				if (cellRepeated >1)
				{
					for(int j=0; j<cellRepeated-1; j++)
					{
						row.Cells .Add (cellTemp);
						i++;
					}
				}
				
				row.Cells .Add (cellTemp);
	
			}
			
			rowheader.RowCollection .Add (row);
			
			table.RowHeader =rowheader;
	
	
			for(int i=startRowIndex; i<endRowIndex; i++)
				
			{
				Row tempRow = new Row (table);
				for(int j=startColumnIndex-1;j<endColumnIndex;j++)
				{
					Cell  cellTemp = m_tableData.table .Rows [i].Cells [j];
					string cellRepeating = cellTemp.ColumnRepeating;
					int   cellRepeated =0;
					
					if (cellRepeating!=null)
						cellRepeated= Int32.Parse (cellTemp.ColumnRepeating );
	
					if (cellRepeated>1)
						
					{
						for(int m=0; m<cellRepeated-1; m++)
						{
							tempRow.Cells .Add (cellTemp);
							i++;
						}
					}
	
					tempRow.Cells .Add (cellTemp);
				}
				
				table.Rows .Add (tempRow);
			}
		}
		
		/// <summary>
		/// create the row serial cell
		/// </summary>
		/// <param name="table"></param>
		/// <param name="SerialNum"></param>
		/// <returns></returns>
		private Cell  CreateRowSerialCell(Table table,int SerialNum)
		{
			Cell   cell = new Cell (table.Document);
			cell.OfficeValueType ="string";
			Paragraph   paragraph = new Paragraph (m_document);
			string   content      = SerialNum.ToString ()+"ŠŠ";
			paragraph.TextContent .Add (new SimpleText (m_document,content));
			cell.Content .Add (paragraph);
			return cell;
		}
	
	
		private void FirstLineLabels()
		{
			RowHeader   rowHeader = new RowHeader (table);
			Row         row       = new Row (table);
			Cell        cell      = CreateNullStringCell(table);
			row.Cells .Add (cell);
			
			for(int i=startColumnIndex; i<=endColumnIndex;i++)
			{
				Cell cellTemp = m_tableData.table .Rows [startRowIndex-1].Cells[i-1];
				int  cellRepeated =0;
				string cellRepeating = cellTemp.ColumnRepeating ;
				
				if (cellRepeating!=null)
					cellRepeated = Int32.Parse (cellTemp.ColumnRepeating);
				
				if (cellRepeated >1)
				{
					for(int j=0; j<cellRepeated-1; j++)
					{
						row.Cells .Add (cellTemp);
						i++;
					}
				}
	
				row.Cells .Add (cellTemp);
	
	
			}
			
			rowHeader.RowCollection .Add (row);
			table.RowHeader = rowHeader;
	
			for(int j=startRowIndex;j<=endRowIndex; j++)
			{
				Row  tempRow     = new Row (table);
				tempRow.Cells .Add (CreateRowSerialCell(table,j+1));
				
				for(int k=startColumnIndex;k<endColumnIndex; k++)
				{
					Cell cellTemp = m_tableData.table .Rows [j].Cells[k];
					int  cellRepeated =0;
					string cellRepeating = cellTemp.ColumnRepeating;
	
					if (cellRepeating!=null)
						cellRepeated=Int32.Parse (cellTemp.ColumnRepeating);
	
					if (cellRepeated >1)
					{
						for(int m=0; m<cellRepeated-1; m++)
						{
							row.Cells .Add (cellTemp);
							j++;
						}
					}
	
					tempRow.Cells .Add (cellTemp);
	
				}
	
				table.Rows .Add (tempRow);
	
			}
		}
		
		/// <summary>
		/// build the data table of the chart according to the struct of the cell range
		/// copy the data from the spreadsheet document table according to the cell range
		/// </summary>
		/// <returns>the data table</returns>
		public Table CreateTableFromCellRange()
		{
			
			
			if (ChartPlotArea.FirstColumnHasLabels() &&
			    ChartPlotArea .FirstLineHasLabels() )
			{
				BothHasLabels();
			}
	
			else if (ChartPlotArea.FirstColumnHasLabels())
			{
				RowHeader   rowHeader = CreateRowHeader(table);
	
				table.RowHeader =rowHeader;
				
				for(int i=startRowIndex; i<endRowIndex; i++)
				{
					table.Rows .Add (m_tableData.table .Rows [i]);
				}
			}
	
			else if (this.ChartPlotArea .FirstLineHasLabels ())
			{
				FirstLineLabels();
			}
	
			else
			{
				NoLabels();
			}
	
			return table;
		}
		
		private void NoLabels()
		{
			RowHeader   rowheader = rowheader = CreateRowHeader(table);
			table.RowHeader         = rowheader;
	
			for (int j=startRowIndex;j<=endRowIndex; j++)
			{
				Row  tempRow     = new Row (table);
				tempRow.Cells .Add (CreateRowSerialCell(table,j));
				
				for (int k = startColumnIndex; k <= endColumnIndex; k++)
				{
	
					Cell cell = m_tableData.table .Rows [j-1].Cells[k-1];
					int  cellRepeated =0;
					string cellRepeating = cell.ColumnRepeating;
					
					if (cellRepeating!=null)
						cellRepeated = Int32.Parse (cell.ColumnRepeating);
	
					if (cellRepeated >1)
					{
						for(int m=0; m<cellRepeated-1; m++)
						{
							tempRow.Cells .Add (cell);
							j++;
						}
					}
	
					tempRow.Cells .Add (cell);
				}
	
				table.Rows .Add (tempRow);
	
			}
		}
	}
}
