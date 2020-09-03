using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BollingerBands.Api
{
    public class BollingerBandsData 
    {
        /// <summary>
        /// Средняя линия
        /// </summary>
        public double ML { get; set; }
        /// <summary>
        /// Верхняя линия
        /// </summary>
        public double TL { get; set; }
        /// <summary>
        /// Нижняя линия
        /// </summary>
        public double BL { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
        
    }
}
