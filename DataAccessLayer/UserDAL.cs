using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDAL
    {
        #region Direct Properties
        public int UserID { get; set; }
        public string EMail { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleID { get; set; }
        #endregion direct properties

        #region indirect properties
        public string RoleName { get; set; }
        #endregion indirect properties

        public override string ToString()
        {
            return $"User: UserID:{UserID,5} EMail:{EMail,25} Birth: {DateOfBirth} RoleID: {RoleID} RoleName: {RoleName} Hash:{Hash} Salt:{Salt}";
        }


    }
}
