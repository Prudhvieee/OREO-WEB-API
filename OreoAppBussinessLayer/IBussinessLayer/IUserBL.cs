using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBussinessLayer.IBussinessLayer
{
    public interface IUserBL
    {
        bool RegisterUser(UserRegistration user);
        UserResponse UserLogin(UserLogin login);
    }
}