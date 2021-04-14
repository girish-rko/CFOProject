using CfO.Models;
using CfO.Models.Interface;
using CfO.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CFO.Data.Repository
{
    //[UsedImplicitly]
    public class UserLoginRepository : IUserLoginRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public UserLoginRepository(ApplicationDbContext applicationDbContext, ISessionProviderService sessionProviderService)
        {
            _applicationDbContext = applicationDbContext;
        }

        public User GetUser(GetModel<User> repositoryModel)
        {
            if (repositoryModel.Id != null)
            {
                //repositoryModel.Where = CanLogIn(repositoryModel.Where.And(x => x.Id == repositoryModel.Id));
            }
            var dbSet = _applicationDbContext.Users.Where(repositoryModel.Where);
            if (repositoryModel.Includes.Any())
            {
                dbSet = repositoryModel.Includes.Aggregate(dbSet, (current, include) => current.Include(include));
            }

            if (!repositoryModel.OrderBys.Any()) return dbSet.FirstOrDefault();

            return dbSet.FirstOrDefault();
        }
        public User InsertUser(User t)
        {
            _applicationDbContext.Users.Add(t);
            Save();
            return t;
        }

        public void UpdateUser(User t)
        {
            _applicationDbContext.Entry(t).State = EntityState.Modified;
            Save();
        }
        public void DeleteUser(User t)
        {
            _applicationDbContext.Entry(t).State = EntityState.Deleted;
            Save();
        }
        #region private methods

        private void Save()
        {
            _applicationDbContext.SaveChanges();
        }
        //private Expression<Func<User, bool>> CanLogIn(Expression<Func<User, bool>> predicate)
        //{
        //    //return predicate.And(x => x.MarkedAsRemoved != true);
        //}
        #endregion
    }
}
