using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement;
using Skywalker.Webshop.Models;

namespace Skywalker.Webshop.Drivers
{
    public class ProductPartDriver : ContentPartDriver<ProductPart>
    {
        protected override string Prefix
        {
            get { return "Product"; }
        }

        /// <summary>
        /// Displaying the part in edit mode on the back end of the site
        /// </summary>
        /// <param name="part"></param>
        /// <param name="shapeHelper"></param>
        /// <returns></returns>
        protected override DriverResult Editor(ProductPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Product_Edit", () => shapeHelper
            .EditorTemplate(TemplateName: "Parts/Product", Model: part, Prefix: Prefix));
        }

        /// <summary>
        /// Handle the postback when the user saves a content item on which the content part is attached
        /// </summary>
        /// <param name="part"></param>
        /// <param name="updater"></param>
        /// <param name="shapeHelper"></param>
        /// <returns></returns>
        protected override DriverResult Editor(ProductPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}
