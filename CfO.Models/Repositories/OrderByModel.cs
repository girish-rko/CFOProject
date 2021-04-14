using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models.Repositories
{
    public class OrderByModel<T>
    {
        public Expression<Func<T, object>> Expression { get; set; }

        public bool Ascending { get; set; } = true;
    }
}
