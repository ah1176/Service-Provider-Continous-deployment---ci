using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.CategoryDto;
using ServiceProvider_BLL.Dtos.VendorDto;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Result<IEnumerable<CategoryResponse>>> GetCategoriesAsync(CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<VendorResponse>>> GetProvidersByCategoryAsync(int categoryId , CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<SubCategoryResponse>>> GetSubCategoryByCategoryAsync(int categoryId , CancellationToken cancellationToken = default);
        Task<Result<CategoryResponse>> AddCategoryAsync(CategoryRequest request, CancellationToken cancellationToken = default);
        Task<Result<SubCategoryResponse>> AddSubCategoryAsync(int categoryId,SubCategoryRequest request, CancellationToken cancellationToken = default);
    }
}
