using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserMapper : Mapper
    {
        int OffsetToUserID;  // expected to be 0
        int OffsetToEmail;   // expected to be 1
        int OffsetToHash;
        int OffsetToSalt;
        int OffsetToDateOfBirth;
        int OffsetToRoleID;
        int OffsetToRoleName;   // expected to be 6

        public UserMapper(System.Data.SqlClient.SqlDataReader reader)
        {
            OffsetToUserID = reader.GetOrdinal("UserID");
            Assert(0 == OffsetToUserID, $"UserID is {OffsetToUserID} not 0 as expected");
            OffsetToEmail = reader.GetOrdinal("EMail");
            Assert(1 == OffsetToEmail, $"Email is {OffsetToEmail} not 1 as expected");
            OffsetToHash = reader.GetOrdinal("Hash");
            OffsetToSalt = reader.GetOrdinal("Salt");
            OffsetToDateOfBirth = reader.GetOrdinal("DateOfBirth");
            OffsetToRoleID = reader.GetOrdinal("RoleID");
            OffsetToRoleName = reader.GetOrdinal("RoleName");
            Assert(6 == OffsetToRoleName, $"RoleName is {OffsetToRoleName} not 6 as expected");


        }

        public UserDAL UserFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            UserDAL ProposedReturnValue = new UserDAL();
            ProposedReturnValue.UserID = reader.GetInt32(OffsetToUserID);
            // reader["UserID"]  is very slow and makes a lot of garbage
            // reader[0] makes a lot of garbage
            // reader.GetInt32(0) is fast, but hard codes the offset to 0
            // reader.GetInt32(OffsetToUserID) is best and allows verification
            ProposedReturnValue.EMail = reader.GetString(OffsetToEmail);
            ProposedReturnValue.Hash = reader.GetString(OffsetToHash);
            ProposedReturnValue.Salt = reader.GetString(OffsetToSalt);
            ProposedReturnValue.DateOfBirth = reader.GetDateTime(OffsetToDateOfBirth);
            ProposedReturnValue.RoleID = reader.GetInt32(OffsetToRoleID);
            ProposedReturnValue.RoleName = reader.GetString(OffsetToRoleName);
            return ProposedReturnValue;
        }
    }
}
