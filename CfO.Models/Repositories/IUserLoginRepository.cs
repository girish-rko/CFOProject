using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models.Repositories
{
    public interface IUserLoginRepository
    {
        User GetUser(GetModel<User> repositoryModel);
        User InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
