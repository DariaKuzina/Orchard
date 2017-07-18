using Orchard.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skywalker.Webshop
{
    /// <summary>
    /// Automatically includes the dependency resources
    /// </summary>
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            // Create and add a new manifest
            var manifest = builder.Add();

            // Define a "common" style sheet
            manifest.DefineStyle("Skywalker.Webshop.Common").SetUrl("common.css");

            // Define the "shoppingcart" style sheet
            manifest.DefineStyle("Skywalker.Webshop.ShoppingCart")
                .SetUrl("shoppingcart.css")

                /*This enables us to just reference "Skywalker.Webshop.ShoppingCart" in our "ShoppingCart.cshtml" template,
                  without having to reference "Webshop.Common" as well. Orchard will automatically include dependent resources.*/
                .SetDependencies("Skywalker.Webshop.Common");

            // Define the "shoppingcartwidget" style sheet 
            manifest.DefineStyle("Skywalker.Webshop.ShoppingCartWidget").SetUrl("shoppingcartwidget.css").SetDependencies("Webshop.Common");

            // Define the "shoppingcart" script and set a dependency on the "jQuery" resource
            manifest.DefineScript("Skywalker.Webshop.ShoppingCart").SetUrl("shoppingcart.js").SetDependencies("jQuery");

        }
    }
}