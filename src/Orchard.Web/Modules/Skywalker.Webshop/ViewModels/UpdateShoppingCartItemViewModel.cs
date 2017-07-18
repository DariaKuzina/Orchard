using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skywalker.Webshop.ViewModels
{
    public class UpdateShoppingCartItemViewModel
    {
        public decimal ProductId { get; set; }
        public bool IsRemoved { get; set; }
        public int Quantity { get; set; }
    }
}