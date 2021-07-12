using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    /// <summary>
    /// Класс для формирования отчётов
    /// </summary>
    public class ReportManager
    {
        private readonly StateRecord[] stateRecords;

        public ReportManager(IEnumerable<StateRecord> stateRecords)
        {
            this.stateRecords = stateRecords.ToArray();
        }
        
        /// <summary>
        /// Первый отчёт
        /// </summary>
        /// <returns>(дата, максимальное кол-во активных сессий в этот день)</returns>
        public IEnumerable<(DateTime date, long maxCount)> Report1()
        {
            if (stateRecords.Length == 0)
            {
                yield break;
            }

            var events = new List<(DateTime eventTime, EventType eventType)>(stateRecords.Length * 2);

            foreach (var stateRecord in stateRecords)
            {
                events.Add((stateRecord.StartTime, EventType.Start));
                events.Add((stateRecord.EndTime, EventType.End));
            }
            
            events.Sort();

            var currentDate = events[0].eventTime.Date;
            long maxCount = 0;
            long count = 0;
            foreach (var e in events)
            {
                // Следующий день
                if (e.eventTime.Date > currentDate)
                {
                    yield return (currentDate, maxCount);
                    currentDate = e.eventTime.Date;
                    maxCount = 0;
                    count = 0;
                }

                if (e.eventType == EventType.Start)
                {
                    count++;
                }
                else
                {
                    count--;
                }

                maxCount = Math.Max(maxCount, count);
            }

            yield return (currentDate, maxCount);
        }

        /// <summary>
        /// Второй отчёт
        /// </summary>
        /// <returns>фио оператора -> название состояния -> количество минут в таком состоянии</returns>
        public Dictionary<string, Dictionary<string, double>> Report2()
        {
            var result = new Dictionary<string, Dictionary<string, double>>();
            foreach (var stateRecord in stateRecords)
            {
                if (!result.TryGetValue(stateRecord.OperatorName, out var statesDict))
                {
                    statesDict = new Dictionary<string, double>();
                    result.Add(stateRecord.OperatorName, statesDict);
                }

                if (!statesDict.ContainsKey(stateRecord.State))
                {
                    statesDict.Add(stateRecord.State, 0);
                }

                statesDict[stateRecord.State] += stateRecord.DurationSec / 60.0;
            }

            return result;
        }
    }
}