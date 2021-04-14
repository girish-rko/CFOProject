//using AutoMapper;
//using Cfo.Business.DTO;
//using Cfo.Business.Services.Interfaces;
//using CfO.Models;
//using CFOProject.Identity;
//using Microsoft.AspNet.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using System.Web;

//namespace CFOProject.Services
//{
//    /// <summary>
//    /// These are methods that only the web ui needs. this should patch us along until we get a chance to implement identity
//    /// </summary>
//    public class WebUserService
//    {
//        private readonly IUserService _userService;
//        private readonly IUserLoginService _userLoginService;
//        private readonly ApplicationUserManager _userManager;
//        //private readonly ApplicationRoleManager _roleManager;
//        private readonly ICfoMailerService _cfoMailerService;
//        private readonly ITronixWebServerService _webServerService;

//        public WebUserService(IUserService userService, IUserLoginService userLoginService, ApplicationUserManager userManager, ICfoMailerService cfoMailerService,
//            ITronixWebServerService webServerService)
//        {
//            _userService = userService;
//            _userManager = userManager;
//            //_roleManager = roleManager;
//            _cfoMailerService = cfoMailerService;
//            _webServerService = webServerService;
//            _userLoginService = userLoginService;
//        }


//        public int RegisterUser(User user, bool sendEmail = true)
//        {
//            Mapper.CreateMap<User, IdentityUser>(); //since IdentityUser only exist in the web app, this mapping can't go in the MapConfig file
//            var identityUser = Mapper.Map<IdentityUser>(user);
//            var generatePassword = MembershipExtensions.GeneratePassword(_userManager);
//            var identityResult = _userManager.Create(identityUser, generatePassword);
//            if (!identityResult.Succeeded) { throw new Exception(identityResult.Errors.FirstOrDefault()); }
//            if (sendEmail)
//            {
//                SendWelcomeEmail(user, generatePassword);
//            }
//            return user.Id;
//        }

//        public int CreateUser(SaveUserDTO createUserDTO)
//        {
//            Mapper.CreateMap<SaveUserDTO, IdentityUser>(); //since IdentityUser only exist in the web app, this mapping can't go in the MapConfig file
//            var identityUser = Mapper.Map<IdentityUser>(createUserDTO);

//            // Format User names consistently
//            identityUser.FName = _userService.FormatNameCase(identityUser.FName);
//            identityUser.LName = _userService.FormatNameCase(identityUser.LName);
//            identityUser.ShowDlrCost = createUserDTO.CanSeeDealerCost;

//            var generatePassword = MembershipExtensions.GeneratePassword(_userManager);
//            var identityResult = _userManager.Create(identityUser, generatePassword);
//            if (!identityResult.Succeeded) { throw new Exception(identityResult.Errors.FirstOrDefault()); }
//            var user = _userLoginService.FindByUserName(identityUser.Email);
//            _userService.SetUserAssociations(user, createUserDTO);
//            SendWelcomeEmail(user, generatePassword);
//            return user.Id;
//        }

//        public void SendWelcomeEmail(User user, string generatePassword)
//        {
//            _cfoMailerService.Send(new SendEmailDto
//            {
//                To = new MailAddress(user.Email),
//                Subject = $"Hi {user.FName}, welcome to TRONIX!",
//                Body = $"You have been added as a user of TRONIX.<br/><br/>Your username is: {user.UserName}<br/>Your initial password is: {generatePassword}<br/><br/>" +
//                      $"To login to TRONIX for the first time, <a href=\"{_webServerService.GetLoginPageUrl()}\">click here</a><br/><br/>Questions?<br/><br/>" +
//                      "Please let us know if there\'s anything we can help you with by replying to this email or by emailing support@tronixtrm.com.",
//                From = SendEmailDto.Support
//            }, true);
//        }

//        public IdentityUser IdentityUser()
//        {
//            if (!HttpContext.Current.User.Identity.IsAuthenticated) return null;
//            return _userManager.FindById(HttpContext.Current.User.Identity.GetUserId<int>());
//        }

//        public IdentityUser GetUserProfileById(int id)
//        {
//            return _userManager.FindById(id);
//        }

//        //public ICollection<IdentityUser> GetUsersInRole(string role)
//        //{
//        //    var identityRole = _roleManager.FindByName(role);

//        //    if (identityRole == null) return null;

//        //    var usersInRole = new List<IdentityUser>();

//        //    foreach (var identityUser in _userManager.Users)
//        //    {
//        //        if (_userManager.IsInRole(identityUser.Id, role)) usersInRole.Add(identityUser);
//        //    }

//        //    return usersInRole;
//        //}
//    }
//}