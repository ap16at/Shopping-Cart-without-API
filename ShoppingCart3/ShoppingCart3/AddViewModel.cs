using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart3
{
    public class AddViewModel
    {
        public string AddDialogText { get; set; }
        public AddViewModel(string addText)
        {
            AddDialogText = addText;
        }
    }
}
