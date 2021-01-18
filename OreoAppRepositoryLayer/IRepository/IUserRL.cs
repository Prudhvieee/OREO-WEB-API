using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppRepositoryLayer.IRepository
{
    public interface IUserRL
    {
        bool RegisterUser(UserRegistration user);
        UserResponse UserLogin(UserLogin login);
    }
}