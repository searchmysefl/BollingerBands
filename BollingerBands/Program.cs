using System;
using System.Linq;
using BollingerBands;
using CandleProvider;

namespace ConsoleRuntime
{
    class Program
    {
        static void Main(string[] args)
        {
            var indicator = new BollingerBandsIndicator(new Provider());

            indicator.Configure(20,2);

            var list = indicator.Calculate(new DateTime(2020, 8, 11), new DateTime(2020, 09, 1)).ToList();

            indicator.SaveResultToFile(@"e:\Documents\BollingerBands\BollingerBands\result.csv", list);

        }
    }
}
