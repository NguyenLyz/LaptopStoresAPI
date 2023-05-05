using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.AutoMapper;
using LaptopStore.Service.Repositories;
using LaptopStore.Service.Repositories.Interfaces;
using LaptopStore.Service.Services;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork;
using LaptopStore.Service.UnitOfWork.Interfaces;
using LatopStore.MoMo.Services;
using LatopStore.MoMo.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var Configuration = provider.GetRequiredService<IConfiguration>();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IF0GenericRepository<>), typeof(F0GenericRepository<>));
builder.Services.AddScoped(typeof(IIntF1GenericRepository<>), typeof(IntF1GenericRepository<>));
builder.Services.AddScoped(typeof(IStringF1GenericRepository<>), typeof(StringF1GenericRepository<>));
builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<INotiFyRepository, NotifyRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISeriesRepository, SeriesRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserBehaviorTrackerRepository, UserBehaviorTrackerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJWTTokenRepository, JWTTokenRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<INotifyService, NotifyService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISeriesService, SeriesService>();
builder.Services.AddScoped<IUserBehaviorTrackerService, UserBehaviorTrackerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMoMoSerivce, MoMoService>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});
builder.Services.AddCors(o => o.AddPolicy("LaptopStoreAPI", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddAuthorization();
var connectionString = Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LaptopStoreDbContext>(option => option.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseCors("LaptopStoreAPI");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
