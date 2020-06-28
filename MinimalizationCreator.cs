using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Runtime.Serialization;
using NPOI.HSSF.Record;
using NPOI.XSSF.Streaming.Values;

namespace TablicaTrojkatna
{
    public class MinimalizationCreator
    
    {
        private XSSFWorkbook workbook;
        private ISheet sheetIn, sheetTrojkatna, sheetFullTrojkatna;
        private List<string> stateList;
        private List<string> argList;
        List<List<List<Vector2>>> statesValuesList;
        private Vector2 yValueCellFirstIndex = new Vector2(1, 1);
        private Vector2 argValueCellFirstIndex = new Vector2(1, 2);

        public MinimalizationCreator(XSSFWorkbook _workbook)
        {
            workbook = _workbook;
            sheetIn = workbook.GetSheet("TablicaStanow");
            
            stateList = GetTitleRowsList();
            argList = GetTitleColumnsList();
            statesValuesList = getTriangleValues(false);

            try
            {
                workbook.RemoveSheetAt(3); 
                workbook.RemoveSheetAt(2);
            }
            catch (Exception)
            {
                Console.WriteLine("Brak arkuszy do zastapienia");
            }

            sheetTrojkatna = workbook.CreateSheet("Trojkatna");
            sheetFullTrojkatna = workbook.CreateSheet("FullTrojkatna");

            BuildTriangleCells(sheetTrojkatna);
            BuildTriangleTitles(sheetTrojkatna);
            BuildTriangleValues(sheetTrojkatna);

            statesValuesList = getTriangleValues(true);

            BuildTriangleCells(sheetFullTrojkatna);
            BuildTriangleTitles(sheetFullTrojkatna);
            BuildTriangleValues(sheetFullTrojkatna);

        }
        private string getStringFromCell(ICell cell)
        {
            if (cell.CellType == CellType.Numeric)
                return cell.NumericCellValue.ToString();

            else if (cell.CellType == CellType.String)
                if (cell.StringCellValue == null)
                    return null;
                else
                    return cell.StringCellValue;
           
            else
                return null;
        }
        private int getIndexFromStringContent(string content)
        {
            foreach (string item in stateList)
            {
                if (item.Equals(content))
                {
                    return stateList.IndexOf(item);
                }
            }
            throw new Exception("Jedna z komórek zawiera nieistniejacy stan");
        }
        private List<string> GetTitleRowsList()
        {
            List<string> rowTitleList = new List<string>();
            for (int i = 1; i < sheetIn.LastRowNum; i++)
            {
                ICell cell = sheetIn.GetRow(i).GetCell(0);
                if (cell != null
                    && cell.CellType != CellType.Blank)
                {
                    Console.WriteLine(cell);
                    if (cell.CellType == CellType.Numeric)
                    {
                        rowTitleList.Add(cell.NumericCellValue.ToString());

                    }
                    else if(cell.CellType == CellType.String)
                    {
                        rowTitleList.Add(cell.StringCellValue);
                    }
                }
            }
            return rowTitleList;
        }
        private List<string> GetTitleColumnsList()
        {
            List<string> colTitleList = new List<string>();
            for (int i = 2; i < sheetIn.GetRow(0).Cells.Count; i++)
            {
                ICell cell = sheetIn.GetRow(0).GetCell(i);
                if (cell != null
                    && cell.CellType != CellType.Blank)
                {
                    Console.WriteLine(cell);
                    if (cell.CellType == CellType.Numeric)
                    {
                        colTitleList.Add(cell.NumericCellValue.ToString());

                    }
                    else if (cell.CellType == CellType.String)
                    {
                        colTitleList.Add(cell.StringCellValue);
                    }
                }
            }
            return colTitleList;
        }
        private void BuildTriangleCells(ISheet sheet)
        {
            for (int i = 0; i < stateList.Count; i++)
            {
                sheet.CreateRow(i);
                for (int j = 0; j < stateList.Count; j++)
                {
                    sheet.GetRow(i).CreateCell(j);
                }
            }
        }
        private  void BuildTriangleTitles(ISheet sheet)
        {
            for (int i = 0; i < stateList.Count-1; i++)
            {
                sheet.GetRow(i + 1).GetCell(0).SetCellValue(stateList[i]);
            }
            for (int i = 1; i <= stateList.Count-1; i++)
            {
                sheet.GetRow(0).GetCell(i).SetCellValue(stateList[i]);
            }
        }
        private  List<List<List<Vector2>>> getTriangleValues(bool fullMinimization)
        {
            List<List<List<Vector2>>> stateValuesList = new List<List<List<Vector2>>>();
            if (stateList.Count >= 2)
            {
                for (int w = 0; w < stateList.Count-1; w++)
                {
                    stateValuesList.Add(new List<List<Vector2>>());

                    for (int c = w + 1; c < stateList.Count; c++)
                    {
                        stateValuesList[w].Add(checkStates(w, c, fullMinimization));
                    }
                }
            }
            return stateValuesList;
        }
        private  void BuildTriangleValues(ISheet sheet)
        {
            int cell_index;
            foreach (List<List<Vector2>> row in statesValuesList)
            {
                cell_index = 0;
                foreach (List<Vector2> cell in row)
                {
                    string content = "";
                    if (cell.Count > 0)
                    {
                        foreach (var vector in cell)
                        {
                            if(vector.X != -1 || vector.Y != -1)
                            {
                                
                                content += $"({stateList[(int)vector.X]}," +
                                    $"{stateList[(int)vector.Y]})";
                            }
                        }
                        if(content.Equals(""))
                        {
                            content = "V";
                        }
                    }
                    else
                    {
                        content = "X";
                    }
                    sheet.GetRow(1 + statesValuesList.IndexOf(row))
                        .GetCell(1 + cell_index + statesValuesList.IndexOf(row))
                        .SetCellValue(content);

                    cell_index++;
                }
            }
        }
        private List<Vector2> checkStates(int stateWindex, int stateCindex, bool fullMinimization)
        {
            List<Vector2> cellContent = new List<Vector2>();

            ICell cellWy, cellCy, cellWarg, cellCarg;

            cellWy = sheetIn.GetRow((int)yValueCellFirstIndex.X + stateWindex)
                .GetCell((int)yValueCellFirstIndex.Y);
            cellCy = sheetIn.GetRow((int)yValueCellFirstIndex.X + stateCindex)
                .GetCell((int)yValueCellFirstIndex.Y);

            string cellW_YString = getStringFromCell(cellWy);
            string cellC_YString = getStringFromCell(cellCy);

            if (cellW_YString == cellC_YString
                          || cellW_YString == "-"
                          || cellC_YString == "-")
            {
                for (int i = 0; i < argList.Count; i++)
                {
                    cellWarg = sheetIn.GetRow((int)argValueCellFirstIndex.X + stateWindex)
                        .GetCell((int)argValueCellFirstIndex.Y + i);
                    cellCarg = sheetIn.GetRow((int)argValueCellFirstIndex.X + stateCindex)
                        .GetCell((int)argValueCellFirstIndex.Y + i);

                    string cellW_ArgString = getStringFromCell(cellWarg);
                    string cellC_ArgString = getStringFromCell(cellCarg);

                    string cellWstateString = stateList[stateWindex];
                    string cellCstateString = stateList[stateCindex];

                    if (cellW_ArgString == null
                        || cellC_ArgString == null
                        || (cellC_ArgString == cellWstateString
                            && cellW_ArgString == cellWstateString)
                        || (cellC_ArgString == cellCstateString
                            && cellW_ArgString == cellCstateString)
                        || (cellC_ArgString == cellWstateString
                            && cellW_ArgString == cellCstateString)
                        || (cellC_ArgString == cellCstateString
                            && cellW_ArgString == cellWstateString)
                        || cellC_ArgString == cellW_ArgString
                        )
                    {
                        cellContent.Add(new Vector2(-1, -1));
                    }
                    else
                    {
                        if (!fullMinimization)
                        {
                            cellContent.Add(new Vector2(getIndexFromStringContent(cellW_ArgString),
                            getIndexFromStringContent(cellC_ArgString)
                                ));
                        }
                        else
                        {
                            foreach (Vector2 vector in checkStates(getIndexFromStringContent(cellW_ArgString),
                                getIndexFromStringContent(cellC_ArgString),
                                fullMinimization))
                            {
                                if(vector.X != -1 || vector.Y != -1)
                                {
                                    cellContent.Add(new Vector2(-1, -1));
                                    
                                }
                            }
                            return new List<Vector2>();
                        }
                    }
                }
            }
            return cellContent;
        }
    }
}
