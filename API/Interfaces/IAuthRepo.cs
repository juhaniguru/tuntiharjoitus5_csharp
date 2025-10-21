using API.Models;

namespace API.Interfaces;
public interface IAuthRepo
{
    public Task<AppUser?> Login(string username, string password);
}