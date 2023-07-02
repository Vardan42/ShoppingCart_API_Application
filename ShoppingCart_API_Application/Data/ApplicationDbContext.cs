using Microsoft.EntityFrameworkCore;
using ShoppingCart_API_Application.Models;
namespace ShoppingCart_API_Application.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }
        public DbSet<Product> Products { get; set; }  
        public DbSet<ProductNumber> ProductNumbers { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }    
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderDetails > OrderDetails { get; set; }  
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<CartItem>  CartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(

                new Product
                {
                    Id=1,
                    Name = "Samsung A 40",
                    Details= "Samsung Group,[3] or simply Samsung (Korean: 삼성; RR: samseong [samsʌŋ]) (stylized as SΛMSUNG), is a South Korean multinational manufacturing conglomerate headquartered in Samsung Town, Seoul, South Korea",
                    CreatedDate = DateTime.Now,
                    ImageUrl= "https://fdn2.gsmarena.com/vv/pics/samsung/samsung-galaxy-a23-1.jpg",
                    Occupancy=5,
                    Rate=200,
                    Sqft=500,
                    UpdatedDate =DateTime.Now,
                    Amenity=""
                }
                );
        }
    }
}
