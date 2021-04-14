using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models.Repositories
{
    public class FindModel<T>
    {

        public OrderByModel<T> OrderBy
        {
            set { OrderBys.Add(value); }
        }

        public List<OrderByModel<T>> OrderBys { get; set; } = new List<OrderByModel<T>>();

        public Expression<Func<T, bool>> Where { get; set; } = x => true;


        public Expression<Func<T, object>> Include
        {
            set { Includes.Add(value); }
        }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    }


    public class FindModel<T, TResult>
    {

        public OrderByModel<T> OrderBy
        {
            set { OrderBys.Add(value); }
        }

        public List<OrderByModel<T>> OrderBys { get; set; } = new List<OrderByModel<T>>();

        public Expression<Func<T, TResult>> Select { get; set; }

        public Expression<Func<T, bool>> Where { get; set; } = x => true;
        public Expression<Func<T, object>> Include
        {
            set { Includes.Add(value); }
        }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    }
}
