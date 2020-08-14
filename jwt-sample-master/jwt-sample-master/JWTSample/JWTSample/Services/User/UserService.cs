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
using JWTSample.ContosoModels;
using JWTSample.Auth;
using VakifIlan;
using System.Configuration;
//using Users = JWTSample.ContosoModels.Users;

namespace JWTSample.Services.User
{
    //DI için oluşturduğumuz arayüzü implemente ediyoruz
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly JwtTestDBContext _dbContext;
        // private readonly CONTOSOContext _dbContext;
        public UserService(IOptions<AppSettings> appSettings, JwtTestDBContext dbContext)
        //   public UserService(IOptions<AppSettings> appSettings, CONTOSOContext dbContext)
        {
            _dbContext = dbContext;
            _appSettings = appSettings.Value;
        }

        //Ekstra bir DTO veya model oluşturmamak için şimdilik değerlerimi geriye tuple olarak dönüyorum.
        // public (string username, string token)? Authenticate(string username, string password)
        public TokenUser Authenticate(string username, string password)
        {
            //Kullanıcının gerçekten olup olmadığı kontrol ediyorum yoksa direk null dönüyorum.
            var dbUser = _dbContext.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);

            if (dbUser == null)
                return null;

            Auth.User user = new Auth.User() { Email = dbUser.UserName, Id = dbUser.Id, Name = dbUser.Name, Password = dbUser.Password, Surname = dbUser.Surname };

            MyTokenHandler tokenHandler = new MyTokenHandler(_appSettings);
            Token token = tokenHandler.CreateAccessToken();

            //_appSettings.

            //Refresh token Users tablosuna işleniyor.
            dbUser.RefreshToken = token.RefreshToken;
            dbUser.RefreshTokenEndDate = token.Expiration.AddMinutes(5);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenEndDate = dbUser.RefreshTokenEndDate;

            _dbContext.SaveChanges();

            return new TokenUser() { Token = token, User = user };

            #region original
            /*   
             *   İlk Versiyon(REfresh TOKEN olmayan)

            // Token oluşturmak için önce JwtSecurityTokenHandler sınıfından instance alıyorum.
            var _tokenHandler = new JwtSecurityTokenHandler();
            //İmza için gerekli gizli anahtarımı alıyorum.
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Özel olarak şu Claimler olsun dersek buraya ekleyebiliriz.
                Subject = new ClaimsIdentity(new[]
                {
                    //İstersek string bir property istersek ClaimsTypes sınıfının sabitlerinden çağırabiliriz.
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim("Name",user.Name),
                    new Claim("Surname",user.Surname)
                }),
                //Tokenın hangi tarihe kadar geçerli olacağını ayarlıyoruz.
                Expires = DateTime.UtcNow.AddMinutes(15),
                //Son olarak imza için gerekli algoritma ve gizli anahtar bilgisini belirliyoruz.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //Token oluşturuyoruz.
            var _token = _tokenHandler.CreateToken(tokenDescriptor);
            //Oluşturduğumuz tokenı string olarak bir değişkene atıyoruz.
            string generatedToken = _tokenHandler.WriteToken(_token);

           // Sonuçlarımızı tuple olarak dönüyoruz.
              return (user.Email, generatedToken);
            // return ("", "");

            */
            #endregion
        }



        public TokenUser IlanAuthenticate(string username, string password)
        {
            

           // var appSettingsSection = Configuration.GetSection("AppSettings");
           // services.Configure<AppSettings>(appSettingsSection);

            // Oluşturduğumuz gizli anahtarımızı byte dizisi olarak alıyoruz.
           // var appSettings = appSettingsSection.Get<AppSettings>();
          
        //    var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            VakifDb db = new VakifDb();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Kullanıcı Adı ve Şifre Boş Olamaz!");
            }

            if (Util.TcKontrol(username))
            {
                var sonuc = db.GetKullanici(username, password);
                if (sonuc.BasariliMi)
                {

                    Auth.User user = new Auth.User()
                    {
                        Email = sonuc.Veri.Rows[0]["EPosta"].ToString(),
                        ApplicantID = int.Parse(sonuc.Veri.Rows[0]["ID"].ToString()),
                        Name = sonuc.Veri.Rows[0]["Adi"].ToString(),
                        Surname = sonuc.Veri.Rows[0]["Soyadi"].ToString()
                    };

                    MyTokenHandler tokenHandler = new MyTokenHandler(_appSettings);
                    Token token = tokenHandler.CreateAccessToken();

                    return new TokenUser() { Token = token, User = user };

                }
                else
                {
                    throw new Exception("Bağlantı Hatası!");
                }

            }
            else

                throw new Exception("Kullanıcı Bulunamadı!");

        }


        public TokenUser IlanRefreshTokenLogin(string refreshToken, int applicantId)
        {
            if (refreshToken != null)
            {
                VakifDb db = new VakifDb();

                var sonuc = db.CheckRefreshToken(refreshToken);

                if (sonuc.Veri.Rows.Count > 0 && (DateTime)(sonuc.Veri.Rows[0]["RefreshTokenEndDate"]) > DateTime.Now)
                {

                    MyTokenHandler tokenHandler = new MyTokenHandler(_appSettings);
                    Token token = tokenHandler.CreateAccessToken();

                    db.SetRefreshToken(token.RefreshToken, applicantId.ToString());

                    Auth.User user = new Auth.User()
                    {
                        Email = sonuc.Veri.Rows[0]["EPosta"].ToString(),
                        ApplicantID = int.Parse(sonuc.Veri.Rows[0]["ID"].ToString()),
                        Name = sonuc.Veri.Rows[0]["Adi"].ToString(),
                        Surname = sonuc.Veri.Rows[0]["Soyadi"].ToString()
                    };

                    return new TokenUser() { Token = token, User = user };
                }
                else {
                    throw new Exception("Refresh token Süresi Doldu!");
                }
            }
            else {
                throw new Exception("RefreshToken boş olamaz!");
            }
        }

        public TokenUser RefreshTokenLogin(string refreshToken)
        {
            if (refreshToken != null)
            {
                Models.Users dbUser = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
                if (dbUser != null && dbUser?.RefreshTokenEndDate > DateTime.Now)
                {
                    MyTokenHandler tokenHandler = new MyTokenHandler(_appSettings);
                    Token token = tokenHandler.CreateAccessToken();

                    dbUser.RefreshToken = token.RefreshToken;
                    dbUser.RefreshTokenEndDate = token.Expiration.AddMinutes(5);
                    _dbContext.SaveChanges();

                    Auth.User user = new Auth.User() { Email = dbUser.UserName, Id = dbUser.Id, Name = dbUser.Name, Password = dbUser.Password, Surname = dbUser.Surname };

                    return new TokenUser() { Token = token, User = user };
                }
            }
            return null;
        }


        public Ingredients GetIngredients()
        {
            return new Ingredients() { Bacon = 0, Meat = 0, Cheese = 0, Salad = 0 };
        }

        public List<Models.Users> getUserList()
        {
            List<Models.Users> items = _dbContext.Users.ToList();// AçıkKapıSosyalHizmetBaş1.Where(item => item.Id == id.ToString()).FirstOrDefault();
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

