using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skywalker.Webshop.Models
{
    /// <summary>
    /// Holds the ID of the product being added as well as the quantity
    /// </summary>
    [Serializable]
    public class ShoppingCartItem
    {

        public int ProductId { get; private set; }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }

            set
            {
                if (value < 0)
                    throw new ArgumentException();

                _quantity = value;
            }
        }

        public ShoppingCartItem()
        {

        }

        public ShoppingCartItem(int productId, int quantity = 1)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}