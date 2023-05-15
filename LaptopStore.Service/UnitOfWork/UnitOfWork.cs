using AutoMapper;
using LaptopStore.Data.Context;
using LaptopStore.Service.Repositories;
using LaptopStore.Service.Repositories.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private readonly LaptopStoreDbContext _context;
        private readonly IMapper _mapper;
        private IAdvertisementRepository advertisementRepository;
        private IBrandRepository brandRepository;
        private ICartRepository cartRepository;
        private ICategoryRepository categoryRepository;
        private ICommentRepository commentRepository;
        private INotiFyRepository notifyRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IOrderRepository orderRepository;
        private IProductRepository productRepository;
        private IRoleRepository roleRepository;
        private ISeriesRepository seriesRepository;
        private ITransactionRepository transactionRepository;
        private IUserBehaviorTrackerRepository userBehaviorTrackerRepository;
        private IUserRepository userRepository;
        private IJWTTokenRepository jwttokenRepository;
        private IOTPRepository oTPRepository;

        public UnitOfWork(LaptopStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IAdvertisementRepository AdvertisementRepository
        {
            get
            {
                if (this.advertisementRepository == null)
                {
                    this.advertisementRepository = new AdvertisementRepository(_context);
                }
                return this.advertisementRepository;
            }
        }
        public IBrandRepository BrandRepository
        {
            get
            {
                if (this.brandRepository == null)
                {
                    this.brandRepository = new BrandRepository(_context);
                }
                return this.brandRepository;
            }
        }
        public ICartRepository CartRepository
        {
            get
            {
                if (this.cartRepository == null)
                {
                    this.cartRepository = new CartRepository(_context, _configuration);
                }
                return this.cartRepository;
            }
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new CategoryRepository(_context);
                }
                return this.categoryRepository;
            }
        }
        public ICommentRepository CommentRepository
        {
            get
            {
                if (this.commentRepository == null)
                {
                    this.commentRepository = new CommentRepository(_context);
                }
                return this.commentRepository;
            }
        }
        public INotiFyRepository NotifyRepository
        {
            get
            {
                if (this.notifyRepository == null)
                {
                    this.notifyRepository = new NotifyRepository(_context);
                }
                return this.notifyRepository;
            }
        }
        public IOrderDetailRepository OrderDetailRepository
        {
            get
            {
                if (this.orderDetailRepository == null)
                {
                    this.orderDetailRepository = new OrderDetailRepository(_context);
                }
                return this.orderDetailRepository;
            }
        }
        public IOrderRepository OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new OrderRepository(_context);
                }
                return this.orderRepository;
            }
        }
        public IProductRepository ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new ProductRepository(_context);
                }
                return this.productRepository;
            }
        }
        public IRoleRepository RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new RoleRepository(_context);
                }
                return this.roleRepository;
            }
        }
        public ISeriesRepository SeriesRepository
        {
            get
            {
                if (this.seriesRepository == null)
                {
                    this.seriesRepository = new SeriesRepository(_context);
                }
                return this.seriesRepository;
            }
        }
        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (this.transactionRepository == null)
                {
                    this.transactionRepository = new TransactionRepository(_context);
                }
                return this.transactionRepository;
            }
        }
        public IUserBehaviorTrackerRepository UserBehaviorTrackerRepository
        {
            get
            {
                if (this.userBehaviorTrackerRepository == null)
                {
                    this.userBehaviorTrackerRepository = new UserBehaviorTrackerRepository(_context);
                }
                return this.userBehaviorTrackerRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(_context);
                }
                return this.userRepository;
            }
        }
        public IJWTTokenRepository JWTTokenRepository
        {
            get
            {
                if(this.jwttokenRepository == null)
                {
                    this.jwttokenRepository = new JWTTokenRepository(_context, _configuration);
                }
                return this.jwttokenRepository;
            }
        }
        public IOTPRepository OTPRepository
        {
            get
            {
                if (this.oTPRepository == null)
                {
                    this.oTPRepository = new OTPRepository(_context);
                }
                return this.OTPRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
