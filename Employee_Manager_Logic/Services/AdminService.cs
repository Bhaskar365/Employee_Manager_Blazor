using Employee_Manager_API.DbClass;
using Employee_Manager_Models.CustomModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Manager_Logic.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;
        public AdminService(AppDbContext context) 
        {
            _context = context;
        }

        public ResponseModel AdminLogin(LoginModel loginModel)
        {
            ResponseModel response = new ResponseModel();

            try 
            {
                var userData = _context.AdminInfo.Where(x => x.Email == loginModel.Email).FirstOrDefault();
                if (userData != null)
                {
                    if (userData.Password == loginModel.Password)
                    {
                        response.Status = true;
                        response.Message = Convert.ToString(userData.Id) + "|" + userData.Name + "|" + userData.Email;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Incorrect password entered";
                    }
                }
                else 
                {
                    response.Status = false;
                    response.Message = "Email not registered. Please check your email Id";
                }
                return response;
            }
            catch (Exception ex) 
            {
                response.Status = false;
                response.Message = "An error has occured. Please try again";

                return response;
            }
        }
    }
}
