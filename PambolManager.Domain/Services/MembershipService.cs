using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PambolManager.Domain.Entities;
using PambolManager.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PambolManager.Domain.Services
{
    public class MembershipService : IMembershipService
    {
        private UserManager<FieldManager> _userManager;

        public MembershipService()
        {
            _userManager = new UserManager<FieldManager>(new UserStore<FieldManager>(new EntitiesContext()));
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel userModel)
        {
            FieldManager user = new FieldManager
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<FieldManager> FindUserAsync(string userName, string password)
        {
            FieldManager user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<FieldManager> FindUserByNameAsync(string userName)
        {
            FieldManager user = await _userManager.FindByNameAsync(userName);

            return user;
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }

    }
}
