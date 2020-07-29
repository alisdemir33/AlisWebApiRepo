using JWTSample.Models;
//using JWTSample.ContosoModels;
using JWTSample.ViewModels;
using System.Collections.Generic;

namespace JWTSample.Services.User
{
    public interface IUserService
    {
        (string username, string token)? Authenticate(string username, string password);     
        List<Users> getUserList();
        Ingredients GetIngredients();
        List<Order> GetOrders();
    }
}
