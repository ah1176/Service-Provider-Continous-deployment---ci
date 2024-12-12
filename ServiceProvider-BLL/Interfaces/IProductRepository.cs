using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.ProductDto;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Result<IEnumerable<ProductsOfVendorDto>>> GetProductsAsync(string VendorId , CancellationToken cancellationToken);
    }
}
