using System;
using System.Collections.Generic;
using System.Linq;


namespace MiniProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            
            AssetList assetList = new AssetList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Welcome to Asset manager 9000!\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            while (true)
            {
                assetList.AddAsset();
                assetList.AssetInfo(assetList.ObtainPadding());
                while (true)
                {
                    Console.WriteLine("\nDo you wish to add more assets type 'y'. type 'n' to exit the program.");
                    input = Console.ReadLine().ToLower().Trim();
                    if (input == "y" || input == "n")
                        break;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                if (input == "n")
                    break;

            }



        }
    }

    public class Office
    {
        public string location;
        public string currency;

        public Office(string location)
        {

            if (location == "hbg" || location.ToLower() == "helsingborg")
            {
                this.location = "Helsingborg";
                this.currency = "SEK";
            }
            else if (location == "bkk" || location.ToLower() == "bangkok")
            {
                this.location = "Bangkok";
                this.currency = "THB";
            }
            else if (location == "la" || location.ToLower() == "los angeles")
            {
                this.location = "Los Angeles";
                this.currency = "USD";
            }


        }

        public double CurrencyConverter(string currency, double price)
        {
            if (currency == "USD")
                return price;
            else if (currency == "SEK")
                return price * 9.03;
            else if (currency == "THB")
                return price * 33.17;
            else
                return price;            
        }

    }

    static class Offices
    {
        public static string[] officeArray = {"hbg", "helsingborg", "bkk", "bangkok", "la", "los angeles"};        
        
        public static bool IsValidOffice(string location)
        {
            if (officeArray.Contains(location))
                return true;
            else
                return false;
        }

    }

}

