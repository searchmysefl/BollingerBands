using BollingerBands.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CandleProvider
{
    public class Provider : ICandleProvider
    {
        private string path = @"e:\Documents\BollingerBands\BollingerBands\Chart.csv";

        public IEnumerable<Candle> GetCandles(DateTime start, DateTime end)
        {
            var candlelist = new List<Candle>();

            string[] candleData = null;

            string[] candleString = File.ReadAllLines(path);

            for (int i = 0; i < candleString.Length; i++)
            {
                if (!String.IsNullOrEmpty(candleString[i]))
                {
                    candleData = candleString[i].Split(";");

                    candlelist.Add(new Candle()
                    {
                        Date = DateParse(candleData[0]),
                        PriceCLose = Convert.ToDouble(candleData[4]),
                        PriceHigh = Convert.ToDouble(candleData[2]),
                        PriceLow = Convert.ToDouble(candleData[3]),
                        PriceOpen = Convert.ToDouble(candleData[1])
                    });
                }
            }

            var result = candlelist.Where(x => x.Date >= start && x.Date <= end);

            return result;
        }

        /// <summary>
        /// Так как мне неизвестен региональный формат даты, сделал дополнительный парсер даты
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private DateTime DateParse(string str)
        {
            string[] dateParse = str.Split(new string[] { "-", " ", ":" }, StringSplitOptions.None);

            int year = int.Parse(dateParse[0]);
            int month = int.Parse(dateParse[2]);
            int day = int.Parse(dateParse[1]);

            int hh = int.Parse(dateParse[3]);
            int mm = int.Parse(dateParse[4]);
            int ss = int.Parse(dateParse[5]);

            DateTime date = new DateTime(year, month, day, hh, mm, ss);

            return date;
        }
    }
}
