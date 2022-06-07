using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advancedWpfProject.Services
{
    public interface ILoginRepository
    {
        bool Login(string loginId, string password);
        string GetNickName(string loginId);
        bool RegisterAccount(string loginId, string password, string nickName);

    }
}
