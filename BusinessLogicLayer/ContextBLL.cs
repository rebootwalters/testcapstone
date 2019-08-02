using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    class ContextBLL :IDisposable
    {
        DataAccessLayer.ContextDAL context = new DataAccessLayer.ContextDAL();

        public void Dispose()
        {
            ((IDisposable)context).Dispose();
        }

        bool Log(Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }

        public ContextBLL()
        {
            try
            {
                string connectionstring;
                connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
                context.ConnectionString = connectionstring;
            }
            catch (Exception ex)  when(Log(ex))
            {
                // this exception does not have a 
                // reasonable handler, simply log it.
            }
        }

        public RoleBLL FindRoleByID(int RoleID)
        {
            RoleBLL ProposedReturnValue = null;
            RoleDAL DataLayerObject = context.FindRoleByID(RoleID);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new RoleBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }

        public List<RoleBLL> GetRoles(int skip, int take)
        {
            List<RoleBLL> ProposedReturnValue = new List<RoleBLL>();
            List<RoleDAL> ListOfDataLayerObjects = context.GetRoles(skip, take);
            foreach(RoleDAL role in ListOfDataLayerObjects )
            {
                RoleBLL BusinessObject = new RoleBLL(role);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
    }
}
