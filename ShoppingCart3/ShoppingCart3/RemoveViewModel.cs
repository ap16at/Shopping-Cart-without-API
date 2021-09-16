using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart3
{
    public class RemoveViewModel
    {
        public string RemoveDialogText { get; set; }
        public RemoveViewModel(string addText)
        {
            RemoveDialogText = addText;
        }
    }
}
