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
        #region basic context stuff
        DataAccessLayer.ContextDAL _context = new DataAccessLayer.ContextDAL();

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }

        bool Log(Exception ex)
        {
            Console.WriteLine(ex);
            Lumberjack.Logger.Log(ex);
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

        public void GenerateNotConnected()
        {
            _context.GenerateNotConnected();
           
        }

        public void GenerateStoredProcedureNotFound()
        {
            
                _context.GenerateStoredProcedureNotFound();
            
        }

        public void GenerateParameterNotIncluded()
        {
            _context.GenerateParameterNotIncluded();
            
        }
        #endregion

        #region Role
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
        #endregion
        #region user
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
        public UserBLL FindUserByEMail(string EMail)
        {
            UserBLL ProposedReturnValue = null;
            UserDAL DataLayerObject = _context.FindUserByEMail(EMail);
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

        #endregion
        #region OwnedItem
        public int CreateOwnedItem(int OwnerID, string ItemDescription, decimal ItemPrice)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateOwnedItem(OwnerID, ItemDescription,ItemPrice );
            return proposedReturnValue;
        }

        public int CreateOwnedItem(OwnedItemBLL OwnedItem)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateOwnedItem(OwnedItem.OwnerID,OwnedItem.ItemDescription,OwnedItem.ItemPrice);
            return proposedReturnValue;
        }

        public void UpdateOwnedItem(int OwnedItemID, int OwnerID, string ItemDescription, decimal ItemPrice)
        {

            _context.UpdateOwnedItem(OwnedItemID, OwnerID, ItemDescription,ItemPrice);

        }

        public void UpdateOwnedItem(OwnedItemBLL OwnedItem)
        {

            _context.UpdateOwnedItem(OwnedItem.OwnedItemID, OwnedItem.OwnerID, OwnedItem.ItemDescription,OwnedItem.ItemPrice);

        }

        public void DeleteOwnedItem(int OwnedItemID)
        {
            _context.DeleteOwnedItem(OwnedItemID);
        }

        public void DeleteOwnedItem(OwnedItemBLL OwnedItem)
        {
            _context.DeleteOwnedItem(OwnedItem.OwnedItemID);
        }


        public OwnedItemBLL FindOwnedItemByID(int OwnedItemID)
        {
            OwnedItemBLL ProposedReturnValue = null;
            OwnedItemDAL DataLayerObject = _context.FindOwnedItemByID(OwnedItemID);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new OwnedItemBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }

 

        public List<OwnedItemBLL> GetOwnedItems(int skip, int take)
        {
            List<OwnedItemBLL> ProposedReturnValue = new List<OwnedItemBLL>();
            List<OwnedItemDAL> ListOfDataLayerObjects = _context.GetOwnedItems(skip, take);
            foreach (OwnedItemDAL OwnedItem in ListOfDataLayerObjects)
            {
                OwnedItemBLL BusinessObject = new OwnedItemBLL(OwnedItem);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public List<OwnedItemBLL> GetOwnedItemsRelatedToUser(int UserID,int skip, int take)
        {
            List<OwnedItemBLL> ProposedReturnValue = new List<OwnedItemBLL>();
            List<OwnedItemDAL> ListOfDataLayerObjects = _context.GetOwnedItemsRelatedToUser(UserID, skip, take);
            foreach (OwnedItemDAL OwnedItem in ListOfDataLayerObjects)
            {
                OwnedItemBLL BusinessObject = new OwnedItemBLL(OwnedItem);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
    
        public int ObtainOwnedItemCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainOwnedItemCount();
            return proposedReturnValue;
        }
        public int ObtainCountOfItemsOwnedByUser (int UserID)
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainCountOfItemsOwnedByUser(UserID);
            return proposedReturnValue;
        }
        public void JustUpdateOwnedItem(int OwnedItemID, int OwnerID, string ItemDescription, decimal ItemPrice)
        {

            _context.UpdateOwnedItem(OwnedItemID, OwnerID, ItemDescription,ItemPrice);

        }

        public void JustUpdateOwnedItem(OwnedItemBLL OwnedItem)
        {

            _context.UpdateOwnedItem(OwnedItem.OwnedItemID, OwnedItem.OwnerID,OwnedItem.ItemDescription, OwnedItem.ItemPrice);

        }
        #endregion
    }
}
