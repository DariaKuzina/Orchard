using Orchard;
using Orchard.Mvc;
using Orchard.Themes;
using Skywalker.Webshop.Models;
using Skywalker.Webshop.Services;
using Skywalker.Webshop.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Skywalker.Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCart _shoppingCart;

        /// <summary>
        /// Access to often-used properties and services
        /// </summary>
        private readonly IOrchardServices _services;


        public ShoppingCartController(IShoppingCart shoppingCart, IOrchardServices services)
        {
            _shoppingCart = shoppingCart;
            _services = services;
        }

        [HttpPost]
        public ActionResult Add(int id)
        {
            _shoppingCart.Add(id, 1);

            return RedirectToAction("Index");
        }

        [Themed]
        public ActionResult Index()
        {
            var shape = _services.New.ShoppingCart(
                Products: _shoppingCart.GetProducts().Select(p=> _services.New.ShoppingCartItem(
                    ProductPart: p.ProductPart,
                    Quantity : p.Quantity,
                    Title : _services.ContentManager.GetItemMetadata(p.ProductPart).DisplayText)).ToList(),
                Total: _shoppingCart.Total(),
                Vat: _shoppingCart.Vat(),
                Subtotal: _shoppingCart.Subtotal()
                );

            // Return a ShapeResult
            return new ShapeResult(this, shape);
        }
        [HttpPost]
        public ActionResult Update(string command, UpdateShoppingCartItemViewModel[] items)
        {
            UpdateShoppingCart(items);

            switch (command)
            {
                case "Checkout":
                    break;
                case "ContinueShopping":
                    break;
                case "Update":
                    break;
            }
            return RedirectToAction("Index");
        }

        private void UpdateShoppingCart(IEnumerable<UpdateShoppingCartItemViewModel> items)
        {

            _shoppingCart.Clear();

            if (items == null)
                return;

            _shoppingCart.AddRange(items
                .Where(item => !item.IsRemoved)
                .Select(item => new ShoppingCartItem(item.ProductId, item.Quantity < 0 ? 0 : item.Quantity))
            );

            _shoppingCart.UpdateItems();
        }
    }
}