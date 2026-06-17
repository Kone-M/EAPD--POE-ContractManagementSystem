using Microsoft.EntityFrameworkCore;
using ContractManagementSystem.Data;
using ContractManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services
builder.Services.AddScoped<IContractService, ContractService>();

// Configure session and temp data
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contracts}/{action=Index}/{id?}");

// Ensure upload directories exist
using (var scope = app.Services.CreateScope())
{
    var webHostEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
    var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads", "contracts");
    if (!Directory.Exists(uploadsFolder))
    {
        Directory.CreateDirectory(uploadsFolder);
        Console.WriteLine("Created uploads directory at: " + uploadsFolder);
    }
}

// Initialize Database with Sample Data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        dbContext.Database.EnsureCreated();
        Console.WriteLine("Database created/updated successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database error: {ex.Message}");
    }
}

app.Run();