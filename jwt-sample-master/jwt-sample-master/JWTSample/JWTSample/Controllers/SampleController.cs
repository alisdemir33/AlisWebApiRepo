using JWTSample.Entities;
using JWTSample.Models;
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
            var user = _userService.Authenticate(authenticateModel.Username, authenticateModel.Password);

            if (user == null)
                return BadRequest("Username or password incorrect!");

            return Ok(new { Username = user.Value.username, Token = user.Value.token });
        }

        [HttpGet]
        public IActionResult GetSummaries() => Ok(Summaries);


        [HttpGet]
        public IActionResult GetAcikKapiBasvuru()
        {
            AçıkKapıSosyalHizmetBaş1 item = _userService.getBasvuruByID(3);
            return Ok(item);
        }

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
