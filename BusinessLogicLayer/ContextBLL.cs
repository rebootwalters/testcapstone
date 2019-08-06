using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class ContextBLL :IDisposable
    {
        DataAccessLayer.ContextDAL _context = new DataAccessLayer.ContextDAL();

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
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
                _context.ConnectionString = connectionstring;
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
            RoleDAL DataLayerObject = _context.FindRoleByID(RoleID);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new RoleBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }

        public List<RoleBLL> GetRoles(int skip, int take)
        {
            List<RoleBLL> ProposedReturnValue = new List<RoleBLL>();
            List<RoleDAL> ListOfDataLayerObjects = _context.GetRoles(skip, take);
            foreach(RoleDAL role in ListOfDataLayerObjects )
            {
                RoleBLL BusinessObject = new RoleBLL(role);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public int ObtainRoleCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainRoleCount();
            return proposedReturnValue;
        }

        public int CreateRole(string RoleName)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateRole(RoleName);
            return proposedReturnValue;
        }
        public int CreateRole(RoleBLL role)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateRole(role.RoleName);
            return proposedReturnValue;
        }

        public void UpdateRole(int RoleID, string RoleName)
        {
           
           _context.UpdateRole(RoleID,RoleName);
          
        }
        public void UpdateRole(RoleBLL Role)
        {

            _context.UpdateRole(Role.RoleID, Role.RoleName);

        }

        public void DeleteRole(int RoleID)
        {
            _context.DeleteRole(RoleID);
        }
        public void DeleteRole(RoleBLL Role)
        {
            _context.DeleteRole(Role.RoleID);
        }

        public int CreateUser(string EMail,string Hash, string Salt, DateTime DateOfBirth, int RoleID)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateUser(EMail,Hash,Salt,DateOfBirth,RoleID);
            return proposedReturnValue;
        }

        public int CreateUser(UserBLL user)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateUser(user.EMail, user.Hash, user.Salt, user.DateOfBirth, user.RoleID);
            return proposedReturnValue;
        }

        public void UpdateUser(int UserID, string EMail, string Hash, string Salt, DateTime DateOfBirth, int RoleID)
        {
           
            _context.UpdateUser(UserID,EMail, Hash, Salt, DateOfBirth, RoleID);
           
        }

        public void UpdateUser(UserBLL user)
        {
            
             _context.UpdateUser(user.UserID,user.EMail, user.Hash, user.Salt, user.DateOfBirth, user.RoleID);
           
        }

        public void DeleteUser(int UserID)
        {
            _context.DeleteUser(UserID);
        }

        public void DeleteUser(UserBLL User)
        {
            _context.DeleteUser(User.UserID);
        }















        public UserBLL FindUserByID(int UserID)
        {
            UserBLL ProposedReturnValue = null;
            UserDAL DataLayerObject = _context.FindUserByID(UserID);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new UserBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }

        public List<UserBLL> GetUsers(int skip, int take)
        {
            List<UserBLL> ProposedReturnValue = new List<UserBLL>();
            List<UserDAL> ListOfDataLayerObjects = _context.GetUsers(skip, take);
            foreach (UserDAL User in ListOfDataLayerObjects)
            {
                UserBLL BusinessObject = new UserBLL(User);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public int ObtainUserCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainUserCount();
            return proposedReturnValue;
        }

        public void JustUpdateUser(int UserID, string EMail, string Hash, string Salt, DateTime DateOfBirth, int RoleID)
        {

            _context.UpdateUser(UserID, EMail, Hash, Salt, DateOfBirth, RoleID);

        }

        public void JustUpdateUser(UserBLL user)
        {

            _context.UpdateUser(user.UserID, user.EMail, user.Hash, user.Salt, user.DateOfBirth, user.RoleID);

        }
    }
}
