using System;
using System.Collections.Generic;

namespace BollingerBands.Api
{
    public interface IBollingerBandsIndicator
    {
        /// <summary>
        /// Метод расчета индикатора
        /// </summary>
        /// <param name="start">Начало расчетного периода</param>
        /// <param name="end">Конец расчетного периода</param>
        /// <returns></returns>
        IEnumerable<BollingerBandsData> Calculate(DateTime start, DateTime end);


        /// <summary>
        /// Метод настройки периода и отклонения индикатора
        /// </summary>
        /// <param name="period"></param>
        /// <param name="deviation"></param>
        void Configure(int period, float deviation);
    }
}
