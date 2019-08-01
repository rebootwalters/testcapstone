using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoleDAL
    {
        #region direct properties
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        #endregion direct properties

        #region indirect properties
        // this class does not have any indirect properties
        // because the Roles Table does not have any Foreign keys
        #endregion indirect properties

        public override string ToString()
        {
            return $"RoleID: {RoleID,5} RoleName:{RoleName}";
        }
    }
}
