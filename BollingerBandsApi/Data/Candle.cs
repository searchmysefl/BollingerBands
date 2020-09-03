using System;
using System.Collections.Generic;
using System.Text;

namespace BollingerBands.Api

{
    public class Candle
    {
        /// <summary>
        /// Дата и время
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Цена закрытия
        /// </summary>
        public double PriceCLose { get; set; }
        /// <summary>
        /// Цена открытия
        /// </summary>
        public double PriceOpen { get; set; }
        /// <summary>
        /// Цена верхний точки (High)
        /// </summary>
        public double PriceHigh { get; set; }
        /// <summary>
        /// Цена нижний точки (Low)
        /// </summary>
        public double PriceLow { get; set; }
    }
}
