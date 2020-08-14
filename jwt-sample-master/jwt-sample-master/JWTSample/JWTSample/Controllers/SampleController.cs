using JWTSample.Entities;
using JWTSample.Models;
//using JWTSample.ContosoModels;
using JWTSample.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace JWTSample.Controllers
{
    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [ApiController]
    //Routing için mesela /Sample/GetSummaries olarak ayarladım.
    [Route("[controller]/[action]")]
    public class SampleController : ControllerBase
    {
        private static readonly string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        private readonly IUserService _userService;
        public SampleController(IUserService userService) => _userService = userService;

        //Burada da AllowAnonymous attribute nü kullanarak bu seferde bu metoda herkesin erişebileceğini söylüyorum.
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthenticateModel authenticateModel)
        {
            //var tokenUser = _userService.Authenticate(authenticateModel.Username, authenticateModel.Password);
            var tokenUser = _userService.IlanAuthenticate(authenticateModel.Username, authenticateModel.Password);

            if (tokenUser == null)
                return BadRequest("Username or password incorrect!");

            return Ok(tokenUser);

            //return Ok(new { Username = user.Value.username, Token = user.Value.token });

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RefreshToken([FromBody] RefreshTokenEntity refreshToken)
        {
            var tokenUser = _userService.RefreshTokenLogin(refreshToken.RefreshToken);

            if (tokenUser == null)
                return BadRequest("Invalid Cred!");

            return Ok(tokenUser);           

        }





        [HttpGet]
        public IActionResult GetSummaries() => Ok(Summaries);
       

        [HttpGet]
        public IActionResult GetUserList()
        {
            List<Users> items = _userService.getUserList();
            return Ok(items);
        }

        [HttpGet]
        public IActionResult GetIngredients()
        {            
            return Ok(_userService.GetIngredients());
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_userService.GetOrders());
        }
    }
}
