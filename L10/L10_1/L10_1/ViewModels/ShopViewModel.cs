using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L10_1.ViewModels
{
    public class ShopViewModel
    {
        public int? Index { get; set; }
        public ShopViewModel() { }
        public ShopViewModel(int inx)
        {
            this.Index = inx;
        }
    }
}
