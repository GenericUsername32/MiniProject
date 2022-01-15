using System;
using System.Collections.Generic;
using System.Linq;


namespace MiniProject
{
    public class Asset
    {
        public string brand;
        public string model;
        public Office office;        
        public double price;
        public DateTime purchaseDate;

        public virtual void AssetInfo(int padding)
        {
           
            Console.WriteLine($"{office.location.PadRight(padding-1)} {this.GetType().Name.PadRight(padding-1)} {brand.PadRight(padding-1)} {model.PadRight(padding-1)} {string.Concat(this.office.CurrencyConverter(this.office.currency,this.price).ToString("F")," ",office.currency).PadRight(padding-1)} {purchaseDate.ToString("yyyy-MM-dd")}");
        }
    }

    public class Phone : Asset
    {
        public Phone(string brand, string model, string office, double price, DateTime purchaseDate)
        {
            this.brand = brand;
            this.model = model;
            this.office = new Office(office);
            this.price = price;
            this.purchaseDate = purchaseDate;
        }
    }

    public class Laptop : Asset
    {
        public Laptop(string brand, string model, string office, double price, DateTime purchaseDate)
        {
            this.brand = brand;
            this.model = model;
            this.office = new Office(office);
            this.price = price;
            this.purchaseDate = purchaseDate;
        }
    }

    class AssetList : Asset
    {
        private List<Asset> assetList = new List<Asset>();
        string assetType;
        string input;

        public void AddAsset()
        {
            while (true)
            {
                while (true)
                {
                    Console.WriteLine("Select which office to place the asset in. Los Angeles, Bangkok or Helsingborg.");
                    input = Console.ReadLine().ToLower().Trim(); ;
                    if (Offices.IsValidOffice(input))
                    {
                        office = new Office(input);
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid office.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                while (true)
                {
                    Console.WriteLine("What type of asset? Laptop or Phone.");
                    input = Console.ReadLine().ToLower().Trim();
                    if (input == "laptop" || input == "phone")
                    {
                        assetType = input;
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid asset type.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.WriteLine($"Whats the brand name of the {assetType}?");
                brand = Console.ReadLine();

                Console.WriteLine("Whats the model name?");
                model = Console.ReadLine();

                while (true)
                {
                    Console.WriteLine($"What is the price of the {assetType} in USD?");
                    if (double.TryParse(Console.ReadLine(), out price))
                    {
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid price.");
                    Console.ForegroundColor = ConsoleColor.Gray;

                }

                while (true)
                {
                    Console.WriteLine("Enter purchase date in format MM/dd/yyyy");

                    if (DateTime.TryParse(Console.ReadLine(), out purchaseDate))
                    {
                        break;
                    }
                }

                if (assetType == "laptop")
                {
                    assetList.Add(new Laptop(brand, model, office.location, price, purchaseDate));
                }
                else if (assetType == "phone")
                {
                    assetList.Add(new Phone(brand, model, office.location, price, purchaseDate));
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{char.ToUpper(assetType[0])}{assetType.Remove(0, 1)} has been added to the list of assets.\n");
                Console.ForegroundColor = ConsoleColor.Gray;

                while (true)
                {
                    Console.WriteLine("\nDo you wish to add more assets to the list? y/n");
                    input = Console.ReadLine().ToLower().Trim();
                    if (input == "y" || input == "n")
                        break;
                    Console.WriteLine("Invalid input.");
                }

                if (input == "n")
                    break;


            }
        }

        public override void AssetInfo(int padding)
        {
            var date1 = DateTime.Now;
            var sortedList = assetList.OrderBy(x => x.office.location).ThenBy(x => (date1.Ticks - x.purchaseDate.Ticks));

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Current list of assets:\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Office".PadRight(padding) + "Asset type".PadRight(padding) + "Brand".PadRight(padding) + "Model".PadRight(padding) + "Price".ToString().PadRight(padding) + "Purchase Date".PadRight(padding));
            foreach (Asset asset in sortedList)
            {
                if ((date1 - asset.purchaseDate).TotalDays > 365 * 3 - 90)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    asset.AssetInfo(padding);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if ((DateTime.Now - asset.purchaseDate).TotalDays > 365 * 3 - 180)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    asset.AssetInfo(padding);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    asset.AssetInfo(padding);
                }
            }
        }

        public int ObtainPadding()
        {
            //this method is used to dynamically change the padding depending on the character length of the properties we want to display

            List<double> paddingList = new List<double>();
            paddingList.Add(assetList.Max(x => x.office.location.Length));
            paddingList.Add(assetList.Max(x => x.brand.Length));
            paddingList.Add(assetList.Max(x => x.model.Length));
            paddingList.Add(Math.Floor(Math.Log10(assetList.Max(x => x.price)) + 1));
            var padding = Convert.ToInt32(paddingList.Max()) + 4;
            if (padding <= 14)
            {
                padding = 14;
            }
            return padding;
        }

    }

}

