using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectTrader
{
    /// <summary>
    /// CSV의 데이터를 표현합니다.
    /// 보통 0번 항목에 컬럼의 이름이 들어가고 1번항목부터 데이터입니다.
    /// </summary>
    public class CsvData
    {
        readonly string[,] csvTable;
        readonly Dictionary<string, int> nameIndexDic;

        public static CsvData LoadFromResources(string resourcePath)
        {
            return new CsvData(Resources.Load<TextAsset>(resourcePath));
        }

        public static CsvData LoadFromStream(Stream stream)
        {
            using (var textReader = new StreamReader(stream))
            {
                return new CsvData(textReader.ReadToEnd());
            }
        }

        public static CsvData LoadFromFile(string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                return LoadFromStream(fileStream);
            }
        }

        public CsvData(string csvText)
        {
            // 라인으로 문자열 자름
            var textLine = csvText.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var textLineCount = textLine.Length;

            // ','로 컬럼 구분후 카운팅
            var column = textLine[0].Split(',');
            var columnCount = column.Length;

            csvTable = new string[textLineCount, columnCount];
            nameIndexDic = new Dictionary<string, int>(columnCount);
            // 이름 인덱스 작성
            for (int i = 0; i < columnCount; i++)
            {
                nameIndexDic.Add(column[i], i);
            }

            for (int i = 0; i < textLineCount; i++)
            {
                column = textLine[i].Split(',');
                var currentColumnCount = columnCount;

                for (int j = 0; j < columnCount; j++)
                {
                    // 현재 컬럼 갯수가 첫라인의 컬럼 갯수보다 작고
                    // j가 현재 컬럼 갯수보다 크면 null할당
                    if (currentColumnCount >= columnCount || j < currentColumnCount)
                        csvTable[i, j] = column[j];
                }
            }
        }

        public CsvData(TextAsset textAsset) : this(textAsset.text)
        {
            // 오버라이드
        }

        public string this[int rowIndex, int columnIndex]
        {
            get
            {
                return csvTable[rowIndex, columnIndex];
            }
        }

        public string this[int lineIndex, string name]
        {
            get
            {
                return csvTable[lineIndex, nameIndexDic[name]];
            }
        }

        public int RowLength => csvTable.GetLength(0);
        public int ColumnLength => csvTable.GetLength(1);

        public int GetInt(int rowIndex, int columnIndex) => int.Parse(this[rowIndex, columnIndex]);
        public long GetLong(int rowIndex, int columnIndex) => long.Parse(this[rowIndex, columnIndex]);
        public float GetFloat(int rowIndex, int columnIndex) => float.Parse(this[rowIndex, columnIndex]);
        public double GetDouble(int rowIndex, int columnIndex) => double.Parse(this[rowIndex, columnIndex]);
        public string GetString(int rowIndex, int columnIndex) => this[rowIndex, columnIndex];

        public int GetInt(int lineIndex, string name) => int.Parse(this[lineIndex, name]);
        public long GetLong(int lineIndex, string name) => long.Parse(this[lineIndex, name]);
        public float GetFloat(int lineIndex, string name) => float.Parse(this[lineIndex, name]);
        public double GetDouble(int lineIndex, string name) => double.Parse(this[lineIndex, name]);
        public string GetString(int lineIndex, string name) => this[lineIndex, name];

        //public void SaveToStream(Stream targetStream)
        //{
        //    using (var streamWriter = new StreamWriter(targetStream))
        //    {
        //        for(int i = 0;i)
        //    }
        //}
    }
}
