using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.InMemory;
using Plugins.DataStore.SQL;
using System.Net.Mime;
using System.Text;
using UseCases.CategoriesUseCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MarketContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
});

builder.Services.AddControllersWithViews();

if (builder.Environment.IsEnvironment("QA"))
{
	builder.Services.AddSingleton<ICategoryRepository, CategoriesInMemoryRepository>();
	builder.Services.AddSingleton<IProductRepository, ProductsInMemoryRepository>();
	builder.Services.AddSingleton<ITransactionRepository, TransactionsInMemoryRepository>();
}
else
{
	builder.Services.AddTransient<ICategoryRepository, CategoriesInMemoryRepository>();
	builder.Services.AddTransient<IProductRepository, ProductSQLRepository>();
	builder.Services.AddTransient<ITransactionRepository, TransactionSQLRepository>();
}


// Category
builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoriesUseCase>();
builder.Services.AddTransient<IViewSelectedCategoryUseCase, ViewSelectedCategoryUseCase>();
builder.Services.AddTransient<IAddCategoryUseCase, AddCategoryUseCase>();
builder.Services.AddTransient<IEditCategoryUseCase, EditCategoryUseCase>();
builder.Services.AddTransient<IDeleteCategoryUseCase, DeleteCategoryUseCase>();

// Product
builder.Services.AddTransient<IViewProductsUseCase, ViewProductsUseCase>();
builder.Services.AddTransient<IViewProductsInCategoryUseCase, ViewProductsInCategoryUseCase>();
builder.Services.AddTransient<IViewSelectedProductUseCase, ViewSelectedProductUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<ISellProductUseCase, SellProductUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();

// Transactions
builder.Services.AddTransient<IGetTodayTransactionsUseCase, GetTodayTransactionsUseCase>();
builder.Services.AddTransient<IRecordTransactionUseCase, RecordTransactionUseCase>();
builder.Services.AddTransient<ISearchTransactionsUseCase, SearchTransactionsUseCase>();



var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();


