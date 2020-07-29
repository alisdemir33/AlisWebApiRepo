using JWTSample.Models;
using JWTSample.ViewModels;
using System.Collections.Generic;

namespace JWTSample.Services.User
{
    public interface IUserService
    {
        (string username, string token)? Authenticate(string username, string password);
        AçıkKapıSosyalHizmetBaş1 getBasvuruByID(long id);
        List<Users> getUserList();
        Ingredients GetIngredients();
        List<Order> GetOrders();
    }
}
