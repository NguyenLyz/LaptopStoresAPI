using AutoMapper;
using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.Repositories.Interfaces;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories
{
    public class ProductRepository : IntF1GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public Product GetBySlug(string slug)
        {
            var result = _context.Products.Where(x => x.Slug == slug).FirstOrDefault();
            return result;
        }
        public async Task SuccessfulProcessing(string orderId)
        {
            var query = from orderDetail in _context.OrderDetails
                        where orderDetail.OrderId == orderId
                        select orderDetail;
            var query2 = from product in _context.Products
                         select product;
            var result = query.ToList();
            var result2 = query2.ToList();
            for (int i = 0; i < query.Count(); i++)
            {
                for (int j = 0; j < query2.Count(); j++)
                {
                    if (result[i].ProductId == result2[j].Id)
                    {
                        result2[j].Sold = result2[j].Sold + result[i].Quantity;
                        result2[j].Available = result2[j].Available - result[i].Quantity;
                    }
                }
            }
            _context.Products.UpdateRange(result2);
            await _context.SaveChangesAsync();
        }
        public async Task CancelProcessing(string orderId)
        {
            var query = from orderDetail in _context.OrderDetails
                        where orderDetail.OrderId == orderId
                        select orderDetail;
            var query2 = from product in _context.Products
                         select product;
            var result = query.ToList();
            var result2 = query2.ToList();
            for (int i = 0; i < query.Count(); i++)
            {
                for (int j = 0; j < query2.Count(); j++)
                {
                    if (result[i].ProductId == result2[j].Id)
                    {
                        result2[j].Sold = result2[j].Sold - result[i].Quantity;
                        result2[j].Available = result2[j].Available + result[i].Quantity;
                    }
                }
            }
            _context.Products.UpdateRange(result2);
            await _context.SaveChangesAsync();
        }
        public async Task<FilterRequestModel> Filter(FilterRequestModel request)
        {
            var query = from product in _context.Products
                        join brand in _context.Brands on product.BrandId equals brand.Id
                        join category in _context.Categories on product.CategoryId equals category.Id
                        join series in _context.Series on product.SeriesId equals series.Id
                        select new {product, brand, category, series};

            if(!string.IsNullOrEmpty(request.Query) || request.Query.Count() != 0)
            {
                query = query.Where(x => x.product.Name.Contains(request.Query) || x.product.Description.Contains(request.Query) || x.brand.Name.Contains(request.Query) || x.category.Name.Contains(request.Query) || x.series.Name.Contains(request.Query));
            }
            if(request.BrandId != 0)
            {
                query = query.Where(x => x.product.BrandId == request.BrandId);
            }
            if(request.CategoryId != 0)
            {
                query = query.Where(x => x.product.CategoryId == request.CategoryId);
            }
            if(request.SeriesId != 0)
            {
                query = query.Where(x => x.product.SeriesId == request.SeriesId);
            }
            if(request.Sort >= 1 && request.Sort <= 3)
            {
                switch (request.Sort)
                {
                    case 1:
                        {
                            query = query.OrderByDescending(x => x.product.Sold);
                            break;
                        };
                    case 2:
                        {
                            query = query.OrderBy(x => (x.product.Price - ((x.product.Discount / 100) * x.product.Price)));
                            break;
                        };
                    case 3:
                        {
                            query = query.OrderByDescending(x => (x.product.Price - ((x.product.Discount / 100) * x.product.Price)));
                            break;
                        };
                };
            }
            else
            {
                query = query.OrderByDescending(x => x.product.Id);
            }
            if(request.MaxPrice != 0)
            {
                query = query.Where(x => (x.product.Price - ((x.product.Discount / 100) * x.product.Price)) <= request.MaxPrice);
            }
            if (request.MinPrice != 0)
            {
                query = query.Where(x => (x.product.Price - ((x.product.Discount / 100) * x.product.Price)) >= request.MinPrice);
            }
            int totalRow = (await query.CountAsync()) / request.PageSize;
            if((await query.CountAsync() % request.PageSize) > 0)
            {
                totalRow++;
            }

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(d => d.product).ToListAsync();

            var result = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(r => new ProductResponseModel
                {
                    Id = r.product.Id,
                    Name = r.product.Name,
                    Slug = r.product.Slug,
                    Brand = r.brand.Name,
                    BrandId = r.product.BrandId,
                    Category = r.category.Name,
                    CategoryId = r.product.CategoryId,
                    Series = r.series.Name,
                    SeriesId = r.product.SeriesId,
                    Price = r.product.Price,
                    Discount = r.product.Discount,
                    Sold = r.product.Sold,
                    Available = r.product.Available
                }).ToListAsync();

            for(var i = 0; i < data.Count(); i++)
            {
                result[i].Images = data[i].Images.Split("$").ToList();
            }
            request.TotalRow = totalRow;
            request.Products = result;
            return request;
        }
        public async Task<List<ProductResponseModel>> GetBestSeller()
        {
            var query = _context.Products.OrderByDescending(x => x.Sold);
            var data = await query.Take(10).ToListAsync();
            var result = await query.Take(10).Select(r => new ProductResponseModel
            {
                Id = r.Id,
                Name = r.Name,
                Slug = r.Slug,
                Price = r.Price,
                Discount = r.Discount,
                Sold = r.Sold,
                Available = r.Available
            }).ToListAsync();
            for(int i = 0; i < data.Count(); i++)
            {
                result[i].Images = data[i].Images.Split("$").ToList();
            }
            return result;
        }
        public async Task<List<ProductResponseModel>> GetNewestProduct()
        {
            var query = _context.Products.OrderByDescending(x => x.Id);
            var data = await query.Take(10).ToListAsync();
            var result = await query.Take(10).Select(r => new ProductResponseModel
            {
                Id = r.Id,
                Name = r.Name,
                Slug = r.Slug,
                Price = r.Price,
                Discount = r.Discount,
                Sold = r.Sold,
                Available = r.Available
            }).ToListAsync();
            for (int i = 0; i < data.Count(); i++)
            {
                result[i].Images = data[i].Images.Split("$").ToList();
            }
            return result;
        }
    }
}
