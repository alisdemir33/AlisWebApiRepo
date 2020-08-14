using System.ComponentModel.DataAnnotations;

namespace JWTSample
{
    //Authenticate işleminde neler geleceğini ve validasyonları için oluşturdum.
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }


    public class RefreshTokenEntity {
        private string refreshToken;

        public string RefreshToken { get => refreshToken; set => refreshToken = value; }
    }
}
