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



        public async Task<Result<IEnumerable<CategoryResponse>>> GetCategoriesAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _context.Categories!
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

            if (!categories.Any())
                return Result.Failure<IEnumerable<CategoryResponse>>(CategoryErrors.NotFound);

            var categoriesResponses = categories.Adapt<IEnumerable<CategoryResponse>>();

            return Result.Success(categoriesResponses);
        }

        public async Task<Result<IEnumerable<VendorResponse>>> GetProvidersByCategoryAsync(int categoryId , CancellationToken cancellationToken = default)
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

        public async Task<Result<IEnumerable<SubCategoryResponse>>> GetSubCategoryByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories!.FindAsync(categoryId, cancellationToken);

            if (category == null)
                return Result.Failure<IEnumerable<SubCategoryResponse>>(CategoryErrors.CategoryNotFound);

            var subCategories = await _context.SubCategories!
                                        .Where(sc => sc.CategoryId == categoryId)
                                        .AsNoTracking()
                                        .ToListAsync(cancellationToken);
            return subCategories is null
                   ?Result.Failure<IEnumerable<SubCategoryResponse>>(CategoryErrors.SubCategoryNotFound)
                   :Result.Success(subCategories.Adapt<IEnumerable<SubCategoryResponse>>());
        }
        public async Task<Result<CategoryResponse>> AddCategoryAsync(CategoryRequest request, CancellationToken cancellationToken = default)
        {
            var categoryIsExisit = await _context.Categories!
                .AnyAsync(x => x.NameEn == request.NameEn || x.NameAr == request.NameAr,cancellationToken);

            if (categoryIsExisit)
                return Result.Failure<CategoryResponse>(CategoryErrors.DuplicateCategory);

            var category = request.Adapt<Category>();

            await _context.AddAsync(category, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(category.Adapt<CategoryResponse>());
        }

        public async Task<Result<SubCategoryResponse>> AddSubCategoryAsync(int categoryId, SubCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var categoryIsExisit = await _context.Categories!.AnyAsync(x => x.Id == categoryId,cancellationToken);

            if (!categoryIsExisit)
                return Result.Failure<SubCategoryResponse>(CategoryErrors.CategoryNotFound);

            var subCategoryIsExisit = await _context.SubCategories!
                .AnyAsync(x => x.NameEn == request.NameEn || x.NameAr == request.NameAr, cancellationToken);

            if (subCategoryIsExisit)
                return Result.Failure<SubCategoryResponse>(CategoryErrors.DuplicateSubCategory);

            var subCategory = request.Adapt<SubCategory>();

            subCategory.CategoryId = categoryId;

            await _context.AddAsync(subCategory, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(subCategory.Adapt<SubCategoryResponse>());
        }

        public async Task<Result> DeleteSubCategoryAsync(int subCategoryId , CancellationToken cancellationToken = default) 
        {
            var subCategory = await _context.SubCategories!.FirstOrDefaultAsync(x => x.Id == subCategoryId);


            if (subCategory == null)
                return Result.Failure(CategoryErrors.SubCategoryNotFound);

            _context.SubCategories!.Remove(subCategory);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
