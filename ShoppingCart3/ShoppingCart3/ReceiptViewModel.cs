using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart3
{
    public class ReceiptViewModel
    {
        public string ReceiptText { get; set; }
        public ReceiptViewModel(string receiptText)
        {
            ReceiptText = receiptText;
        }
    }
}
