using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BollingerBands.Api;

namespace BollingerBands
{
    public class BollingerBandsIndicator : IBollingerBandsIndicator
    {
        /// <summary>
        /// Период индикатора по умолчанию
        /// </summary>
        private int _period = 15;
        
        /// <summary>
        /// Отклонение индикатора по умолчанию
        /// </summary>
        private float _deviation = 1.5F;

        private ICandleProvider _candleProvider;

        public BollingerBandsIndicator(ICandleProvider candleProvider)
        {
            _candleProvider = candleProvider;
        }

        public IEnumerable<BollingerBandsData> Calculate(DateTime start, DateTime end)
        {
            double TL, ML, BL, StdDev;

            var listCandle = _candleProvider.GetCandles(start, end).ToList();

            var listBollingerBandsData = new List<BollingerBandsData>();

            //Очередь хранит в себе цены закрытия свечей, необходимые для расчета индикатора
            Queue<double> queuePriceClose = new Queue<double>();

            //Переменная хранит в себе сумму цен закрытия. Используется в формуле для расчета средней линии (ML)
            double sumPriceCandle = 0;

            //Первые записи свечей согласно периоду индикатора
            var listFirstCandle = listCandle.GetRange(0, _period);

            foreach (var candle in listFirstCandle)
            {
                queuePriceClose.Enqueue(candle.PriceCLose);
            }

            for (int i = _period-1; i < listCandle.Count; i++)
            {
                if (i >= _period)
                {
                    queuePriceClose.Dequeue();

                    queuePriceClose.Enqueue(listCandle[i].PriceCLose);
                }

                sumPriceCandle = 0;

                foreach (var priceClose in queuePriceClose)
                {
                    sumPriceCandle += priceClose;
                }

                ML = sumPriceCandle / _period;

                double sumStdDev = 0;

                foreach (var priceClose in queuePriceClose)
                {
                    sumStdDev += Math.Pow(priceClose - ML, 2);
                }

                StdDev = Math.Sqrt(sumStdDev / _period);

                TL = ML + (_deviation * StdDev);

                BL = ML - (_deviation * StdDev);

                listBollingerBandsData.Add(new BollingerBandsData() {ML = ML, BL = BL, TL = TL, Date = listCandle[i].Date});
            }

            return listBollingerBandsData;
        }

        public void Configure(int period, float deviation)
        {
            _period = period;
            _deviation = deviation;
        }

        /// <summary>
        /// Метод сохраняет результата расчета индикатора в файл
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="list"></param>
        public void SaveResultToFile(string savePath, IList<BollingerBandsData> list)
        {
            using (StreamWriter sw = new StreamWriter(savePath, false, Encoding.Default))
            {
                string[] parse = new string[list.Count];

                for (int i = 0; i < list.Count; i++)
                {
                    parse[i] = $"{list[i].Date};{list[i].TL};{list[i].ML};{list[i].BL}";
                }

                foreach (string str in parse)
                {
                    sw.WriteLine(str);
                }

                Console.WriteLine("Запись закончена");
            }
        }
    }
}
