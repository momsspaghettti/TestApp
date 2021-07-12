using System;

namespace TestApp
{
    /// <summary>
    /// Еденичная запись состояния оператора
    /// </summary>
    public class StateRecord
    {
        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// Имя оператора
        /// </summary>
        public string OperatorName { get; set; }
        
        /// <summary>
        /// Состояние
        /// </summary>
        public string State { get; set; }
        
        /// <summary>
        /// Длительность, сек
        /// </summary>
        public long DurationSec { get; set; }
    }
}