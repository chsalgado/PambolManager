using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PambolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PambolManager.Domain.Services
{
    public interface IMembershipService : IDisposable
    {
        Task<IdentityResult> RegisterUserAsync(RegisterViewModel userModel);
        Task<FieldManager> FindUserAsync(string userName, string password);
        Task<FieldManager> FindUserByNameAsync(string userName);
        Task<FieldManager> FindAsync(UserLoginInfo loginInfo);
        Task<IdentityResult> CreateAsync(FieldManager user);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
    }
}
