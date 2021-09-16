using ShoppingCart3.Dialogs;
using ShoppingCart3.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShoppingCart3
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (File.Exists($"{AppDataPaths.GetDefault().LocalAppData}\\saveFile.txt"))
            {
                DataContext = JsonConvert.DeserializeObject<MainViewModel>(File.ReadAllText($"{AppDataPaths.GetDefault().LocalAppData}\\saveFile.txt"));
            }
            else
            {
                DataContext = new MainViewModel();
            }
        }

        private async void AddToCart(object sender, RoutedEventArgs e)
        {
            var diag = new AddDialog((DataContext as MainViewModel).ProductType());
            var result = await diag.ShowAsync();
            if(result == ContentDialogResult.Primary)
            {
                var amount = diag.AmountA;
                if (diag.Conditional)
                {
                    (DataContext as MainViewModel).AddToCart(amount);
                }
            }
        }

        private async void RemoveFromCart(object sender, RoutedEventArgs e)
        {
            var diag = new RemoveDialog((DataContext as MainViewModel).ProductCType());
            var result = await diag.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var amount = diag.AmountR;
                if (diag.Conditional)
                {
                    (DataContext as MainViewModel).RemoveFromCart(amount);
                }
            }
        }

        private async void Checkout(object sender, RoutedEventArgs e)
        {
            var diag = new ReceiptDialog((DataContext as MainViewModel).Receipt());
            await diag.ShowAsync();
        }

        private void PreviousInventory(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).PreviousInventory();
        }

        private void PreviousCart(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).PreviousCart();
        }

        private void NextInventory(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).InventoryNext();
        }

        private void NextCart(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).NextCart();
        }
    }
}
