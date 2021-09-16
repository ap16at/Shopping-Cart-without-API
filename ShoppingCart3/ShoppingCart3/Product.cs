using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ShoppingCart3.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DisplayCart
        {
            get
            {
                return $"| {Name,-21} | {Price.ToString("C"),-18} | {Id,-5} | {Description,-25}";
            }

        }
        public virtual double Price { get; set; }
        public int Id { get; set; }
    }

    class ProductByQuantity : Product
    {
        public ProductByQuantity(){}
        public double UnitPrice { get; set; }
        public double Units { get; set; }
        private double price;
        public override double Price
        {
            get { return price = UnitPrice * Units; }
            set { price = value; }
        }
        public string Receipt()
        {
            string item = string.Format("Product -> {0} || Price -> {1:C} || Amount -> {2} units", Name, Price.ToString("C"), Units);
            return item;
        }
    }

    class ProductByWeight : Product
    {
        public ProductByWeight(){}
        public double PricePerOunce { get; set; }
        public double Ounces { get; set; }
        private double price;
        public override double Price
        {
            get { return price = PricePerOunce * Ounces; }
            set { price = value; }

        }
        public string Receipt()
        {
            string item = string.Format("Product -> {0} || Price -> {1:C} || Amount -> {2} ounces", Name, Price.ToString("C"), Ounces);
            return item;
        }
    }
}
