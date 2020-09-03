using System;
using System.Collections.Generic;

namespace BollingerBands.Api
{
    public interface ICandleProvider
    {
        /// <summary>
        /// Метод предоставляет данные для расчета индикатора
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        IEnumerable<Candle> GetCandles(DateTime start, DateTime end);
    }
}