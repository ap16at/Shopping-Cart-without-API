using ShoppingCart3.Models;
using ShoppingCart3.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ShoppingCart3.Dialogs
{
    public sealed partial class RemoveDialog : ContentDialog
    {
        public bool Conditional { get; set; }
        public RemoveDialog(string itemType)
        {
            this.InitializeComponent();
            DataContext = new RemoveViewModel(itemType);
        }
        public string AmountR { get; set; }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.AmountR = AmountRTextBox.Text;
            Conditional = true;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Conditional = false;
        }
    }
}
