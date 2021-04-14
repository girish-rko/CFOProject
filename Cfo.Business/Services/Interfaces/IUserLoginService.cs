using CfO.Models;
using CfO.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfo.Business.Services.Interfaces
{
    public interface IUserLoginService
    {
        void Add(User user);
        void Remove(User aspNetUser);
        User FindById(int id);
        User FindByUserName(string userName);
        void Update(User aspNetUser);
        IQueryable<User> GetAll();
        bool ValidateIpRestrictions(int userId, string[] currentUserIp);
        UserDTO GetUserInfoForLogin(int userID);
        void SendForgotPasswordLink(User user, string url);
        void RollbackUserInsert(User user);
    }
}
