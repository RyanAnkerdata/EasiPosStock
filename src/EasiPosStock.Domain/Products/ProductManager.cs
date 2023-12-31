using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EasiPosStock.Products
{
    public abstract class ProductManagerBase : DomainService
    {
        protected IProductRepository _productRepository;

        public ProductManagerBase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public virtual async Task<Product> CreateAsync(
        string? productName = null, string? productCode = null)
        {

            var product = new Product(
             GuidGenerator.Create(),
             productName, productCode
             );

            return await _productRepository.InsertAsync(product);
        }

        public virtual async Task<Product> UpdateAsync(
            Guid id,
            string? productName = null, string? productCode = null
        )
        {

            var product = await _productRepository.GetAsync(id);

            product.ProductName = productName;
            product.ProductCode = productCode;

            return await _productRepository.UpdateAsync(product);
        }

    }
}