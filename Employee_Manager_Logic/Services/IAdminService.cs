using Employee_Manager_Models.CustomModels;
using Employee_Manager_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Manager_Logic.Services
{
    public interface IAdminService
    {
        ResponseModel AdminLogin(LoginModel loginModel);
        ResponseModel AdminRegister(AdminInfo adminInfo);
    }
}
