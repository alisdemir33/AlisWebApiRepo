﻿using JWTSample.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JWTSample.Auth
{
    public class MyTokenHandler
    {
      //  public IConfiguration Configuration { get; set; }
        private readonly AppSettings _appSettings;

        public MyTokenHandler(AppSettings appSettings)
        {
            //Configuration = configuration;
            _appSettings = appSettings;

        }
        //Token üretecek metot.
        public Token CreateAccessToken(User user)
        {
            Token tokenInstance = new Token();

            //Security  Key'in simetriğini alıyoruz.
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            //SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz.
            tokenInstance.Expiration = DateTime.Now.AddMinutes(5);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                //issuer: Configuration["Token:Issuer"],
                //audience: Configuration["Token:Audience"],
                expires: tokenInstance.Expiration,//Token süresini 5 dk olarak belirliyorum
                notBefore: DateTime.Now,//Token üretildikten ne kadar süre sonra devreye girsin ayarlıyouz.
                signingCredentials: signingCredentials
                );

            //Token oluşturucu sınıfında bir örnek alıyoruz.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Token üretiyoruz.
            tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);

            //Refresh Token üretiyoruz.
            tokenInstance.RefreshToken = CreateRefreshToken();
            return tokenInstance;
        }

        //Refresh Token üretecek metot.
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
