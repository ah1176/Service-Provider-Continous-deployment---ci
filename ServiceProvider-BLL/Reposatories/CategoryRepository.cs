using Mapster;
using Microsoft.EntityFrameworkCore;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.CategoryDto;
using ServiceProvider_BLL.Dtos.VendorDto;
using ServiceProvider_BLL.Errors;
using ServiceProvider_BLL.Interfaces;
using ServiceProvider_DAL.Data;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Reposatories
{
    public class CategoryRepository : BaseRepository<Category> , ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<CategoryResponse>>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await _context.Categories!
                            .ToListAsync(cancellationToken);

            var categoriesResponses = categories.Adapt<IEnumerable<CategoryResponse>>();

            if (categoriesResponses == null)
                return Result.Failure<IEnumerable<CategoryResponse>>(CategoryErrors.NotFound);

            return Result.Success(categoriesResponses);
        }

        public async Task<Result<IEnumerable<VendorResponse>>> GetProvidersByCategoryAsync(int categoryId , CancellationToken cancellationToken)
        {
            var subcategoryIds = await _context.SubCategories!
                .Where(sc => sc.CategoryId == categoryId)
                .Select(sc => sc.Id)
                .ToListAsync(cancellationToken);

            var vendors = await _context.VendorSubCategories!
                .Include(vsc => vsc.Vendor)
                .Where(vsc => subcategoryIds.Contains(vsc.SubCategoryId))
                .Select(vsc => vsc.Vendor.Adapt<VendorResponse>())
                .ToListAsync(cancellationToken);

            return vendors.Any()
                ? Result.Success<IEnumerable<VendorResponse>>(vendors)
                : Result.Failure<IEnumerable<VendorResponse>>(VendorErrors.NotFound);
        }
    }
}
