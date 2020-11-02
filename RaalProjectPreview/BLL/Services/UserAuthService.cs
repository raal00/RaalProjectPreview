using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.BLL.Models.Home.Request;
using RaalProjectPreview.BLL.Models.Home.ServiceModels.Response;
using RaalProjectPreview.DAL.Models.DBModels;
using RaalProjectPreview.DAL.Repository;
using RaalProjectPreview.Security.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaalProjectPreview.BLL.Services
{
    public class UserAuthService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly User_RoleRepository _user_RoleRepository;
        private readonly AuthUserDataReposirory _authUserDataReposirory;
        public UserAuthService()
        {
            _customerRepository = new CustomerRepository();
            _user_RoleRepository = new User_RoleRepository();
            _authUserDataReposirory = new AuthUserDataReposirory();
        }

        public ServiceLoginResponse LoginUser(LoginRequestModel model)
        {
            ServiceLoginResponse response = new ServiceLoginResponse();
            AuthUserData userData = new AuthUserData();
            userData.Login = model.Login;
            userData.PasswordHash = model.Password.GetHashCode().ToString();
            userData = _authUserDataReposirory.GetUserByLoginAndPasswordHash(userData);
            if (userData == null)
            {
                response.responseStatus = ResponseStatus.Failed;
                response.Role = Security.Roles.ClientRole.Unauthorized;
                response.Name = null;
                response.Message = "Unauthorized user";
                return response;
            }
            response.UserId = userData.CustomerId;
            response.Name = _customerRepository.GetCustomerNameById(userData.CustomerId);
            response.Role = _user_RoleRepository.GetRoleByCustomerId(userData.CustomerId);
            response.responseStatus = ResponseStatus.Completed;
            response.Message = $"Login user succsess!\nName: {response.Name}\tRole: {response.Role}";
            return response;
        }
        public ResponseStatus SignInUser(AuthRequestModel model) {
            
            Customer customer = new Customer();
            customer.Address = model.Address;
            customer.Discount = 0;
            customer.Name = model.Name;
            customer = _customerRepository.Create(customer);

            string date = DateTime.Now.Year.ToString();
            string lastCode = _customerRepository.GetIdOfLastUser().ToString();
            while (lastCode.Length < 4) lastCode += '0';
            customer.Code = date + "-" + lastCode;

            UserRole user_Role = new UserRole();
            user_Role.ClientRole = ClientRole.Customer;
            user_Role.CustomerId = customer.Id;
            _user_RoleRepository.Create(user_Role);
            AuthUserData authUserData = new AuthUserData();
            authUserData.Login = model.Login;
            authUserData.PasswordHash = model.Password.GetHashCode().ToString();
            authUserData.CustomerId = customer.Id;
            _authUserDataReposirory.Create(authUserData);

            return ResponseStatus.Completed;
        }
    }
}