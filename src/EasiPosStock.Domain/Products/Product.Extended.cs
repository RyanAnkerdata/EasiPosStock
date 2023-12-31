using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace EasiPosStock.Products
{
    public class Product : ProductBase
    {
        //<suite-custom-code-autogenerated>
        protected Product()
        {

        }

        public Product(Guid id, string? productName = null, string? productCode = null)
            : base(id, productName, productCode)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}