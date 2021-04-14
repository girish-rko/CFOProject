using AutoMapper;
using Cfo.Business.Services.Interfaces;
using CfO.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFOProject.Identity
{
    public class UserStore : IUserStore<IdentityUser, int>,
        IUserPasswordStore<IdentityUser, int>, IUserSecurityStampStore<IdentityUser, int>,
        IUserLockoutStore<IdentityUser, int>, IUserTwoFactorStore<IdentityUser, int>, IUserEmailStore<IdentityUser, int>,
        IUserPhoneNumberStore<IdentityUser, int>, IQueryableUserStore<IdentityUser, int>
    {
        private readonly IUserLoginService _userLoginService;

        public IQueryable<IdentityUser> Users => throw new NotImplementedException();

        public UserStore(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        //public Task CreateAsync(IdentityUser identityUser)
        //{
        //    if (identityUser == null)
        //        throw new ArgumentNullException(nameof(identityUser));

        //    var aspNetUser = new User();
        //    Mapper.Map(identityUser, aspNetUser);
        //    if (_userLoginService.FindByUserName(aspNetUser.UserName) == null)
        //    {
        //        _userLoginService.Add(aspNetUser);
        //    }
        //    else
        //    {
        //        _userLoginService.Update(aspNetUser);
        //    }
        //    return Task.FromResult(0);
        //}

        //public Task DeleteAsync(IdentityUser identityUser)
        //{
        //    if (identityUser == null)
        //        throw new ArgumentNullException(nameof(identityUser));

        //    var aspNetUser = new User();

        //    Mapper.Map(identityUser, aspNetUser);

        //    _userLoginService.Remove(aspNetUser);
        //    return Task.FromResult(0);
        //}

        //public Task<IdentityUser> FindByIdAsync(int userId)
        //{
        //    var aspNetUser = _userLoginService.FindById(userId);
        //    return Task.FromResult(Mapper.Map<IdentityUser>(aspNetUser));
        //}

        //public Task<IdentityUser> FindByNameAsync(string userName)
        //{
        //    var aspNetUser = _userLoginService.FindByUserName(userName);
        //    return Task.FromResult(Mapper.Map<IdentityUser>(aspNetUser));
        //}

        //public Task UpdateAsync(IdentityUser identityUser)
        //{
        //    if (identityUser == null) throw new ArgumentException(nameof(identityUser));

        //    var aspNetUser = _userLoginService.FindById(identityUser.Id);
        //    if (aspNetUser == null)
        //        throw new ArgumentException("IdentityUser does not correspond to a User entity.", nameof(identityUser));

        //    Mapper.Map(identityUser, aspNetUser);

        //    _userLoginService.Update(aspNetUser);
        //    return Task.FromResult(0);
        //}

        //public Task<IdentityUser> FindByEmailAsync(string email)
        //{
        //    return Task.FromResult(Mapper.Map<IdentityUser>(_userLoginService.FindByUserName(email)));
        //}

        public Task<string> GetPasswordHashAsync(IdentityUser identityUser)
        {
            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));
            return Task.FromResult(identityUser.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser identityUser)
        {
            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));
            return Task.FromResult(!string.IsNullOrWhiteSpace(identityUser.PasswordHash));
        }

        public Task SetPasswordHashAsync(IdentityUser identityUser, string passwordHash)
        {
            identityUser.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(IdentityUser identityUser)
        {
            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));
            return Task.FromResult(identityUser.SecurityStamp);
        }

        public Task SetSecurityStampAsync(IdentityUser identityUser, string stamp)
        {
            return Task.FromResult(identityUser.SecurityStamp = stamp);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            return Task.FromResult(DateTimeOffset.Parse(user.LockoutEndDateUtc.GetValueOrDefault().ToString()));
        }

        public Task SetLockoutEndDateAsync(IdentityUser identityUser, DateTimeOffset lockoutEnd)
        {
            return Task.FromResult(identityUser.LockoutEndDateUtc = lockoutEnd.DateTime);
        }

        public Task<int> IncrementAccessFailedCountAsync(IdentityUser identityUser)
        {
            return Task.FromResult(identityUser.AccessFailedCount++);
        }

        public Task ResetAccessFailedCountAsync(IdentityUser identityUser)
        {
            return Task.FromResult(identityUser.AccessFailedCount = 0);
        }

        public Task<int> GetAccessFailedCountAsync(IdentityUser identityUser)
        {
            return Task.FromResult(identityUser.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(IdentityUser identityUser)
        {
            return Task.FromResult(identityUser.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(IdentityUser identityUser, bool enabled)
        {
            return Task.FromResult(identityUser.LockoutEnabled = enabled);
        }

        public Task SetTwoFactorEnabledAsync(IdentityUser identityUser, bool enabled)
        {
            return Task.FromResult(identityUser.TwoFactorEnabled = enabled);
        }

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser identityUser)
        {
            return Task.FromResult(identityUser.TwoFactorEnabled);
        }

        public Task SetEmailAsync(IdentityUser identityUser, string email)
        {
            return Task.FromResult(identityUser.Email = email);
        }

        public Task<string> GetEmailAsync(IdentityUser identityUser)
        {
            return Task.FromResult(identityUser.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser identityUser)
        {
            return Task.FromResult(identityUser.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(IdentityUser identityUser, bool confirmed)
        {
            return Task.FromResult(identityUser.EmailConfirmed = confirmed);
        }

        public Task SetPhoneNumberAsync(IdentityUser identityUser, string phoneNumber)
        {
            return Task.FromResult(identityUser.PhoneNumber = phoneNumber);
        }

        public Task<string> GetPhoneNumberAsync(IdentityUser identityUser)
        {
            return Task.FromResult(identityUser.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser identityUser)
        {
            return Task.FromResult(identityUser.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(IdentityUser identityUser, bool confirmed)
        {
            return Task.FromResult(identityUser.PhoneNumberConfirmed = confirmed);
        }

        public void Dispose()
        {
            // Dispose does nothing since we want IoC to manage the lifecycle
        }

        public Task CreateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        //public IQueryable<IdentityUser> Users
        //{
        //    get
        //    {
        //        var identityUsers = Mapper.Map<List<IdentityUser>>(_userLoginService.GetAll());
        //        return identityUsers.AsQueryable();
        //    }
        //}
    }
}