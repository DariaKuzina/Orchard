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
            /*We named our shape "Parts_Product_Edit", and its template can be found in "Parts/Product".
              Because we are talking about editing a Content Part, Orchard will prefix that path with
              "~/Orchard.Webshop/Views/EditorTemplates", so the full path will be:
              "~/Orchard.Webshop/Views/EditorTemplates/Parts/Product.cshtml".*/

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

        /// <summary>
        /// Display ProductPart on the front end
        /// </summary>
        /// <param name="part"></param>
        /// <param name="displayType"></param>
        /// <param name="shapeHelper"></param>
        /// <returns></returns>
        protected override DriverResult Display(ProductPart part, string displayType, dynamic shapeHelper)
        {
            // To return more than 1 shape, use the Combined method to create a "CombinedShapeResult" object.
            return Combined(

             ContentShape("Parts_Product", () => shapeHelper.Parts_Product(
                Price: part.UnitPrice,
                Sku: part.Sku)),

             ContentShape("Parts_Product_AddButton", () => shapeHelper.Parts_Product_AddButton(ProductId: part.Id))
            );
        }
    }
}
