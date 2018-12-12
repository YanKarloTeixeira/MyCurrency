using ConsoleMyCurrency.Helper;
using ConsoleMyCurrency.Models;
using ConsoleMyCurrency.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleMyCurrency
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 3600000;
            aTimer.Enabled = true;
            Console.WriteLine("=======================================");
            Console.WriteLine("|");
            Console.WriteLine("| Start :" + DateTime.Now);
            Console.WriteLine("| My currency system updating is running...");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("| Press \'q\' to quit.");
            Console.WriteLine("|");
            Console.WriteLine("=======================================");
            FixerIO.GenerateRate();
            while (Console.Read() != 'q') ;


            //Console.WriteLine(r);
            //Rate rate = JsonConvert.DeserializeObject<Rate>(r);


            Console.ReadKey();
        }

        public static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            FixerIO.GenerateRate();
            //Console.WriteLine(DateTime.Now.ToLocalTime());
        }

    }

    //public static class FixerIO
    //{

    //    public static async void GenerateRate()
    //    {
    //        HttpClient client = new HttpClient();
    //        client.DefaultRequestHeaders.Accept.Clear();
    //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //        client.BaseAddress = new
    //            Uri("http://data.fixer.io/api/latest?access_key=eb017afd3ade2c91f2bb31a462dc569e&symbols=CAD,USD,BRL,CNY,KRW&format=1");

    //        //HttpContent content;
    //        HttpResponseMessage response = await client.GetAsync("");
    //        HttpResponseMessage CurrencyResponse;

    //        string json = null;
    //        //json = "{ 'success':true,'timestamp':1544010546,'base':'EUR','date':'2018-12-05','rates':{'CAD':1.507613,'USD':1.134939,'BRL':4.358561,'CNY':7.783295,'KRW':1262.499979}}";
    //        RateJson r = null;
    //        HttpResponseMessage position;
    //        bool BaseCurrencyToCAD = true;
    //        Double CADConvertion = 0;
    //        if (response.IsSuccessStatusCode)
    //            //if (json.Length > 0)
    //        {
    //            json = await response.Content.ReadAsStringAsync();
    //            r = RateJson.FromJson(json);

    //            DateTime dt = DateTime.Now; // Or your date, as long as it is in DateTime format
    //            string tmp = dt.ToString("yyMMddHHmm");


    //            ConsolePosition p = new ConsolePosition();
    //            Console.WriteLine("========(" + DateTime.Now + ")========");
    //            Console.WriteLine("| Timestamp...: " + r.Timestamp);
    //            Console.WriteLine("| Base........:  CAD");
    //            foreach (var item in r.Rates)
    //            {
    //                //Converting BASE EUR to BASE CAD
    //                if (BaseCurrencyToCAD)
    //                {
    //                    p.Base = "CAD";
    //                    p.CurrencyName = "EUR";
    //                    CADConvertion = Convert.ToDouble(Math.Round(1 / item.Value, 5));
    //                    p.Value = CADConvertion;
    //                    Console.WriteLine("|");
    //                    Console.WriteLine("| Currency....: " + p.CurrencyName);
    //                    Console.WriteLine("| Value.......: " + p.Value);
    //                    BaseCurrencyToCAD = false;
    //                }
    //                else
    //                {
    //                    Console.WriteLine("|");
    //                    Console.WriteLine("| Currency....: " + item.Key);
    //                    Console.WriteLine("| Value.......: " + item.Value * CADConvertion);
    //                    p.Base = r.Base;
    //                    p.CurrencyName = item.Key;
    //                    p.Value = item.Value * CADConvertion;
    //                }
    //                p.BaseValue = 0;
    //                p.CurrencyTimeStamp = tmp;

    //                position = GlobalVariables.WebApiClient.PostAsJsonAsync("Positions", p).Result;
    //                if (position.IsSuccessStatusCode)
    //                    Console.WriteLine("| *** SUCESSFULL ***");
    //                else
    //                    Console.WriteLine("| !!! FAILED !!!");

    //                //CurrencyResponse = GlobalVariables.WebApiClient.GetAsync("AlertsByPosition/" + p.CurrencyName + "/" + p.Value).Result;
    //                CurrencyResponse = GlobalVariables.WebApiClient.GetAsync("Alerts").Result;
    //                if (CurrencyResponse.IsSuccessStatusCode)
    //                {
    //                    var alerts = CurrencyResponse.Content.ReadAsAsync<IEnumerable<ConsoleAlert>>().Result;
    //                    alerts = (from a in alerts where a.CurrencyName == p.CurrencyName && a.BestValue >= p.Value select a);
    //                    if (alerts.Count() > 0)
    //                    {
    //                        var DeniedEmails = EmailHelper.SendEmails(alerts, p.Value);
    //                        if (DeniedEmails.Count() > 0)
    //                        {
    //                            foreach (ConsoleAlert alert in DeniedEmails)
    //                            {
    //                                //var response = GlobalVariables.WebApiClient.GetAsync("DeletePosition/" + alert.AlertId.ToString()).Result;
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //            Console.WriteLine("======================================");

    //        }
    //        //return ("Internal Server error");
    //        //return (json);

    //    }

    //}
}