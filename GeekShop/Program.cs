using GeekShop.Services;
using GeekShop.Services.IService;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
              .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
              .AddOpenIdConnect("oidc", options =>
              {
                  options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
                  options.GetClaimsFromUserInfoEndpoint = true;
                  options.ClientId = "geek_shopping";
                  options.ClientSecret = "my_super_secret";
                  options.ResponseType = "code";
                  options.ClaimActions.MapJsonKey("role", "role", "role");
                  options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                  options.TokenValidationParameters.NameClaimType = "name";
                  options.TokenValidationParameters.RoleClaimType = "role";
                  options.Scope.Add("geek_shopping");
                  options.SaveTokens = true;
              }
          );

builder.Services.AddHttpClient<IProductService, ProductService>(c =>
c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]));

builder.Services.AddHttpClient<ICartService, CartService>(c =>
c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartAPI"]));

builder.Services.AddHttpClient<ICouponService, CouponService>(c =>
c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"]));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
