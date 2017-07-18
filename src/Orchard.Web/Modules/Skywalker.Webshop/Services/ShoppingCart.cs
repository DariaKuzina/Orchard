using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skywalker.Webshop.Models;
using Orchard;
using Orchard.ContentManagement;

namespace Skywalker.Webshop.Services
{
    public class ShoppingCart : IShoppingCart
    {
        private const string _shoppingCartSession = "ShoppingCart";

        /// <summary>
        /// Access to the current request and related data
        /// </summary>
        private readonly IWorkContextAccessor _workContextAccessor;

        /// <summary>
        /// Orchard's API to content item management
        /// </summary>
        private readonly IContentManager _contentManager;

        private decimal _taxRate = 0.19m;

        private HttpContextBase HttpContext
        {
            get { return _workContextAccessor.GetContext().HttpContext; }
        }

        private List<ShoppingCartItem> ItemsInternal
        {
            get
            {
                var items = (List<ShoppingCartItem>)HttpContext.Session[_shoppingCartSession];

                if (items == null)
                {
                    items = new List<ShoppingCartItem>();
                    HttpContext.Session[_shoppingCartSession] = items;
                }

                return items;
            }
        }
        public IEnumerable<ShoppingCartItem> Items
        {
            get
            {
                { return ItemsInternal.AsEnumerable(); }
            }
        }

        public ShoppingCart(IWorkContextAccessor workContextAccessor, IContentManager contentManager)
        {
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
        }

        public void Add(int productId, int quantity = 1)
        {
            var item = Items.SingleOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                item = new ShoppingCartItem(productId, quantity);
                ItemsInternal.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public ProductPart GetProduct(int productId)
        {
            return _contentManager.Get<ProductPart>(productId);
        }

        public int ItemCount()
        {
            return Items.Sum(x => x.Quantity);
        }

        public void Remove(int productId)
        {
            var item = Items.SingleOrDefault(x => x.ProductId == productId);

            if (item == null)
                return;

            ItemsInternal.Remove(item);
        }

        public decimal Subtotal()
        {
            return Items.Select(x => GetProduct(x.ProductId).UnitPrice * x.Quantity).Sum();
        }

        public decimal Total()
        {
            return Subtotal() + Vat();
        }

        public void UpdateItems()
        {
            ItemsInternal.RemoveAll(x => x.Quantity == 0);
        }

        public decimal Vat()
        {
            return Subtotal() * _taxRate;
        }

        private void Clear()
        {
            ItemsInternal.Clear();
            UpdateItems();
        }

        public IEnumerable<ProductQuantity> GetProducts()
        {
            var ids = Items.Select(x => x.ProductId).ToList();

            var productParts = _contentManager.GetMany<ProductPart>(ids, VersionOptions.Latest, QueryHints.Empty).ToArray();

            var query = from item in Items
                        join productPart in productParts on item.ProductId equals productPart.Id
                        select new ProductQuantity()
                        {
                            ProductPart = productPart,
                            Quantity = item.Quantity
                        };

            return query;
        }
}
}