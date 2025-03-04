using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.ProductDto;
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
    public class ProductRepository : BaseRepository<Product> , IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<Result<IEnumerable<ProductResponse>>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            var products = await _context.Products!.
                                AsNoTracking()
                                .ToListAsync(cancellationToken: cancellationToken);


            return products is null ?
                Result.Failure<IEnumerable<ProductResponse>>(ProductErrors.ProductsNotFound)
                : Result.Success(products.Adapt<IEnumerable<ProductResponse>>());

        }

        public async Task<Result<ProductResponse>> GetProductAsync(int id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products!.FindAsync(id, cancellationToken);

            return product is null
                ? Result.Failure<ProductResponse>(ProductErrors.ProductNotFound)
                :Result.Success(product.Adapt<ProductResponse>());
        }

        public async Task<Result<ProductResponse>> AddProductAsync(string vendorId, ProductRequest request, CancellationToken cancellationToken = default)
        {
            var subCategoryExists = await _context.SubCategories!.AnyAsync(sc => sc.Id == request.SubCategoryId, cancellationToken: cancellationToken);

            if(!subCategoryExists)
                return Result.Failure<ProductResponse>(SubCategoryErrors.SubCategoryNotFound);

            var product = request.Adapt<Product>();

            product.VendorId = vendorId;
            product.CreatedAt = DateTime.UtcNow;

            await _context.Products!.AddAsync(product, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Adapt<ProductResponse>());
            
        }

        public async Task<Result> UpdateProductAsync(int id, UpdateProductRequest request, string vendorId, CancellationToken cancellationToken = default)
        {
            var currentProduct = await _context.Products!.FirstOrDefaultAsync(p => p.Id == id && p.VendorId == vendorId, cancellationToken: cancellationToken); 

            if (currentProduct is null) 
                return Result.Failure(ProductErrors.ProductNotFound);

            // request.Adapt(currentProduct);
            currentProduct.NameEn = request.NameEn;
            currentProduct.NameAr = request.NameAr;
            currentProduct.Description = request.Description;
            currentProduct.Price = request.Price;
            currentProduct.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeleteProductAsync(int id, string vendorId, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products!.FirstOrDefaultAsync(p => p.Id == id && p.VendorId == vendorId, cancellationToken: cancellationToken);

            if (product is null)
                return Result.Failure(ProductErrors.ProductNotFound);

            _context.Remove(product);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
