using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.WebUI;
using Windows.UI.Xaml;

namespace ShoppingCart3.Models
{
    public class InventoryItem
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual double Price { get; set; }

        public int Id { get; set; }

        public string DisplayInventory
        {
            get
            {
                return $"| {Name,-21} | {Price.ToString("C"),-18} | {Id, -5} | {Description,-25}";
            }

        }
    }
    public class InventoryItemByQuantity : InventoryItem
    {
        public double UnitPrice { get; set; }
        private double price;
        public override double Price
        {
            get { return price = UnitPrice; }
            set { price = value; }
        }
    }
    public class InventoryItemByWeight : InventoryItem
    {
        public double PricePerOunce { get; set; }
        private double price;
        public override double Price
        {
            get { return price = PricePerOunce; }
            set { price = value; }
        }
    }
}
