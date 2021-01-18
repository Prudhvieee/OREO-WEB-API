using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBussinessLayer.IBussinessLayer
{
    public interface IAdminBL
    {
        bool RegisterAdmin(AdminRegister Admin);
        AdminResponse AdminLogin(AdminLogin Admin);
    }
}
