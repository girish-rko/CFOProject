using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace CFOProject.Identity
{
    public class MembershipExtensions
    {
        /// <summary>
        /// Generates a random password that follows the <seealso cref="ApplicationUserManager"/> PassswordValidator rules setup in IdentityConfig
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public static string GeneratePassword(ApplicationUserManager userManager)
        {
            var rnd = new Random();
            var passwordValidator = (Microsoft.AspNet.Identity.PasswordValidator)userManager.PasswordValidator;
            passwordValidator.RequiredLength = 6; //todo the password validator is set to a min length of 1 because there are current users with bad passwords (3,4 characters)

            while (true)
            {
                var password = Membership.GeneratePassword(passwordValidator.RequiredLength, 0);

                if ((passwordValidator.RequireDigit && !password.Any(char.IsDigit)) || (passwordValidator.RequireLowercase && !password.Any(char.IsLower)) || (passwordValidator.RequireUppercase && !password.Any(char.IsUpper)))
                    continue;

                if (!passwordValidator.RequireNonLetterOrDigit) password = Regex.Replace(password, @"[^a-zA-Z0-9]", m => rnd.Next(0, 10).ToString());
                return password;
            }
        }
    }
}