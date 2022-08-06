using Adesso.Application.Utilities.Security;
using Adesso.Domain.Enums;
using Adesso.Domain.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Adesso.Infrastructure.Persistence.Contexts;

public class SeedData
{
    public async Task SeedAsync(IConfiguration configuration)
    {
        var dbContextBuilder = new DbContextOptionsBuilder();
        dbContextBuilder.UseSqlServer(configuration["AdessoDbConnectionString"]);
        var context = new AdessoDbContext(dbContextBuilder.Options);

        var randomGenerator = new Random();

        //users
        var users = new List<User>();
        for (int i = 0; i < 100; i++)
        {
            var faker = new Faker("tr");
            var user = new User()
            {
                EmailAddress = faker.Internet.Email(),
                Password = PasswordEncryptor.Encrypt(faker.Internet.Password()),
                CreatedDate = faker.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now),
            };

            users.Add(user);
        }


        var userIds = users.Select(i => i.Id).ToList();

        //userDetails
        var userDetails = new List<UserDetail>();
        for (int i = 0; i < 100; i++)
        {
            var faker = new Faker("tr");
            var userDetail = new UserDetail()
            {
                FirstName = faker.Person.FirstName,
                LastName = faker.Person.LastName,
                Gender = faker.Person.Gender.ToString() == "Male" ? Genders.Male : Genders.Female,
                UserId = i + 1,
                CreatedDate = faker.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now),
            };

            userDetails.Add(userDetail);
        }




        //categories
        var categories = new List<Category>();
        var categoryList = new List<string>() { "Bilgisayar", "Telefon", "Televizyon", "Kamera" };
        for (int i = 0; i < 4; i++)
        {
            var faker = new Faker("tr");

            var category = new Category()
            {
                Name = categoryList[i],
                CreatedDate = faker.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now),
            };

            categories.Add(category);
        }

        var categoryIds = new List<int> { 1, 2, 3, 4 };

        //products
        var products = new List<Product>();

        for (int i = 0; i < 200; i++)
        {
            var faker = new Faker("tr");
            var product = new Product()
            {
                CreatedDate = faker.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now),
                CategoryId = randomGenerator.Next(1, 5),
                ImagePath = "default.png",
                Name = faker.Commerce.ProductName(),
                Price = randomGenerator.Next(15, 15000),
            };
            products.Add(product);
        }





        //await context.Categories.AddRangeAsync(categories);
        //await context.Users.AddRangeAsync(users);

        //await context.Products.AddRangeAsync(products);
        //await context.UserDetails.AddRangeAsync(userDetails);


        await context.SaveChangesAsync();
    }

}
