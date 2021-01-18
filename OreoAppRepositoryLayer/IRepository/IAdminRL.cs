using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppRepositoryLayer.IRepository
{
    public interface IAdminRL
    {
        bool RegisterAdmin(AdminRegister Admin);
        AdminResponse AdminLogin(AdminLogin Admin);
    }
}