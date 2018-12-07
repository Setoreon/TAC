using System.Collections.Generic;
using accounts.tac.local.Models;

namespace accounts.tac.local.Repositories
{   
    public interface IFedAuthLoginButtonRepository
    {
        IEnumerable<FedAuthLoginButton> GetAll();
    }
}