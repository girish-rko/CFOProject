using CFOProject.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CFOProject.Identity
{
    public class ApplicationUserManager : UserManager<IdentityUser, int>
    {
        public ApplicationUserManager(IUserStore<IdentityUser, int> store, IDataProtectionProvider dataProtectionProvider,
            EmailService emailService, SmsService smsService) : base(store)
        {
            UserValidator = new UserValidator<IdentityUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<IdentityUser, int>
            {
                MessageFormat = "Your security code is {0}"
            });
            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<IdentityUser, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            EmailService = emailService;
            SmsService = smsService;
            UserTokenProvider = new DataProtectorTokenProvider<IdentityUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
        }
    }
}