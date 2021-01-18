using OreoAppBussinessLayer.IBussinessLayer;
using OreoAppCommonLayer.Model;
using OreoAppRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBussinessLayer.BusinessLayer
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public bool RegisterUser(UserRegistration user)
        {
            try
            {
                return this.userRL.RegisterUser(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public UserResponse UserLogin(UserLogin login)
        {
            try
            {
                return this.userRL.UserLogin(login);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
