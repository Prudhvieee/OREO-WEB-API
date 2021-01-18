using OreoAppBussinessLayer.IBussinessLayer;
using OreoAppCommonLayer.Model;
using OreoAppRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBussinessLayer.BusinessLayer
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public AdminResponse AdminLogin(AdminLogin Admin)
        {
            try
            {
                return this.adminRL.AdminLogin(Admin);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool RegisterAdmin(AdminRegister Admin)
        {
            try
            {
                return this.adminRL.RegisterAdmin(Admin);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
