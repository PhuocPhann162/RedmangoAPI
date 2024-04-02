using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedMango_API.Models;

namespace RedMango_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    Id = 1,
                    Name = "Spring Roll",
                    Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    Image = "https://fucorestaurantimages.blob.core.windows.net/redmango/spring roll.jpg",
                    Price = 7.99,
                    Category = "Appetizer",
                    SpecialTag = ""
                }, new MenuItem
                {
                    Id = 2,
                    Name = "Idli",
                    Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    Image = "https://fucorestaurantimages.blob.core.windows.net/redmango/idli.jpg",
                    Price = 8.99,
                    Category = "Appetizer",
                    SpecialTag = ""
                }, new MenuItem
                {
                    Id = 3,
                    Name = "Panu Puri",
                    Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    Image = "https://fucorestaurantimages.blob.core.windows.net/redmango/pani puri.jpg",
                    Price = 8.99,
                    Category = "Appetizer",
                    SpecialTag = "Best Seller"
                }, new MenuItem
                {
                    Id = 4,
                    Name = "Hakka Noodles",
                    Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    Image = "https://fucorestaurantimages.blob.core.windows.net/redmango/hakka noodles.jpg",
                    Price = 10.99,
                    Category = "Entrée",
                    SpecialTag = ""
                }, new MenuItem
                {
                    Id = 5,
                    Name = "Malai Kofta",
                    Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    Image = "https://fucorestaurantimages.blob.core.windows.net/redmango/malai kofta.jpg",
                    Price = 12.99,
                    Category = "Entrée",
                    SpecialTag = "Top Rated"
                }, new MenuItem
                {
                    Id = 6,
                    Name = "Paneer Pizza",
                    Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    Image = "https://fucorestaurantimages.blob.core.windows.net/redmango/paneer pizza.jpg",
                    Price = 11.99,
                    Category = "Entrée",
                    SpecialTag = ""
                }, new MenuItem
                {
                    Id = 7,
                    Name = "Paneer Tikka",
                    Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    Image = "https://fucorestaurantimages.blob.core.windows.net/redmango/paneer tikka.jpg",
                    Price = 13.99,
                    Category = "Entrée",
                    SpecialTag = "Chef's Special"
                }, new MenuItem
                {
                    Id = 8,
                    Name = "Carrot Love",
                    Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    Image = "https://fucorestaurantimages.blob.core.windows.net/redmango/carrot love.jpg",
                    Price = 4.99,
                    Category = "Dessert",
                    SpecialTag = ""
                }, new MenuItem
                {
                    Id = 9,
                    Name = "Rasmalai",
                    Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    Image = "https://fucorestaurantimages.blob.core.windows.net/redmango/rasmalai.jpg",
                    Price = 4.99,
                    Category = "Dessert",
                    SpecialTag = "Chef's Special"
                }, new MenuItem
                {
                    Id = 10,
                    Name = "Sweet Rolls",
                    Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    Image = "https://fucorestaurantimages.blob.core.windows.net/redmango/sweet rolls.jpg",
                    Price = 3.99,
                    Category = "Dessert",
                    SpecialTag = "Top Rated"
                });
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                Code = "10OFF",
                DiscountAmount = 10,
                MinAmount = 20,
            }, new Coupon
            {
                Id = 2,
                Code = "20OFF",
                DiscountAmount = 20,
                MinAmount = 40,
            }, new Coupon
            {
                Id = 3,
                Code = "50OFF",
                DiscountAmount = 50,
                MinAmount = 300,
            });


            modelBuilder.Entity<Review>().HasData(new Review
            {
                Id = 1,
                Comment = "Really Delicious!! I have never tried it before",
                Stars = 5,
                MenuItemId = 2,
                UserId = "0d65520d-107f-440e-aa41-ed1f492c86ff",
                CreatedAt = DateTime.Now,
            }, new Review
            {
                Id = 2,
                Comment = "Yummy!! I love this food. It exceeded my expectations",
                Stars = 4,
                MenuItemId = 2,
                UserId = "12b39b7b-ae91-437a-b939-56fdb95685f4",
                CreatedAt = DateTime.Now,
            }, new Review
            {
                Id = 3,
                Comment = "Great Food!!I love this food. It exceeded my expectations",
                Stars = 4,
                MenuItemId = 2,
                UserId = "8d9dc5f6-ad81-4558-b5a8-84b3cf4bdca7",
                CreatedAt = DateTime.Now,
            }, new Review
            {
                Id = 4,
                Comment = "So Tasteful!! I will try again soon",
                Stars = 5,
                MenuItemId = 1,
                UserId = "12b39b7b-ae91-437a-b939-56fdb95685f4",
                CreatedAt = DateTime.Now,
            }, new Review
            {
                Id = 5,
                Comment = "Worst than Vietnamese Food. Just a simple food",
                Stars = 3,
                MenuItemId = 3,
                UserId = "20b88091-8f9e-4778-a794-4efc3e16b112",
                CreatedAt = DateTime.Now,
            });
        }
    }
}
