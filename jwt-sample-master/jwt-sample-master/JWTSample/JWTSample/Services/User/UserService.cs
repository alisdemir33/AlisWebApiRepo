using JWTSample.Helpers;
using JWTSample.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JWTSample.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using JWTSample.ViewModels;

namespace JWTSample.Services.User
{
    //DI için oluşturduğumuz arayüzü implemente ediyoruz
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly JwtTestDBContext _dbContext;
        public UserService(IOptions<AppSettings> appSettings, JwtTestDBContext dbContext)
        {
            _dbContext = dbContext;
            _appSettings = appSettings.Value;
        }

        //Ekstra bir DTO veya model oluşturmamak için şimdilik değerlerimi geriye tuple olarak dönüyorum.
        public (string username, string token)? Authenticate(string username, string password)
        {
            //Kullanıcının gerçekten olup olmadığı kontrol ediyorum yoksa direk null dönüyorum.
            var user = _dbContext.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);

            if (user == null)
                return null;

            // Token oluşturmak için önce JwtSecurityTokenHandler sınıfından instance alıyorum.
            var tokenHandler = new JwtSecurityTokenHandler();
            //İmza için gerekli gizli anahtarımı alıyorum.
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Özel olarak şu Claimler olsun dersek buraya ekleyebiliriz.
                Subject = new ClaimsIdentity(new[]
                {
                    //İstersek string bir property istersek ClaimsTypes sınıfının sabitlerinden çağırabiliriz.
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim("Name",user.Name),
                    new Claim("Surname",user.Surname)
                }),
                //Tokenın hangi tarihe kadar geçerli olacağını ayarlıyoruz.
                Expires = DateTime.UtcNow.AddMinutes(15),
                //Son olarak imza için gerekli algoritma ve gizli anahtar bilgisini belirliyoruz.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //Token oluşturuyoruz.
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //Oluşturduğumuz tokenı string olarak bir değişkene atıyoruz.
            string generatedToken = tokenHandler.WriteToken(token);

            //Sonuçlarımızı tuple olarak dönüyoruz.
            return (user.UserName, generatedToken);
        }

        public AçıkKapıSosyalHizmetBaş1 getBasvuruByID(long id)
        {
            AçıkKapıSosyalHizmetBaş1 item = _dbContext.AçıkKapıSosyalHizmetBaş1.Where(item => item.Id == id.ToString()).FirstOrDefault();
            return item;
        }


        public Ingredients GetIngredients()
        {
            return new Ingredients() { Bacon = 0, Meat = 0, Cheese = 0, Salad = 0 };
        }

        public List<Users> getUserList()
        {
            List<Users> items = _dbContext.Users.ToList();// AçıkKapıSosyalHizmetBaş1.Where(item => item.Id == id.ToString()).FirstOrDefault();
            return items;
        }

        public List<Order> GetOrders()
        {

            List<Order> orders = new List<Order>()
            {
                new Order()
                {
                    Price = 11,
                    OrderData = new OrderData()
                    {
                        Street = "bultr",
                        DeliveryMethod = "fastest",
                        Email = "a@a.com",
                        Zipcode = "123456",
                        Name = "ali",
                        Country = "TR"
                    },
                    Ingredients = new Ingredients()
                    {
                        Bacon = 1,
                        Meat = 2,
                        Salad = 1,
                        Cheese = 1
                    }

                }

            };

            return orders;
        }
    }
}

