using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Skywalker.Webshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skywalker.Webshop.Handlers
{
    public class ProductPartHandler : ContentHandler
    {
        public ProductPartHandler(IRepository<ProductPartRecord> repository)
        {
            //Enables Orchard to save and load our ProductPart information when it needs to
            Filters.Add(StorageFilter.For(repository));
        }
    }
}