using Cfo.Business.DTO;
using CfO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfo.Business.Services.Interfaces
{
    public interface IUserService
    {
        User Get(int id, bool includeMarkedAsRemoved = false);
        bool Retire(int userId);
        bool Activate(int userId);
        void SetUserAssociations(User user, SaveUserDTO userDTO);
        int Update(int id, SaveUserDTO userDTO);
        int Update(User user);
        int UpdateReportingRole(User user, LookerReportActivityLogDTO lookerReportActivityLogDTO);
        bool IsEmailAvailable(string email, int userIdBeingUpdated = 0);
        Func<User, string> GetCompanyExpression();
        string FormatNameCase(string name);
        bool EmailIsUnique(string email, int currentUserId);
        void UpdateProfile(int userId, string firstName, string lastName, string phoneNumber, string email, string faxNumber);
        List<User> GetUsersByDealer(int dealerId);
        //IQueryable<User> Search(UserSearchDTO criteria);
        //IQueryable<User> Search(UserSearchReportingDTO criteria);
        //void InsertUserDealers(List<UserDealer> userDealers, bool saveChanges);
        //void InsertUserSellingStyles(List<UserSellingStyle> sellingStyles, bool saveChanges);
        //void SetReleaseNoteRead(DateTime date);
        int GetLookerLicenseUsedCount(int dealerId);

        int GetUsedReportingLicenses(int dealerId);
    }
}
