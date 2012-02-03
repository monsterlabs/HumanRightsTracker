/*
 * Created by SharpDevelop.
 * User: darius.damalakas
 * Date: 2009.04.30
 * Time: 08:56
 * 
 */
using AODL.Document.Helper;
using System;
using System.Collections;
using System.Globalization;
using AODL.Document.Content;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Text;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;

namespace AODL.Utils
{
	public class ContentMocker
	{
		/// <summary>
		/// Gets paragraph from cell
		/// </summary>
		/// <param name="cell"></param>
		/// <returns></returns>
		private Paragraph GetParagraph(Cell cell)
		{
			if (cell.Content.Count == 0)
				return null;
			Paragraph p = cell.Content[0] as Paragraph;
			return p;
		}
		
		/// <summary>
		/// Returns index of the given in the given table
		/// </summary>
		[Obsolete]
		public int RowIndex(Table table, Row row)
		{
			IList Rows = table.Rows as IList;
			return Rows.IndexOf(row);
		}
		
		public IContent CloneAny(IContent content)
		{
			if (content is Table)
				return CloneTable(content as Table);
			if (content is Cell)
			{
				Cell cell = content as Cell;
				return CloneCell(cell);
			}
			else
				return new MainContentProcessor(content.Document)
					.CreateContent(content.Node);
		}
		
		public ContentCollection CloneContentCollection(ContentCollection contentCollection)
		{
			ContentCollection clonedContent = new ContentCollection();
			
			int index = 0;
			foreach (IContent content in contentCollection)
			{
				index ++;
				try
				{
					clonedContent.Add(CloneAny(content));
				}
				catch (ContentMockerException e)
				{
					throw new ContentMockerException(string.Format(
						"Could not clone element {0} in given collection. Item " +
						" index was {1}",
						content.Node.Value,
						index), e);
				}
			}
			return clonedContent;
		}
		
		private Paragraph CloneParagraph(Paragraph paragraph)
		{
			return paragraph.Clone() as Paragraph;
		}
		
		private Column CloneColumn(Column column, Table table)
		{
			Column clonedColumn = new Column(table, column.StyleName);
			clonedColumn.ColumnStyle = column.ColumnStyle;
			return column;
		}
		
		private Row CloneRow(Row row, Table table)
		{
			Row newRow = new Row(table, row.StyleName);
			foreach (Cell cell in row.Cells)
			{
				newRow.Cells.Add(CloneCell(cell));
			}
			return row;
		}
		
		static int tableNumber = 100;
		
		/// <summary>
		/// Gets the width of the column cell.
		/// </summary>
		/// <param name="columns">The columns.</param>
		/// <param name="tableWith">The table with.</param>
		/// <returns></returns>
		[Obsolete]
		private string GetColumnCellWidth(int columns, double tableWith)
		{
			double ccWidth	= (double)((tableWith/(double)columns));
			return ccWidth.ToString("F2").Replace(",",".");
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
		private Table CreateTextDocumentTable(
			AODL.Document.TextDocuments.TextDocument document,
			string tableName,
			string styleName,
			int rows,
			int columns,
			double width,
			Table originalTable)
		{
			string tableCnt							= document.DocumentMetadata.TableCount.ToString();
			Table table								= new Table(document, tableName, styleName);
			table.TableStyle.TableProperties.Width	= width.ToString().Replace(",",".")+"cm";

			for(int i=0; i < columns; i++)
			{
				Column column						= new Column(table, originalTable.ColumnCollection[i].StyleName);
				//column.ColumnStyle.ColumnProperties.Width = GetColumnCellWidth(columns, width);
				table.ColumnCollection.Add(column);
			}

			for(int ir=0; ir < rows; ir++)
			{
				Row row								= new Row(table, originalTable.Rows[ir].StyleName);
				
				for(int ic=0; ic < columns; ic++)
				{
					Cell cell						= new Cell(table.Document, originalTable.Rows[ir].Cells[ic].StyleName);
					//if (useBorder)
					//	cell.CellStyle.CellProperties.Border = Border.NormalSolid;
					row.Cells.Add(cell);
				}

				table.Rows.Add(row);
			}

			return table;
		}
		
		private int GetColumnCount(Table table)
		{
			int columnCount = 0;
			foreach (Row row in table.Rows)
			{
				if (row.Cells.Count > columnCount)
					columnCount = row.Cells.Count;
			}
			return columnCount;
		}
		
		private double ConvertToCM(string width)
		{
			try
			{
				bool isInch = SizeConverter.IsInch(width);
				NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
				width = width.Replace(",", ".");
				double w = SizeConverter.GetDoubleFromAnOfficeSizeValue(width);
				if (isInch)
					w = SizeConverter.InchToCm(w);
				return w;
			}
			catch (System.FormatException e)
			{
				throw new ContentMockerException(string.Format(
					"Failed to convert {0} into decimal value", width),  e);
			}
		}
		
		private Table CloneTable(Table table)
		{
			try
			{
				System.Text.StringBuilder builder = new System.Text.StringBuilder();
				
				double widht = ConvertToCM(table.TableStyle.TableProperties.Width);
				
				int rowCount = table.Rows.Count;
				int columnCount = GetColumnCount(table);
				
				Table clonedTable = CreateTextDocumentTable(
					table.Document as TextDocument,
					"generatedTable" + tableNumber,
					table.StyleName,
					rowCount,
					columnCount,
					widht,
					table);
				
				for (int i = 0; i < table.ColumnCollection.Count; i++)
				{
					clonedTable.ColumnCollection[i].ColumnStyle.ColumnProperties.Width =
						table.ColumnCollection[i].ColumnStyle.ColumnProperties.Width;
				}
				
				for (int i1 = 0; i1 < table.Rows.Count; i1++)
				{
					Row row = clonedTable.Rows[i1];
					Row clonedRow = CloneRow(table.Rows[i1], clonedTable);
					for (int i = 0; i < row.Cells.Count; i++)
					{
						CopyCellContens(table.Rows[i1].Cells[i], row.Cells[i]);
					}
				}
				return clonedTable;
			}
			catch (Exception e)
			{
				throw new ContentMockerException("Could not clone table", e);
			}
		}
		
		private void CopyCellContens(Cell source, Cell destination)
		{
			foreach (IContent content in source.Content)
			{
				IContent clonedContent = new MainContentProcessor(content.Document)
					.CreateContent(content.Node);
				destination.Content.Add(clonedContent);
			}
		}
		
		/// <summary>
		/// Sets all test in cell to given string
		/// </summary>
		[Obsolete]
		public void SetText(Cell cell, string text)
		{
			foreach (IContent content in cell.Content)
			{
				if (content is Paragraph == false)
					continue;
				
				Paragraph p = content as Paragraph;
				foreach (IText cellText in p.TextContent)
				{
					cellText.Text = text;
				}
			}
		}
		
		/// <summary>
		/// Makes a copy of a cell
		/// </summary>
		/// <returns></returns>
		private Cell CloneCell(Cell sourceCell)
		{
			Cell clonedCell = new Cell(sourceCell.Document, sourceCell.StyleName);
			foreach (IContent clonedContent in CloneContentCollection(
				new ContentCollection(sourceCell.Content)))
			{
				clonedCell.Content.Add(clonedContent);
			}
			
			return clonedCell;
		}
	}
}
