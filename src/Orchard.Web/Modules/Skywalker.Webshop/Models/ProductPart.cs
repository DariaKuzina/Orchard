using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skywalker.Webshop.Models
{
    /// <summary>
    /// Custom content part
    /// Allows to turn any content type into a product that has a unit price and a sku
    /// </summary>
    public class ProductPart : ContentPart<ProductPartRecord>
    {

        public decimal UnitPrice
        {
            get { return Record.UnitPrice; }
            set { Record.UnitPrice = value; }
        }

        public string Sku
        {
            get { return Record.Sku; }
            set { Record.Sku = value; }
        }
    }
}
