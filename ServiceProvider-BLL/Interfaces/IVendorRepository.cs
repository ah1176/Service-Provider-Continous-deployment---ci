using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.ProductDto;
using ServiceProvider_BLL.Dtos.VendorDto;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Interfaces
{
    public interface IVendorRepository : IBaseRepository<Vendor>
    {
        Task<Result<IEnumerable<VendorResponse>>> GetAllProviders(CancellationToken cancellationToken = default);
        Task<Result<VendorResponse>> GetProviderDetails(string providerId,CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<ProductsOfVendorDto>>> GetProviderMenuAsync(string providerId , CancellationToken cancellationToken);
        Task<Result<UpdateVendorResponse>> UpdateVendorAsync(string id,UpdateVendorResponse vendorDto, CancellationToken cancellationToken = default);
        Task<Result<ChangeVendorPasswordResponse>> ChangeVendorPasswordAsync(string id, ChangeVendorPasswordResponse vendorDto, CancellationToken cancellationToken = default);
    }
}
