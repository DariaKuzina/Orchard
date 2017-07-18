using Orchard.Projections.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Projections.Descriptors.Filter;
using Orchard.Localization;
using Skywalker.Webshop.Models;

namespace Skywalker.Webshop.Filters
{
    public class ProductPartFilter : IFilterProvider
    {
        public Localizer T { get; set; }

        public ProductPartFilter()
        {
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeFilterContext describe)
        {
            describe.For(
                "Content",   // The category of this filter
                T("Content"),    // The name of the filter (not used in 1.4)
                T("Content"))     // The description of the filter (not used in 1.4)

                 // Defines the actual filter (we could define multiple filters using the fluent syntax)
                 .Element("ProductParts", // Type of the element
                 T("ProductParts"), // Name of the element
                 T("ProductParts"), // Description of the element
                 ApplyFilter, // Delegate to a method that performs the actual filtering for this element
                 DisplayFilter // Delegate to a method that returns a descriptive string for this element
                 );
        }

        //Filter that will select all content items who have a ProductPart attached
        private void ApplyFilter(FilterContext context)
        {
            // Set the Query property of the context parameter to any IHqlQuery. In our case, we use a default query
            // and narrow it down by joining with the ProductPartRecord.
            context.Query = context.Query.Join(x => x.ContentPartRecord(typeof(ProductPartRecord)));
        }

        private LocalizedString DisplayFilter(FilterContext context)
        {
            return T("Content with ProductPart");
        }
    }
}