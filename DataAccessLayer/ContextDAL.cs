using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Lumberjack;

namespace DataAccessLayer
{
    public class ContextDAL : IDisposable
    {
        #region Context stuff
        SqlConnection _connection;
        public ContextDAL()
        {
            _connection = new SqlConnection();
        }
        public string ConnectionString
        {
            get { return _connection.ConnectionString; }
            set { _connection.ConnectionString = value; }
        }
        void EnsureConnected()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                // there is nothing to do if I am connected
            }
            else if (_connection.State == System.Data.ConnectionState.Broken)
            {
                _connection.Close();
                _connection.Open();
            }
            else if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            else
            {
                // other states need no processing
            }


        }

        bool Log(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Logger.Log(ex);
            return false;
        }
        public void Dispose()
        {
            _connection.Dispose();
        }

        #endregion

        #region simulate Exceptions for testing
        // this region is to create DAL layer Exceptions on purpose in order to test the to see if the exceptions are poperly caught and logged.
        public int GenerateNotConnected()
        {
            int proposedReturnValue = -1;
            try
            {
                // by commenting out the EnsureConnected below, this method MAY throw an
                // exception IF it is the First call on a ContextDAL

                //EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainRoleCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }
        public int GenerateStoredProcedureNotFound()
        {
            int proposedReturnValue = -1;
            try
            {
                

                EnsureConnected();
                // the name of the stored procdeure is incorrect, so it should throw an exception
                using (SqlCommand command = new SqlCommand("XXXX", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }
        public int GenerateParameterNotIncluded()
        {
            int proposedReturnValue = -1;
            try
            {


                EnsureConnected();
                // the parameter to the stored procdeure is incorrect, so it should throw an exception
                using (SqlCommand command = new SqlCommand("FindRoleByRoleID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // the following line is where the parameter name is incorrect
                    command.Parameters.AddWithValue("@XXXX", 1);
                    object answer = command.ExecuteReader();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        #endregion


        #region Role Stuff
        public RoleDAL FindRoleByID(int RoleID)
        {
            RoleDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindRoleByRoleID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    // need to configure the Text, Type and Parameters
                    // text was configured in the ctor (Line 58)
                    // type in line 60
                    // parameters in line 61

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RoleMapper m = new RoleMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.RoleFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 Role with key {RoleID}");

                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;

        }

        public List<RoleDAL> GetRoles(int skip, int take)
        {
            List<RoleDAL> proposedReturnValue = new List<RoleDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetRoles", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RoleMapper m = new RoleMapper(reader);
                        while (reader.Read())
                        {
                            RoleDAL r = m.RoleFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public int ObtainRoleCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainRoleCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int CreateRole(string RoleName)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleName", RoleName);
                    command.Parameters.AddWithValue("@RoleID", 0);
                    command.Parameters["@RoleID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    proposedReturnValue =
                        Convert.ToInt32(command.Parameters["@RoleID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public void UpdateRole(int RoleID, string RoleName)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleName", RoleName);
                    command.Parameters.AddWithValue("@RoleID", RoleID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

        public void DeleteRole(int RoleID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoleID", RoleID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

  

        #endregion

        #region User stuff
        public UserDAL FindUserByID(int UserID)
        {
            UserDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindUserByUserID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    // need to configure the Text, Type and Parameters
                    // text was configured in the ctor (Line 58)
                    // type in line 60
                    // parameters in line 61

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.UserFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 User with key {UserID}");

                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;

        }
        public UserDAL FindUserByEMail(string EMail)
        {
            UserDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindUserByEMail", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EMail", EMail);
                    // need to configure the Text, Type and Parameters
                    // text was configured in the ctor (Line 58)
                    // type in line 60
                    // parameters in line 61

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.UserFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 User with key {EMail}");

                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;

        }

        public List<UserDAL> GetUsers(int skip, int take)
        {
            List<UserDAL> proposedReturnValue = new List<UserDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetUsers", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        while (reader.Read())
                        {
                            UserDAL r = m.UserFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public int ObtainUserCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainUserCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int CreateUser(string EMail, string Hash, string Salt, DateTime DateOfBirth, int RoleID)
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", 0);
                    command.Parameters.AddWithValue("@EMail", EMail);
                    command.Parameters.AddWithValue("@Hash", Hash);
                    command.Parameters.AddWithValue("@Salt", Salt);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.Parameters["@UserID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    ProposedReturnValue =
                        Convert.ToInt32(command.Parameters["@UserID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }

        public void UpdateUser(int UserID, string EMail, string Hash, string Salt, DateTime DateOfBirth, int RoleID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@EMail", EMail);
                    command.Parameters.AddWithValue("@Hash", Hash);
                    command.Parameters.AddWithValue("@Salt", Salt);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@RoleID", RoleID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

        public void DeleteUser(int UserID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

        #endregion

        #region OwnedItem stuff
        public OwnedItemDAL FindOwnedItemByID(int ItemID)
        {
            OwnedItemDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindOwnedItemByID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ItemID", ItemID);
                    // need to configure the Text, Type and Parameters
                    // text was configured in the ctor (Line 58)
                    // type in line 60
                    // parameters in line 61

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        OwnedItemMapper m = new OwnedItemMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.OwnedItemFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 User with key {ItemID}");

                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;

        }
     

        public List<OwnedItemDAL> GetOwnedItems(int skip, int take)
        {
            List<OwnedItemDAL> proposedReturnValue = new List<OwnedItemDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetOwnedItems", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        OwnedItemMapper m = new OwnedItemMapper(reader);
                        while (reader.Read())
                        {
                            OwnedItemDAL r = m.OwnedItemFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }
        public List<OwnedItemDAL> GetOwnedItemsRelatedToUser (int UserID, int skip, int take)
        {
            List<OwnedItemDAL> proposedReturnValue = new List<OwnedItemDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetOwnedItemsRelatedToUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    command.Parameters.AddWithValue("@userID", UserID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        OwnedItemMapper m = new OwnedItemMapper(reader);
                        while (reader.Read())
                        {
                            OwnedItemDAL r = m.OwnedItemFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }
        public int ObtainOwnedItemCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainItemCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }
        public int ObtainCountOfItemsOwnedByUser(int UserID)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainCountOfItemsOwnedByUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int CreateOwnedItem(int OwnerID, string ItemDescription, decimal ItemPrice)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateOwnedItem", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OwnerID", OwnerID);
                    command.Parameters.AddWithValue("@ItemDescription", ItemDescription);
                    command.Parameters.AddWithValue("@ItemPrice", ItemPrice);

                    command.Parameters.AddWithValue("@OwnedItemID", 0);
                    command.Parameters["@OwnedItemID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    proposedReturnValue =
                        Convert.ToInt32(command.Parameters["@OwnedItemID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public void UpdateOwnedItem(int OwnedItemID, int OwnerID, string ItemDescription, decimal ItemPrice)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateOwnedItem", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ItemID", OwnedItemID);
                    command.Parameters.AddWithValue("@OwnerID", OwnerID);
                    command.Parameters.AddWithValue("@ItemDescription", ItemDescription);
                    command.Parameters.AddWithValue("@ItemPrice", ItemPrice);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

        public void DeleteOwnedItem(int ItemID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteOwnedItem", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@OwnedItemID", ItemID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        #endregion
    }
}
