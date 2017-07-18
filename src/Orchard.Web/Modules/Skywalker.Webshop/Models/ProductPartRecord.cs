using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skywalker.Webshop.Models
{
    /// <summary>
    /// Data entity to store information for a content part
    /// </summary>
    public class ProductPartRecord  : ContentPartRecord
    {
        //Must be virtual for NHibernate
        public virtual decimal UnitPrice { get; set; }
        public virtual string Sku { get; set; }
    }
}
