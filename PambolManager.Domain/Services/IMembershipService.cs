﻿using Microsoft.AspNet.Identity;
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
        Task<IdentityResult> RegisterUserAsync(UserModel userModel);
        Task<IdentityUser> FindUserAsync(string userName, string password);
    }
}