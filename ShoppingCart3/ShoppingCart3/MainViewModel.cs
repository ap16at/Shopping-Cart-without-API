using ShoppingCart3.Models;
using Newtonsoft.Json;
using System.IO;
using Windows.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart3.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<InventoryItem> Inventory { get; set; }
        public ObservableCollection<InventoryItem> InventoryWindow { get; set; }
        public InventoryItem SelectedProduct { get; set; }
        public Product SelectedProductC { get; set; }
        public ObservableCollection<Product> Cart { get; set; }
        public ObservableCollection<Product> CartWindow { get; set; }
        public string AddDialogText { get; set; }
        public string RemoveDialogText { get; set; }

        public string Subtotal => $" Subtotal {Cart.Sum(i => i.Price):C}";
        public string Tax => $" Tax {((Cart.Sum(i => i.Price)) * 0.07):C}";
        public string Total => $" Total {((Cart.Sum(i => i.Price)) * 1.07):C}";
        public string ReceiptDisplay => $"{Subtotal}\n{Tax}\n{Total}";
        private int currentPageI = 1;
        private int currentPageC = 1;
        private int pageSize = 5;

        public MainViewModel()
        {
            Inventory = new ObservableCollection<InventoryItem>();
            Cart = new ObservableCollection<Product>();
            CartWindow = new ObservableCollection<Product>();
            InventoryWindow = new ObservableCollection<InventoryItem>();

            Inventory.Add(new InventoryItemByQuantity { Name = "Lightbulb", Id = 101, UnitPrice = 1.50, Description = "1 lightbulb" });
            Inventory.Add(new InventoryItemByQuantity { Name = "Ketchup", Id = 102, UnitPrice = 6.99, Description = "1 Heinz Ketchup bottle" });
            Inventory.Add(new InventoryItemByQuantity { Name = "Toilet Paper", Id = 103, UnitPrice = 2.49, Description = "1 roll" });
            Inventory.Add(new InventoryItemByQuantity { Name = "Paper Towel Roll", Id = 104, UnitPrice = 2.99, Description = "1 roll" });
            Inventory.Add(new InventoryItemByQuantity { Name = "Toothbrush", Id = 105, UnitPrice = 4.99, Description = "1 toothbrush" });
            Inventory.Add(new InventoryItemByQuantity { Name = "Coke", Id = 106, UnitPrice = 1.99, Description = "1 2-liter bottle" });
            Inventory.Add(new InventoryItemByQuantity { Name = "Lays potato chips", Id = 107, UnitPrice = 2.99, Description = "1 bag of chips" });
            Inventory.Add(new InventoryItemByQuantity { Name = "Spaghetti", Id = 108, UnitPrice = 5.99, Description = "1 box of pasta" });
            Inventory.Add(new InventoryItemByQuantity { Name = "Can of soup", Id = 109, UnitPrice = 6.99, Description = "Chicken noodle" });
            Inventory.Add(new InventoryItemByQuantity { Name = "Frying Pan", Id = 110, UnitPrice = 17.99, Description = "Non-stick" });
            Inventory.Add(new InventoryItemByWeight { Name = "Apples", Id = 201, PricePerOunce = 2.79, Description = "Red apple" });
            Inventory.Add(new InventoryItemByWeight { Name = "Bannanas", Id = 202, PricePerOunce = 1.34, Description = "Yellow banana" });
            Inventory.Add(new InventoryItemByWeight { Name = "Ribeye", Id = 203, PricePerOunce = 3.50, Description = "Ribeye steak" });
            Inventory.Add(new InventoryItemByWeight { Name = "Chicken breast", Id = 204, PricePerOunce = 2.50, Description = "Cage free" });
            Inventory.Add(new InventoryItemByWeight { Name = "Salmon", Id = 205, PricePerOunce = 2.35, Description = "Alaskan" });
            Inventory.Add(new InventoryItemByWeight { Name = "Potatoes", Id = 206, PricePerOunce = 0.69, Description = "yellow" });
            Inventory.Add(new InventoryItemByWeight { Name = "Onions", Id = 207, PricePerOunce = 0.69, Description = "sweet onions" });
            Inventory.Add(new InventoryItemByWeight { Name = "Avocados", Id = 208, PricePerOunce = 1.28, Description = "Mexican Hass" });
            Inventory.Add(new InventoryItemByWeight { Name = "Red Bell Peppers", Id = 209, PricePerOunce = 0.99, Description = "Organic" });
            Inventory.Add(new InventoryItemByWeight { Name = "Green Bell Peppers", Id = 210, PricePerOunce = 0.99, Description = "Organic" });

            GetWindowI(currentPageI);
            GetWindowC(currentPageC);
        }

        public string ProductType()
        {
            string type;

            if(SelectedProduct is InventoryItemByQuantity)
            {
                return type = "How many Units?\n";
            }
            else if(SelectedProduct is InventoryItemByWeight)
            {
                return type = "How many Ounces?\n";
            }
            else
            {
                return null;
            }

        }

        public string ProductCType()
        {
            string type;

            if (SelectedProductC is ProductByQuantity)
            {
                return type = "How many Units?\n";
            }
            else if (SelectedProductC is ProductByWeight)
            {
                return type = "How many Ounces?\n";
            }
            else
            {
                return null;
            }

        }

        public void AddToCart(string amount)
        {
            int amountInt = 0;
            double amountDouble = 0;

            if(amount == null || SelectedProduct == null)
            {
                return;
            }
            else if (amount.Contains("."))
            {
                try{ amountDouble = Convert.ToDouble(amount); }
                catch{ return; }
            }
            else
            {
                try{ amountInt = int.Parse(amount); }
                catch { return; }
            }

            if (SelectedProduct == null)
            {
                return;
            }
            else if (amountInt == 0 && amountDouble == 0)
            {
                return;
            }

            if (SelectedProduct is InventoryItemByQuantity)
            {
                if (Cart.Any(i => i.Name.Equals(SelectedProduct.Name)))
                {
                    var temp = new Product();
                    temp = Cart.FirstOrDefault(i => i.Name.Equals(SelectedProduct.Name));
                    double unitsTemp = ((temp as ProductByQuantity).Units);
                    double newAmount = unitsTemp + amountInt;
                    Cart.Remove(temp);
                    Cart.Add(new ProductByQuantity
                    {
                        Name = (SelectedProduct as InventoryItemByQuantity).Name,
                        Id = (SelectedProduct as InventoryItemByQuantity).Id,
                        Description = (SelectedProduct as InventoryItemByQuantity).Description,
                        UnitPrice = (SelectedProduct as InventoryItemByQuantity).UnitPrice,
                        Units = newAmount
                    });
                }
                else
                {
                    Cart.Add(new ProductByQuantity
                    {
                        Name = (SelectedProduct as InventoryItemByQuantity).Name,
                        Id = (SelectedProduct as InventoryItemByQuantity).Id,
                        Description = (SelectedProduct as InventoryItemByQuantity).Description,
                        UnitPrice = (SelectedProduct as InventoryItemByQuantity).UnitPrice,
                        Units = amountInt
                    });
                }
            }
            else if (SelectedProduct is InventoryItemByWeight)
            {
                if(amountDouble == 0)
                {
                    amountDouble = amountInt;
                }
                if (Cart.Any(i => i.Name.Equals(SelectedProduct.Name)))
                {
                    var temp = new Product();
                    temp = Cart.FirstOrDefault(i => i.Name.Equals(SelectedProduct.Name));
                    double ouncesTemp = ((temp as ProductByWeight).Ounces);
                    double newAmount = ouncesTemp + amountDouble;
                    Cart.Remove(temp);
                    Cart.Add(new ProductByWeight
                    {
                        Name = (SelectedProduct as InventoryItemByWeight).Name,
                        Id = (SelectedProduct as InventoryItemByWeight).Id,
                        Description = (SelectedProduct as InventoryItemByWeight).Description,
                        PricePerOunce = (SelectedProduct as InventoryItemByWeight).PricePerOunce,
                        Ounces = newAmount
                    });
                }
                else
                {
                    Cart.Add(new ProductByWeight
                    {
                        Name = (SelectedProduct as InventoryItemByWeight).Name,
                        Id = (SelectedProduct as InventoryItemByWeight).Id,
                        Description = (SelectedProduct as InventoryItemByWeight).Description,
                        PricePerOunce = (SelectedProduct as InventoryItemByWeight).PricePerOunce,
                        Ounces = amountDouble
                    });
                }    
            }

            SelectedProduct = null;
            NotifyPropertyChanged("SelectedProduct");
            NotifyPropertyChanged("ReceiptDisplay");
            GetWindowC(currentPageC);
        }

        public void RemoveFromCart(string amount)
        {
            int amountInt = 0;
            double amountDouble = 0;

            if (amount == null || SelectedProductC == null)
            {
                return;
            }
            else if (amount.Contains("."))
            {
                try { amountDouble = Convert.ToDouble(amount); }
                catch { return; }
            }
            else
            {
                try { amountInt = int.Parse(amount); }
                catch { return; }
            }

            if (SelectedProductC == null)
            {
                return;
            }
            else if (amountInt == 0 && amountDouble == 0)
            {
                return;
            }
            if (SelectedProductC is ProductByQuantity)
            {
                if ((SelectedProductC as ProductByQuantity).Units < amountInt)
                {
                    return;
                }
                else if((SelectedProductC as ProductByQuantity).Units == amountInt)
                {
                    Cart.Remove(SelectedProductC);
                }
                else
                {
                    var temp = new Product();
                    temp = Cart.FirstOrDefault(i => i.Name.Equals(SelectedProductC.Name));
                    double unitsTemp = ((temp as ProductByQuantity).Units);
                    double newAmount = unitsTemp - amountInt;
                    Cart.Remove(temp);
                    Cart.Add(new ProductByQuantity
                    {
                        Name = (SelectedProductC as ProductByQuantity).Name,
                        Id = (SelectedProductC as ProductByQuantity).Id,
                        Description = (SelectedProductC as ProductByQuantity).Description,
                        UnitPrice = (SelectedProductC as ProductByQuantity).UnitPrice,
                        Units = newAmount
                    });
                }
            }
            else if (SelectedProductC is ProductByWeight)
            {
                if (amountDouble == 0)
                {
                    amountDouble = amountInt;
                }
                if ((SelectedProductC as ProductByWeight).Ounces < amountDouble)
                {
                    return;
                }
                else if((SelectedProductC as ProductByWeight).Ounces == amountDouble)
                {
                    Cart.Remove(SelectedProductC);
                }
                else
                {
                    var temp = new Product();
                    temp = Cart.FirstOrDefault(i => i.Name.Equals(SelectedProductC.Name));
                    double ouncesTemp = ((temp as ProductByWeight).Ounces);
                    double newAmount = ouncesTemp - amountDouble;
                    Cart.Remove(temp);
                    Cart.Add(new ProductByWeight
                    {
                        Name = (SelectedProductC as ProductByWeight).Name,
                        Id = (SelectedProductC as ProductByWeight).Id,
                        Description = (SelectedProductC as ProductByWeight).Description,
                        PricePerOunce = (SelectedProductC as ProductByWeight).PricePerOunce,
                        Ounces = newAmount
                    });
                }
            }

            SelectedProduct = null;
            NotifyPropertyChanged("SelectedProduct");
            NotifyPropertyChanged("ReceiptDisplay");
            GetWindowC(currentPageC);
        }

        public string Receipt()
        {
            string ReceiptText;
            double subtotal = 0;
            double tax = 0;
            double total = 0;

            string header = "\nPress Okay to exit program.\n--------------------------------------------------------------------------------------\n";

            string items = null;

            for(int i = 0; i < Cart.Count; i++)
            {
                if (Cart[i] is ProductByQuantity)
                {

                    string temp = (Cart[i] as ProductByQuantity).Receipt();
                    temp += "\n";
                    items += temp;
                    subtotal += (Cart[i] as ProductByQuantity).Price;
                }
                else if (Cart[i] is ProductByWeight)
                {
                    string temp = (Cart[i] as ProductByWeight).Receipt();
                    temp += "\n";
                    items += temp;
                    subtotal += (Cart[i] as ProductByWeight).Price;
                }
            }
            tax = 0.07 * subtotal;
            total = subtotal + tax;
            string divider = "--------------------------------------------------------------------------------------\n";

            string sub = "Subtotal: $" + string.Format("{0:0.00}", subtotal) + "\n";
            string tx = "Tax: $" + string.Format("{0:0.00}", tax) + "\n";
            string tot = "TOTAL: $" + string.Format("{0:0.00}", total);

            ReceiptText = header + items + divider + sub + tx + tot;

            File.WriteAllText($"{AppDataPaths.GetDefault().LocalAppData}\\saveFile.txt", JsonConvert.SerializeObject(this));

            return ReceiptText;
            
        }

        /// <summary>
        /// Navigation Section for Inventory
        /// </summary>

        private int lastPageI
        {
            get
            {
                var val = Inventory.Count / pageSize;

                if (Inventory.Count % pageSize > 0)
                {
                    val++;
                }

                return val;
            }
        }

        public void GetWindowI(int currentPage)
        {
            if (InventoryWindow.Count > 0)
            {
                InventoryWindow.Clear();
            }

            for (int i = (currentPage - 1) * pageSize; i < (currentPage - 1) * pageSize + pageSize && i < Inventory.Count; i++)
            {
                InventoryWindow.Add(Inventory[i]);
            }
        }

        public void PreviousInventory()
        {
            if (currentPageI - 1 <= 0)
            {
            }
            else
            {
                currentPageI--;
                GetWindowI(currentPageI);
            }
        }

        public void InventoryNext()
        {
            if (currentPageI + 1 > lastPageI)
            {
            }
            else
            {
                currentPageI++;
                GetWindowI(currentPageI);
            }
        }

        /// <summary>
        /// Navigation Section for Cart
        /// </summary>
        private int lastPageC
        {
            get
            {
                var val = Cart.Count / pageSize;

                if (Cart.Count % pageSize > 0)
                {
                    val++;
                }

                return val;
            }
        }

        public void GetWindowC(int currentPage)
        {
            if (CartWindow.Count > 0)
            {
                CartWindow.Clear();
            }
            for (int i = (currentPage - 1) * pageSize; i < (currentPage - 1) * pageSize + pageSize && i < Cart.Count; i++)
            {
                CartWindow.Add(Cart[i]);
            }
        }

        public void PreviousCart()
        {
            if (currentPageC - 1 <= 0)
            {
            }
            else
            {
                currentPageC--;
                GetWindowC(currentPageC);
            }
        }

        public void NextCart()
        {
            if (currentPageC + 1 > lastPageC)
            {
            }
            else
            {
                currentPageC++;
                GetWindowC(currentPageC);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
