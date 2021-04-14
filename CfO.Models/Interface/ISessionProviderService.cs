using CfO.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models.Interface
{
    public interface ISessionProviderService
    {
        UserDTO CurrentUser { get; }
        //SessionDealerDTO CurrentDealer { get; }
    }
}
