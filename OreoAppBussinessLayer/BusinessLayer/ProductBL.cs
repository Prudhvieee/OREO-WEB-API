using OreoAppBussinessLayer.IBussinessLayer;
using OreoAppCommonLayer.Model;
using OreoAppRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBussinessLayer.BusinessLayer
{
    public class ProductBL:IProductBL
    {
        private readonly IProductRL productRL;

        public ProductBL(IProductRL productRL)
        {
            this.productRL = productRL;
        }
        public List<Product> GetAllProducts()
        {
            try
            {
                return this.productRL.GetAllProducts();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool AddProduct(Product product)
        {
            try
            {
                return this.productRL.AddProduct(product);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
