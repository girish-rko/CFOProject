using AutoMapper;
using CfO.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CFOProject.Identity
{
    public class IdentityUser : User, IUser<int>
    {
        public IdentityUser()
        {
            //Id = Guid.NewGuid().ToString();
            EmailConfirmed = false;
            PhoneNumberConfirmed = false;
            TwoFactorEnabled = false;
            LockoutEnabled = false;
            AccessFailedCount = 0;
        }

        public IdentityUser(string userName) : this()
        {
            UserName = userName;
        }

        [IgnoreMap]
        public List<string> Roles { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<IdentityUser, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}