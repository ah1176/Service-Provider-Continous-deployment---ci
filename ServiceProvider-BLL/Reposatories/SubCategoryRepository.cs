﻿using Mapster;
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
    public class SubCategoryRepository : BaseRepository<SubCategory> , ISubCategoryRepository
    {
        private readonly AppDbContext _context;

        public SubCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Result<IEnumerable<SubCategoryResponse>>> GetSubCategoriesAsync(CancellationToken cancellationToken = default)
        {
            var subcategories = await _context.SubCategories!
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

            if (!subcategories.Any())
                return Result.Failure<IEnumerable<SubCategoryResponse>>(SubCategoryErrors.SubCategoryNotFound);

            var subcategoriesResponses = subcategories.Adapt<IEnumerable<SubCategoryResponse>>();

            return Result.Success(subcategoriesResponses);
        }
    }
}
