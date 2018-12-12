using ConsoleMyCurrency.Helper;
using ConsoleMyCurrency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ConsoleMyCurrency.Utilities
{
    public static class FixerIO
    {

        public static async void GenerateRate()
        {
            /**************************
            *  Get FixerIO quotations
            **************************/

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync(
                "http://data.fixer.io/api/latest?access_key=eb017afd3ade2c91f2bb31a462dc569e&symbols=CAD,USD,BRL,CNY,KRW&format=1").Result;
            
            /*************************************************************************************************************/


            HttpResponseMessage CurrencyResponse; // Get the alerts' best values to be checked
            HttpResponseMessage position; // gets how was quotations storing in Position table

            string json = null; // get the currencies quotations 
            //json = "{ 'success':true,'timestamp':1544010546,'base':'EUR','date':'2018-12-05','rates':{'CAD':1.507613,'USD':1.134939,'BRL':4.358561,'CNY':7.783295,'KRW':1262.499979}}";


            RateJson r = null; // Class that can deserialize the json object
            bool BaseCurrencyToCAD = true; // Check the 1st round => Prepares the base conversion from Euro to Canadian Dollar 
                                           //(The free API does not permit a different base rather than EURO)

            Double CADConvertion = 0; // Canadian Dollar base conversor


            //if (json.Length > 0)
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                r = RateJson.FromJson(json);

                DateTime dt = DateTime.Now; // Or your date, as long as it is in DateTime format
                string tmp = dt.ToString("yyyyMMddHHmm");


                ConsolePosition p = new ConsolePosition();
                Console.WriteLine("========(" + DateTime.Now + ")========");
                Console.WriteLine("| Timestamp...: " + r.Timestamp);
                Console.WriteLine("| Base........:  CAD");
                foreach (var item in r.Rates)
                {
                    //Converting BASE EUR to BASE CAD
                    if (BaseCurrencyToCAD)
                    {
                        p.Base = "CAD";
                        p.CurrencyName = "EUR";
                        CADConvertion = Convert.ToDouble(Math.Round(1 / item.Value, 5));
                        p.Value = CADConvertion;
                        Console.WriteLine("|");
                        Console.WriteLine("| Currency....: " + p.CurrencyName);
                        Console.WriteLine("| Value.......: " + p.Value);
                        BaseCurrencyToCAD = false;
                    }
                    else
                    {
                        Console.WriteLine("|");
                        Console.WriteLine("| Currency....: " + item.Key);
                        Console.WriteLine("| Value.......: " + item.Value * CADConvertion);
                        p.Base = r.Base;
                        p.CurrencyName = item.Key;
                        p.Value = item.Value * CADConvertion;
                    }
                    p.BaseValue = 0;
                    p.CurrencyTimeStamp = tmp;

                    position = GlobalVariables.WebApiClient.PostAsJsonAsync("Positions", p).Result;
                    if (position.IsSuccessStatusCode)
                        Console.WriteLine("| *** SUCESSFULL ***");
                    else
                        Console.WriteLine("| !!! FAILED !!!");

                    CurrencyResponse = GlobalVariables.WebApiClient.GetAsync("Alerts").Result;
                    if (CurrencyResponse.IsSuccessStatusCode)
                    {
                        var alerts = CurrencyResponse.Content.ReadAsAsync<IEnumerable<ConsoleAlert>>().Result;
                        alerts = (from a in alerts where a.CurrencyName == p.CurrencyName && a.BestValue >= p.Value select a);
                        if (alerts.Count() > 0)
                        {
                            var DeniedEmails = EmailHelper.SendEmails(alerts, p.Value);
                            if (DeniedEmails.Count() > 0)
                            {
                                foreach (ConsoleAlert alert in DeniedEmails)
                                {
                                    HttpResponseMessage resp = GlobalVariables.WebApiClient.GetAsync("DeletePosition/" + alert.AlertId.ToString()).Result;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("======================================");

            }

        }

    }
    //class FixerIO
    //{
    //}
}
