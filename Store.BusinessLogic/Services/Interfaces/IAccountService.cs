using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService 
    {
        public Task<ClaimsIdentity> GetIdentity(string mail, string password);
    }
}
