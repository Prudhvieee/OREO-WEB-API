using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppRepositoryLayer.IRepository
{
    public interface IProductRL
    {
        List<Product> GetAllProducts();
        bool AddProduct(Product product);
    }
}
