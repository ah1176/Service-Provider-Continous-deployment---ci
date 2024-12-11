using ServiceProvider_BLL.Interfaces;
using ServiceProvider_DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Reposatories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IApplicationUserRepository ApplicationUsers { get; private set; }
        public IVendorRepository Vendors { get; private set; }
        public IVendorSubCategoryRepository VendorSubCategories { get; private set; }
        public ISubCategoryRepository SubCategories { get; private set; }
        public IReviewRepository Reviews { get; private set; }
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderProductRepository OrderProducts { get; private set; }
        public ICartRepository Carts { get; private set; }
        public ICartProductRepository CartProducts { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IShippingRepository Shippings { get; private set; }
        public IPaymentRepository Payments { get; private set; }
        public IMessageRepository Messages { get; private set; }



        public UnitOfWork(AppDbContext context)
        { 
            _context = context;

            ApplicationUsers = new ApplicationUserRepository(_context);
            Vendors = new VendorRepository(_context);
            Products = new ProductRepository(_context);
            VendorSubCategories = new VendorSubCategoryRepository(_context);
            SubCategories = new SubCategoryRepository(_context);
            Reviews = new ReviewRepository(_context);
            Orders = new OrderRepository(_context);
            OrderProducts = new OrderProductRepository(_context);
            Carts = new CartRepository(_context);
            CartProducts = new CartProductRepository(_context);
            Categories = new CategoryRepository(_context);
            Shippings = new ShippingRepository(_context);
            Payments = new PaymentRepository(_context);
            Messages = new MessageRepository(_context);
        }

        public async Task<int> Complete() =>
            await _context.SaveChangesAsync();

        public void Dispose() =>
            _context.Dispose();
    }
}
