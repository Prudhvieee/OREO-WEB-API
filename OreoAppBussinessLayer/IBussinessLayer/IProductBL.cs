using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBussinessLayer.IBussinessLayer
{
    public interface IProductBL
    {
        List<Product> GetAllProducts();
        bool AddProduct(Product product);
    }
}
 