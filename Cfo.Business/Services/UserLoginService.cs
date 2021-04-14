using Cfo.Business.DTO;
using Cfo.Business.Services.Interfaces;
using CfO.Models;
using CfO.Models.DTO;
using CfO.Models.Enums;
using CfO.Models.Interface;
using CfO.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Cfo.Business.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserLoginRepository _userRepository;
        //private readonly IDealerLoginRepository _dealerRepository;
        private readonly ILoggerService _logger;
        private readonly ICfoMailerService _cfoMailerService;
        //private readonly ISellingStyleService _sellingStyleService;
        //private readonly IRepository<UserSellingStyle> _userSellingStyleRepository;
        //private readonly IRepository<UserDealer> _userDealerRepository;

        public UserLoginService(
            IUserLoginRepository userInitialLoginRepository,  ICfoMailerService cfoMailerService,

         ILoggerService logger)
        {
            _userRepository = userInitialLoginRepository;
            //_dealerRepository = dealerRepository;
            _cfoMailerService = cfoMailerService;
            _logger = logger;
            //be a little careful with this. the other methods being used by the login service don't do data access so as to avoid using the session user object before logging in.
            //_sellingStyleService = sellingStyleService;
            //_userSellingStyleRepository = userSellingStyleRepository;
            //_userDealerRepository = userDealerRepository;
        }


        public void Add(User user)
        {
            _userRepository.InsertUser(user);
        }
        //this will hard error if the user has any foreign keys, should only be used as a rollback during insert
        public void RollbackUserInsert(User user)
        {
            //delete selling styles
            //if (user.UserSellingStyles.Count > 0)
            //{
            //    _userSellingStyleRepository.Delete(user.UserSellingStyles.ToList());
            //}
            ////delete user dealers
            //if (user.UserDealers.Count > 0)
            //{
            //    _userDealerRepository.Delete(user.UserDealers.ToList());
            //}
            //delete user 
            _userRepository.DeleteUser(user);
        }
        public void Remove(User aspNetUser)
        {
            throw new NotImplementedException("This method is required for Identity but we don't actually use at the moment");
        }

        public User FindById(int id)
        {
            return _userRepository.GetUser(new GetModel<User>
            {
                Id = id
            });
        }

        public User FindByUserName(string userName)
        {
            return _userRepository.GetUser(new GetModel<User>
            {
                Where = x => x.UserName == userName,
                Includes = GetBasicUserIncludes()
            });
        }

        public void Update(User aspNetUser) //todo this is incomplete
        {
            var user = _userRepository.GetUser(new GetModel<User>
            {
                Id = aspNetUser.Id
            });

            user.PasswordHash = aspNetUser.PasswordHash;

            _userRepository.UpdateUser(user);
        }
        public IQueryable<User> GetAll()
        {
            throw new NotImplementedException("This method is required for Identity but we don't actually use at the moment");
        }
        //public bool ValidateIpRestrictions(int userId, string[] currentUserIp)
        //{
        //    var user = _userRepository.GetUser(new GetModel<User>
        //    {
        //        Id = userId
        //    });
        //    if ((user.IsIpRestricted == null || !user.IsIpRestricted.Value) ||
        //        (user.UserRole.UserRoleTypeId != (int)UserRoleTypeEnum.DealerGroup && user.UserRole.UserRoleTypeId != (int)UserRoleTypeEnum.Dealer)) return true;

        //    var primaryDealer = user.PrimaryDealer;
        //    if (primaryDealer == null) return false;

        //    var allowedIps = primaryDealer?.AllowedIpAddress?.Split(',') ?? new string[0];

        //    // ReSharper disable once ConvertClosureToMethodGroup
        //    return allowedIps.Any(i => currentUserIp.Contains(i));
        //}

        public UserDTO GetUserInfoForLogin(int userID)
        {
            //_logger.AddTrail($"{nameof(userID)}: {userID}");
            if (userID == 0) return null;
            var userDb = _userRepository.GetUser(new GetModel<User>
            {
                Where = x => x.Id == userID,
                Includes = GetBasicUserIncludes()
            });
            if (userDb == null) return null;
            var admins = new List<UserRoleEnum>
            {
                //NOTE: does not include agency admin
                UserRoleEnum.ProvenCfoAdmin,
                //UserRoleEnum.TpaAdmin,
                //UserRoleEnum.DealerAdmin,
                //UserRoleEnum.DealerGroupAdmin
            };
            var user = new UserDTO
            {
                Id = userDb.Id,
                UserName = userDb.UserName,
                IsInactive = userDb.Inactive,
                Email = userDb.Email,
                FirstName = userDb.FName,
                LastName = userDb.LName,
                //MiddleInitial = userDb.MInit,
                PhoneNumber = userDb.PhoneNumber,
                //FaxNumber = userDb.FaxNumber,
                RoleId = userDb.UserRoleId,
                //RoleTypeId = userDb.UserRole.UserRoleTypeId,
                RequirePasswordChange = userDb.ReqPwdChange.GetValueOrDefault()
            };
            //    ProducerCode = userDb.ProducerCode,
            //    DMSProducerNumber = userDb.DmsProducerNumber,
            //    CgwUserId = userDb.CgwUserId,
            //    SawReleaseNoticeDate = userDb.SawReleaseNoticeDate,
            //    Authorizations = new UserAuthorizations()
            //    {
            //        CanFinalizeDeals = userDb.CanFinalizeDeals.GetValueOrDefault(),
            //        CanSetDefaultTemplate = userDb.CanSetDefaultTemplate.GetValueOrDefault(),
            //        CanOverrideCustomerPrice = userDb.CanOverrideCustomerPrice.GetValueOrDefault(),
            //        CanSeeDealerCost = userDb.ShowDlrCost.GetValueOrDefault(),
            //        CanIssueContracts = userDb.CanIssueContracts.GetValueOrDefault(),
            //        CanViewReports = userDb.AnalysisReporting.GetValueOrDefault(),
            //        CanFinalizeDealsForOthers = userDb.CanFinalizeDealsForOthers.GetValueOrDefault(),
            //    },
            //    IsAdmin = admins.Contains((UserRoleEnum)userDb.UserRoleId),
            //    SelectedOwnerGroup = 0, // userDb.DefaultOwnerGroup //todo ↓
            //    AgencyID = 0, //userDb.AgencyID //todo both of these are 0 from the old procedure cause someone took out the code to populate those so who cares?
            //    RequireProfileUpdate = !string.IsNullOrEmpty(userDb.Email) && !string.IsNullOrEmpty(userDb.PhoneNumber) && !string.IsNullOrEmpty(userDb.FaxNumber),
            //    PrimaryDealerCode = userDb.PrimaryDealerCode.GetValueOrDefault(0)
            //};

            //var primaryDealer = userDb.PrimaryDealer;
            ////User Permissions
            //if (user.RoleTypeId == (int)UserRoleTypeEnum.Agency)
            //{
            //    var agency = userDb.UserAgency.Agency;
            //    user.AgencyID = agency.Id;
            //    user.AgencyName = agency.AgencyName;
            //}
            //var userDealers = _dealerRepository.GetDealersForUserLogin(new FindModel<Dealer>(), user.Id, (UserRoleEnum)user.RoleId);
            //user.Dealers = userDealers.Select(x => x.Id).Distinct().ToList();
            ////It is rare but could happen (because of shifting dealer group to agency, tpa etc) that the primary dealer gets out of sync with the dealer list and the user no longer has access to that dealer.
            ////This section of code does a self-heal should this happen.
            //if (user.PrimaryDealerCode != 0 && !user.Dealers.Contains(user.PrimaryDealerCode))
            //{
            //    user.PrimaryDealerCode = 0;
            //    userDb.PrimaryDealerCode = null;
            //    _userRepository.UpdateUser(userDb);
            //}
            ////a nice touch..  if the user is assigned to only one dealer but doesn't have a default dealer, set it to the one available
            //if (user.PrimaryDealerCode == 0 && user.Dealers.Count == 1)
            //{
            //    primaryDealer = userDealers.First();
            //    user.PrimaryDealerCode = primaryDealer.Id;
            //}
            //user.ActiveSellingStyles = GetActiveSellingStylesForInitialUserLogin((UserRoleEnum)user.RoleId, primaryDealer, userDb.UserSellingStyles.ToList()).ToList();
            //user.AllSellingStyles = GetAllSellingStylesForInitialUserLogin((UserRoleEnum)user.RoleId, userDealers, userDb.UserSellingStyles.ToList()).ToList();

            return user;

        }

        public void SendForgotPasswordLink(User user, string url) //todo this should be converted to use the MVC password reset method
        {
            if (user == null) return;

            _cfoMailerService.Send(new SendEmailDto
            {
                Subject = $"Hi {user.FName}, here\'s how to reset your TRONIX password.",
                Body = "We have received a request to have your password reset for my.tronixtrm.com.<br/><br/>If you did not make this request, " +
                       $"please ignore this email.<br/><br/>Your username is your email address. To reset your password, <a href=\"{url}\">click here</a><br/><br/>Questions?" +
                       "<br/><br/>Please let us know if there\'s anything we can help you with by replying to this email or by emailing support@tronixtrm.com.",
                From = SendEmailDto.Support,
                To = new MailAddress(user.Email)
            }, true);
        }

        #region private methods

        //private IEnumerable<SellingStyleSerializableDTO> GetActiveSellingStylesForInitialUserLogin(UserRoleEnum userRole, Dealer primaryDealer, List<UserSellingStyle> userSellingStyles)
        //{
        //    return GetActiveSellingStylesPerDealerOnInitialUserLogin(userRole, primaryDealer, userSellingStyles);
        //}
        //private IEnumerable<SellingStyleSerializableDTO> GetAllSellingStylesForInitialUserLogin(UserRoleEnum userRole, List<Dealer> dealers, List<UserSellingStyle> userSellingStyles)
        //{
        //    List<SellingStyleSerializableDTO> ret = new List<SellingStyleSerializableDTO>();
        //    foreach (var dealer in dealers)
        //    {
        //        ret.AddRange(GetActiveSellingStylesPerDealerOnInitialUserLogin(userRole, dealer, userSellingStyles));
        //    }
        //    return ret.DistinctBy(x => x.Id).OrderByDescending(x =>
        //            x.Name == SellingStyles.Deductive.ToString()).ThenByDescending(x => x.Name == SellingStyles.Traditional.ToString())
        //        .ToList();
        //}
        //private List<SellingStyleSerializableDTO> GetActiveSellingStylesPerDealerOnInitialUserLogin(UserRoleEnum userRole, Dealer dealer, List<UserSellingStyle> userSellingStyles)
        //{
        //    if (dealer == null) return new List<SellingStyleSerializableDTO>();
        //    var dealerSellingStyles = dealer.DealerSellingStyles.Select(s => new SellingStyleSerializableDTO()
        //    {
        //        Id = s.SellingStyleId,
        //        Name = s.SellingStyle.Name
        //    }).ToList();
        //    var dealerSellingStylesId = dealerSellingStyles.Select(x => x.Id).ToList();
        //    return _sellingStyleService.GetActiveSellingStylesForUserRole(userRole, dealerSellingStyles, userSellingStyles);
        //}
        private List<Expression<Func<User, object>>> GetBasicUserIncludes()
        {
            var includes = new List<Expression<Func<User, object>>>
            {
                //c => c.DealJacketOptions,
                //c => c.UserAgency,
                //c => c.UserDealers,
                //c => c.UserRole,
                //c => c.UserRole.UserRoleType,
                //c => c.UserSellingStyles,
                //c => c.UserTpa
            };
            return includes;
        }

        public bool ValidateIpRestrictions(int userId, string[] currentUserIp)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
