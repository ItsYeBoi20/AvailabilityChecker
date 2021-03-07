using System;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace AvailableChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckAmazon("https://www.amazon.de/-/en/dp/B08X8X5CWR/");
            Console.ReadLine();
        }

        static void CheckAmazon(string website)
        {
            while (true)
            {
                Console.Title = "Checking Every 1.5 Seconds...";

                Console.OutputEncoding = Encoding.UTF8;
                WebClient wc = new WebClient();
                string download = wc.DownloadString(website);
                var splitSource = Regex.Split(download, "<");
                foreach (string item in splitSource)
                {
                    if (item.Contains("class=\"a-size-medium a-color-price") && item.Contains("priceBlockBuyingPriceString"))
                    {
                        var splitted = Regex.Split(item, ">");
                        Console.OutputEncoding = Encoding.UTF8;
                        char c = '€';
                        string replaced = splitted[1].Replace("Â â‚¬", c.ToString());

                        Console.SetCursorPosition(0, 0);
                        Console.Write("Amazon: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(replaced);
                        Console.ResetColor();
                    }
                    else if (item.Contains("class=\"a-size-medium a-color-price") && !item.Contains("Â â") && !item.Contains("price-value"))
                    {
                        string replaced = item.Replace("\n", "");
                        var splitted = Regex.Split(replaced, ">");
                        string replaced1 = splitted[1].Replace("Ã¼", "ü");

                        if (!replaced1.Contains("â‚¬"))
                        {
                            Console.SetCursorPosition(0, 0);
                            Console.Write("Amazon: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(replaced1);
                            Console.ResetColor();
                        }
                    }
                }
                Thread.Sleep(1500);
            }
        }

        static void yourOwnSite(string website)
        {
            //Add your own site
        }
    }
}
