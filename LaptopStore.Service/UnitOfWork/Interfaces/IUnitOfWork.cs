using LaptopStore.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IAdvertisementRepository AdvertisementRepository { get; }
        IBrandRepository BrandRepository { get; }
        ICartRepository CartRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICommentRepository CommentRepository { get; }
        INotiFyRepository NotifyRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        IRoleRepository RoleRepository { get; }
        ISeriesRepository SeriesRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IUserBehaviorTrackerRepository UserBehaviorTrackerRepository { get; }
        IUserRepository UserRepository { get; }
        IJWTTokenRepository JWTTokenRepository { get; }
        IOTPRepository OTPRepository { get; }

        void Dispose();
        Task SaveAsync();
    }
}
