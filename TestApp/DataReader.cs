using System;
using System.Collections.Generic;
using System.IO;

namespace TestApp
{
    /// <summary>
    /// Читает данные из файла
    /// </summary>
    public class DataReader
    {
        /// <summary>
        /// Достать из файла все записи
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        public IEnumerable<StateRecord> ReadoutRecords(string fileName)
        {
            using var file = new StreamReader(fileName);
            // Пропускаем строку заголовка
            file.ReadLine();
            // Дальше читаем построчно
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var lineArr = line.Split(";");
                yield return new StateRecord
                {
                    StartTime = DateTime.Parse(lineArr[0]),
                    EndTime = DateTime.Parse(lineArr[1]),
                    OperatorName = lineArr[3],
                    State = lineArr[4],
                    DurationSec = long.Parse(lineArr[5])
                };
            }
        }
    }
}