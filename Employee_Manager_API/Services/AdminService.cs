using Employee_Manager_API.DbClass;
using Employee_Manager_Models.CustomModels;
using Employee_Manager_Models.Models;
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

        public ResponseModel Register(AdminInfo adminInfo)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var userData = _context.AdminRegister.Where(x => x.Email == adminInfo.Email).Any();
                if (!userData)
                {
                    AdminInfo _register = new AdminInfo();
                    _register.Name = adminInfo.Name;
                    _register.Email = adminInfo.Email;
                    _register.Password = adminInfo.Password;
                    _register.CreatedOn = DateTime.Now.ToString();

                    _context.Add(_register);
                    _context.SaveChanges();

                    LoginModel loginModel = new LoginModel()
                    {
                        Email = adminInfo.Email,
                        Password = adminInfo.Password
                    };

                    response = Login(loginModel);

                    response.Status = true;
                    response.Message = "User is registered successfully!";
                }
                else 
                {
                    response.Status = false;
                    response.Message = "Email address already exists. Check email again";
                };
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "An error has occured. Please try again";

                return response;
            }
        }
        public ResponseModel Login(LoginModel loginModel)
        {
            ResponseModel response = new ResponseModel();
            try 
            {
                var userData = _context.AdminRegister.Where(x => x.Email == loginModel.Email).FirstOrDefault();
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
