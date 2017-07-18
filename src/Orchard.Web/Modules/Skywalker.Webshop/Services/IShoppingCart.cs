using Orchard;
using Skywalker.Webshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skywalker.Webshop.Services
{
    /// <summary>
    /// ShoppingCart interface
    /// </summary>
    //  in order for Orchard to be able to register our class with the dependency container, we need to inherit from IDependency
    public interface IShoppingCart : IDependency
    {
        IEnumerable<ShoppingCartItem> Items { get; }

        void Add(int productId, int quantity = 1);

        void Remove(int productId);

        ProductPart GetProduct(int productId);

        IEnumerable<ProductQuantity> GetProducts();

        decimal Subtotal();

        decimal Vat();

        decimal Total();

        int ItemCount();

        void UpdateItems();
    }
}
