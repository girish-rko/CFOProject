//using Cfo.Business.DTO;
//using Cfo.Business.Services.Interfaces;
//using CfO.Models;
//using CfO.Models.Interface;
//using CfO.Models.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Cfo.Business.Services
//{
//    public class UserService : IUserService
//    {
//        //private readonly ICompanyAssociationsService _companyAssociationService;
//        //private readonly IUserRoleService _userRoleService;
//        //private readonly ISellingStyleService _sellingStyleService;
//        //private readonly IDealerService _dealerService;
//        private readonly IRepository<User> _userRepository;
//        //private readonly IRepository<UserTpa> _userTpaRepository;
//        private readonly ILoggerService _logger;
//        //private readonly IRepository<Dealer> _dealerRepository;
//        //private readonly IRepository<AgencyDealer> _agencyDealerRepository;
//        //private readonly IRepository<AgentDealer> _agentDealerRepository;
//        //private readonly IRepository<UserDealer> _userDealerRepository;
//        //private readonly IRepository<UserSellingStyle> _userSellingStyleRepository;
//        //private readonly IAuthorizationService _authorizationService;
//        //private readonly ISessionProviderService _sessionProviderService;
//        //private readonly ILookerReportActivityLogService _lookerReportActivityLogService;

//        public UserService(ICompanyAssociationsService companyAssociationService,
//            IUserRoleService userRoleService, IDealerService dealerService, ISellingStyleService sellingStyleService,
//            IRepository<User> userRepository,
//            IRepository<Dealer> dealerRepository, IRepository<AgentDealer> agentDealerRepository,
//            IRepository<AgencyDealer> agencyDealerRepository, IRepository<UserDealer> userDealerRepository, IRepository<UserSellingStyle> userSellingStyleRepository,
//            IRepository<UserTpa> userTpaRepository, IAuthorizationService authorizationService, ISessionProviderService sessionProviderService,
//            ILoggerService logger, ILookerReportActivityLogService lookerReportActivityLogService)
//        {
//            _companyAssociationService = companyAssociationService;
//            _userRoleService = userRoleService;
//            _dealerService = dealerService;
//            _userRepository = userRepository;
//            _dealerRepository = dealerRepository;
//            _agentDealerRepository = agentDealerRepository;
//            _agencyDealerRepository = agencyDealerRepository;
//            _userDealerRepository = userDealerRepository;
//            _userTpaRepository = userTpaRepository;
//            _logger = logger;
//            _sellingStyleService = sellingStyleService;
//            _userSellingStyleRepository = userSellingStyleRepository;
//            _authorizationService = authorizationService;
//            _sessionProviderService = sessionProviderService;
//            _lookerReportActivityLogService = lookerReportActivityLogService;
//        }

//        public User Get(int id, bool includeMarkedAsRemoved = false)
//        {
//            return _userRepository.Find(new FindModel<User>
//            {
//                Where = x => x.Id == id && (includeMarkedAsRemoved || x.MarkedAsRemoved != true),
//                Includes = GetBasicUserIncludes(),
//            }).FirstOrDefault();
//        }

//        public bool Retire(int userId)
//        {
//            var user = _userRepository.Get(new GetModel<User>
//            {
//                Id = userId
//            });
//            if (user == null) return false;
//            user.Inactive = true;
//            user.RetireDate = DateTime.UtcNow;
//            user.ModDate = user.RetireDate;
//            user.ModBy = userId;
//            _userRepository.Update(user);
//            return true;
//        }

//        public bool Activate(int userId)
//        {
//            var user = _userRepository.Get(new GetModel<User>
//            {
//                Id = userId
//            });
//            if (user == null) return false;
//            user.Inactive = false;
//            user.RetireDate = null;
//            user.ModDate = DateTime.UtcNow;
//            user.ModBy = userId;
//            _userRepository.Update(user);
//            return true;
//        }

//        public void SetUserAssociations(User user, SaveUserDTO userDTO)
//        {
//            _companyAssociationService.SetUserCompanyAssociations(user, userDTO);
//            _sellingStyleService.SetUserSellingStyles(user, userDTO);
//            _userRepository.Update(user);
//        }

//        public int Update(User user)
//        {
//            _userRepository.Update(user);
//            return user.Id;
//        }
//        public int UpdateReportingRole(User user, LookerReportActivityLogDTO lookerReportActivityLogDTO)
//        {
//            _userRepository.Update(user, false);
//            _lookerReportActivityLogService.AddUserReportingRoleLog(lookerReportActivityLogDTO);
//            return user.Id;
//        }

//        public int Update(int id, SaveUserDTO userDTO)
//        {
//            //NTRON-1579
//            var user = Get(id, false);
//            if (user == null)
//            {
//                throw new Exception("User not found.");
//            }
//            //since userTpa doesn't have its own id to check against for insert or update, detect this now.
//            //var hasTpaAssociationOrig = user.UserTpa != null;
//            // Format User names consistently
//            //Question - why are we doing this? Usually people know how to format their own names (says someone with a capital letter in the middle of her last name).
//            //Still probably a low priority item. - pg 2019-03-09
//            userDTO.FName = FormatNameCase(userDTO.FName);
//            userDTO.LName = FormatNameCase(userDTO.LName);

//            user.FName = userDTO.FName;
//            user.LName = userDTO.LName;
//            user.Title = userDTO.Title;
//            user.UserName = userDTO.UserName;
//            user.PhoneNumber = userDTO.PhoneNumber;
//            user.Phone2 = userDTO.Phone2;
//            user.FaxNumber = userDTO.FaxNumber;
//            user.Email = userDTO.Email;
//            user.ModBy = userDTO.ModBy;
//            user.ModDate = userDTO.ModDate;
//            user.UserRoleId = userDTO.UserRoleId;
//            user.ShowDlrCost = userDTO.CanSeeDealerCost;
//            user.CanOverrideCustomerPrice = userDTO.CanOverrideCustomerPrice;
//            user.CanFinalizeDeals = userDTO.CanFinalizeDeals;
//            user.CanFinalizeDealsForOthers = userDTO.CanFinalizeDealsForOthers;
//            user.AnalysisReporting = userDTO.AnalysisReporting;
//            user.ChangeLenderFinanceReserve = userDTO.ChangeLenderFinanceReserve;
//            user.CanIssueContracts = userDTO.CanIssueContracts;
//            user.CanModifyAllUsersDeals = userDTO.CanModifyAllUsersDeals;
//            user.CanDisableDmsPush = userDTO.CanDisableDmsPush;
//            user.IsLookerUser = userDTO.IsLookerUser;
//            user.CanSetDefaultTemplate = userDTO.CanSetDefaultTemplate;
//            user.IsIpRestricted = userDTO.IsIpRestricted;
//            user.ProducerCode = userDTO.ProducerCode;
//            user.DmsProducerNumber = userDTO.DMSProducerNumber;
//            user.LookerUserRoleId = userDTO.LookerUserRoleId;

//            _companyAssociationService.SetUserCompanyAssociations(user, userDTO);

//            //if (user.UserTpa != null)
//            //{
//            //    if (!hasTpaAssociationOrig)
//            //    {
//            //        _userTpaRepository.Insert(user.UserTpa, false);

//            //    }
//            //    else
//            //    {
//            //        _userTpaRepository.Update(user.UserTpa, false);
//            //    }
//            //}
//            _sellingStyleService.UpdateUserSellingStyles(user, userDTO);

//            _userRepository.Update(user);

//            return id;
//        }

//        /// <inheritdoc />
//        /// <summary>
//        /// Checks if the email address is available.
//        /// </summary>
//        /// <param name="email"></param>
//        /// <param name="userIdBeingUpdated">If this is included, this user is excluded from the email availability search. 
//        /// Use this when you're editing a user, and you don't want to check if their existing email address is already in use.</param>
//        /// <returns></returns>
//        public bool IsEmailAvailable(string email, int userIdBeingUpdated = 0)
//        {
//            //since this is an EF call, the comparison will be case-insensitive
//            //using where instead of single to support existing schema where uniqueness isn't enforced
//            var usersWithEmail = _userRepository.Find(new FindModel<User>
//            {
//                Where = u => u.Email == email || u.UserName == email
//            });

//            //if there aren't any users with this email, then the email is available
//            if (!usersWithEmail.Any()) return true;

//            //there are users with the email at this point

//            //if the user isn't being updated, then we can assume this email isn't available
//            //if the user being updated already has this email, then it's still available to them
//            return userIdBeingUpdated != 0 && usersWithEmail.Select(u => u.Id).Contains(userIdBeingUpdated);
//        }

//        public Func<User, string> GetCompanyExpression()
//        {
//            var tpaRoles = _userRoleService.GetRolesInUserRoleType(UserRoleTypeEnum.Tpa).Select(c => (int?)c).ToList();
//            var agencyRoles = _userRoleService.GetRolesInUserRoleType(UserRoleTypeEnum.Agency).Select(c => (int?)c).ToList();
//            var dealerGroupRoles = _userRoleService.GetRolesInUserRoleType(UserRoleTypeEnum.DealerGroup).Select(c => (int?)c).ToList();
//            var dealerRoles = _userRoleService.GetRolesInUserRoleType(UserRoleTypeEnum.Dealer).Select(c => (int?)c).ToList();
//            var tronixRoles = _userRoleService.GetRolesInUserRoleType(UserRoleTypeEnum.Tronix).Select(c => (int?)c).ToList();

//            return c => tpaRoles.Contains(c.UserRoleId) ? c.UserTpa?.Tpa.FullName :
//                          (dealerGroupRoles.Contains(c.UserRoleId) || dealerRoles.Contains(c.UserRoleId)) ? c.PrimaryDealer != null ? c.PrimaryDealer.Name : " Dealer User - No Company Set" :
//                          agencyRoles.Contains(c.UserRoleId) ? c.UserAgency?.Agency.AgencyName :
//                          tronixRoles.Contains(c.UserRoleId) ? "Tronix" : "Invalid Role";
//        }

//        public string FormatNameCase(string name)
//        {
//            //TODO: given that my last name is 'Porter-Buhl' some day I'd like to find out why we're doing this and if it is necessary.
//            var result = "";
//            if (name == null) return result;
//            // tokenize with spaces
//            var arrResult = name.Split(' ');
//            try
//            {
//                for (var i = 0; i < arrResult.Length; i++)
//                {
//                    if (arrResult[i].IndexOf("'", StringComparison.Ordinal) > 0)
//                    {
//                        // Handle apostrophes
//                        arrResult[i] =
//                            $"{arrResult[i].Substring(0, 1).ToUpper()}{arrResult[i].Substring(1, arrResult[i].IndexOf("'", StringComparison.Ordinal) - 1)}\'{arrResult[i].Substring(arrResult[i].IndexOf("'", StringComparison.Ordinal) + 1, 1).ToUpper()}{arrResult[i].Substring(arrResult[i].IndexOf("'", StringComparison.Ordinal) + 2, arrResult[i].Length - (arrResult[i].IndexOf("'", StringComparison.Ordinal) + 2)).ToLower()}";
//                    }
//                    else
//                    {
//                        arrResult[i] = arrResult[i].Substring(0, 1).ToUpper() + arrResult[i].Substring(1, arrResult[i].Length - 1).ToLower();
//                    }
//                    result += arrResult[i] + " ";
//                }
//            }
//            catch (Exception)
//            {
//            }

//            return result.Trim();
//        }

//        public bool EmailIsUnique(string email, int currentUserId)
//        {
//            var user = _userRepository.Get(new GetModel<User>
//            {
//                Where = x => x.Email == email && x.Id != currentUserId
//            });
//            return user == null;
//        }

//        public void UpdateProfile(int userId, string firstName, string lastName, string phoneNumber, string email, string faxNumber)
//        {
//            //todo we could probably move this back into Identity
//            var user = _userRepository.Get(new GetModel<User>
//            {
//                Id = userId,
//                Includes = GetBasicUserIncludes(),
//            });

//            user.FName = firstName;
//            user.LName = lastName;
//            user.PhoneNumber = phoneNumber;
//            user.Email = email;
//            user.FaxNumber = faxNumber;

//            _userRepository.Update(user);
//        }

//        public List<User> GetUsersByDealer(int dealerId)
//        {
//            return _userRepository.Find(new FindModel<User>
//            {
//                //Where = x => x.UserDealers.Select(y => y.DealerCode).Contains(dealerId),
//                //Includes = GetBasicUserIncludes()
//            });
//        }

//        public int GetLookerLicenseUsedCount(int dealerId)
//        {
//            throw new NotImplementedException();
//        }

//        public int GetUsedReportingLicenses(int dealerId)
//        {
//            throw new NotImplementedException();
//        }
//        //public IQueryable<User> Search(UserSearchReportingDTO criteria)
//        //{
//        //    var predicate = GetPredicate(criteria);

//        //    predicate = predicate.And(c => c.IsLookerUser == true);
//        //    predicate = predicate.And(c => c.UserDealers.Select(y => y.DealerCode).Contains(_sessionProviderService.CurrentDealer.Id));

//        //    if (criteria.UseLicenseSearch)
//        //    {
//        //        criteria.LicenseUtilization.SelectedId = int.Parse(criteria.LicenseSearchText);
//        //    }
//        //    if (!criteria.LicenseUtilization.SelectedId.HasValue)
//        //    {
//        //        criteria.LicenseUtilization.SelectedId = 1;
//        //    }

//        //    if (criteria.LicenseUtilization.SelectedId == (int?)ReportingLicensesEnum.UsedLicenses)
//        //    {
//        //        predicate = predicate.And(c => c.LookerUserRoleId != null);
//        //        predicate = predicate.And(c => c.LookerUserRoleId != (int)UserRoleLookerReportEnum.CannotViewReports);
//        //        predicate = predicate.And(c => c.LookerUserRoleId != (int)UserRoleLookerReportEnum.BasicReportViewer);
//        //    }
//        //    else
//        //    {
//        //        predicate = predicate.And(c => c.LookerUserRoleId == null || c.LookerUserRoleId == (int?)UserRoleLookerReportEnum.BasicReportViewer || c.LookerUserRoleId == (int?)UserRoleLookerReportEnum.CannotViewReports);
//        //    }

//        //    var users = _userRepository.FindQueryable(new FindModel<User>
//        //    {
//        //        Where = CanEdit(predicate, _sessionProviderService.CurrentUser),
//        //        Includes = GetUserSearchIncludes()
//        //    });
//        //    return users;
//        //}
//        //public IQueryable<User> Search(UserSearchDTO criteria)
//        //{
//        //    var predicate = GetPredicate(criteria);
//        //    var users = _userRepository.FindQueryable(new FindModel<User>
//        //    {
//        //        Where = CanEdit(predicate, _sessionProviderService.CurrentUser),
//        //        Includes = GetUserSearchIncludes()
//        //    });
//        //    return users;
//        //}

//        //public Expression<Func<User, bool>> GetPredicate(UserSearchDTO criteria)
//        //{
//        //    var predicate = PredicateExtensions.Begin<User>().And(x => true);
//        //    if (criteria.UseSimpleSearch)
//        //    {
//        //        if (!criteria.SimpleSearchText.IsNullOrWhiteSpace())
//        //        {
//        //            var searchTerms = Helpers.SimpleSearchHelper.GetSearchList(criteria.SimpleSearchText);
//        //            foreach (var term in searchTerms)
//        //            {
//        //                predicate = predicate.Or(x => x.UserName.Contains(term));
//        //                predicate = predicate.Or(x => x.FName.Contains(term));
//        //                predicate = predicate.Or(x => x.LName.Contains(term));
//        //                predicate = predicate.Or(x => x.UserRole.Name.Contains(term));
//        //                //could also add more complicated logic to search by 'company name' if ever asked.
//        //            }
//        //            //search company name
//        //        }
//        //    }
//        //    else
//        //    {
//        //        if (!string.IsNullOrWhiteSpace(criteria.FirstName))
//        //        {
//        //            predicate = predicate.And(c => c.FName.Contains(criteria.FirstName.Trim()));
//        //        }

//        //        if (!string.IsNullOrWhiteSpace(criteria.LastName))
//        //        {
//        //            predicate = predicate.And(c => c.LName.Contains(criteria.LastName.Trim()));
//        //        }

//        //        if (!string.IsNullOrWhiteSpace(criteria.UserName))
//        //        {
//        //            predicate = predicate.And(c => c.UserName.Contains(criteria.UserName.Trim()));
//        //        }

//        //        if (criteria.UserRole.SelectedId.HasValue)
//        //        {
//        //            predicate = predicate.And(c => c.UserRoleId == criteria.UserRole.SelectedId.Value);
//        //        }

//        //        if (criteria.TPA.SelectedId.HasValue)
//        //        {
//        //            predicate = predicate.And(c => c.UserTpa.TpaId == criteria.TPA.SelectedId.Value);
//        //        }

//        //        if (criteria.Agency.SelectedId.HasValue)
//        //        {
//        //            predicate = predicate.And(c => c.UserAgency.AgencyId == criteria.Agency.SelectedId.Value);
//        //        }

//        //        //if we have a dealer id to search by we don't also have to search by owner group
//        //        if (criteria.Dealer.SelectedId.HasValue)
//        //        {
//        //            predicate = predicate.And(c => (c.UserDealers.Any(ud => ud.Dealer.Id == criteria.Dealer.SelectedId.Value))
//        //           || (c.UserAgency.Agency.AgencyDealers.Any(da => da.Dealer.Id == criteria.Dealer.SelectedId.Value)));
//        //        }
//        //        else if (criteria.DealerGroup.SelectedId.HasValue)
//        //        {
//        //            predicate = predicate.And(c => (c.UserDealers.Any(ud => ud.Dealer.OwnerCode == criteria.DealerGroup.SelectedId.Value))
//        //            || (c.UserAgency.Agency.AgencyDealers.Any(da => da.Dealer.OwnerCode == criteria.DealerGroup.SelectedId.Value)));

//        //        }

//        //        if (criteria.ActiveUsersOnly.HasValue)
//        //        {
//        //            predicate = predicate.And(c => c.Inactive != criteria.ActiveUsersOnly.Value);
//        //        }

//        //        if (!string.IsNullOrWhiteSpace(criteria.Email))
//        //        {
//        //            predicate = predicate.And(c => c.Email.Contains(criteria.Email));
//        //        }
//        //    }

//        //    return predicate;
//        //}
//        //public void InsertUserDealers(List<UserDealer> userDealers, bool saveChanges)
//        //{
//        //    _userDealerRepository.Insert(userDealers, saveChanges);

//        //}
//        //public void InsertUserSellingStyles(List<UserSellingStyle> sellingStyles, bool saveChanges)
//        //{
//        //    _userSellingStyleRepository.Insert(sellingStyles, saveChanges);
//        //}
//        //public void SetReleaseNoteRead(DateTime date)
//        //{
//        //    var user = Get(_sessionProviderService.CurrentUser.Id);
//        //    user.SawReleaseNoticeDate = date;
//        //    _userRepository.Update(user);
//        //}

//        //public int GetLookerLicenseUsedCount(int dealerId)
//        //{
//        //    return GetUsersByDealer(dealerId).Count(u => u.LookerUserRoleId != null && u.IsLookerUser == true
//        //    && u.LookerUserRoleId != (int)UserRoleLookerReportEnum.CannotViewReports && u.LookerUserRoleId != (int)UserRoleLookerReportEnum.BasicReportViewer);
//        //}


//        //public int GetUsedReportingLicenses(int dealerId)
//        //{
//        //    return _userRepository.Count(x => x.PrimaryDealerCode == dealerId && (x.LookerUserRoleId != null &&
//        //        x.LookerUserRoleId != (int?)UserRoleLookerReportEnum.BasicReportViewer &&
//        //        x.LookerUserRoleId != (int?)UserRoleLookerReportEnum.CannotViewReports));
//        //}

//        #region private methods

//        //private Expression<Func<User, bool>> CanEdit(Expression<Func<User, bool>> predicate, UserDTO user)
//        //{
//        //    //NTRON-1579
//        //    predicate = predicate.And(x => x.MarkedAsRemoved != true);

//        //    switch ((UserRoleEnum)user.RoleId)
//        //    {
//        //        //Tronix Admins can always view everything
//        //        case UserRoleEnum.TronixAdmin:
//        //            return predicate;
//        //        //TPA Admins can edit other tpa admins for the same company
//        //        case UserRoleEnum.TpaAdmin:
//        //            //NTRON-1525 - can see & edit currently associated TPA, Agency, dealer group and dealer users
//        //            return predicate.And(
//        //                //tpa users
//        //                x => x.UserRoleId == (int)UserRoleEnum.TpaAdmin && x.UserTpa.Tpa.UserTpas.Any(y => y.Id == user.Id)
//        //                //dealer & dealergroup users, while the db canview allows inactive, this should only allow active
//        //                || x.UserDealers.Any(z => z.Dealer.DealerTpaConfigurations.Any(a => a.SystemIntegrationStatusId == (int)Status.Active && a.Tpa.UserTpas.Any(b => b.Id == user.Id)))
//        //                //agency
//        //                || x.UserAgency.Agency.TpaAgencies.Any(c => c.Tpa.UserTpas.Any(d => d.Id == user.Id))
//        //            );

//        //        case UserRoleEnum.AgencyAdmin:
//        //            return predicate.And(x => x.UserAgency.Agency.UserAgencies.Any(y => y.UserId == user.Id));

//        //        case UserRoleEnum.DealerGroupAdmin:
//        //            return predicate
//        //                .And(x => x.UserDealers.Any(z => z.Dealer.UserDealers.Any(y => y.UserId == user.Id)))
//        //                .And(x => x.UserRoleId == (int)UserRoleEnum.DealerProducer
//        //                          || x.UserRoleId == (int)UserRoleEnum.DealerService
//        //                          || x.UserRoleId == (int)UserRoleEnum.DealerServiceWithSales
//        //                          || x.UserRoleId == (int)UserRoleEnum.DealerAdmin
//        //                          || x.UserRoleId == (int)UserRoleEnum.DealerGroupAdmin
//        //                          || x.UserRoleId == (int)UserRoleEnum.DealerAccounting
//        //                          || x.UserRoleId == (int)UserRoleEnum.DealerGroupAccounting);

//        //        case UserRoleEnum.DealerAdmin:
//        //            return predicate
//        //                .And(x => x.UserDealers.Any(z => z.Dealer.UserDealers.Any(y => y.UserId == user.Id)))
//        //                .And(x => x.UserRoleId == (int)UserRoleEnum.DealerProducer
//        //                          || x.UserRoleId == (int)UserRoleEnum.DealerService
//        //                          || x.UserRoleId == (int)UserRoleEnum.DealerServiceWithSales
//        //                          || x.UserRoleId == (int)UserRoleEnum.DealerAdmin
//        //                          || x.UserRoleId == (int)UserRoleEnum.DealerAccounting);
//        //        case UserRoleEnum.Unknown:
//        //        default:
//        //            throw new NotImplementedException($"Not implemented for user role: { user.RoleId }");
//        //    }
//        //}
//        //private List<Expression<Func<User, object>>> GetBasicUserIncludes()
//        //{
//        //    var includes = new List<Expression<Func<User, object>>>
//        //    {
//        //        c => c.DealJacketOptions,
//        //        c => c.UserAgency,
//        //        c => c.UserDealers,
//        //        c => c.UserRole,
//        //        c => c.UserRole.UserRoleType,
//        //        c => c.UserSellingStyles,
//        //        c => c.UserTpa
//        //    };
//        //    return includes;
//        //}
//        //private List<Expression<Func<User, object>>> GetUserSearchIncludes()
//        //{
//        //    var includes = new List<Expression<Func<User, object>>>
//        //    {
//        //        c => c.DealJacketOptions,
//        //        c => c.UserAgency,
//        //        c => c.UserDealers,
//        //        c => c.UserRole,
//        //        c => c.UserRole.UserRoleType,
//        //        c => c.UserSellingStyles,
//        //        c => c.UserTpa,
//        //        c => c.UserDealers.Select(x => x.Dealer),
//        //        c=>c.UserAgency.Agency

//        //    };
//        //    return includes;
//        //}
//        #endregion
//    }
//}
