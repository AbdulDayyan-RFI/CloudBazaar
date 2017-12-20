using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Service;

namespace TEB.Api.Controllers
{
    [AllowAnonymous]
    public class ProductController : BaseApiController
    {
        public readonly IProductService _productService;
        public readonly IProduct_SpecificationAttribute_MappingService _product_SpecificationAttribute_MappingService;

        public ProductController(IProductService productService, IProduct_SpecificationAttribute_MappingService product_SpecificationAttribute_MappingService)
        {
            _productService = productService;
            _product_SpecificationAttribute_MappingService = product_SpecificationAttribute_MappingService;
        }

        [HttpPost]
        public IHttpActionResult InsertProduct(Product model)
        {
            return RunInSafe(() =>
            {
                var data = _productService.InsertProduct(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        public IHttpActionResult GetProductById(int Id)
        {
            return RunInSafe(() =>
            {
                var data = _productService.GetProductById(Id);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult UpdateProduct(Product model)
        {
            return RunInSafe(() =>
            {
                var data = _productService.UpdateProduct(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int Id)
        {
            return RunInSafe(() =>
            {
                var data = _productService.DeleteProduct(Id);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult SearchProducts(SearchProductModel model)
        {
            return RunInSafe(() =>
            {
                var data = _productService.SearchProducts(model);
                tebResponse.Data = data.Result;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        
        public IHttpActionResult GetProductSpecificationAttributes(int productId)
        {
            return RunInSafe(() =>
            {
                var data = _product_SpecificationAttribute_MappingService.GetProductSpecificationAttributes(productId);
                tebResponse.Data = data.Result;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        public IHttpActionResult SearchProductByName(string ProductName)
        {
            return RunInSafe(() =>
            {
                var data = _productService.GetProductByName(ProductName);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }
    }
}