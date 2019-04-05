using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pinger.Authorization.Models;

namespace Pinger.Authorization
{
    public interface IAuthorization
    {
        Task<bool> Login(LoginModel model, HttpContext httpContext);
    }
}