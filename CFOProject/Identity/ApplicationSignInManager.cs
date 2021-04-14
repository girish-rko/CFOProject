using Cfo.Business.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CFOProject.Identity
{
    public class ApplicationSignInManager : SignInManager<IdentityUser, int>
    {
        private readonly IUserLoginService _userService;

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager, IUserLoginService userService)
            : base(userManager, authenticationManager)
        {
            _userService = userService;
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication, DependencyResolver.Current.GetService<IUserLoginService>());
        }

        /// <summary>
        /// Sign in the user in using the user name and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="ipAddress"></param>
        /// <param name="isPersistent"></param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        public SignInStatus TronixPasswordSignIn(string userName, string password, string[] ipAddress, bool isPersistent = false, bool shouldLockout = false)
        {
            var user = UserManager?.FindByName(userName);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (user.Inactive)
            {
                return SignInStatus.Failure;
            }
            if (UserManager.IsLockedOut(user.Id))
            {
                return SignInStatus.LockedOut;
            }
            if (UserManager.CheckPassword(user, password))
            {
                UserManager.ResetAccessFailedCount(user.Id);
                return SignInOrTwoFactor(user, isPersistent, ipAddress);
            }
            if (!shouldLockout) return SignInStatus.Failure;

            // If lockout is requested, increment access failed count which might lock out the user
            UserManager.AccessFailed(user.Id);
            return UserManager.IsLockedOut(user.Id) ? SignInStatus.LockedOut : SignInStatus.Failure;
        }

        private SignInStatus SignInOrTwoFactor(IdentityUser user, bool isPersistent, string[] ipAddress)
        {
            if (!_userService.ValidateIpRestrictions(user.Id, ipAddress)) return SignInStatus.IpRestriction;

            var id = Convert.ToString(user.Id);
            if (UserManager.GetTwoFactorEnabled(user.Id)
            && (UserManager.GetValidTwoFactorProviders(user.Id)).Count > 0
            && !AuthenticationManager.TwoFactorBrowserRemembered(id))
            {
                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
                AuthenticationManager.SignIn(identity);
                return SignInStatus.RequiresVerification;
            }
            Task.Run(async () => await SignInAsync(user, isPersistent, false)).Wait();
            user.LastLogin = DateTime.UtcNow;
            UserManager.Update(user);
            return SignInStatus.Success;
        }
    }

    public enum SignInStatus
    {
        Success,
        LockedOut,
        RequiresVerification,
        Failure,
        IpRestriction
    }
}