using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class RoleBLL
    {
        
        [System.ComponentModel.DataAnnotations.Display(Name ="Brians Display")]
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public RoleBLL()
        {

        }

        public RoleBLL(DataAccessLayer.RoleDAL dal)
        {
            this.RoleID = dal.RoleID;
            this.RoleName = dal.RoleName;
        }

        public override string ToString()
        {
            return $"RoleID: {RoleID,5} RoleName:{RoleName}";
        }
    }
}
